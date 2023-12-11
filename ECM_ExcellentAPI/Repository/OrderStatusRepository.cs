using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderStatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<OrderStatus> UpdateAsync(OrderStatus entity)
        {
            _db.OrderStatuses.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
