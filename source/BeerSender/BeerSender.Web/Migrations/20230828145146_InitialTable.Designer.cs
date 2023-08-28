﻿// <auto-generated />
using System;
using BeerSender.Web.Event_store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeerSender.Web.Migrations
{
    [DbContext(typeof(Event_context))]
    [Migration("20230828145146_InitialTable")]
    partial class InitialTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeerSender.Web.Event_store.Aggregate_event", b =>
                {
                    b.Property<Guid>("Aggregate_id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Event_Number")
                        .HasColumnType("int");

                    b.Property<string>("Event_Payload")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("varchar");

                    b.Property<string>("Event_Type")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar");

                    b.HasKey("Aggregate_id", "Event_Number");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
