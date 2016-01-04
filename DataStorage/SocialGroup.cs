using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    public class SocialGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string OfficialWebUrl { get; set; }
        public string FacebookPageUrl { get; set; }
        public string Description { get; set; }
        public bool IsSchool { get; set; }
        public bool IsAssociation { get; set; }
        public int CityId { get; set; }
        public string photoUrl { get; set; }

        public AffinityType AffinityType { get; set; }
        public City City { get; set; }
    }
}
