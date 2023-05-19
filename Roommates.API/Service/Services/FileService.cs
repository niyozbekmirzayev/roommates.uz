using AutoMapper;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Service.Interfaces;

namespace Roommates.Api.Service.Services
{
    public class FileService : BaseService, IFileService
    {
        public FileService(IHttpContextAccessor httpContextAccessor, 
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }
    }
}
