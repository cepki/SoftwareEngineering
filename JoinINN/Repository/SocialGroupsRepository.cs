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
                    .ToList();
            }
        }

        public bool AddNewGroup()//SocialGroup newUser, ICollection<int> idsOfAffinities
        {
            var socGroup = new SocialGroup()
            {
                Name = "description",
                EmailAddress = " jSJAjsja",
                Description = "s ajjaj",
                IsAssociation = false,
                IsSchool = true,
                CityId = 1
            };

            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                try
                {
                    //var listOfAffinities = idsOfAffinities.Select(x => context.AffinityTypes.First(y => (y.Id == x)));
                    //foreach (var affinity in listOfAffinities)
                    //{
                    //    newUser.AffinityTypes.Add(affinity);
                    //}
                    context.SocialGroups.Add(socGroup);
                    context.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException dbEx)
                {
                    //foreach (var validationErrors in dbEx.EntityValidationErrors)
                    //{
                    //    foreach (var validationError in validationErrors.ValidationErrors)
                    //    {
                    //        Trace.TraceInformation("Property: {0} Error: {1}",
                    //                                validationError.PropertyName,
                    //                                validationError.ErrorMessage);
                    //    }
                    //}
                    return false;
                }
            }
        }

        public SocialGroup GetUserWithThisId(int id)
        {
            using (var context = new JoinINN.Infrastructure.GroupsDb())
            {
                return context.SocialGroups
                    .FirstOrDefault(x => x.Id == id);
                    //.Include("City")
                    //.Include("AffinityTypes")
            }
        }

    }
}