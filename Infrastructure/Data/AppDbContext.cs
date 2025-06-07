using Microsoft.EntityFrameworkCore;
using skills_test.Domain.Models;

namespace skills_test.Infrastructure.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Person> Persons => Set<Person>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Skill)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}