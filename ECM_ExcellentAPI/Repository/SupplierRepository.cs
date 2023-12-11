using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISuppilerRepository
    {
        private readonly ApplicationDbContext _db;
        public SupplierRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Supplier> UpdateAsync(Supplier entity)
        {
            _db.Suppliers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
