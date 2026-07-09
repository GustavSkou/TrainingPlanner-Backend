using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Infrastructure.Contracts;

namespace TrainingPlanner.Application.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;

        public TrainingPlanService(ITrainingPlanRepository trainingPlanRepository)
        {
            _trainingPlanRepository = trainingPlanRepository;
        }

        public Task<bool> CreateTrainingPlan(TrainingPlanDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<TrainingPlan> GetPlanById(int id)
        {
            return await _trainingPlanRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TrainingPlan>> GetPlansByUserId(int userId)
        {
            return await _trainingPlanRepository.GetByUserIdAsync(userId);            
        }

        private bool IsTrainingPlanValid(TrainingPlanDTO dto) {
            throw new NotImplementedException();
        }
    }
}
