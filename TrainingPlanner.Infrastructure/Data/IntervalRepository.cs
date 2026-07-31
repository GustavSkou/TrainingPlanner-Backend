using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Domain.Contracts;

namespace TrainingPlanner.Infrastructure.Data
{
    public class IntervalRepository : IIntervalRepository
    {
        public Task<Interval> AddAsync(Interval interval) => throw new NotImplementedException();

        public Task<Interval> GetByIdAsync(int id) => throw new NotImplementedException();

        public Task<IEnumerable<Interval>> GetBySegmentIdAsync(int segmentId) => throw new NotImplementedException();
    }
}