using BO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder config = new ConfigurationBuilder();
            config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = config.Build();
            var conStr = root.GetConnectionString("MyDb");
            optionsBuilder.UseSqlServer(conStr);
        }
        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Page> Pages { get; set; }

        public DbSet<ProductCategory> Product_Category_Mappings { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Product_Category
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");
                entity.HasKey(cp => new { cp.ProductId, cp.CategoryId });
                entity.HasOne<Product>(cp => cp.Product)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(cp => cp.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Category>(cp => cp.Category)
                    .WithMany(c => c.ProductCategory)
                    .HasForeignKey(cp => cp.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.ProductId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.CategoryId)
                    .HasColumnType("char")
                    .HasMaxLength(6)
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
                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.QuantityInStock)
                    .HasColumnType("int")
                    .IsRequired(true);
                entity.Property(e => e.View)
                   .HasColumnType("int")
                   .IsRequired(true);
                entity.Property(e => e.Published)
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
                    .HasMaxLength(6)
                    .IsRequired(true)
                    .IsUnicode(false);


                //foreignkey
                entity.HasOne<Brand>(s => s.Brand)
                    .WithMany(b => b.Products)
                    .HasForeignKey(s => s.BrandId);



                entity.HasMany<OrderDetail>(b => b.OrderDetails)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany<Wishlist>(b => b.Wishlists)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            #endregion
            #region Picture
            modelBuilder.Entity<Picture>(entity =>
            {

                entity.ToTable("Picture");
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(16)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ObjectId)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode(false);
                entity.Property(e => e.ObjectType)
                   .HasColumnType("varchar")
                   .HasMaxLength(50)
                   .IsRequired(true)
                   .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Published)
                    .HasColumnType("bit")
                    .IsRequired(true);

                //entity.HasOne<Product>(p => p.Product)
                //.WithMany(i => i.Pictures)
                //.HasForeignKey(i => i.ObjectId)
                //.OnDelete(DeleteBehavior.Restrict);
                // entity.HasOne<Brand>(p => p.Brand)
                // .WithOne(i => i.Picture)
                // .HasForeignKey<Picture>(i => i.ObjectId)
                // .OnDelete(DeleteBehavior.Cascade);
                // entity.HasOne<Category>(p => p.Category)
                // .WithOne(i => i.Picture)
                // .HasForeignKey<Picture>(i => i.ObjectId)
                // .OnDelete(DeleteBehavior.Cascade);
                // entity.HasOne<Post>(p => p.Post)
                //.WithOne(i => i.Picture)
                //.HasForeignKey<Picture>(i => i.ObjectId)
                //.OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
            #region Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(6)
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
                
                entity.Property(e => e.Published)
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

                //entity.HasOne<Picture>(c => c.Picture)
                //.WithOne(i => i.Category)
                //.HasForeignKey<Picture>(i => i.ObjectId)
                //.OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
            #region Brand
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(6)
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
               
                entity.Property(e => e.Published)
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

                //entity.HasOne<Picture>(c => c.Picture)
                // .WithOne(i => i.Brand)
                // .HasForeignKey<Picture>(i => i.ObjectId)
                // .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
            #region Post
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("varchar")
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
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.FullDescription)
                    .HasColumnType("ntext")
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.View)
                  .HasColumnType("int")
                  .IsRequired(true);
                entity.Property(e => e.Published)
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

                //entity.HasOne<Picture>(c => c.Picture)
                //.WithOne(i => i.Post)
                //.HasForeignKey<Picture>(i => i.ObjectId)
                //.OnDelete(DeleteBehavior.Cascade);
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
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.LastName)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Birthday)
                   .HasColumnType("date")
                   .IsRequired(false);
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
                entity.Property(e => e.Role)
                  .HasColumnType("int")
                  .IsRequired(true);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasColumnType("varchar")
                    .HasMaxLength(64)
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
                entity.Property(e => e.Published)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.Deleted)
                    .HasColumnType("bit")
                    .IsRequired(true);

                entity.HasMany<Order>(b => b.Orders)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany<Wishlist>(b => b.Wishlists)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);



                //entity.HasOne<Picture>(c => c.Picture)
                //.WithOne(i => i.User)
                //.HasForeignKey<Picture>(i => i.ObjectId)
                //.OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
            #region Role
            //modelBuilder.Entity<Role>(entity =>
            //{
            //    entity.ToTable("Role");
            //    entity.HasKey(e => e.Id);
            //    entity.Property(e => e.Id).ValueGeneratedNever()
            //        .HasColumnType("int")
            //        .IsRequired(true)
            //        .IsUnicode(false);
            //    entity.Property(e => e.Name)
            //        .HasColumnType("nvarchar")
            //        .HasMaxLength(50)
            //        .IsRequired(true)
            //        .IsUnicode(true);                
            //});
            #endregion
            #region Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.UserId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Amount)
                    .HasColumnType("decimal")
                    .HasPrecision(18, 2)
                    .IsRequired(true);
                entity.Property(e => e.State)
                    .HasColumnType("int")
                    .IsRequired(true);
                entity.Property(e => e.DeliveryEmail)
                   .HasColumnType("varchar")
                   .HasMaxLength(100)
                   .IsRequired(true)
                   .IsUnicode(false);
                entity.Property(e => e.DeliveryPhone)
                   .HasColumnType("varchar")
                   .HasMaxLength(10)
                   .IsRequired(true)
                   .IsUnicode(false);
                entity.Property(e => e.DeliveryAddress)
                  .HasColumnType("nvarchar")
                  .HasMaxLength(250)
                  .IsRequired(true)
                  .IsUnicode(true);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
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
                entity.Property(e => e.Note)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(false)
                    .IsUnicode(true);


                entity.HasMany<OrderDetail>(b => b.OrderDetails)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion
            #region OrderDetail
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.OrderId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ProductId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
                entity.Property(e => e.Quantity)
                    .HasColumnType("int")
                    .IsRequired(true);
                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal")
                    .HasPrecision(18, 2)
                    .IsRequired(true);


            });
            #endregion
            #region Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ObjectId)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.ObjectType)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.UserId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.OrderDetailId)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(false)
                    .IsUnicode(false);
                entity.Property(e => e.Content)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(500)
                    .IsRequired(false)
                    .IsUnicode(true);
                entity.Property(e => e.Published)
                    .HasColumnType("bit")
                    .IsRequired(true);
                entity.Property(e => e.ParentId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true).HasPrecision(3);
                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(false).HasPrecision(3);
                entity.Property(e => e.Star)
                    .HasColumnType("int")
                    .IsRequired(false);

                entity.HasMany(x => x.Children)
                    .WithOne(x => x.Parent)
                    .HasForeignKey(x => x.ParentId);

                //entity.HasOne<User>(b => b.User)
                //.WithMany(i => i.Comments)
                //.HasForeignKey(i => i.UserId)
                //.OnDelete(DeleteBehavior.Restrict);
                //entity.HasOne<Product>(b => b.Product)
                //.WithMany(i => i.Comments)
                //.HasForeignKey(i => i.ObjectId)
                //.OnDelete(DeleteBehavior.Restrict);
                //entity.HasOne<Post>(b => b.Post)
                //.WithMany(i => i.Comments)
                //.HasForeignKey(i => i.ObjectId)
                //.OnDelete(DeleteBehavior.Cascade);
                //entity.HasOne<OrderDetail>(b => b.OrderDetail)
                //.WithOne(i => i.Comment)
                //.HasForeignKey<Comment>(i => i.OrderDetailId)
                //.OnDelete(DeleteBehavior.Cascade);

            });
            #endregion

            #region Wishlist
            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("Wishlist");
                entity.HasKey(w => new { w.ProductId, w.UserId });
                entity.HasOne<Product>(w => w.Product)
                    .WithMany(w => w.Wishlists)
                    .HasForeignKey(w => w.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<User>(w => w.User)
                    .WithMany(w => w.Wishlists)
                    .HasForeignKey(w => w.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(w => w.ProductId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(w => w.UserId)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
            });
            #endregion

            #region Banner
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banner");
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(6)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Content)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.SubContent)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Published)
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
                entity.Property(e => e.Order)
                    .HasColumnType("int")
                    .IsRequired(true);
                entity.Property(e => e.LinkTo)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(false).IsUnicode(false);
            });
            #endregion
            #region Question
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(12)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Content)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Email)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired(true)
                    .HasPrecision(3);
            });
            #endregion
            #region Contact
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(6)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Content)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Type)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Published)
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
            });
            #endregion
            #region Page
            modelBuilder.Entity<Page>(entity =>
            {
                entity.ToTable("Page");
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.Id)
                    .HasColumnType("char")
                    .HasMaxLength(6)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Title)
                    .HasColumnType("nvarchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Slug)
                    .HasColumnType("varchar")
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Content)
                    .HasColumnType("ntext")
                    .IsRequired(true)
                    .IsUnicode(true);
                entity.Property(e => e.Type)
                    .HasColumnType("varchar")
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
                entity.Property(e => e.Published)
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
            });
            #endregion
        }
    }
}
