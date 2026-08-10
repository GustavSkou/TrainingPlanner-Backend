using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        string issuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT issuer is not configured.");
        string audience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT audience is not configured.");
        string key = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITrainingTypeService, TrainingTypeService>();
        builder.Services.AddScoped<ITrainingPlanService, TrainingPlanService>();

        var app = builder.Build();
        app.Urls.Add("http://localhost:5001");
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers().RequireAuthorization();

        app.Run();
    }
}
