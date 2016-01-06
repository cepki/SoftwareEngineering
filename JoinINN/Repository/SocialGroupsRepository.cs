using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataStorage;
using System.Data.Entity.Validation;

namespace JoinINN.Repository
{
    public class SocialGroupsRepository
    {
        public ICollection<SocialGroup> GetAllGroups()
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return context.SocialGroups
                    .Include("City")
                    .Include("AffinityType")
                    .ToList();
            }
        }

        public bool AddNewGroup(SocialGroup socGroup)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                try
                {
                    socGroup.AffinityType = context.AffinityTypes.FirstOrDefault(x => x.Id == socGroup.AffinityType_Id);
                    socGroup.City = context.Cities.FirstOrDefault(x => x.Id == socGroup.CityId);
                    context.SocialGroups.Add(socGroup);
                    context.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException dbEx)
                {
                    return false;
                }
            }
        }

        public SocialGroup GetUserWithThisId(int id)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return context.SocialGroups
                    .Include("City")
                    .Include("AffinityType")
                    .FirstOrDefault(x => x.Id == id);
            }
        }


        public void EditUser(SocialGroup socGroup)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                try
                {
                    var existingSocGroup = context.SocialGroups.FirstOrDefault(x => x.Id == socGroup.Id);

                    existingSocGroup.AffinityType = context.AffinityTypes.FirstOrDefault(x => x.Id == socGroup.AffinityType_Id);
                    existingSocGroup.Password = socGroup.Password;
                    existingSocGroup.Name = socGroup.Name;
                    existingSocGroup.OfficialWebUrl = socGroup.OfficialWebUrl;
                    existingSocGroup.FacebookPageUrl = socGroup.FacebookPageUrl;
                    existingSocGroup.photoUrl = socGroup.photoUrl;
                    existingSocGroup.ContactNumber = socGroup.ContactNumber;
                    existingSocGroup.Description = socGroup.Description;

                    context.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                }
            }
        }



    }
}