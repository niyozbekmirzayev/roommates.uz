using Roommates.Api.Helpers;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data
{
    public class PrepDatabase
    {
        public readonly IConfiguration configuration;
        public readonly ApplicationDbContext dbContext;

        public PrepDatabase(IConfiguration configuration, ApplicationDbContext dbContext)
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
                if (addSeedData && !dbContext.Users.Any())
                {
                    Console.WriteLine("Seeding database");

                    var posts = new List<Post>()
                    {
                        new Post()
                        {
                             Title = "Appartment with 2 rooms for renting",
                             Address = "New York, Wall Street, 54",
                             IsForSelling = false,
                             Description = "Good and peaceful place",
                             PreferedUserGender = Gender.NotSpecified,
                             ViewedCount = 1,
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

                    var emails = new List<Email>
                    {
                        new Email()
                        {
                            Type = EmailType.EmailVerification,
                            EmailAddress = "clearadms@gmail.com",
                            ExpirationDate = DateTime.UtcNow.AddHours(1),
                            VerifiedDate = DateTime.UtcNow
                        },
                        new Email()
                        {
                            Type = EmailType.EmailVerification,
                            EmailAddress = "johndoe@gmail.com",
                            ExpirationDate = DateTime.UtcNow.AddHours(1),
                            VerifiedDate = DateTime.UtcNow
                        }

                    };

                    var users = new List<User>()
                    {
                        new User()
                        {
                            FirstName = "Clear",
                            LastName = "Adams",
                            Gender = Gender.Female,
                            Birthdate = DateTime.UtcNow,
                            PhoneNumber = "7857485748",
                            EmailAddress = "clearadms@gmail.com",
                            EmailVerifiedDate = DateTime.UtcNow,
                            Password = "password".ToSHA256(),
                            PhoneNumberVerifiedDate = null,
                            EmailVerifications = new List<Email>
                            {
                                emails.First(l => l.EmailAddress == "clearadms@gmail.com")
                            }
                        },
                        new User()
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Birthdate = DateTime.UtcNow,
                            Gender = Gender.Male,
                            PhoneNumber = "7857353448",
                            EmailAddress = "johndoe@gmail.com",
                            EmailVerifiedDate = DateTime.UtcNow,
                            PhoneNumberVerifiedDate = DateTime.UtcNow,
                            Password = "password".ToSHA256(),
                            EmailVerifications = new List<Email>
                            {
                                emails.First(l => l.EmailAddress == "johndoe@gmail.com")
                            }
                        },

                    };

                    dbContext.Users.AddRange(users);

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
