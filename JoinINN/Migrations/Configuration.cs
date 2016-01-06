namespace JoinINN.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DataStorage;
    using System.Security.Cryptography;
    using System.Text;
    internal sealed class Configuration : DbMigrationsConfiguration<JoinINN.Infrastructure.GroupsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JoinINN.Infrastructure.GroupsDb context)
        {
            var mySHA256 = SHA256Managed.Create();

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
                new Admin {Id=0, Username="ebehmen", Password=sha256_hash("ebehmen") },
                new Admin {Id=1, Username="mjelaska", Password=sha256_hash("mjelaska") },
                new Admin {Id=2, Username="mceprnja", Password=sha256_hash("mceprnja") }
            };

            var socialGroups = new[]
            {
                new SocialGroup { Name = "Klub mladih Split", EmailAddress = "klubmladih.split@gmail.com", Password = sha256_hash("12345678"), ContactNumber = "0953509829", OfficialWebUrl="http://www.klubmladihsplit.hr/", FacebookPageUrl="https://www.facebook.com/klubmladihsplit/?fref=ts", Description="Prostor za rad, učenje, druženje i zabavu mladih. Program financira Ministarstvo socijalne politike i mladih.", IsSchool=false, IsAssociation=true, photoUrl="https://scontent-vie1-1.xx.fbcdn.net/hphotos-xpt1/v/t1.0-9/10592750_527662070710011_3258294634430693170_n.png?oh=200336d25482ea5a4df95fca6dd17266&oe=5705B5F1", AffinityType=afinityTypes[3], City=cities[0] },
                new SocialGroup { Name = "Volonterski centar Zagreb", EmailAddress = "kontakt@vcst.info", Password = sha256_hash("12345678"), ContactNumber = "091824791", OfficialWebUrl="http://www.vcst.info/", FacebookPageUrl="https://www.facebook.com/volonterskicentarzagreb/?fref=ts", Description="Volonterski centar Split redovito održava bazu kontakata s osobama koje žele volontirati kao i s organizacijama koje trebaju volontere.", IsSchool=false, IsAssociation=true, photoUrl="http://www.zagrebonline.hr/wp-content/uploads/2014/01/volonterski-centar.jpg", AffinityType=afinityTypes[3],City=cities[1]  },
                new SocialGroup { Name = "Udruga Sunce", EmailAddress = "info@sunce-st.org", Password = sha256_hash("12345678"), ContactNumber = "098747828", OfficialWebUrl="http://sunce-st.org/", FacebookPageUrl="https://www.facebook.com/Udruga-za-prirodu-okoli%C5%A1-i-odr%C5%BEivi-razvoj-Sunce-241406625901590/", Description="Zauzimamo se za povećanje standarda i unapređenje sustava zaštite okoliša i prirode.", IsSchool=false, IsAssociation=true, photoUrl="https://nespalionici.files.wordpress.com/2011/11/hr-logo-gore-color.jpg", AffinityType=afinityTypes[7], City=cities[0]  },
                new SocialGroup { Name = "Ivora - škola informatike", EmailAddress = "informacije@ivora.hr", Password = sha256_hash("12345678"), ContactNumber = "099188292", OfficialWebUrl="http://ivora.hr/", FacebookPageUrl="https://www.facebook.com/ivora.eu", Description="Ivora - škola informatike vodeća je ustanova u Republici Hrvatskoj za obrazovanje odraslih.", IsSchool=false, IsAssociation=true, photoUrl="http://ivora.hr/images/logo.jpg", AffinityType=afinityTypes[6], City=cities[1] },
                new SocialGroup { Name = "Glazbena škola Blagoja Berse", EmailAddress = "glazbena@bersa.hr", Password = sha256_hash("12345678"), ContactNumber = "0991849288", OfficialWebUrl="http://bersa.hr/", FacebookPageUrl="https://www.facebook.com/profile.php?id=100008410572108&fref=ts", Description="Najčešći motiv za upis u glazbenu školu je želja da se nauči svirati odabrani instrument. Glazbena škola će ipak pružiti mnogo više od toga..", IsSchool=false, IsAssociation=true, photoUrl="http://www.ezadar.hr/repository/image_raw/375431/large/", AffinityType=afinityTypes[2], City=cities[1] },
                new SocialGroup { Name = "Udruga za autizam Zagreb", EmailAddress = "uaz.zagreb@gmail.com", Password = sha256_hash("12345678"), ContactNumber = "0953001555", OfficialWebUrl="http://www.autizam-zagreb.com/", FacebookPageUrl="https://www.facebook.com/autizam.zg/?fref=ts", Description="Misija Udruge za autizam – Zagreb je zaštita i promicanje prava i poboljšanje kvalitete života osoba s autizmom na području Grada Zagreba i Zagrebačke županije.", IsSchool=false, IsAssociation=true, photoUrl="http://www.autizam-suzah.hr/images/clanice_logo/Logo_-_Zagreb_-_Zagreb.jpg", AffinityType=afinityTypes[7], City=cities[1]  }
            };

            context.Admins.AddRange(admins);
            context.AffinityTypes.AddRange(afinityTypes);
            context.Cities.AddRange(cities);
            context.SocialGroups.AddRange(socialGroups);
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
