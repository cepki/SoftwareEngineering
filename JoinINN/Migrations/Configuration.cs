namespace JoinINN.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataStorage;

    internal sealed class Configuration : DbMigrationsConfiguration<JoinINN.Infrastructure.GroupsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JoinINN.Infrastructure.GroupsDb context)
        {
            var afinityTypes = new[]
            {
                new AffinityType { Id = 0, Name = "Sport" },
                new AffinityType { Id = 1, Name = "Umjetnost" },
                new AffinityType { Id = 2, Name = "Glazba" },
                new AffinityType { Id = 3, Name = "Volontiranje" },
                new AffinityType { Id = 4, Name = "Strani jezici" },
                new AffinityType { Id = 5, Name = "Obrazovanje" },
                new AffinityType { Id = 6, Name = "Tehnologija" },
                new AffinityType { Id = 7, Name = "Zdravlje" }
            };

            var cities = new[]
            {
                new City { Id = 0, Name = "Split"},
                new City { Id = 1, Name = "Zagreb"}
            };

            var admins = new[]
            {
                new Admin {Id=0, Username="ebehmen", Password="ebehmen" },
                new Admin {Id=1, Username="mjelaska", Password="mjelaska" },
                new Admin {Id=2, Username="mceprnja", Password="mceprnja" }
            };

            var socialGroups = new[]
            {
                new SocialGroup { Name = "Klub mladih Split", EmailAddress = "klubmladih.split@gmail.com", Password = "1234", ContactNumber = "123456789", OfficialWebUrl="http://www.klubmladihsplit.hr/", FacebookPageUrl="https://www.facebook.com/klubmladihsplit/?fref=ts", Description="Prostor za rad, učenje, druženje i zabavu mladih. Program financira Ministarstvo socijalne politike i mladih.", IsSchool=false, IsAssociation=true, photoUrl="https://scontent-vie1-1.xx.fbcdn.net/hphotos-xpt1/v/t1.0-9/10592750_527662070710011_3258294634430693170_n.png?oh=200336d25482ea5a4df95fca6dd17266&oe=5705B5F1", AffinityType=afinityTypes[0] },
                new SocialGroup { Name = "Volonterski centar split", EmailAddress = "kontakt@vcst.info", Password="1234", ContactNumber = "123456789", OfficialWebUrl="http://www.klubmladihsplit.hr/", FacebookPageUrl="https://www.facebook.com/klubmladihsplit/?fref=ts", Description="Prostor za rad, učenje, druženje i zabavu mladih. Program financira Ministarstvo socijalne politike i mladih.", IsSchool=false, IsAssociation=true, photoUrl="https://scontent-vie1-1.xx.fbcdn.net/hphotos-xpt1/v/t1.0-9/10592750_527662070710011_3258294634430693170_n.png?oh=200336d25482ea5a4df95fca6dd17266&oe=5705B5F1", AffinityType=afinityTypes[0] },
                new SocialGroup { Name = "Udruga Sunce", EmailAddress = "info@sunce-st.org", Password = "1234", ContactNumber = "123456789", OfficialWebUrl="http://www.klubmladihsplit.hr/", FacebookPageUrl="https://www.facebook.com/klubmladihsplit/?fref=ts", Description="Prostor za rad, učenje, druženje i zabavu mladih. Program financira Ministarstvo socijalne politike i mladih.", IsSchool=false, IsAssociation=true, photoUrl="https://scontent-vie1-1.xx.fbcdn.net/hphotos-xpt1/v/t1.0-9/10592750_527662070710011_3258294634430693170_n.png?oh=200336d25482ea5a4df95fca6dd17266&oe=5705B5F1", AffinityType=afinityTypes[0] },
                new SocialGroup { Name = "Ivora - škola informatike", EmailAddress = "informacije@ivora.hr", Password = "1234", ContactNumber = "123456789", OfficialWebUrl="http://www.klubmladihsplit.hr/", FacebookPageUrl="https://www.facebook.com/klubmladihsplit/?fref=ts", Description="Prostor za rad, učenje, druženje i zabavu mladih. Program financira Ministarstvo socijalne politike i mladih.", IsSchool=false, IsAssociation=true, photoUrl="https://scontent-vie1-1.xx.fbcdn.net/hphotos-xpt1/v/t1.0-9/10592750_527662070710011_3258294634430693170_n.png?oh=200336d25482ea5a4df95fca6dd17266&oe=5705B5F1", AffinityType=afinityTypes[0] },
                new SocialGroup { Name = "Glazbena škola Blagoja Berse", EmailAddress = "glazbena@bersa.hr", Password = "1234", ContactNumber = "123456789", OfficialWebUrl="http://www.klubmladihsplit.hr/", FacebookPageUrl="https://www.facebook.com/klubmladihsplit/?fref=ts", Description="Prostor za rad, učenje, druženje i zabavu mladih. Program financira Ministarstvo socijalne politike i mladih.", IsSchool=false, IsAssociation=true, photoUrl="https://scontent-vie1-1.xx.fbcdn.net/hphotos-xpt1/v/t1.0-9/10592750_527662070710011_3258294634430693170_n.png?oh=200336d25482ea5a4df95fca6dd17266&oe=5705B5F1", AffinityType=afinityTypes[0] },
                new SocialGroup { Name = "Udruga za autizam Zagreb", EmailAddress = "uaz.zagreb@gmail.com", Password = "1234", ContactNumber = "123456789", OfficialWebUrl="http://www.klubmladihsplit.hr/", FacebookPageUrl="https://www.facebook.com/klubmladihsplit/?fref=ts", Description="Prostor za rad, učenje, druženje i zabavu mladih. Program financira Ministarstvo socijalne politike i mladih.", IsSchool=false, IsAssociation=true, photoUrl="https://scontent-vie1-1.xx.fbcdn.net/hphotos-xpt1/v/t1.0-9/10592750_527662070710011_3258294634430693170_n.png?oh=200336d25482ea5a4df95fca6dd17266&oe=5705B5F1", AffinityType=afinityTypes[0] }
            };

            context.Admins.AddRange(admins);
            context.AffinityTypes.AddRange(afinityTypes);
            context.Cities.AddRange(cities);
            context.SocialGroups.AddRange(socialGroups);
        }

    }
}
