using System;
using System.Collections.Generic;
using System.Text;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Infrastructure.Contracts
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<bool> DoesEmailExists(string mail);
        //Task<User> GetUsers();
        Task<User> GetUsersById(int id);
        Task<IEnumerable<User>> GetUsersById(IEnumerable<int> id);
    
    }
}
