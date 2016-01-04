using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public interface IGroupsDataSource
    {
        IQueryable<City> Cities { get; }
        IQueryable<SocialGroup> SocialGroups { get; }
        IQueryable<AffinityType> AffinityTypes { get; }
        IQueryable<Admin> Admins { get; }
    }
}
