using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Domain.Contracts;

namespace TrainingPlanner.Infrastructure.Data
{
    internal class TrainingPlanRepository : ITrainingPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingPlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingPlan> AddAsync(TrainingPlan plan)
        {
            await _context.TrainingPlans.AddAsync(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<IEnumerable<TrainingPlan>> GetAllAsync()
        {
            return await _context.TrainingPlans
                .Include(tp => tp.TrainingType)
                .Include(tp => tp.Workout)
                .ToListAsync();
        }

        public async Task<TrainingPlan?> GetByIdAsync(int id)
        {
            return await _context.TrainingPlans
            .Where(tp => tp.Id == id)
            .Select(tp => new TrainingPlan {
                Id = tp.Id,
                UserId = tp.UserId,
                TrainingTypeId = tp.TrainingTypeId,
                Name = tp.Name,
                Description = tp.Description,
                Date = tp.Date,
                CreatedAt = tp.CreatedAt,
                UpdatedAt = tp.UpdatedAt,

                TrainingType = tp.TrainingType != null ? new TrainingType {
                    Id = tp.TrainingType.Id,
                    Name = tp.TrainingType.Name,
                    Description = tp.TrainingType.Description
                } : null,
                Workout = tp.Workout == null ? null : new Workout
                {
                    Id = tp.Workout.Id,
                    UserId = tp.Workout.UserId,
                    TrainingPlanId = tp.Workout.TrainingPlanId,
                    Name = tp.Workout.Name,
                    Description = tp.Workout.Description,
                    DurationMinutes = tp.Workout.DurationMinutes,
                    DistanceMeters = tp.Workout.DistanceMeters,
                    Pace = tp.Workout.Pace,
                    PaceUnit = tp.Workout.PaceUnit,
                    Notes = tp.Workout.Notes,
                    CreatedAt = tp.Workout.CreatedAt,
                    UpdatedAt = tp.Workout.UpdatedAt
                }
            }).FirstOrDefaultAsync();
        }

        public async Task<TrainingPlan> UpdateAsync(TrainingPlan plan)
        {
            _context.TrainingPlans.Update(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<IEnumerable<TrainingPlan>> GetByUserIdAsync(int userId)
        {
            return await _context.TrainingPlans
            .Where(tp => tp.UserId == userId)
            .Select(tp => new TrainingPlan {
                Id = tp.Id,
                UserId = tp.UserId,
                TrainingTypeId = tp.TrainingTypeId,
                Name = tp.Name,
                Description = tp.Description,
                Date = tp.Date,
                CreatedAt = tp.CreatedAt,
                UpdatedAt = tp.UpdatedAt,
                
                TrainingType = tp.TrainingType != null ? new TrainingType {
                    Id = tp.TrainingType.Id,
                    Name = tp.TrainingType.Name,
                    Description = tp.TrainingType.Description
                } : null,
                Workout = tp.Workout == null ? null : new Workout
                {
                    Id = tp.Workout.Id,
                    UserId = tp.Workout.UserId,
                    TrainingPlanId = tp.Workout.TrainingPlanId,
                    Name = tp.Workout.Name,
                    Description = tp.Workout.Description,
                    DurationMinutes = tp.Workout.DurationMinutes,
                    DistanceMeters = tp.Workout.DistanceMeters,
                    Pace = tp.Workout.Pace,
                    PaceUnit = tp.Workout.PaceUnit,
                    Notes = tp.Workout.Notes,
                    CreatedAt = tp.Workout.CreatedAt,
                    UpdatedAt = tp.Workout.UpdatedAt
                }
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<TrainingPlan>> GetByUserIdsAsync(IEnumerable<int> userIds)
        {
            return await _context.TrainingPlans
            .Where(tp => userIds.Contains(tp.Id))
            .Select(tp => new TrainingPlan {
                Id = tp.Id,
                UserId = tp.UserId,
                TrainingTypeId = tp.TrainingTypeId,
                Name = tp.Name,
                Description = tp.Description,
                Date = tp.Date,
                CreatedAt = tp.CreatedAt,
                UpdatedAt = tp.UpdatedAt,
                
                TrainingType = tp.TrainingType != null ? new TrainingType {
                    Id = tp.TrainingType.Id,
                    Name = tp.TrainingType.Name,
                    Description = tp.TrainingType.Description
                } : null,
                Workout = tp.Workout == null ? null : new Workout
                {
                    Id = tp.Workout.Id,
                    UserId = tp.Workout.UserId,
                    TrainingPlanId = tp.Workout.TrainingPlanId,
                    Name = tp.Workout.Name,
                    Description = tp.Workout.Description,
                    DurationMinutes = tp.Workout.DurationMinutes,
                    DistanceMeters = tp.Workout.DistanceMeters,
                    Pace = tp.Workout.Pace,
                    PaceUnit = tp.Workout.PaceUnit,
                    Notes = tp.Workout.Notes,
                    CreatedAt = tp.Workout.CreatedAt,
                    UpdatedAt = tp.Workout.UpdatedAt
                }
            })
            .ToListAsync();
        }
    }
}
