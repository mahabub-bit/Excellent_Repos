using ECM_ExcellentWeb.Model.Dto;

namespace ECM_ExcellentWeb.Service.IService
{
    public interface ICustomerAddressService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(CustomerAddressCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(CustomerAddressUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);

    }
}
