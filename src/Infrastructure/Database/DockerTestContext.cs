using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Infrastructure.Database;
public class DockerTestContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DockerTestContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<User>().HasKey(x => x.Id);
    }
}
