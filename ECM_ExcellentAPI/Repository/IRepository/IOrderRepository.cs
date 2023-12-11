using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        Task<PurchaseOrder> UpdateAsync(PurchaseOrder entity);
    }

}
