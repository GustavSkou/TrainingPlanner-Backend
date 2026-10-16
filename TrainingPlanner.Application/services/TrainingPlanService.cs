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

            if (dto.Workout != null)
            {
                plan.Workouts.Add(MapWorkout(dto.Workout, plan));
            }

            return await _trainingPlanRepository.AddAsync(plan);
            /* 
                The passed "plan" object itself, is modifed to represent the entity after being saved to the db.
                So the plan variable could just be return, reather than return the method result, which is the same object.
                BUT, maybe this is a more logical solution 
            */
        }

        public Task<IEnumerable<TrainingPlan>> GetPlans()
        {
            return _trainingPlanRepository.GetAllAsync();
        }

        public async Task<TrainingPlan> UpdateTrainingPlan(TrainingPlanDTO dto)
        {
            if (!dto.Id.HasValue || !await IsTrainingPlanValid(dto))
                throw new ArgumentException("Invalid training plan data.");

            var plan = await _trainingPlanRepository.GetByIdAsync(dto.Id.Value);
            plan.UserId = dto.UserId;
            plan.TrainingTypeId = dto.TrainingTypeId;
            plan.Name = dto.Name;
            plan.Description = dto.Description;
            plan.Date = dto.Date.ToUniversalTime();
            plan.UpdatedAt = DateTime.UtcNow;

            if (dto.Workout != null)
            {
                var workout = plan.Workouts.FirstOrDefault(w => w.Id == dto.Workout.Id)
                    ?? plan.Workouts.FirstOrDefault();
                if (workout != null)
                {
                    workout.UserId = dto.Workout.UserId;
                    workout.Name = dto.Workout.Name;
                    workout.Description = dto.Workout.Description;
                    workout.DurationMinutes = dto.Workout.DurationMinutes;
                    workout.DistanceMeters = dto.Workout.DistanceMeters;
                    workout.Pace = dto.Workout.Pace;
                    workout.PaceUnit = dto.Workout.PaceUnit;
                    workout.Notes = dto.Workout.Notes;
                    workout.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await _trainingPlanRepository.UpdateAsync(plan);
        }

        private static Workout MapWorkout(WorkoutDTO dto, TrainingPlan plan)
        {
            var workout = new Workout
            {
                Id = dto.Id ?? 0,
                UserId = dto.UserId,
                TrainingPlan = plan,
                Name = dto.Name,
                Description = dto.Description,
                DurationMinutes = dto.DurationMinutes,
                DistanceMeters = dto.DistanceMeters,
                Pace = dto.Pace,
                PaceUnit = dto.PaceUnit,
                Notes = dto.Notes,
                CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };

            foreach (var segmentDto in dto.Segments)
            {
                var segment = new Segment
                {
                    Id = segmentDto.Id ?? 0,
                    Workout = workout,
                    Order = segmentDto.Order,
                    RepeatCount = segmentDto.RepeatCount,
                    Notes = segmentDto.Notes
                };
                foreach (var intervalDto in segmentDto.Intervals)
                {
                    segment.Intervals.Add(new Interval
                    {
                        Id = intervalDto.Id ?? 0,
                        Segment = segment,
                        Order = intervalDto.Order,
                        DistanceMeters = intervalDto.DistanceMeters,
                        DurationSeconds = intervalDto.DurationSeconds,
                        TargetPaceSecondsPerKm = intervalDto.TargetPaceSecondsPerKm,
                        TargetPaceSecondsPerKmUpperBound = intervalDto.TargetPaceSecondsPerKmUpperBound,
                        TargetPaceSecondsPerKmLowerBound = intervalDto.TargetPaceSecondsPerKmLowerBound,
                        Notes = intervalDto.Notes
                    });
                }
                workout.Segments.Add(segment);
            }

            return workout;
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
