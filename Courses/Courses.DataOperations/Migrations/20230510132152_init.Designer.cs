﻿// <auto-generated />
using System;
using Courses.DataOperations.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Courses.DataOperations.Migrations
{
    [DbContext(typeof(CoursesCatalogDbContext))]
    [Migration("20230510132152_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Courses.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Development"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Art"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Language"
                        });
                });

            modelBuilder.Entity("Courses.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CourseImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TotalHours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CourseImage = "https://loremflickr.com/320/240",
                            Description = "Learn with fun",
                            Name = "C# Basics",
                            TotalHours = 50
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            CourseImage = "https://loremflickr.com/320/240",
                            Description = "Learn Advanced C#",
                            Name = "C# Advanced",
                            TotalHours = 50
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            CourseImage = "https://loremflickr.com/320/240",
                            Description = "Like Bob Ross :)",
                            Name = "Painting Course",
                            TotalHours = 50
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 3,
                            CourseImage = "https://loremflickr.com/320/240",
                            Description = "Learn Spanish",
                            Name = "Spanish",
                            TotalHours = 50
                        });
                });

            modelBuilder.Entity("Courses.Entities.Course", b =>
                {
                    b.HasOne("Courses.Entities.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Courses.Entities.Category", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
