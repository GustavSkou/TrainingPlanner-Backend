using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Contracts;
using TrainingPlanner.Application.Contracts;

namespace TrainingPlanner.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(UserDTO dto)
        {
            if (!IsUserValid(dto))
                throw new Exception($"{dto} is not valid");

            if (await DoesUserExists(dto))
                throw new Exception($"{dto} already exists in database");

            User user = new () {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                LoginProvider = dto.LoginProvider,  
                NameIdentifier = dto.NameIdentifier,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _userRepository.AddAsync(user);
        }

        private bool IsUserValid(UserDTO dto)
        {
            return  !string.IsNullOrEmpty(dto.FirstName) || 
                    !string.IsNullOrEmpty(dto.LastName) || 
                    !string.IsNullOrEmpty(dto.Email) || 
                    !string.IsNullOrEmpty(dto.NameIdentifier) || 
                    !string.IsNullOrEmpty(dto.LoginProvider);
        }

        private async Task<bool> DoesUserExists(UserDTO dto)
        {
            return await _userRepository.DoesEmailExists(dto.Email);
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}