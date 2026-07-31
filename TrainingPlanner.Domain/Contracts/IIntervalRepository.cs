using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Domain.Contracts
{
    public interface IIntervalRepository
    {
        Task<Interval> AddAsync(Interval interval);
        Task<Interval> GetByIdAsync(int id);
        Task<IEnumerable<Interval>> GetBySegmentIdAsync(int segmentId);
    }
}