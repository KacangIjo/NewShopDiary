using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;

[assembly: OwinStartup(typeof(ShopDiaryApp.API.Startup))]

namespace ShopDiaryApp.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
    //        services.AddIdentity<ApplicationUser, IdentityRole>()
    //    .AddEntityFrameworkStores<ShopDiaryDbContext>()
    //    .AddDefaultTokenProviders();

    //        services.AddAuthentication().AddGoogle(googleOptions =>
    //{
    //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
    //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
    //});
            ConfigureAuth(app);
        }

//      
    }
}
