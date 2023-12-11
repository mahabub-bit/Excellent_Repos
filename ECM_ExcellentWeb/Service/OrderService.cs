using ECM_ExcellentWeb.Model.Dto;
using ECM_ExcellentWeb.Models;
using ECM_ExcellentWeb.Service.IService;
using ECM_Utility;

namespace ECM_ExcellentWeb.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string excellentUrl;
        public OrderService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = httpClient;
            excellentUrl = configuration.GetValue<string>("ServiceUrls:ExcellentAPI");
        }
        public Task<T> CreateAsync<T>(OrderCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = excellentUrl + "/api/orderAPI",
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = excellentUrl + "/api/orderAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = excellentUrl + "/api/orderAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = excellentUrl + "/api/orderAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(OrderUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = excellentUrl + "/api/orderAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
