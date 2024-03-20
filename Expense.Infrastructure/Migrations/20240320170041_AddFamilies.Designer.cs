﻿// <auto-generated />
using Expense.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Expense.Infrastructure.Migrations
{
    [DbContext(typeof(ExpenseDbContext))]
    [Migration("20240320170041_AddFamilies")]
    partial class AddFamilies
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Expense.Domain.Model.Attendee", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("character varying(36)");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("character varying(36)");

                    b.Property<string>("FamilyOwnerId")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("FamilyOwnerId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("Expense.Domain.Model.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Expense.Domain.Model.Expense", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("character varying(36)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("AttendeeId")
                        .IsRequired()
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("AttendeeId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Expense.Domain.Model.Attendee", b =>
                {
                    b.HasOne("Expense.Domain.Model.Event", "Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Expense.Domain.Model.Attendee", "FamilyOwner")
                        .WithMany("FamilyDependents")
                        .HasForeignKey("FamilyOwnerId");

                    b.Navigation("Event");

                    b.Navigation("FamilyOwner");
                });

            modelBuilder.Entity("Expense.Domain.Model.Expense", b =>
                {
                    b.HasOne("Expense.Domain.Model.Attendee", "Attendee")
                        .WithMany("Expenses")
                        .HasForeignKey("AttendeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendee");
                });

            modelBuilder.Entity("Expense.Domain.Model.Attendee", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("FamilyDependents");
                });

            modelBuilder.Entity("Expense.Domain.Model.Event", b =>
                {
                    b.Navigation("Attendees");
                });
#pragma warning restore 612, 618
        }
    }
}
