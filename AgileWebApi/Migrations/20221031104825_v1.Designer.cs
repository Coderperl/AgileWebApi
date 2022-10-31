﻿// <auto-generated />
using System;
using AgileWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgileWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221031104825_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AgileWebApi.Data.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ElevatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TechnicianId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ElevatorId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("AgileWebApi.Data.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("Issue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("AgileWebApi.Data.Elevator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Door")
                        .HasColumnType("bit");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastInspection")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaximumWeight")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NextInspection")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Reboot")
                        .HasColumnType("bit");

                    b.Property<bool>("ShutDown")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Elevators");
                });

            modelBuilder.Entity("AgileWebApi.Data.Technician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Technicians");
                });

            modelBuilder.Entity("AgileWebApi.Data.Case", b =>
                {
                    b.HasOne("AgileWebApi.Data.Elevator", "Elevator")
                        .WithMany()
                        .HasForeignKey("ElevatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgileWebApi.Data.Technician", "Technician")
                        .WithMany()
                        .HasForeignKey("TechnicianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Elevator");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("AgileWebApi.Data.Comment", b =>
                {
                    b.HasOne("AgileWebApi.Data.Case", null)
                        .WithMany("Comments")
                        .HasForeignKey("CaseId");
                });

            modelBuilder.Entity("AgileWebApi.Data.Case", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
