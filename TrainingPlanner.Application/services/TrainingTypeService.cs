using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Infrastructure.Contracts;

namespace TrainingPlanner.Application.Services
{
    public class TrainingTypeService : ITrainingTypeService
    {
        private readonly ITrainingTypeRepository _trainingTypeRepository;

        public TrainingTypeService(ITrainingTypeRepository trainingTypeRepository)
        {
            _trainingTypeRepository = trainingTypeRepository;
        }

        public Task<TrainingType> GetTypeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TrainingType>> GetTypes()
        {
            throw new NotImplementedException();
        }
    }
}
