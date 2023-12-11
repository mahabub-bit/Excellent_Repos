using ECM_ExcellentWeb.Model.Dto;
using ECM_ExcellentWeb.Models;
using ECM_ExcellentWeb.Service.IService;
using ECM_Utility;

namespace ECM_ExcellentWeb.Service
{
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string excellentUrl;

        public CompanyService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = httpClient;
            excellentUrl = configuration.GetValue<string>("ServiceUrls:ExcellentAPI");
        }

        public Task<T> CreateAsync<T>(CompanyCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = excellentUrl + "/api/companyAPI",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = excellentUrl + "/api/companyAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = excellentUrl + "/api/companyAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = excellentUrl + "/api/companyAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CompanyUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = excellentUrl + "/api/companyAPI/" + dto.Id,
                Token = token
            });
        }
    }
}
