using DataStorage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JoinINN.Infrastructure
{
    public class GroupsDb : DbContext, IGroupsDataSource
    {
        public GroupsDb() : base("DefaultConnection")
        {

        }

        public DbSet<SocialGroup> SocialGroups { get; set; }
        public DbSet<AffinityType> AffinityTypes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<City> Cities { get; set; }

        IQueryable<AffinityType> IGroupsDataSource.AffinityTypes
        {
            get
            {
                return AffinityTypes;
            }
        }

        IQueryable<SocialGroup> IGroupsDataSource.SocialGroups
        {
            get
            {
                return SocialGroups;
            }
        }

        IQueryable<City> IGroupsDataSource.Cities
        {
            get
            {
                return Cities;
            }
        }

        IQueryable<Admin> IGroupsDataSource.Admins
        {
            get
            {
                return Admins;
            }
        }
    }
}