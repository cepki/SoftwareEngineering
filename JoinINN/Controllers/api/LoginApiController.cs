using DataStorage;
using JoinINN.Models;
using JoinINN.Repository;
using JoinINN.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace JoinINN.Controllers.api
{
    public class LoginApiController : ApiController
    {
        private readonly MixedRepository mixedRepository = new MixedRepository();
        private readonly HttpCookieLoginService loginService = new HttpCookieLoginService();

        [HttpPost]
        public HttpResponseMessage SignIn(LoginModel loginModel)
        {
            var authenticated = mixedRepository.IsThisAnyonesUsernameAndPassword(loginModel.Username, loginModel.Password);

            if (authenticated)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.Created, true);
                loginService.SignIn(loginModel.Username, true);
                return response;
            }

            return this.Request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError());
        }


        [HttpGet]
        public LogedUser WhoAmI()
        {
            var logedInUserByUsername = loginService.TryGetSignedInUserId();

            if (logedInUserByUsername == null)
            {
                return null;
            }

            //we have to find his role
            var logedInUser = new LogedUser();
            logedInUser.Username = logedInUserByUsername;
            logedInUser.Role = mixedRepository.WhatIsMyRole(logedInUser.Username);

            return logedInUser;
        }

        [HttpGet]
        public void SignOut()
        {
            loginService.SignOut();
        }


        [HttpGet]
        public SocialGroup GetLogedUser()
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                var logedInUserByUsername = loginService.TryGetSignedInUserId();

                var me = context.SocialGroups
                    .Include("AffinityType")
                    .FirstOrDefault(x => x.EmailAddress == logedInUserByUsername);
                return me;
            }
        }

        [HttpGet]
        public string GetHashOfThisPassword([FromUri] string id)
        {
            if(id != null)
            {
                return sha256_hash(id);
            }
            else
            {
                return null;
            }
        }

        public static String sha256_hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Join("", hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}
