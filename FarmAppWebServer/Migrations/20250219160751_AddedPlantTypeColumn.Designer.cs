﻿// <auto-generated />
using System;
using FarmAppWebServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FarmAppWebServer.Migrations
{
    [DbContext(typeof(FarmAppDbContext))]
    [Migration("20250219160751_AddedPlantTypeColumn")]
    partial class AddedPlantTypeColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("FarmAppWebServer.Models.PlantTypesData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("HarvestedPrice")
                        .HasColumnType("int");

                    b.Property<int>("MaxGrowState")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SecondsToGrowOneState")
                        .HasColumnType("int");

                    b.Property<int>("StorePrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PlantTypesDataValues");
                });

            modelBuilder.Entity("FarmAppWebServer.Models.PlayerData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentPlayerLevel")
                        .HasColumnType("int");

                    b.Property<long>("CurrentPlayerXP")
                        .HasColumnType("bigint");

                    b.Property<int>("FarmLevel")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastLoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastToken")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PlayerDataValues");
                });

            modelBuilder.Entity("FarmAppWebServer.Models.PlayersPlantsInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentGrowState")
                        .HasColumnType("int");

                    b.Property<int>("GrowStateInSeconds")
                        .HasColumnType("int");

                    b.Property<DateTime>("InitialPlantTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("PlantType")
                        .HasColumnType("int");

                    b.Property<int>("PotIndex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PlayersPlantsInstanceValues");
                });

            modelBuilder.Entity("FarmAppWebServer.Models.UserRegistrationInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordEncrypted")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UserRegistrationInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
