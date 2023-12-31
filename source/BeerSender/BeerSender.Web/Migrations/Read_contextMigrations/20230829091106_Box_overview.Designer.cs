﻿// <auto-generated />
using System;
using BeerSender.Web.Read_store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BeerSender.Web.Migrations.Read_contextMigrations
{
    [DbContext(typeof(Read_context))]
    [Migration("20230829091106_Box_overview")]
    partial class Box_overview
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeerSender.Web.Read_store.Box_overview", b =>
                {
                    b.Property<Guid>("Box_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Open_spaces")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Box_id");

                    b.ToTable("Box_overviews");
                });
#pragma warning restore 612, 618
        }
    }
}
