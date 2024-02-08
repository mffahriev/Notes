using Core.DTOs;

namespace Core.Interfaces
{
    public interface IRegistration
    {
        public Task Registration(RegisterUserDTO dto);
    }
}
