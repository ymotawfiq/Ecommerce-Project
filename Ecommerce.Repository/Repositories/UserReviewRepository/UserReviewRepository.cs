

using Ecommerce.Data;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Repositories.UserReviewRepository
{
    public class UserReviewRepository : IUserReview
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<SiteUser> _userManager;
        public UserReviewRepository(ApplicationDbContext _dbContext, UserManager<SiteUser> _userManager)
        {
            this._dbContext = _dbContext;
            this._userManager = _userManager;
        }

        public async Task<UserReview> AddUserReviewAsync(UserReview userReview)
        {
            try
            {
                await _dbContext.UserReview.AddAsync(userReview);
                await SaveChangesAsync();
                return userReview;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserReview> DeleteUserReviewByIdAsync(Guid userReviewId)
        {
            try
            {
                UserReview userReview = await GetUserReviewByIdAsync(userReviewId);
                _dbContext.UserReview.Remove(userReview);
                await SaveChangesAsync();
                return userReview;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserReview>> GetAllUserReviewsAsync()
        {
            try
            {
                return await _dbContext.UserReview.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserReview>> GetAllUserReviewsByOrderLineIdAsync(Guid orderLineId)
        {
            try
            {
                return
                    from u in await GetAllUserReviewsAsync()
                    where u.OrderId == orderLineId
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserReview>> GetAllUserReviewsByUserUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(usernameOrEmail);
                return
                    from u in await GetAllUserReviewsAsync()
                    where u.UserId == user?.Id
                    select u;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserReview> GetUserReviewByIdAsync(Guid userReviewId)
        {
            try
            {
                UserReview? userReview = await _dbContext.UserReview
                    .Where(e => e.Id == userReviewId).FirstOrDefaultAsync();
                return userReview;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserReview> UpdateUserReviewAsync(UserReview userReview)
        {
            try
            {
                UserReview userReview1 = await GetUserReviewByIdAsync(userReview.Id);
                userReview1.OrderId = userReview.OrderId;
                userReview1.Rate = userReview.Rate;
                userReview1.UserId = userReview.UserId;
                userReview1.Comment = userReview.Comment;
                await SaveChangesAsync();
                return userReview1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserReview> UpsertAsync(UserReview userReview)
        {
            try
            {
                UserReview userReview1 = await GetUserReviewByIdAsync(userReview.Id);
                if (userReview1 == null)
                {
                    return await AddUserReviewAsync(userReview);
                }
                return await UpdateUserReviewAsync(userReview);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
