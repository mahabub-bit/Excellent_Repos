using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface ISuppilerRepository : IRepository<Supplier>
    {
        Task<Supplier> UpdateAsync(Supplier entity);
    }

}
