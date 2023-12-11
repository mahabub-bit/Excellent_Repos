using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class ProductRateHistoryRepository : Repository<Product_Rate_History>, IProductRateHistoryRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRateHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Product_Rate_History> UpdateAsync(Product_Rate_History entity)
        {
            _db.ProductRates.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
