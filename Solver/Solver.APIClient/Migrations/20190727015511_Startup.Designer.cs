﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solver.DataAccessLayer;

namespace Solver.APIClient.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20190727015511_Startup")]
    partial class Startup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Solver.Entities.Models.Elements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("Quantity");

                    b.Property<int?>("WorkingDaysId");

                    b.HasKey("Id");

                    b.HasIndex("WorkingDaysId");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("Solver.Entities.Models.WorkingDays", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<int>("WorkDays");

                    b.HasKey("Id");

                    b.ToTable("WorkingDays");
                });

            modelBuilder.Entity("Solver.Entities.Models.Elements", b =>
                {
                    b.HasOne("Solver.Entities.Models.WorkingDays", "WorkingDays")
                        .WithMany("Elements")
                        .HasForeignKey("WorkingDaysId");
                });
#pragma warning restore 612, 618
        }
    }
}
