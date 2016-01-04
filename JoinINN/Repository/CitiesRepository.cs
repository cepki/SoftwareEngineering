using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinINN.Repository
{
    public class CitiesRepository
    {
        public ICollection<City> GetAllCities()
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return context.Cities
                    .ToList();

            }
        }
    }
}