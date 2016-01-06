using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace JoinINN.Repository
{
    public class MixedRepository
    {
        public bool IsThisAnyonesUsernameAndPassword(string username, string password)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                var hashedPassword = sha256_hash(password);
                return (context.Admins.Any(x => x.Username == username && x.Password == hashedPassword)) ||
                    (context.SocialGroups.Any(x => x.EmailAddress == username && x.Password == hashedPassword));
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