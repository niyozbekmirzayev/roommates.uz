using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Roommates.Data;
using Roommates.Data.IRepositories;
using Roommates.Data.Repositories;
using Roommates.Domain.Models.Roommates;
using Roommates.Service.Interfaces;
using Roommates.Service.Mapping;
using Roommates.Service.Response;
using Roommates.Service.Services;
using Roommates.Service.ViewModels;
using Xunit;

namespace Roommates.Test
{
    public class IdentityServiceTest
    {
        private ApplicationDbContext applicationDbContext;
        private IdentityService identityService;
        private IUserRepository userRepository;
        private IMapper mapper;
        private Mock<IEmailService> moqEmailService;
        private Mock<ILogger<IdentityService>> moqLogger;
        private Mock<IConfiguration> moqConfiguration;

        public IdentityServiceTest() 
        {
            mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfig());
            }).CreateMapper();

            moqEmailService = new();
            moqConfiguration = new();
            moqLogger = new();
        }

        private void ConfigureDatabase() 
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            applicationDbContext = new ApplicationDbContext(dbOptions);
            userRepository = new UserRepository(applicationDbContext);
        }

        private void ConfigureService() 
        {
            identityService = new IdentityService(userRepository, mapper, moqEmailService.Object, moqLogger.Object, moqConfiguration.Object);
        }

        #region TestCases
        
        [Fact]
        public async Task CreateUserAsync_WhenDuplicateEmailIsGiven_ReturnsErrorDuplicateData() 
        {
            // Arrange
            ConfigureDatabase();
            ConfigureService();

            var existUser = new User()
            {
                 FirstName = "Wick",
                 Email = "fake@gmail.com",
                 Password = "password"
            };

            await userRepository.AddAsync(existUser);
            await userRepository.SaveChangesAsync();

            var view = new CreateUserViewModel()
            {
                FirstName = "John",
                Email = existUser.Email,
                Password = "password",
            };

            // Act
            var response = await identityService.CreateUserAsync(view);

            // Assert
            Assert.Equal(ResponseCodes.ERROR_DUPLICATE_DATA, response.ResponseCode); 
        }

        #endregion
    }
}
