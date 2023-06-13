using AutoMapper;
using Roommates.Api.Data.IRepositories.Base;
using Roommates.Api.Interfaces.Base;

namespace Roommates.Api.Services.Base
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;
        protected readonly IUnitOfWorkRepository _unitOfWorkRepository;
        protected readonly ILogger _logger;

        public BaseService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _configuration = configuration;
            _unitOfWorkRepository = unitOfWorkRepository;
            _logger = logger;
        }
    }
}
