using ECM_ExcellentAPI.Model;
using ECM_ExcellentAPI.Model.Dto;

namespace ECM_ExcellentAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
