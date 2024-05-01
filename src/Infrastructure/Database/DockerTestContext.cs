using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Infrastructure.Database;
public class DockerTestContext : DbContext
{
    //public DbSet<Distribution> Distributions { get; set; }
    public DbSet<DistributionConfig> DistributionConfigs { get; set; }

    public DockerTestContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DistributionConfig>().HasKey(x => x.Id);
        modelBuilder.Entity<DistributionConfig>().Property(x => x.TriggerConfigJson).HasColumnName("TriggerConfig").HasColumnType("jsonb");
        modelBuilder.Entity<DistributionConfig>().Property(x => x.AssetSelectorPatternJson).HasColumnName("AssetSelectorPattern").HasColumnType("jsonb");
    }
}
