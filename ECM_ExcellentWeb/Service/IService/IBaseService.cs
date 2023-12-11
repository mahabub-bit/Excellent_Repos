using ECM_ExcellentWeb.Models;

namespace ECM_ExcellentWeb.Service.IService
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
