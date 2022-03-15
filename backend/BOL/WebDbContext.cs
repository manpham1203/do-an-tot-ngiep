using BOL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class WebDbContext : DbContext
    {
        public WebDbContext() { }
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-ABENUK5\\SQLEXPRESS;Database=KhoaLuanTotNghiep;User Id=sa;Password=123456;");
        }
        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product_Category_Mapping> Product_Category_Mappings { get; set; }
        //public DbSet<Product_Brand_Mapping> Product_Brand_Mappings { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Product_Category_Mapping
            modelBuilder.Entity<Product_Category_Mapping>(entity =>
            {
                //1 dong product => nhieu dong category_product
                //1 dong category => nhieu dong category_product
                entity.ToTable("Product_Category_Mapping");
                entity.HasKey(cp => new { cp.ProductId, cp.CategoryId });
                entity.HasOne<Product>(cp => cp.Product)
                    .WithMany(p => p.Product_Category_Mapping)
                    .HasForeignKey(cp => cp.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Category>(cp => cp.Category)
                    .WithMany(c => c.Product_Category_Mapping)
                    .HasForeignKey(cp => cp.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
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
                entity.Property(e => e.Slug)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Price)
                    .HasColumnType("decimal")
                    .HasPrecision(18, 2)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.PriceDiscount)
                    .HasColumnType("decimal")
                    .HasPrecision(18, 2)
                    .IsRequired(false)
                    .IsUnicode(false);
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
                    .IsRequired(false)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit")
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .IsUnicode(false)
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
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit")
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasPrecision(3);
                entity.Property(e => e.Ordinal)
                    .HasColumnType("int")
                    .IsRequired(false)
                    .IsUnicode(false);
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
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit")
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasPrecision(3);
                entity.Property(e => e.Ordinal)
                    .HasColumnType("int")
                    .IsRequired(false)
                    .IsUnicode(false);
            });
            #endregion
            #region Product_Brand_Mapping
            //modelBuilder.Entity<Product_Brand_Mapping>(entity =>
            //{
            //    //1 dong product => nhieu 1 dong mapping
            //    //1 dong brand => nhieu dong mapping
            //    entity.ToTable("Product_Brand_Mapping");
            //    entity.HasKey(pbm => new { pbm.ProductId, pbm.BrandId });
            //    entity.HasOne<Product>(pbm => pbm.Product)
            //        .WithOne(p => p.Product_Brand_Mapping)
            //        .HasForeignKey<Product_Brand_Mapping>(pbm => pbm.ProductId)
            //        .OnDelete(DeleteBehavior.Restrict);
            //    entity.HasOne<Brand>(pbm => pbm.Brand)
            //        .WithMany(b => b.Product_Brand_Mapping)
            //        .HasForeignKey(pbm => pbm.BrandId)
            //        .OnDelete(DeleteBehavior.Restrict);
            //});
            #endregion
        }
    }
}
