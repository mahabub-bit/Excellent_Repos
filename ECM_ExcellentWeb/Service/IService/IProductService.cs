﻿using ECM_ExcellentWeb.Model.Dto;

namespace ECM_ExcellentWeb.Service.IService
{
    public interface IProductService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(ProductCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(ProductUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
