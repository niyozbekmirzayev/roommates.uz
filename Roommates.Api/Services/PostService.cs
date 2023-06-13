using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Data.IRepositories.Base;
using Roommates.Api.Helpers;
using Roommates.Api.Interfaces;
using Roommates.Api.ViewModels;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Services
{
    public class PostService : BaseService, IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IUserPostRepository userPostRepository;
        private readonly IStaticFeaturesRepository staticFeaturesRepository;
        private readonly IDynamicFeatureRepository dynamicFeatureRepository;

        public PostService(
           IPostRepository postRepository,
           IMapper mapper,
           ILogger<PostService> logger,
           IUnitOfWorkRepository unitOfWorkRepository,
           IHttpContextAccessor httpContextAccessor,
           IUserPostRepository userPostRepository,
           IStaticFeaturesRepository staticFeaturesRepository,
           IDynamicFeatureRepository dynamicFeatureRepository,
           IConfiguration configuration) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
            this.dynamicFeatureRepository = dynamicFeatureRepository;
            this.staticFeaturesRepository = staticFeaturesRepository;
            this.postRepository = postRepository;
            this.userPostRepository = userPostRepository;
        }

        public async Task<BaseResponse> CreatePostAsync(CreatePostViewModel viewModel)
        {
            var response = new BaseResponse();
            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            var newLocation = mapper.Map<Location>(viewModel.Location);
            newLocation = await unitOfWorkRepository.LocationRepository.AddAsync(newLocation);

            var newPost = mapper.Map<Post>(viewModel);
            newPost.LocationId = newLocation.Id;
            newPost.Create(currentUserId);
            newPost = await postRepository.AddAsync(newPost);

            if (viewModel.AppartmentViewFiles != null && viewModel.AppartmentViewFiles.Any(l => l.ActionType == ActionType.Create))
            {
                var filesViews = viewModel.AppartmentViewFiles.Where(l => l.ActionType == ActionType.Create).OrderBy(l => l.Sequence).ToList();
                var files = unitOfWorkRepository.FileRepository.GetAll().Where(l => filesViews.Select(l => l.FileId).Contains(l.Id));

                short sequence = 1;
                foreach (var fileView in filesViews)
                {
                    var file = files.FirstOrDefault(l => l.Id == fileView.FileId);
                    if (file != null)
                    {
                        file.IsTemporary = false;
                        unitOfWorkRepository.FileRepository.Update(file);

                        var newFilePost = new FilePost()
                        {
                            FileId = file.Id,
                            PostId = newPost.Id,
                            Sequence = sequence
                        };

                        await unitOfWorkRepository.FilePostRepository.AddAsync(newFilePost);
                        sequence++;
                    }
                }
            }

            if (await postRepository.SaveChangesAsync() > 0)
            {
                response.Data = newPost.Id;
                response.ResponseCode = ResponseCodes.SUCCESS_ADD_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }

        public async Task<BaseResponse> LikePostAsync(Guid postId)
        {
            var response = new BaseResponse();
            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            bool isPostExist = postRepository.GetAll().Any(l => l.Id == postId);
            if (!isPostExist)
            {
                response.Error = new BaseError("post not found", ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            bool isAlreadyLiked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Liked);
            if (isAlreadyLiked)
            {
                response.Error = new BaseError("post is already liked", ErrorCodes.Conflict);
                response.ResponseCode = ResponseCodes.ERROR_DUPLICATE_DATA;

                return response;
            }

            var dislikedPost = userPostRepository.GetAll().FirstOrDefault(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Disliked);
            if (dislikedPost != null)
            {
                userPostRepository.Remove(dislikedPost);
            }

            var newLikedPost = new UserPost
            {
                PostId = postId,
                UserId = currentUserId,
                UserPostRelationType = UserPostRelationType.Liked,
            };
            newLikedPost = await userPostRepository.AddAsync(newLikedPost);

            if (await postRepository.SaveChangesAsync() > 0)
            {
                response.Data = newLikedPost.Id;
                response.ResponseCode = ResponseCodes.SUCCESS_ADD_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }

        public async Task<BaseResponse> DislikePostAsync(Guid postId) 
        {
            var response = new BaseResponse();
            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            bool isPostExist = postRepository.GetAll().Any(l => l.Id == postId);
            if (!isPostExist)
            {
                response.Error = new BaseError("post not found", ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            bool isAlreadyDisliked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Disliked);
            if (isAlreadyDisliked)
            {
                response.Error = new BaseError("post is already disliked", ErrorCodes.Conflict);
                response.ResponseCode = ResponseCodes.ERROR_DUPLICATE_DATA;

                return response;
            }

            var likedPost = userPostRepository.GetAll().FirstOrDefault(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Liked);
            if (likedPost != null)
            {
                userPostRepository.Remove(likedPost);
            }

            var newDislikedPost = new UserPost
            {
                PostId = postId,
                UserId = currentUserId,
                UserPostRelationType = UserPostRelationType.Disliked,
            };
            newDislikedPost = await userPostRepository.AddAsync(newDislikedPost);

            if (await postRepository.SaveChangesAsync() > 0)
            {
                response.Data = newDislikedPost.Id;
                response.ResponseCode = ResponseCodes.SUCCESS_ADD_DATA;

                return response;
            }

            response.Error = new BaseError("no changes made in the database");
            response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

            return response;
        }

        public async Task<BaseResponse> ViewPostAsync(Guid postId)
        {
            var response = new BaseResponse();
            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            var post = postRepository.GetAll().Include(l => l.AppartmentViewFiles)
                                              .Include(l => l.Author)
                                              .Include(l => l.Location)
                                              .Include(l => l.DynamicFeatures)
                                              .Include(l => l.StaticFeatures)
                                              .FirstOrDefault(l => l.Id == postId);
            if (post == null)
            {
                response.Error = new BaseError("post not found", ErrorCodes.NotFoud);
                response.ResponseCode = ResponseCodes.ERROR_NOT_FOUND_DATA;

                return response;
            }

            bool isAlreadyViewed = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Viewed);
            if (!isAlreadyViewed)
            {
                var newViewedPost = new UserPost
                {
                    UserId = currentUserId,
                    PostId = postId,
                    UserPostRelationType = UserPostRelationType.Viewed,
                };
                newViewedPost = await userPostRepository.AddAsync(newViewedPost);

                post.ViewsCount++;
                postRepository.Update(post);

                if (await postRepository.SaveChangesAsync() > 0)
                {
                    bool isLiked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Liked);
                    bool IsDisliked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Disliked);

                    var postView = mapper.Map<ViewPostViewModel>(post);
                    postView.IsLiked = isLiked;
                    postView.IsDisliked = IsDisliked;

                    postView.LikesCount = userPostRepository.GetAll().Count(l => l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Liked);
                    postView.DislikesCount = userPostRepository.GetAll().Count(l => l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Disliked);

                    response.Data = postView;
                    response.ResponseCode = ResponseCodes.SUCCESS_GET_DATA;

                    return response;
                }

                response.Error = new BaseError("no changes made in the database");
                response.ResponseCode = ResponseCodes.ERROR_SAVE_DATA;

                return response;
            }
            else
            {
                bool isLiked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Liked);
                bool IsDisliked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Disliked);

                var postView = mapper.Map<ViewPostViewModel>(post);
                postView.IsLiked = isLiked;
                postView.IsDisliked = IsDisliked;

                postView.LikesCount = userPostRepository.GetAll().Count(l => l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Liked);
                postView.DislikesCount = userPostRepository.GetAll().Count(l => l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Disliked);

                response.Data = postView;
                response.ResponseCode = ResponseCodes.SUCCESS_GET_DATA;

                return response;
            }
        }
    }
}
