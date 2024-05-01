﻿// <auto-generated />
using System;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    [DbContext(typeof(DockerTestContext))]
    partial class DockerTestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.DistributionConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Channel")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TriggerType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DistributionConfigs");
                });

            modelBuilder.Entity("Domain.Entities.DistributionConfig", b =>
                {
                    b.OwnsOne("object", "AssetSelectorPattern", b1 =>
                        {
                            b1.Property<long>("DistributionConfigId")
                                .HasColumnType("bigint");

                            b1.HasKey("DistributionConfigId");

                            b1.ToTable("DistributionConfigs");

                            b1.ToJson("AssetSelectorPattern");

                            b1.WithOwner()
                                .HasForeignKey("DistributionConfigId");
                        });

                    b.OwnsOne("object", "TriggerConfig", b1 =>
                        {
                            b1.Property<long>("DistributionConfigId")
                                .HasColumnType("bigint");

                            b1.HasKey("DistributionConfigId");

                            b1.ToTable("DistributionConfigs");

                            b1.ToJson("TriggerConfig");

                            b1.WithOwner()
                                .HasForeignKey("DistributionConfigId");
                        });

                    b.Navigation("AssetSelectorPattern");

                    b.Navigation("TriggerConfig");
                });
#pragma warning restore 612, 618
        }
    }
}
