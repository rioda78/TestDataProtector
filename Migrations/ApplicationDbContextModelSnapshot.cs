﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestDataProtector.Data;

namespace TestDataProtector.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("TestDataProtector.Models.Provinsi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nama")
                        .HasColumnType("TEXT");

                    b.Property<int>("No")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Provinsi");
                });
#pragma warning restore 612, 618
        }
    }
}