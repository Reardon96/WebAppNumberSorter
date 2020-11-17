﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppNumberSorter.Models;
using WebAppNumberSorter.Repositories;

namespace WebAppNumberSorter.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201115152041_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("WebAppNumberSorter.Models.SortDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<Guid>("SortId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("SortTime")
                        .HasColumnType("bigint");

                    b.Property<string>("SortType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SortedNumbers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnsortedNumbers")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SortDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
