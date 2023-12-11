using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface ICategoryTypeRepository : IRepository<CategoryType>
    {
        Task<CategoryType> UpdateAsync(CategoryType entity);
    }
}
