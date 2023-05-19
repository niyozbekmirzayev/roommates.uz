﻿using AutoMapper;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Service.Interfaces;

namespace Roommates.Api.Service.Services
{
    public abstract class BaseService : IBaseService
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly IMapper mapper;
        protected readonly IConfiguration configuration;
        protected readonly IUnitOfWorkRepository unitOfWorkRepository;
        protected readonly ILogger logger;

        public BaseService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.configuration = configuration;
            this.unitOfWorkRepository = unitOfWorkRepository;
            this.logger = logger;
        }
    }
}