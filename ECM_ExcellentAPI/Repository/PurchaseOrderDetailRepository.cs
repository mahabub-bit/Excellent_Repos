using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class PurchaseOrderDetailRepository : Repository<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseOrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<PurchaseOrderDetail> UpdateAsync(PurchaseOrderDetail entity)
        {
            _db.PurchaseOrdersDetail.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
