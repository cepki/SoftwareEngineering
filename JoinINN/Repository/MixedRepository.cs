using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinINN.Repository
{
    public class MixedRepository
    {
        public bool IsThisAnyonesUsernameAndPassword(string username, string password)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return (context.Admins.Any(x => x.Username == username && x.Password == password)) ||
                    (context.SocialGroups.Any(x => x.EmailAddress == username && x.Password == password));
            }
        }

        public string WhatIsMyRole(string usernameOfLogedInUser)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                if (context.Admins.Any(x => x.Username == usernameOfLogedInUser))
                {
                    return "admin";
                }
                else
                {
                    return "user";
                }
            }
        }
    }
}