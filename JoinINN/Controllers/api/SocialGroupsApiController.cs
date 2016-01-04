using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JoinINN.Repository;
using DataStorage;

namespace JoinINN.Controllers.api
{
    public class SocialGroupsApiController : ApiController
    {
        private readonly SocialGroupsRepository groupsRepository = new SocialGroupsRepository();

        [HttpGet]
        public ICollection<SocialGroup> GetAllGroups()
        {
            return groupsRepository.GetAllGroups()
                .ToList();
        }

        [HttpPost]
        public HttpResponseMessage MakeNewUser(SocialGroup newUser)
        {
            try
            {
                //var dataUser = UserMapper.Map(newUser);
                //var idsOfAffinities = newUser.AffinityTypes.Select(x => x.Id).ToList<int>();
                groupsRepository.AddNewGroup(newUser);
                var response = this.Request.CreateResponse(HttpStatusCode.Created, true);
                return response;
            }
            catch (Exception e)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(e.ToString()));
                return response;
            }
        }

        [HttpGet]
        public SocialGroup GetGroupWithThisId(int id)
        {
            var user = groupsRepository.GetUserWithThisId(id);

            try
            {
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }


    }
}
