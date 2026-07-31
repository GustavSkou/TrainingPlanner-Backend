using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<bool> DoesEmailExists(string mail);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetUsersById(IEnumerable<int> ids);
    }
}
