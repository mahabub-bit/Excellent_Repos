using ECM_ExcellentAPI.Data;
using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Repository.IRepository;

namespace ECM_ExcellentAPI.Repository
{
    public class CategoryTypeRepository : Repository<CategoryType>, ICategoryTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<CategoryType> UpdateAsync(CategoryType entity)
        {
            _db.CategoryTypes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
