using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TrainingPlanner.Infrastructure.Data;

public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var connectionString = BuildConfiguration(args)["AZURE_SQL_CONNECTIONSTRING"];


        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private static IConfigurationRoot BuildConfiguration(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        var apiAssembly = TryLoadAssembly("TrainingPlanner.API");
        var apiProjectPath = FindApiProjectPath(basePath);

        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddEnvironmentVariables()
            .AddCommandLine(args);

        if (!string.IsNullOrWhiteSpace(apiProjectPath))
        {
            configurationBuilder
                .AddJsonFile(Path.Combine(apiProjectPath, "appsettings.json"), optional: true)
                .AddJsonFile(Path.Combine(apiProjectPath, "appsettings.Development.json"), optional: true);
        }

        if (apiAssembly is not null)
        {
            configurationBuilder.AddUserSecrets(apiAssembly, optional: true);
        }

        return configurationBuilder.Build();
    }

    private static Assembly? TryLoadAssembly(string assemblyName)
    {
        try
        {
            return Assembly.Load(assemblyName);
        }
        catch
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly => string.Equals(assembly.GetName().Name, assemblyName, StringComparison.Ordinal));
        }
    }

    private static string? FindApiProjectPath(string basePath)
    {
        var candidates = new[]
        {
            Path.GetFullPath(Path.Combine(basePath, "TrainingPlanner.API")),
            Path.GetFullPath(Path.Combine(basePath, "..", "TrainingPlanner.API")),
            Path.GetFullPath(Path.Combine(basePath, "..", "..", "TrainingPlanner.API"))
        };

        return candidates.FirstOrDefault(candidate => File.Exists(Path.Combine(candidate, "appsettings.json")));
    }
}
