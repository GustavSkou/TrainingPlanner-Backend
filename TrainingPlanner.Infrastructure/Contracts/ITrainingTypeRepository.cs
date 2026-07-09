using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Infrastructure.Contracts
{
    public interface ITrainingTypeRepository
    {
        Task<IEnumerable<TrainingType>> GetAllAsync();
        Task<TrainingType> GetByIdAsync(int id);
        Task<TrainingType> AddAsync(TrainingType trainingType);
    }
}
