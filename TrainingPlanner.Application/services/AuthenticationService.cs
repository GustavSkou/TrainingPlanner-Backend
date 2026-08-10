using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Contracts;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                throw new UnauthorizedAccessException("Email and password are required.");
            }

            User? user = await _userRepository.GetUserByEmail(dto.Email);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var passwordHasher = new PasswordHasher<User>();
            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            string issuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT issuer is not configured.");
            string audience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT audience is not configured.");
            string key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.UtcNow.AddHours(24);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new LoginResponseDTO
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt,
                UserId = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}".Trim()
            };
        }
    }
}
