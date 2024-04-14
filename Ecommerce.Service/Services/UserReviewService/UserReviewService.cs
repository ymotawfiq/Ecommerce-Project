

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Extensions;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Ecommerce.Repository.Repositories.OrderLineRepository;
using Ecommerce.Repository.Repositories.UserReviewRepository;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Service.Services.UserReviewService
{
    public class UserReviewService : IUserReviewService
    {
        private readonly IUserReview _userReviewRepository;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IOrderLine _orderLineRepository;
        public UserReviewService(IUserReview _userReviewRepository, UserManager<SiteUser> _userManager,
            IOrderLine _orderLineRepository)
        {
            this._orderLineRepository = _orderLineRepository;
            this._userManager = _userManager;
            this._userReviewRepository = _userReviewRepository;
        }
        public async Task<ApiResponse<UserReview>> AddUserReviewAsync(UserReviewDto userReviewDto)
        {
            if (userReviewDto == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            var user = await _userManager.FindByEmailAsync(userReviewDto.UsernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    StatusCode = 400
                };
            }
            OrderLine orderLine = await _orderLineRepository.GetOrderLineByIdAsync(userReviewDto.OrderId);
            if (orderLine == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "Order not found",
                    StatusCode = 400
                };
            }
            UserReview userReview = new UserReview
            {
                Comment = userReviewDto.Comment,
                OrderId = userReviewDto.OrderId,
                Rate = userReviewDto.Rate,
                UserId = user.Id
            };
            UserReview newUserReview = await _userReviewRepository.AddUserReviewAsync(userReview);
            return new ApiResponse<UserReview>
            {
                IsSuccess = true,
                Message = "User review saved successfully",
                StatusCode = 201,
                ResponseObject = newUserReview
            };
        }

        public async Task<ApiResponse<UserReview>> DeleteUserReviewByIdAsync(Guid userReviewId)
        {
            UserReview userReview = await _userReviewRepository.GetUserReviewByIdAsync(userReviewId);
            if (userReview == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "User review not found",
                    StatusCode = 400
                };
            }
            UserReview deletedUserReview = await _userReviewRepository.DeleteUserReviewByIdAsync(userReviewId);
            return new ApiResponse<UserReview>
            {
                IsSuccess = true,
                Message = "User review Deleted successfully",
                StatusCode = 200,
                ResponseObject = deletedUserReview
            };
        }

        public async Task<ApiResponse<IEnumerable<UserReview>>> GetAllUserReviewsAsync()
        {
            var usersReviews = await _userReviewRepository.GetAllUserReviewsAsync();
            if (usersReviews.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserReview>>
                {
                    IsSuccess = true,
                    Message = "No reviews found",
                    StatusCode = 200,
                    ResponseObject = usersReviews
                };
            }
            return new ApiResponse<IEnumerable<UserReview>>
            {
                IsSuccess = true,
                Message = "Reviews found successfully",
                StatusCode = 200,
                ResponseObject = usersReviews
            };
        }

        public async Task<ApiResponse<IEnumerable<UserReview>>> GetAllUserReviewsByOrderLineIdAsync(Guid orderLineId)
        {
            OrderLine orderLine = await _orderLineRepository.GetOrderLineByIdAsync(orderLineId);
            if (orderLine == null)
            {
                return new ApiResponse<IEnumerable<UserReview>>
                {
                    IsSuccess = false,
                    Message = "Order line not found",
                    StatusCode = 400,
                };
            }
            var usersReviews = await _userReviewRepository.GetAllUserReviewsByOrderLineIdAsync(orderLineId);
            if (usersReviews.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserReview>>
                {
                    IsSuccess = true,
                    Message = "No reviews found",
                    StatusCode = 200,
                    ResponseObject = usersReviews
                };
            }
            return new ApiResponse<IEnumerable<UserReview>>
            {
                IsSuccess = true,
                Message = "Reviews found successfully",
                StatusCode = 200,
                ResponseObject = usersReviews
            };
        }

        public async Task<ApiResponse<IEnumerable<UserReview>>> GetAllUserReviewsByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            var user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<IEnumerable<UserReview>>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    StatusCode = 400,
                };
            }
            var usersReviews = await _userReviewRepository.GetAllUserReviewsByUserUsernameOrEmailAsync(usernameOrEmail);
            if (usersReviews.ToList().Count == 0)
            {
                return new ApiResponse<IEnumerable<UserReview>>
                {
                    IsSuccess = true,
                    Message = "No reviews found",
                    StatusCode = 200,
                    ResponseObject = usersReviews
                };
            }
            return new ApiResponse<IEnumerable<UserReview>>
            {
                IsSuccess = true,
                Message = "Reviews found successfully",
                StatusCode = 200,
                ResponseObject = usersReviews
            };
        }

        public async Task<ApiResponse<UserReview>> GetUserReviewByIdAsync(Guid userReviewId)
        {
            UserReview userReview = await _userReviewRepository.GetUserReviewByIdAsync(userReviewId);
            if (userReview == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "User review not found",
                    StatusCode = 400
                };
            }
            return new ApiResponse<UserReview>
            {
                IsSuccess = true,
                Message = "User review found successfully",
                StatusCode = 200,
                ResponseObject = userReview
            };
        }

        public async Task<ApiResponse<UserReview>> UpdateUserReviewAsync(UserReviewDto userReviewDto)
        {
            if (userReviewDto == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "Input must not be null",
                    StatusCode = 400
                };
            }
            if (userReviewDto.Id == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "User review id must not be null",
                    StatusCode = 400
                };
            }
            var user = await _userManager.FindByEmailAsync(userReviewDto.UsernameOrEmail);
            if (user == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "User not found",
                    StatusCode = 400
                };
            }
            OrderLine orderLine = await _orderLineRepository.GetOrderLineByIdAsync(userReviewDto.OrderId);
            if (orderLine == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "Order not found",
                    StatusCode = 400
                };
            }
            UserReview oldUserReview = await _userReviewRepository
                .GetUserReviewByIdAsync(new Guid(userReviewDto.Id));
            if (oldUserReview == null)
            {
                return new ApiResponse<UserReview>
                {
                    IsSuccess = false,
                    Message = "User review not found",
                    StatusCode = 400
                };
            }
            UserReview userReview = new UserReview
            {
                Id = new Guid(userReviewDto.Id),
                Comment = userReviewDto.Comment,
                OrderId = userReviewDto.OrderId,
                Rate = userReviewDto.Rate,
                UserId = user.Id
            };
            UserReview updatedUserReview = await _userReviewRepository.UpdateUserReviewAsync(userReview);
            return new ApiResponse<UserReview>
            {
                IsSuccess = true,
                Message = "User review updated successfully",
                StatusCode = 200,
                ResponseObject = updatedUserReview
            };
        }
    }
}
