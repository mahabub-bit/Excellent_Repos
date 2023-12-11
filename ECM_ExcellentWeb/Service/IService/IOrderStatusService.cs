using ECM_ExcellentWeb.Model.Dto;

namespace ECM_ExcellentWeb.Service.IService
{
    public interface IOrderStatusService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(OrderStatusCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(OrderStatusUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
