using HappyFarmer.Entities;
using HappyFarmer.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace HappyFarmer.DataAccess.Concrete
{
    public class FarmerContext : DbContext//IdentityDbContext<FarmerApplicationUser>
    {
        private static string ConnectionString { get; set; }
        public static void SetConnectionString(string connectionString)
        {
            if (ConnectionString == null)
            {
                ConnectionString = connectionString;
            }
        }
        public FarmerContext(DbContextOptions<FarmerContext> options) : base(options) { }


        public FarmerContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //"server=localhost;port=3306;database=HappyFarmerDB;uid=root;password=;CharSet=utf8;Convert Zero Datetime=true; Allow Zero Datetime=true"
            optionsBuilder.UseMySQL(ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FarmerProductCategory>().HasKey(a => new { a.CategoryId, a.ProductId });
            modelBuilder.Entity<FarmerProductCategory>()
             .HasOne(bc => bc.Category)
             .WithMany(b => b.ProductCategories)
             .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<FarmerProductCategory>()
              .HasOne(bc => bc.Product)
              .WithMany(c => c.ProductCategories)
              .HasForeignKey(bc => bc.ProductId);

            modelBuilder.Entity<FarmerMessage>()
             .HasOne(bc => bc.Users)
             .WithMany(b => b.Messages)
             .HasForeignKey(bc => bc.UserSenderId);

            modelBuilder.Entity<FarmerMessage>()
           .HasOne(bc => bc.Users)
           .WithMany(b => b.Messages)
           .HasForeignKey(bc => bc.UserReceiverId);

            modelBuilder.Entity<FarmerMultipleProductImages>()
              .HasOne(bc => bc.Product)
              .WithMany(c => c.MultipleProductImages)
              .HasForeignKey(bc => bc.ProductId);

            modelBuilder.Entity<FarmerBlog>()
             .HasOne(bc => bc.User)
             .WithMany(c => c.Blogs)
             .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<FarmerGlobalMessage>()
             .HasOne(bc => bc.User)
             .WithMany(c => c.GlobalMessages)
             .HasForeignKey(bc => bc.SenderId);

            modelBuilder.Entity<FarmerBlog>()
           .HasOne(bc => bc.User)
           .WithMany(c => c.Blogs)
           .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<FarmerComment>()
           .HasOne(bc => bc.BlogComments)
           .WithMany(c => c.Comments)
           .HasForeignKey(bc => bc.BlogId);

            modelBuilder.Entity<ProductComment>()
          .HasOne(bc => bc.Product)
          .WithMany(c => c.ProductComments)
          .HasForeignKey(bc => bc.ProductId);

            modelBuilder.Entity<FarmerOrder>()
           .HasOne(bc => bc.User)
           .WithMany(c => c.Orders)
           .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<FarmerCartItem>()
            .HasOne(bc => bc.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(bc => bc.CartId);

            modelBuilder.Entity<FarmerOrderItem>()
           .HasOne(bc => bc.FarmerOrder)
           .WithMany(c => c.FarmerOrderItems)
           .HasForeignKey(bc => bc.OrderId);

           modelBuilder.Entity<FarmerOrderItem>()
          .HasOne(bc => bc.FarmerProduct)
          .WithMany(c => c.FarmerOrderItems)
          .HasForeignKey(bc => bc.ProductId);

            modelBuilder.Entity<FarmerAboutUs>().HasKey(a => new { a.Id });

            //modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(login => login.UserId);
            //modelBuilder.Entity<IdentityUserRole<string>>().HasKey(login => login.UserId);
            //modelBuilder.Entity<IdentityUserClaim<string>>().HasKey(login => login.UserId);
            //modelBuilder.Entity<IdentityUserToken<string>>().HasKey(login => login.UserId);
        }

        public virtual DbSet<FarmerProduct> Products { get; set; }
        public virtual DbSet<FarmerCategory> FarmerCategory { get; set; }
        public virtual DbSet<FarmerAboutUs> AboutUs { get; set; }
        public virtual DbSet<FarmerMenu> Menus { get; set; }
        public virtual DbSet<FarmerSlider> Sliders { get; set; }
        public virtual DbSet<FarmerMessage> Messages { get; set; }
        public virtual DbSet<FarmerGlobalMessage> GlobalMessages { get; set; }
        public virtual DbSet<FarmerUser> FarmerUser { get; set; }
        public virtual DbSet<FarmerAdminMessage> AdminMessages { get; set; }
        public virtual DbSet<FarmerGeneralSettings> GeneralSettings { get; set; }
        public virtual DbSet<FarmerMultipleProductImages> MultipleProductImages { get; set; }
        public virtual DbSet<FarmerSecurityInformation> SecurityInformations { get; set; }
        public virtual DbSet<FarmerComment> Comments { get; set; }
        public virtual DbSet<FarmerBlog> Blogs { get; set; }
        public virtual DbSet<FarmerCart> Carts { get; set; }
        public virtual DbSet<FarmerCartItem> FarmerCartItems { get; set; }
        public virtual DbSet<FarmerOrder> Orders { get; set; }
        public virtual DbSet<FarmerOrderItem> OrderItems { get; set; }
        public virtual DbSet<FarmerBanner> Banners { get; set; }
        public virtual DbSet<ProductComment> ProductComments { get; set; }
        public virtual DbSet<FarmerCities> Cities { get; set; }
        public virtual DbSet<FarmerDistrincs> Distrincs { get; set; }
    }
}
