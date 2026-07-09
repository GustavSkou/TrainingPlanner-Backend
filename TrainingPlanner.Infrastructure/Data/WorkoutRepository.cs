using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Infrastructure.Contracts;

namespace TrainingPlanner.Infrastructure.Data
{
    internal class WorkoutRepository : IWorkoutRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Workout> AddAsync(Workout workout)
        {
            await _context.Workouts.AddAsync(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<Workout> GetByIdAsync(int id)
        {
            return await _context.Workouts.FirstAsync(w => w.Id == id);
        }

        public async Task<IEnumerable<Workout>> GetByUserIdAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Workout>> GetByTrainingPlanIdAsync(int trainingPlanId)
        {
            return await _context.Workouts
                .Where(w => w.TrainingPlanId == trainingPlanId)
                .ToListAsync();
        }
    }
}
