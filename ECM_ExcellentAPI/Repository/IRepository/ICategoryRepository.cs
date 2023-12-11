using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface ICustomerAddressRepository : IRepository<CustomerAddress>
    {
        Task<CustomerAddress> UpdateAsync(CustomerAddress entity);
    }
}
