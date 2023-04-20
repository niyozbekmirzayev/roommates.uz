using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Roommates.Domain.Enums;
using Roommates.Domain.Models.Locations;
using Roommates.Domain.Models.Posts;
using Roommates.Domain.Models.Roommates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data
{
    public class PrepDatabase
    {
        public readonly IConfiguration configuration;
        public readonly RoommatesDbContext dbContext;

        public PrepDatabase(IConfiguration configuration, RoommatesDbContext dbContext)
        {
            this.configuration = configuration;
            this.dbContext = dbContext;

            SeedData();
        }

        public void SeedData()
        {
            var seedDataInfo = configuration["AddSeedData"];
            if (!string.IsNullOrEmpty(seedDataInfo))
            {
                bool addSeedData = bool.Parse(seedDataInfo);
                if (addSeedData && !dbContext.Roommates.Any())
                {
                    var posts = new List<Post>()
                    {
                        new Post()
                        {
                             Title = "Appartment with 2 rooms for renting",
                             Address = "New York, Wall Street, 54",
                             IsForSelling = false,
                             Description = "Good and peace place",
                             PreferedRoommateGender = Gender.NotSpecified,
                             ViewedTime = 1,
                             CurrencyType = CurrencyType.USD,
                             Price = 2000,
                             PricePeriodType = PricePeriodType.Monthly,
                             RoomsCount = 2,
                             Location = new Location
                             {
                                 Name = "New York, Wall Street, 54",
                                 Latitude = 38.8951,
                                 Longitude = -77.0364
                             },
                        }
                    };

                    var roommates = new List<Roommate>()
                    {
                        new Roommate()
                        {
                            FirstName = "Clear",
                            LastName = "Adams",
                            Gender = Gender.Female,
                            PhoneNumber = "7857485748",
                            IsPhoneNumberVerified = false,
                            LikedPosts = posts,
                        },
                        new Roommate()
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Gender = Gender.Male,
                            PhoneNumber = "7857353448",
                            IsPhoneNumberVerified = true,
                            OwnPosts = posts,
                        },

                    };

                    dbContext.Roommates.AddRange(roommates);

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
