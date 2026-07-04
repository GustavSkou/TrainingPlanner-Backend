using TrainingPlanner.Application.DTOs;

namespace TrainingPlanner.Application.Contracts
{
    public interface IAuthService
    {
        public Task<bool> Register(UserDTO userDTO);
        public Task<bool> Login(UserDTO userDTO);
    }
}