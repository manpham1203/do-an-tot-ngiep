﻿// <auto-generated />
using System;
using BO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BO.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BO.Entities.Banner", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("char(6)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("LinkTo")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("SubContent")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.HasKey("Id");

                    b.ToTable("Banner");
                });

            modelBuilder.Entity("BO.Entities.Brand", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("char(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("BO.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("char(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("BO.Entities.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<string>("Content")
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("ObjectId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("OrderDetailId")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ParentId")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<int?>("Star")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BO.Entities.Contact", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("BO.Entities.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<decimal?>("Amount")
                        .IsRequired()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("DeliveryEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DeliveryPhone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<double?>("Discount")
                        .HasColumnType("float");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(true)
                        .HasColumnType("char(12)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("BO.Entities.OrderDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("BO.Entities.Picture", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(16)
                        .IsUnicode(false)
                        .HasColumnType("char(16)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("ObjectId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("BO.Entities.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("ntext");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<int>("View")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BO.Entities.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<string>("BrandId")
                        .IsRequired()
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("char(6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PriceDiscount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<int>("View")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Product");
                });

            modelBuilder.Entity("BO.Entities.ProductCategory", b =>
                {
                    b.Property<string>("ProductId")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<string>("CategoryId")
                        .HasMaxLength(6)
                        .IsUnicode(false)
                        .HasColumnType("char(6)");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("BO.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(false)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("BO.Entities.Wishlist", b =>
                {
                    b.Property<string>("ProductId")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.Property<string>("UserId")
                        .HasMaxLength(12)
                        .IsUnicode(false)
                        .HasColumnType("char(12)");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Wishlist");
                });

            modelBuilder.Entity("BO.Entities.Comment", b =>
                {
                    b.HasOne("BO.Entities.Comment", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("BO.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BO.Entities.Order", b =>
                {
                    b.HasOne("BO.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BO.Entities.OrderDetail", b =>
                {
                    b.HasOne("BO.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BO.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BO.Entities.Product", b =>
                {
                    b.HasOne("BO.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("BO.Entities.ProductCategory", b =>
                {
                    b.HasOne("BO.Entities.Category", "Category")
                        .WithMany("ProductCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BO.Entities.Product", "Product")
                        .WithMany("ProductCategory")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BO.Entities.Wishlist", b =>
                {
                    b.HasOne("BO.Entities.Product", "Product")
                        .WithMany("Wishlists")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BO.Entities.User", "User")
                        .WithMany("Wishlists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BO.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("BO.Entities.Category", b =>
                {
                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("BO.Entities.Comment", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("BO.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("BO.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductCategory");

                    b.Navigation("Wishlists");
                });

            modelBuilder.Entity("BO.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Orders");

                    b.Navigation("Wishlists");
                });
#pragma warning restore 612, 618
        }
    }
}
