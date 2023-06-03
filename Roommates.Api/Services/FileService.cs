using AutoMapper;
using MimeKit;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.IRepositories.Base;
using Roommates.Api.Helpers;
using Roommates.Api.Interfaces;
using Roommates.Infrastructure.Response;
using File = Roommates.Infrastructure.Models.File;

namespace Roommates.Api.Services
{
    public class FileService : BaseService, IFileService
    {
        private IFileRepository fileRepository;

        public FileService(IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            IFileRepository fileRepository,
            ILogger logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<BaseResponse> UploadFile(IFormFile file)
        {
            var response = new BaseResponse();

            if (file == null || file.Length == 0)
            {
                response.Error = new BaseError("file has no content", code: ErrorCodes.BadRequest);
                response.ResponseCode = ResponseCodes.ERROR_INVALID_DATA;

                return response;
            }

            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            byte[] fileData;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileData = memoryStream.ToArray();
            }

            Path.GetExtension(file.FileName);

            var newFile = new File
            {
                Name = file.FileName,
                Extension = Path.GetExtension(file.FileName),
                MimeType = MimeTypes.GetMimeType(file.FileName),
                CreatedById = currentUserId,
                Content = fileData,
                IsTemporary = true
            };

            newFile = await fileRepository.AddAsync(newFile);

            if (await fileRepository.SaveChangesAsync() > 0)
            {
                response.Data = newFile.Id;
                response.ResponseCode = ResponseCodes.SUCCESS_ADD_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }
    }
}
