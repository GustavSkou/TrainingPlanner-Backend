using System;
using System.Threading.Tasks;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Domain.Contracts;

namespace TrainingPlanner.Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public Task<TrainingType> GetWorkoutById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
