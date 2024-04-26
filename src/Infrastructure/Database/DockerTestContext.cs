﻿using Domain.Entities;
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

        modelBuilder.Entity<User>().ToTable("users").HasKey(x => x.Id);
        modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<User>().Property(x => x.Firstname).HasColumnName("firstname");
        modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnName("lastname");
    }
}
