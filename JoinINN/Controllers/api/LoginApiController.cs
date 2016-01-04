using JoinINN.Models;
using JoinINN.Repository;
using JoinINN.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [HttpDelete]
        public void SignOut()
        {
            loginService.SignOut();
        }

    }
}
