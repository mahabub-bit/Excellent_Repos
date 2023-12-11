using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface IProductRateHistoryRepository : IRepository<Product_Rate_History>
    {
        Task<Product_Rate_History> UpdateAsync(Product_Rate_History entity);
    }

}
