using Microsoft.EntityFrameworkCore;
using skills_test.Domain.Models;

namespace skills_test.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Person> Persons => Set<Person>();
}