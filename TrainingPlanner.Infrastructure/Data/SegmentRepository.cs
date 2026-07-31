using TrainingPlanner.Domain.Entities;
using TrainingPlanner.Domain.Contracts;

namespace TrainingPlanner.Infrastructure.Data
{
    public class SegmentRepository : ISegmentRepository
    {
        public Task<Segment> AddAsync(Segment segment) => throw new NotImplementedException();

        public Task<Segment> GetByIdAsync(int id) => throw new NotImplementedException();

        public Task<IEnumerable<Segment>> GetByWorkoutIdAsync(int workoutId) => throw new NotImplementedException();
    }
}

