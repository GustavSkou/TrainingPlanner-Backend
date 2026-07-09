using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Infrastructure.Contracts
{
    public interface IWorkoutRepository
    {
        Task<Workout> AddAsync(Workout workout);
        Task<Workout> GetByIdAsync(int id);
        Task<IEnumerable<Workout>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Workout>> GetByTrainingPlanIdAsync(int trainingPlanId);
    }
}
