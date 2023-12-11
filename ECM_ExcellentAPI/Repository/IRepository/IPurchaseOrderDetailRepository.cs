using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface IPurchaseOrderDetailRepository : IRepository<PurchaseOrderDetail>
    {
        Task<PurchaseOrderDetail> UpdateAsync(PurchaseOrderDetail entity);
    }
}
