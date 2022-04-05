using BO.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() { }
        //public AppDbContext(DbContextOptions options) : base(options) { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-ABENUK5\\SQLEXPRESS;Database=KhoaLuanTotNghiep;User Id=sa;Password=123456;");
        }
        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandImage> BrandImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryImage> CategoryImages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<ProductCategory> Product_Category_Mappings { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Product_Category
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                //1 dong product => nhieu dong category_product
                //1 dong category => nhieu dong category_product
                entity.ToTable("ProductCategory");
                entity.HasKey(cp => new { cp.ProductId, cp.CategoryId });
                entity.HasOne<Product>(cp => cp.Product)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(cp => cp.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Category>(cp => cp.Category)
                    .WithMany(c => c.ProductCategory)
                    .HasForeignKey(cp => cp.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.ProductId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ProductId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
            });
            #endregion
            #region Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.Property(e => e.Slug)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Price)
                    .HasColumnType("decimal")
                    .HasPrecision(18, 2)
                    .IsRequired(true);
                entity.Property(e => e.PriceDiscount)
                    .HasColumnType("decimal")
                    .HasPrecision(18, 2)
                    .IsRequired(false);
                entity.Property(e => e.FullDescription)
                    .HasColumnType("ntext")
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.ShortDescription)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(1000)
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.Quantity)
                    .HasColumnType("int")
                    .IsRequired(true);
                entity.Property(e => e.Views)
                   .HasColumnType("int")
                   .IsRequired(true);
                entity.Property(e => e.Likes)
                   .HasColumnType("int")
                   .IsRequired(true);
                entity.Property(e => e.Pulished)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .HasPrecision(3);
                entity.Property(e => e.BrandId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);


                //foreignkey
                entity.HasOne<Brand>(s => s.Brand)
                    .WithMany(b => b.Products)
                    .HasForeignKey(s => s.BrandId);

                entity.HasMany<ProductImage>(p => p.ProductImage)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            #endregion
            #region ProductImage
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage");
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ProductId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Pulished)
                    .HasColumnType("bit")
                    .IsRequired(true);
            });
            #endregion
            #region Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.Property(e => e.Slug)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ShortDescription)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(1000)
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.FullDescription)
                    .HasColumnType("ntext")
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.Pulished)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .HasPrecision(3);
                entity.Property(e => e.Ordinal)
                    .HasColumnType("int")
                    .IsRequired(false);

                entity.HasMany<CategoryImage>(c => c.CategoryImage)
                .WithOne(i => i.Catgeory)
                .HasForeignKey(i => i.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
            #region CategoryImage
            modelBuilder.Entity<CategoryImage>(entity =>
            {
                entity.ToTable("CategoryImage");
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.CategoryId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Pulished)
                    .HasColumnType("bit")
                    .IsRequired(true);
            });
            #endregion
            #region Brand
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.Property(e => e.Slug)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ShortDescription)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(1000)
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.FullDescription)
                    .HasColumnType("ntext")
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.Pulished)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .HasPrecision(3);
                entity.Property(e => e.Ordinal)
                    .HasColumnType("int")
                    .IsRequired(false);

                entity.HasMany<BrandImage>(b => b.BrandImage)
                .WithOne(i => i.Brand)
                .HasForeignKey(i => i.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
            #region BrandImage
            modelBuilder.Entity<BrandImage>(entity =>
            {
                entity.ToTable("BrandImage");
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.BrandId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Pulished)
                    .HasColumnType("bit")
                    .IsRequired(true);
            });
            #endregion
            #region Post
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Title)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.HasIndex(e => e.Slug).IsUnique();
                entity.Property(e => e.Slug)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ShortDescription)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(1000)
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.FullDescription)
                    .HasColumnType("ntext")
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.Views)
                  .HasColumnType("int")
                  .IsRequired(true);
                entity.Property(e => e.Likes)
                   .HasColumnType("int")
                   .IsRequired(true);
                entity.Property(e => e.Pulished)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .HasPrecision(3);
                entity.Property(e => e.Image)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(false)
                    .IsUnicode(false);
            });
            #endregion
            #region User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.LastName)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Birthday)
                   .HasColumnType("date")
                   .IsRequired(false);
                   //.HasPrecision(3);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email)
                   .HasColumnType("varchar")
                   .HasMaxLength(100)
                   .IsRequired(false)
                   .IsUnicode(false);
                entity.HasIndex(e => e.PhoneNumber).IsUnique();
                entity.Property(e => e.PhoneNumber)
                   .HasColumnType("varchar")
                   .HasMaxLength(10)
                   .IsRequired(false)
                   .IsUnicode(false);
                entity.Property(e => e.Address)
                  .HasColumnType("nvarchar")
                  .HasMaxLength(250)
                  .IsRequired(false)
                  .IsUnicode(true);
                entity.Property(e => e.RoleId)
                  .HasColumnType("int")
                  .IsRequired(true);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username)
                    .HasColumnType("varchar")
                    .HasMaxLength(1000)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasColumnType("varchar")
                    .HasMaxLength(1000)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .HasPrecision(3);

                entity.HasOne<Role>(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId);
            });
            #endregion
            #region Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever()
                    .HasColumnType("int")
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);                
            });
            #endregion
        }
    }
}
