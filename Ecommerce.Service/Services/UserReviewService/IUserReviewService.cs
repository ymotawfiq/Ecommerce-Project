

using Ecommerce.Data.DTOs;
using Ecommerce.Data.Models.ApiModel;
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Service.Services.UserReviewService
{
    public interface IUserReviewService
    {
        Task<ApiResponse<UserReview>> AddUserReviewAsync(UserReviewDto userReviewDto);
        Task<ApiResponse<UserReview>> UpdateUserReviewAsync(UserReviewDto userReviewDto);
        Task<ApiResponse<UserReview>> DeleteUserReviewByIdAsync(Guid userReviewId);
        Task<ApiResponse<UserReview>> GetUserReviewByIdAsync(Guid userReviewId);
        Task<ApiResponse<IEnumerable<UserReview>>> GetAllUserReviewsAsync();
        Task<ApiResponse<IEnumerable<UserReview>>> GetAllUserReviewsByOrderLineIdAsync(Guid orderLineId);
        Task<ApiResponse<IEnumerable<UserReview>>> GetAllUserReviewsByUserUsernameOrEmailAsync(string usernameOrEmail);
    }
}
