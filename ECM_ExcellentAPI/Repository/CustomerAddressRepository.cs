using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerAddressRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<CustomerAddress> UpdateAsync(CustomerAddress entity)
        {
            _db.CustomersAddress.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
