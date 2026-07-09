using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Application.DTOs;

namespace TrainingPlanner.Application.Contracts
{
    public interface ITrainingTypeService
    {
        public Task<TrainingType> GetTypeById(int id);
        public Task<IEnumerable<TrainingType>> GetTypes();
    }
}