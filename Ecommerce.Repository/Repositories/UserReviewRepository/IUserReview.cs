
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.UserReviewRepository
{
    public interface IUserReview
    {
        Task<UserReview> AddUserReviewAsync(UserReview userReview);
        Task<UserReview> UpdateUserReviewAsync(UserReview userReview);
        Task<UserReview> DeleteUserReviewByIdAsync(Guid userReviewId);
        Task<UserReview> GetUserReviewByIdAsync(Guid userReviewId);
        Task<UserReview> UpsertAsync(UserReview userReview);
        Task SaveChangesAsync();
        Task<IEnumerable<UserReview>> GetAllUserReviewsAsync();
        Task<IEnumerable<UserReview>> GetAllUserReviewsByOrderLineIdAsync(Guid orderLineId);
        Task<IEnumerable<UserReview>> GetAllUserReviewsByUserUsernameOrEmailAsync(string usernameOrEmail);
    }
}
