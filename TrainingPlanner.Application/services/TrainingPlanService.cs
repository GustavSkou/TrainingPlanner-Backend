using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.DTOs;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Domain.Contracts;

namespace TrainingPlanner.Application.Services
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;
        private readonly ITrainingTypeService _trainingTypeService;

        public TrainingPlanService(ITrainingPlanRepository trainingPlanRepository, ITrainingTypeService trainingTypeService)
        {
            _trainingPlanRepository = trainingPlanRepository;
            _trainingTypeService = trainingTypeService;
        }

        public async Task<TrainingPlan> CreateTrainingPlan(TrainingPlanDTO dto)
        {
            if (!await IsTrainingPlanValid(dto)) {
                throw new ArgumentException("Invalid training plan data.");
            }

            TrainingPlan plan = new TrainingPlan
            {
                UserId = dto.UserId,
                TrainingTypeId = dto.TrainingTypeId,
                Name = dto.Name,
                Description = dto.Description,
                Date = dto.Date.ToUniversalTime(),
                CreatedAt = DateTime.Now.ToUniversalTime(),
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

        private async Task<bool> IsTrainingPlanValid(TrainingPlanDTO dto)
        {
            return dto != null &&
                   dto.UserId > 0 &&
                   dto.TrainingTypeId > 0 &&
                   //dto.TrainingTypeId <= (await _trainingTypeService.GetTypes()).Count() &&
                   dto.Date != default;
        }
    }
}
