using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace E_COMMERCE_APP.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, Role, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserRefreshToken> RefreshTokens { get; set; }
        public DbSet<Category>         Categories    { get; set; }
        public DbSet<Product>          products      { get; set; }
        public DbSet<Cart>             Carts         { get; set; }
        public DbSet<Order>            Orders        { get; set; }
        public DbSet<Review>           Reviews       { get; set; }
        public DbSet<ProductImages>    ProductImages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
