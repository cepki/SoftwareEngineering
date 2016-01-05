using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JoinINN.Repository
{
    public class AdminRepository
    {
        public ICollection<Admin> GetAllAdmins()
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return context.Admins
                    .ToList();
            }
        }

        public bool AdminDeleteUser(int id)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                try
                {
                    var groupToDelete = context.SocialGroups.FirstOrDefault(x => x.Id == id);
                    context.SocialGroups.Remove(groupToDelete);
                    context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

    }

}