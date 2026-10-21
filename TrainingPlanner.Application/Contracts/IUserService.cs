using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Application.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetUsersById(IEnumerable<int> ids);
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByEmail(string email);

        Task<User> CreateUserAsync(UserDTO dto);
    }
}