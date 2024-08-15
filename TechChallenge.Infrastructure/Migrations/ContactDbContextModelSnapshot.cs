﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechChallenge.Infrastructure.Context;

#nullable disable

namespace TechChallenge.Infrastructure.Migrations
{
    [DbContext(typeof(ContactDbContext))]
    partial class ContactDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TechChallenge.Infrastructure.Entities.Contact", b =>
                {
                    b.Property<Guid>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DddId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.HasKey("ContactId");

                    b.HasIndex("DddId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TechChallenge.Infrastructure.Entities.DDD", b =>
                {
                    b.Property<Guid>("DDDId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DDDCode")
                        .HasColumnType("int");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DDDId");

                    b.HasIndex("RegionId");

                    b.ToTable("DDDs");
                });

            modelBuilder.Entity("TechChallenge.Infrastructure.Entities.Region", b =>
                {
                    b.Property<Guid>("RegionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RegionId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("TechChallenge.Infrastructure.Entities.Contact", b =>
                {
                    b.HasOne("TechChallenge.Infrastructure.Entities.DDD", "Ddd")
                        .WithMany("Contacts")
                        .HasForeignKey("DddId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ddd");
                });

            modelBuilder.Entity("TechChallenge.Infrastructure.Entities.DDD", b =>
                {
                    b.HasOne("TechChallenge.Infrastructure.Entities.Region", "Region")
                        .WithMany("DDDs")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("TechChallenge.Infrastructure.Entities.DDD", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("TechChallenge.Infrastructure.Entities.Region", b =>
                {
                    b.Navigation("DDDs");
                });
#pragma warning restore 612, 618
        }
    }
}
