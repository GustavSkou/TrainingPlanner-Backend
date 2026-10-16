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
                .Include(tp => tp.Workouts)
                .ToListAsync();
        }

        public async Task<TrainingPlan> GetByIdAsync(int id)
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
                Workouts = tp.Workouts.Select(w => new Workout
                {
                    Id = w.Id,
                    UserId = w.UserId,
                    TrainingPlanId = w.TrainingPlanId,
                    Name = w.Name,
                    Description = w.Description,
                    DurationMinutes = w.DurationMinutes,
                    DistanceMeters = w.DistanceMeters,
                    Pace = w.Pace,
                    PaceUnit = w.PaceUnit,
                    Notes = w.Notes,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                }).ToList()
            }).FirstAsync();
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
                Workouts = tp.Workouts.Select(w => new Workout
                {
                    Id = w.Id,
                    UserId = w.UserId,
                    TrainingPlanId = w.TrainingPlanId,
                    Name = w.Name,
                    Description = w.Description,
                    DurationMinutes = w.DurationMinutes,
                    DistanceMeters = w.DistanceMeters,
                    Pace = w.Pace,
                    PaceUnit = w.PaceUnit,
                    Notes = w.Notes,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                }).ToList()
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
                } : null
            })
            .ToListAsync();
        }
    }
}
