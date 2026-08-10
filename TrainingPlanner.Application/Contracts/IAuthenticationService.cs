using TrainingPlanner.Application.DTOs;

namespace TrainingPlanner.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto);
    }
}
