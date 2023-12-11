using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface ICategoryRepository :IRepository<Category>
    {
        Task<Category> UpdateAsync(Category entity);
    }
}
