using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Application.DTOs;

namespace TrainingPlanner.Application.Contracts
{
    public interface IWorkoutService
    {
        public Task<TrainingType> GetWorkoutById(int id);
    }
}