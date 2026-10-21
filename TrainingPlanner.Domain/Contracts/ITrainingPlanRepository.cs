using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Domain.Contracts
{
    public interface ITrainingPlanRepository
    {
        Task<TrainingPlan> AddAsync(TrainingPlan plan);
        Task<IEnumerable<TrainingPlan>> GetAllAsync();
        Task<TrainingPlan?> GetByIdAsync(int id);
        Task<TrainingPlan> UpdateAsync(TrainingPlan plan);
        Task<IEnumerable<TrainingPlan>> GetByUserIdAsync(int userId);
        Task<IEnumerable<TrainingPlan>> GetByUserIdsAsync(IEnumerable<int> userIds);
    }
}
