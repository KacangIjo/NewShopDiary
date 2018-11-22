using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ShopDiaryApp.API.NotificationScheduler;
using ShopDiaryProject.Domain.Models;
using ShopDiaryProject.EF;

[assembly: OwinStartup(typeof(ShopDiaryApp.API.Startup))]

namespace ShopDiaryApp.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
