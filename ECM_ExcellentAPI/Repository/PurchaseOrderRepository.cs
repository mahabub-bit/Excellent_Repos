using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<PurchaseOrder> UpdateAsync(PurchaseOrder entity)
        {
            _db.PurchaseOrders.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
