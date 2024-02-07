using Core.DTOs;

namespace Core.Interfaces
{
    public interface IRegistration
    {
        public Task<TokenDTO> Registration(RegisterUserDTO dto);
    }
}
