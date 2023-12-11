using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> UpdateAsync(Customer entity);

    }

}
