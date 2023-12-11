using ECM_ExcellentWeb.Model.Dto;

namespace ECM_ExcellentWeb.Service.IService
{
    public interface IOrderService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(OrderCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(OrderUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);

    }
}
