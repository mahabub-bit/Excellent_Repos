using ECM_ExcellentWeb.Models.Dto;

namespace ECM_ExcellentWeb.Service.IService
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO objToCreate);
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
    }
}
