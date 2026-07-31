using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Infrastructure.Contracts;

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
                } : null,

                /*Workouts = tp.Workouts.Select(w => new Workout {
                    Id = w.Id,
                    UserId = w.UserId,
                    TrainingPlanId = w.TrainingPlanId,
                    Name = w.Name,
                    Description = w.Description,
                    DurationMinutes = w.DurationMinutes,
                    DistanceMeters = w.DistanceMeters,
                    Notes = w.Notes,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                }).ToList()*/
            })
            .ToListAsync();
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
    }
}
