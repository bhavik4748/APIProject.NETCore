﻿// <auto-generated />
using System;
using APIProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIProject.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240508065032_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIProject.Entities.Audit", b =>
                {
                    b.Property<int>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuditId"));

                    b.Property<int>("DataTableId")
                        .HasColumnType("int");

                    b.Property<string>("DataTableName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<int>("WorkFlowId")
                        .HasColumnType("int");

                    b.HasKey("AuditId");

                    b.HasIndex("WorkFlowId");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("APIProject.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("APIProject.Entities.EmployeeWorkflowAction", b =>
                {
                    b.Property<int>("EmployeeWorkflowActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeWorkflowActionId"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkflowActionId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeWorkflowActionId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("WorkflowActionId");

                    b.ToTable("EmployeeWorkflowActions");
                });

            modelBuilder.Entity("APIProject.Entities.EmployeeWorkflowState", b =>
                {
                    b.Property<int>("EmployeeWorkflowStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeWorkflowStateId"));

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkflowStateId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeWorkflowStateId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("WorkflowStateId");

                    b.ToTable("EmployeeWorkflowStates");
                });

            modelBuilder.Entity("APIProject.Entities.Workflow", b =>
                {
                    b.Property<int>("WorkflowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkflowId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WorkflowDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkflowName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkflowId");

                    b.ToTable("Workflows");
                });

            modelBuilder.Entity("APIProject.Entities.WorkflowAction", b =>
                {
                    b.Property<int>("WorkflowActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkflowActionId"));

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StateFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StateTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("int");

                    b.HasKey("WorkflowActionId");

                    b.HasIndex("WorkflowId");

                    b.ToTable("WorkflowActions");
                });

            modelBuilder.Entity("APIProject.Entities.WorkflowState", b =>
                {
                    b.Property<int>("WorkflowStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkflowStateId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkflowId")
                        .HasColumnType("int");

                    b.HasKey("WorkflowStateId");

                    b.HasIndex("WorkflowId");

                    b.ToTable("WorkflowStates");
                });

            modelBuilder.Entity("APIProject.Entities.Audit", b =>
                {
                    b.HasOne("APIProject.Entities.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkFlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("APIProject.Entities.EmployeeWorkflowAction", b =>
                {
                    b.HasOne("APIProject.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APIProject.Entities.WorkflowAction", "WorkflowAction")
                        .WithMany()
                        .HasForeignKey("WorkflowActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("WorkflowAction");
                });

            modelBuilder.Entity("APIProject.Entities.EmployeeWorkflowState", b =>
                {
                    b.HasOne("APIProject.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APIProject.Entities.WorkflowState", "WorkflowState")
                        .WithMany()
                        .HasForeignKey("WorkflowStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("WorkflowState");
                });

            modelBuilder.Entity("APIProject.Entities.WorkflowAction", b =>
                {
                    b.HasOne("APIProject.Entities.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("APIProject.Entities.WorkflowState", b =>
                {
                    b.HasOne("APIProject.Entities.Workflow", "Workflow")
                        .WithMany()
                        .HasForeignKey("WorkflowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workflow");
                });
#pragma warning restore 612, 618
        }
    }
}
