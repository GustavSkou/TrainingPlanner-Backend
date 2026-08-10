using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Contracts;

using Microsoft.AspNetCore.Identity;
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

            string hashedpassword = HashPassword(dto);

            User user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hashedpassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return user;
        }

        private string HashPassword(UserDTO dto)
        {
            PasswordHasher<UserDTO> passwordHasher = new PasswordHasher<UserDTO>();
            string hashedpassword = passwordHasher.HashPassword(dto, dto.Password);
            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(dto, hashedpassword, dto.Password);

            if (verificationResult != PasswordVerificationResult.Success)
                throw new Exception("Password hashing failed");
            return hashedpassword;
        }

        private bool IsUserValid(UserDTO dto)
        {
            return !string.IsNullOrEmpty(dto.FirstName) || !string.IsNullOrEmpty(dto.LastName) || !string.IsNullOrEmpty(dto.Email) || !string.IsNullOrEmpty(dto.Password);
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