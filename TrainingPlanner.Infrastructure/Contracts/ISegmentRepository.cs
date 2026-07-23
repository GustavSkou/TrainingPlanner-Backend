using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Infrastructure.Contracts
{
    public interface ISegmentRepository
    {
        Task<Segment> AddAsync(Segment segment);
        Task<Segment> GetByIdAsync(int id);
        Task<IEnumerable<Segment>> GetByWorkoutIdAsync(int workoutId);
    }
}