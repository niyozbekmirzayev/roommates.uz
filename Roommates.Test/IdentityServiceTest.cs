using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Roommates.Api.Data;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.Repositories;
using Roommates.Api.Service.Interfaces;
using Roommates.Api.Service.Mapping;
using Roommates.Api.Service.Services;
using Roommates.Api.Service.ViewModels;
using Roommates.Infrastructure.Models;
using Roommates.Infrastructure.Response;
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
        private Mock<IUserRepository> moqUserRepository;
        private Mock<IUnitOfWorkRepository> moqUnitOfWorkRepository;
        private Mock<IHttpContextAccessor> moqHttpContextAccessor;

        public IdentityServiceTest()
        {
            mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingConfig());
            }).CreateMapper();

            moqEmailService = new();
            moqConfiguration = new();
            moqLogger = new();
            moqUserRepository = new();
            moqUnitOfWorkRepository = new();
            moqHttpContextAccessor = new();
        }

        private void ConfigureDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            applicationDbContext = new ApplicationDbContext(dbOptions);
            userRepository = new UserRepository(applicationDbContext);
        }

        private void ConfigureService(IUserRepository userRepository)
        {
            identityService = new IdentityService(userRepository, mapper, moqEmailService.Object, moqLogger.Object, moqUnitOfWorkRepository.Object, moqHttpContextAccessor.Object, moqConfiguration.Object);
        }

        #region TestCases

        #region CreateUserAsync

        [Fact]
        public async Task CreateUserAsync_WhenEmailDuplicateIsDuplicate_ReturnsErrorDuplicateData()
        {
            // Arrange
            ConfigureDatabase();
            ConfigureService(userRepository);

            var existUser = new User()
            {
                FirstName = "Wick",
                EmailAddress = "fake@gmail.com",
                Password = "password"
            };

            await userRepository.AddAsync(existUser);
            await userRepository.SaveChangesAsync();

            var view = new CreateUserViewModel()
            {
                FirstName = "John",
                EmailAddress = existUser.EmailAddress,
                Password = "password",
            };

            // Act
            var response = await identityService.CreateUserAsync(view);

            // Assert
            Assert.Equal(ResponseCodes.ERROR_DUPLICATE_DATA, response.ResponseCode);
        }

        [Fact]
        public async Task CreateUserAsync_WhenParametersAreValid_UserIsSaved_ReturnsSuccessAddData()
        {
            // Arrange
            ConfigureDatabase();
            ConfigureService(userRepository);

            var view = new CreateUserViewModel()
            {
                FirstName = "John",
                EmailAddress = "fake@gmail.com",
                Password = "password",
            };

            // Act
            var response = await identityService.CreateUserAsync(view);

            // Assert
            Assert.Equal(ResponseCodes.SUCCESS_ADD_DATA, response.ResponseCode);

            var addedUser = userRepository.Get(Guid.Parse(response.Data.ToString()));
            Assert.NotNull(addedUser);
        }

        [Fact]
        public async Task CreateUserAsync_NoChangesMadeInDatabase_ReturnsErrorSaveData()
        {
            // Arrange
            moqUserRepository.Setup(l => l.SaveChangesAsync()).Returns(Task.FromResult(0));
            moqUserRepository.Setup(l => l.GetAll(It.IsAny<bool>())).Returns(new List<User>().AsQueryable());

            var moqAddedUser = new User()
            {
                Id = Guid.NewGuid(),
            };
            moqUserRepository.Setup(l => l.AddAsync(It.IsAny<User>())).Returns(Task.FromResult(moqAddedUser));

            ConfigureService(moqUserRepository.Object);

            var view = new CreateUserViewModel()
            {
                FirstName = "John",
                EmailAddress = "fake@gmail.com",
                Password = "password",
            };

            // Act
            var response = await identityService.CreateUserAsync(view);

            // Assert
            Assert.Equal(ResponseCodes.ERROR_SAVE_DATA, response.ResponseCode);
        }

        #endregion

        #endregion
    }
}
