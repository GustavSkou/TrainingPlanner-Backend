using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Infrastructure.Contracts;

namespace TrainingPlanner.Infrastructure.Data
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DoesEmailExists(string email)
        {
            return await _context.Users
            .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetUsersById(int id)
        {
            return await _context.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersById(IEnumerable<int> ids)
        {
            if(ids.Count() <= 0) 
                return new List<User>();

            return await _context.Users
                .Where(u => ids.Contains(u.Id))
                .ToListAsync();
        }
    }
}
