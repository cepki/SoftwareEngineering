using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinINN
{
    public class Service
    {
        public List<SocialGroup> GetAllGroups()
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return context.SocialGroups.ToList();
            }
        }
    }
}