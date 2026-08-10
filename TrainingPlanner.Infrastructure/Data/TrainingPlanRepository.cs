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
                } : null
            }).FirstAsync();
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
                } : null
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
