using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Infrastructure.Context;
using E_COMMERCE_APP.Services.Abstracts;
using E_COMMERCE_APP.Services.BackGroundServices;
using E_COMMERCE_APP.Services.ServicesImp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services
{
    public static class ServicesDI
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddHostedService<SetOrderStatusServices>();
            services.AddScoped<IApplicationUserSrvices, ApplicationUserServices>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IAuthorizationServices, AuthorizationServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
            


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddTransient<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            services.AddDistributedMemoryCache();

            return services;
        }
    }
}


