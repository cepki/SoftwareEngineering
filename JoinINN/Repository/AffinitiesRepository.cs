using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinINN.Repository
{
    public class AffinitiesRepository
    {
        public ICollection<AffinityType> GetAllAffinityTypes()
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return context.AffinityTypes
                    .ToList();
            }
        }
    }
}