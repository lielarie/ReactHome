using Microsoft.EntityFrameworkCore;
using ReactHome.Server.Models;

namespace ReactHome.Server.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    public DbSet<ToDoTask> Tasks => Set<ToDoTask>();

    public DbSet<Tag> Tags => Set<Tag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(u =>
        {
            u.HasKey(x => x.Id);

            u.Property(x => x.FullName)
                .HasMaxLength(50)
                .IsRequired();

            u.Property(x => x.Phone)
                .HasMaxLength(20)
                .IsRequired();

            u.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            u.HasIndex(x => x.Email)
                .IsUnique();
        });

        modelBuilder.Entity<ToDoTask>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ToDoTask>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Tasks)
            .UsingEntity(j => j.ToTable("TaskTags"));

        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();
    }
}
