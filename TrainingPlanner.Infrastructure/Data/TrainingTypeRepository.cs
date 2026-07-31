using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Domain.Contracts;

namespace TrainingPlanner.Infrastructure.Data
{
    internal class TrainingTypeRepository : ITrainingTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public TrainingTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TrainingType> AddAsync(TrainingType trainingType)
        {
            await _context.TrainingTypes.AddAsync(trainingType);
            await _context.SaveChangesAsync();
            return trainingType;
        }

        public async Task<IEnumerable<TrainingType>> GetAllAsync()
        {
            return await _context.TrainingTypes.ToListAsync();
        }

        public async Task<TrainingType> GetByIdAsync(int id)
        {
            return await _context.TrainingTypes.FirstAsync(t => t.Id == id);
        }
    }
}
