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

        public async Task<TrainingPlan> CreateTrainingPlan(TrainingPlanDTO dto)
        {
            TrainingPlan plan = new TrainingPlan
            {
                UserId = dto.UserId,
                TrainingTypeId = dto.TrainingTypeId,
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };

            return await _trainingPlanRepository.AddAsync(plan);
            /* 
                The passed "plan" object itself, is modifed to represent the entity after being saved to the db.
                So the plan variable could just be return, reather than return the method result, which is the same object.
                BUT, maybe this is a more logical solution 
            */
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
