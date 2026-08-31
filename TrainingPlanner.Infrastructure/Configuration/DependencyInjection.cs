using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TrainingPlanner.Infrastructure.Data;
using TrainingPlanner.Domain.Contracts;

namespace TrainingPlanner.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITrainingPlanRepository, TrainingPlanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITrainingTypeRepository, TrainingTypeRepository>();
            services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            services.AddScoped<ISegmentRepository, SegmentRepository>();
            services.AddScoped<IIntervalRepository, IntervalRepository>();

            return services;
        }
    }
}
