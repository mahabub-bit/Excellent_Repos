using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface IOrderStatusRepository : IRepository<OrderStatus>
    {
        Task<OrderStatus> UpdateAsync(OrderStatus entity);
    }

}
