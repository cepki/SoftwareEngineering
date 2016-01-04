using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JoinINN.Repository;

namespace JoinINN.Controllers.api
{
    public class CitiesApiController : ApiController
    {
        private readonly CitiesRepository citiesRepo = new CitiesRepository();

        [HttpGet]
        public ICollection<City> GetAllCities()
        {
            return citiesRepo.GetAllCities()
                .ToList();
        }
    }
}
