﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Roommates.Api.Data.IRepositories;
using Roommates.Api.Helpers;
using Roommates.Api.Service.Interfaces;
using Roommates.Api.Service.ViewModels;
using Roommates.Infrastructure.Enums;
using Roommates.Infrastructure.Models;
using Roommates.Infrastructure.Response;

namespace Roommates.Api.Service.Services
{
    public class PostService : BaseService, IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IUserPostRepository userPostRepository;

        public PostService(
           IPostRepository postRepository,
           IMapper mapper,
           ILogger<PostService> logger,
           IUnitOfWorkRepository unitOfWorkRepository,
           IHttpContextAccessor httpContextAccessor,
           IUserPostRepository userPostRepository,
           IConfiguration configuration) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
            this.postRepository = postRepository;
            this.userPostRepository = userPostRepository;
        }

        public async Task<BaseResponse> CreatePostAsync(CreatePostViewModel viewModel)
        {
            var response = new BaseResponse();
            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            var newLocation = mapper.Map<Location>(viewModel.Location);
            newLocation.AuthorUserId = currentUserId;
            newLocation = await unitOfWorkRepository.LocationRepository.AddAsync(newLocation);

            var newPost = mapper.Map<Post>(viewModel);
            newPost.CreatedByUserId = currentUserId;
            newPost.LocationId = newLocation.Id;

            newPost = await postRepository.AddAsync(newPost);

            // Saving AppartmentViewFiles
            if (viewModel.AppartmentViewFiles != null && viewModel.AppartmentViewFiles.Any(l => l.ActionType == ActionType.Create))
            {
                var filesIds = viewModel.AppartmentViewFiles.Where(l => l.ActionType == ActionType.Create).Select(l => l.FileId);
                var files = unitOfWorkRepository.FileRepository.GetAll().Where(l => filesIds.Contains(l.Id));

                foreach (var file in files)
                {
                    var newFilePost = new FilePost()
                    {
                        FileId = file.Id,
                        PostId = newPost.Id,
                    };

                    await unitOfWorkRepository.FilePostRepository.AddAsync(newFilePost);
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
            if(isAlreadyLiked) 
            {
                response.Error = new BaseError("post is already liked", ErrorCodes.Conflict);
                response.ResponseCode = ResponseCodes.ERROR_DUPLICATE_DATA;

                return response;
            }

            var dislikedPost = userPostRepository.GetAll().FirstOrDefault(l => l.UserId == currentUserId && l.Id == postId && l.UserPostRelationType == UserPostRelationType.Disliked);
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

        public async Task<BaseResponse> ViewPostAsync(Guid postId) 
        {
            var response = new BaseResponse();
            var currentUserId = WebHelper.GetUserId(httpContextAccessor.HttpContext);

            var post = postRepository.GetAll().Include(l => l.AppartmentViewFiles)
                                              .Include(l => l.CreatedByUser)
                                              .Include(l => l.Location)
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

                post.ViewedCount++;
                postRepository.Update(post);

                if (await postRepository.SaveChangesAsync() > 0)
                {
                    bool isLiked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Liked);
                    bool IsDisliked = userPostRepository.GetAll().Any(l => l.UserId == currentUserId && l.PostId == postId && l.UserPostRelationType == UserPostRelationType.Disliked);

                    var postView = mapper.Map<ViewPostViewModel>(post);
                    postView.IsLiked = isLiked;
                    postView.IsDisliked = IsDisliked;

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

                response.Data = postView;
                response.ResponseCode = ResponseCodes.SUCCESS_GET_DATA;

                return response;
            }
        }
    }
}
