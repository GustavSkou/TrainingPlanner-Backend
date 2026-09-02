using Microsoft.EntityFrameworkCore;
using TrainingPlanner.Domain.Entities;

namespace TrainingPlanner.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TrainingType> TrainingTypes { get; set; }
    public DbSet<TrainingPlan> TrainingPlans { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Segment> WorkoutSegments { get; set; }
    public DbSet<Interval> WorkoutIntervals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.LoginProvider).HasMaxLength(100);
            entity.Property(e => e.NameIdentifier).HasMaxLength(256);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // TrainingType configuration
        modelBuilder.Entity<TrainingType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).IsRequired();

            entity.HasData(
                new TrainingType
                {
                    Id = 1,
                    Name = "Running",
                    CreatedAt = DateTime.Today.ToUniversalTime()
                },
                new TrainingType
                {
                    Id = 2,
                    Name = "Cycling",
                    CreatedAt = DateTime.Today.ToUniversalTime()
                },
                new TrainingType
                {
                    Id = 3,
                    Name = "Swimming",
                    CreatedAt = DateTime.Today.ToUniversalTime()
                },
                new TrainingType
                {
                    Id = 4,
                    Name = "Workout",
                    CreatedAt = DateTime.Today.ToUniversalTime()
                });
        });

        // TrainingPlan configuration
        modelBuilder.Entity<TrainingPlan>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);

            entity.HasOne(e => e.User)
                .WithMany(u => u.TrainingPlans)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.TrainingType)
                .WithMany(t => t.TrainingPlans)
                .HasForeignKey(e => e.TrainingTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Workout configuration
        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DurationMinutes).IsRequired();
            entity.Property(e => e.DistanceMeters).IsRequired();
            entity.Property(e => e.Notes).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Workouts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.TrainingPlan)
                .WithMany(tp => tp.Workouts)
                .HasForeignKey(e => e.TrainingPlanId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Segment configuration
        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Order).IsRequired();
            entity.Property(e => e.RepeatCount).IsRequired();
            entity.Property(e => e.Notes).HasMaxLength(1000);

            entity.HasOne(e => e.Workout)
                .WithMany(w => w.Segments)
                .HasForeignKey(e => e.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Interval configuration
        modelBuilder.Entity<Interval>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Order).IsRequired();
            entity.Property(e => e.DistanceMeters);
            entity.Property(e => e.DurationSeconds);
            entity.Property(e => e.TargetPaceSecondsPerKm);
            entity.Property(e => e.TargetPaceSecondsPerKmUpperBound);
            entity.Property(e => e.TargetPaceSecondsPerKmLowerBound);
            entity.Property(e => e.Notes).HasMaxLength(1000);

            entity.HasOne(e => e.Segment)
                .WithMany(s => s.Intervals)
                .HasForeignKey(e => e.SegmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

