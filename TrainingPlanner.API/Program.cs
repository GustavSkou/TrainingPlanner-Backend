using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.Services;
using TrainingPlanner.Infrastructure.Configuration;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(p =>
                p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        //Console.WriteLine("connection string:" + connectionString);
        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseNpgsql(connectionString));
        
        builder.Services.AddControllers();
        builder.Services.AddControllers().AddJsonOptions(options => {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITrainingTypeService, TrainingTypeService>();
        builder.Services.AddScoped<ITrainingPlanService, TrainingPlanService>();

        var app = builder.Build();
        app.Urls.Add("http://localhost:5001");
        app.UseCors();
        app.MapControllers();

        app.Run();
    }
}
