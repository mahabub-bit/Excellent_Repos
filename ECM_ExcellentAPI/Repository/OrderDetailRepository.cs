using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<OrderDetail> UpdateAsync(OrderDetail entity)
        {
            _db.OrdersDetail.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
