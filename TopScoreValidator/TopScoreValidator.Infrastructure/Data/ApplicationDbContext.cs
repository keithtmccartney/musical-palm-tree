using Microsoft.EntityFrameworkCore;
using TopScoreValidator.Domain.Entities;

namespace TopScoreValidator.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ValidWord> ValidWords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ValidWord>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Value).IsRequired().HasMaxLength(500);

            entity.HasIndex(e => e.Value).IsUnique();
        });
    }
}
