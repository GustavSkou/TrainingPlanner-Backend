using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

using TrainingPlanner.Infrastructure.Data;
using TrainingPlanner.Infrastructure.Contracts;

namespace TrainingPlanner.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine("connection string:" + connectionString);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            

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
