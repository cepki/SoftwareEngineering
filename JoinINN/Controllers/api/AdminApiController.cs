using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JoinINN.Repository;
using JoinINN.Models;

namespace JoinINN.Controllers.api
{
    public class AdminApiController : ApiController
    {
        private readonly AdminRepository adminRepository = new AdminRepository();

        [HttpGet]
        public HttpResponseMessage AdminDeleteGroup([FromUri]int id)
        {
            var test = adminRepository.AdminDeleteUser(id);

            if (test == true)
            {
                return this.Request.CreateResponse(HttpStatusCode.Accepted, true);
            }
            else
            {
                return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError());
            }
        }

        [HttpGet]
        public Informations GetAllInformationsForAdmin()
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                var newInf = 
                    new Informations(
                        context.SocialGroups.Count(),
                        context.Visitors.First().NumberOfVisits,
                        context.Admins.Count(),
                        context.SocialGroups.Count(),
                        context.SocialGroups.Count(x => x.CityId == 1),
                        context.SocialGroups.Count(x => x.CityId == 2)
                        );
                return newInf;
            }

        }
    }
}
