using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrainingPlanner.Application.Contracts;
using TrainingPlanner.Application.Services;
using TrainingPlanner.Infrastructure.Configuration;
using TrainingPlanner.Infrastructure.Data;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(p =>
                p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

        Console.WriteLine("connection string:" + connectionString);
        Console.WriteLine("API-KEY:" + builder.Configuration["API-KEY"]);
        
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

        app.Use(async (httpContext, next) =>
        {
            if (!httpContext.Request.Headers.TryGetValue("API-KEY", out var apiKey)) {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Missing API-KEY");
                return; 
            }

            if (!app.Configuration["API-KEY"].Equals(apiKey)) {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("invalid API-KEY");
                return;
            }

            await next(httpContext);   
        });
        app.Run();
    }
}
