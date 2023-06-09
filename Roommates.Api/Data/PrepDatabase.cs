﻿using Roommates.Api.Helpers;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Data
{
    public class PrepDatabase
    {
        public const string SCHEMA_NAME = "roomates";
        public readonly IConfiguration configuration;
        public readonly ApplicationDbContext dbContext;

        public PrepDatabase(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            this.configuration = configuration;
            this.dbContext = dbContext;

            //CreateScheme(SCHEMA_NAME);
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
                            Id = Guid.Parse("3fc263c0-9093-4848-ac96-c8a828345dee"),
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
                            Id = Guid.Parse("82ed2037-6217-452d-832a-78adcb25a812"),
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
                            },
                        },

                    };

                    var posts = new List<Post>()
                    {
                        new Post()
                        {
                             Id = Guid.Parse("d45b9bee-559b-4de5-a8e8-ae1608eb7133"),
                             StaticFeatures = new StaticFeatures()
                             {
                                IsForSelling = false,
                                PreferedClientType = ClientType.All,
                                CurrencyType = CurrencyType.USD,
                                Price = 2000,
                                PricePeriodType = PricePeriodType.Monthly,
                                RoomsCount = 2,
                             },
                             Location = new Location
                             {
                                Name = "New York, Wall Street, 54",
                                Latitude = 38.8951,
                                Longitude = -77.0364,
                             },
                             Title = "Appartment with 2 rooms for renting",
                             Description = "Good and peaceful place",
                             ViewsCount = 1,
                             CreatedById = users.First().Id,
                        }
                    };

                    var usersPosts = new List<UserPost>()
                    {
                        new UserPost()
                        {
                            UserId = users.Last().Id,
                            UserPostRelationType = UserPostRelationType.Viewed,
                            PostId = posts.First().Id,
                        }
                    };

                    dbContext.Posts.AddRange(posts);
                    dbContext.Users.AddRange(users);
                    dbContext.UserPosts.AddRange(usersPosts);

                    dbContext.SaveChanges();
                }
            }
        }

        /* public void CreateScheme(string schemaName) 
         {
             var schemas = GetSchemas();

             bool schemaExists = schemas.Contains(schemaName);
             if (!schemaExists) 
             {
                 dbContext.Database.Migrate();
                 dbContext.SaveChanges();
             }

         }

         public List<string> GetSchemas()
         {
             var schemas = new List<string>();

             using (var connection = dbContext.Database.GetDbConnection())
             {
                 connection.Open();

                 var command = connection.CreateCommand();
                 command.CommandText = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA";

                 using (var reader = command.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         var schemaName = reader.GetString(0);
                         schemas.Add(schemaName);
                     }
                 }
             }

             return schemas;
         }*/
    }
}
