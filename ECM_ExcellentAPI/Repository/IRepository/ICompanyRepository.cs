using ECM_ExcellentAPI.Model;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> UpdateAsync(Company entity);

    }

}
