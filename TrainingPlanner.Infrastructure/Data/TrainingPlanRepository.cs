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
                .Include(tp => tp.Workouts)
                .Include(tp => tp.TrainingType)
                .FirstAsync(tp => tp.Id == id);
        }

        public async Task<IEnumerable<TrainingPlan>> GetByUserIdAsync(int userId)
        {
            return await _context.TrainingPlans
                .Where(tp => tp.UserId == userId)
                .Include(tp => tp.Workouts)
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingPlan>> GetByUserIdsAsync(IEnumerable<int> userIds)
        {
            if (userIds == null || !userIds.Any())
                return new List<TrainingPlan>();

            return await _context.TrainingPlans
                .Where(tp => userIds.Contains(tp.UserId))
                .Include(tp => tp.Workouts)
                .ToListAsync();
        }
    }
}
