using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UpdateAsync(Product entity);
    }

}
