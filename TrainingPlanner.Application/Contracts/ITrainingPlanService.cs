using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Application.DTOs;

namespace TrainingPlanner.Application.Contracts
{
    public interface ITrainingPlanService
    {
        public Task<TrainingPlan> GetPlanById(int id);
        public Task<IEnumerable<TrainingPlan>> GetPlansByUserId(int userId);
        public Task<bool> CreateTrainingPlan(TrainingPlanDTO dto);
    }
}