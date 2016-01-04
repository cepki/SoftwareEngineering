using DataStorage;
using JoinINN.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JoinINN.Controllers.api
{
    public class AffinitiesApiController : ApiController
    {
        private readonly AffinitiesRepository affinitiRepo = new AffinitiesRepository();

        [HttpGet]
        public ICollection<AffinityType> GetAllAffinites()
        {
            return affinitiRepo.GetAllAffinityTypes()
                    .ToList();
        }
    }
}
