﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.DataBaseContext;

#nullable disable

namespace Inventario.Migrations
{
    [DbContext(typeof(WareHouseDataContext))]
    partial class WareHouseDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Entry", b =>
                {
                    b.Property<int>("EntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EntryId"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<string>("LoteCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("EntryId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Models.Exit", b =>
                {
                    b.Property<int>("ExistsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExistsId"));

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ExistsId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Exits");
                });

            modelBuilder.Entity("Models.Group", b =>
                {
                    b.Property<int>("GroupsUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupsUserId"));

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("GroupsUserId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            GroupsUserId = 1,
                            Level = 1,
                            Name = "Administradores",
                            Status = true
                        },
                        new
                        {
                            GroupsUserId = 2,
                            Level = 2,
                            Name = "Vendedores",
                            Status = true
                        },
                        new
                        {
                            GroupsUserId = 3,
                            Level = 2,
                            Name = "Administrador Inventarios",
                            Status = true
                        });
                });

            modelBuilder.Entity("Models.Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityOfExistances")
                        .HasColumnType("int");

                    b.Property<int>("WareHouseId")
                        .HasColumnType("int");

                    b.HasKey("InventoryId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WareHouseId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("Models.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoId"));

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("PhotoId");

                    b.HasIndex("ProductId");

                    b.ToTable("Photos");

                    b.HasData(
                        new
                        {
                            PhotoId = 1,
                            FilePath = "./photos/default.png",
                            ProductId = 1
                        });
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<double>("Buy_Price")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Sell_Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Buy_Price = 1.0,
                            Description = "Sample",
                            Name = "Sample",
                            Sell_Price = 2.0
                        });
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("Enable")
                        .HasColumnType("bit");

                    b.Property<int>("GroupsUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("GroupsUserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Enable = true,
                            GroupsUserId = 1,
                            LastLogin = new DateTime(2023, 6, 9, 19, 14, 2, 553, DateTimeKind.Local).AddTicks(2211),
                            LastName = "",
                            Name = "",
                            Password = "admin",
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("Models.WareHouse", b =>
                {
                    b.Property<int>("WareHouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WareHouseId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("WareHouseId");

                    b.ToTable("WareHouses");
                });

            modelBuilder.Entity("Models.Entry", b =>
                {
                    b.HasOne("Models.Inventory", "Inventory")
                        .WithMany("Entries")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("Models.Exit", b =>
                {
                    b.HasOne("Models.Inventory", "Inventory")
                        .WithMany("Exits")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("Models.Inventory", b =>
                {
                    b.HasOne("Models.Product", "Product")
                        .WithMany("Inventories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.WareHouse", "WareHouse")
                        .WithMany("Inventories")
                        .HasForeignKey("WareHouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("WareHouse");
                });

            modelBuilder.Entity("Models.Photo", b =>
                {
                    b.HasOne("Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.HasOne("Models.Group", "GroupsUser")
                        .WithMany("Users")
                        .HasForeignKey("GroupsUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupsUser");
                });

            modelBuilder.Entity("Models.Group", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Models.Inventory", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("Exits");
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("Models.WareHouse", b =>
                {
                    b.Navigation("Inventories");
                });
#pragma warning restore 612, 618
        }
    }
}
