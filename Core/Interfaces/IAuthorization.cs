using Core.DTOs;

namespace Core.Interfaces
{
    public interface IAuthorization
    {
        Task<TokenDTO> GetAccessToken(LoginUserDTO dto);
    }
}
