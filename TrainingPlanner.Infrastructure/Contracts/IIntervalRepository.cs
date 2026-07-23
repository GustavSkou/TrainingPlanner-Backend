using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Infrastructure.Contracts
{
    public interface IIntervalRepository
    {
        Task<Interval> AddAsync(Interval interval);
        Task<Interval> GetByIdAsync(int id);
        Task<IEnumerable<Interval>> GetBySegmentIdAsync(int segmentId);
    }
}