
using Ecommerce.Data.Models.Entities;

namespace Ecommerce.Repository.Repositories.PromotionRepository
{
    public interface IPromotion
    {
        public Promotion AddPromotion(Promotion promotion);
        public Promotion UpdatePromotion(Promotion promotion);
        public Promotion GetPromotionById(Guid id);
        public Promotion DeletePromotionById(Guid id);
        public IEnumerable<Promotion> GetAllPromotions();
        public void SaveChanges();
        public Promotion Upsert(Promotion promotion);
    }
}
