using E_COMMERCE_APP.Data.Helpers;
using E_COMMERCE_APP.Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using E_COMMERCE_APP.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using E_COMMERCE_APP.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace E_COMMERCE_APP.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructureDependencies(
            this IServiceCollection services,
            string connectionString,
            IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });


            var jwtSettings   = new JwtSettings();
            var emailSettings = new EmailSettings();

            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);

            services.AddSingleton(jwtSettings);
            services.AddSingleton(emailSettings);

            services.AddIdentity<ApplicationUser, Role>(options =>
            {
                options.Password.RequireDigit     = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail   = true;
            })
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders(); ;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme             = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken                 = true;
                options.RequireHttpsMetadata      = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {                            
                    ValidateIssuer           = true,
                    ValidIssuer              = jwtSettings.Issuer,
                    ValidateAudience         = true,
                    ValidAudience            = jwtSettings.Audience,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey         = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });



            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IProductRepository,      ProductRepository>();
            services.AddScoped<ICategoryRepository,     CategoryRepository>();
            services.AddScoped<ICartRepository,         CartRepository>();
            services.AddScoped<IOrderRepository,        OrderRepository>();
            services.AddScoped<IReviewRepository,       ReviewRepository>();

            return services;
        }
    }
}
