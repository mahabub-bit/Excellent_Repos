using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> UpdateAsync(Order entity);
    }

}
