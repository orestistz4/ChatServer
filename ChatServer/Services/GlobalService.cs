using ChatServer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Services
{
    public class GlobalService
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IConfiguration Configuration { get; set; }

        public static T GetServiceDI<T>()
        {
            using (var scoped = ServiceProvider.CreateScope())
            {
                var serviceProvider = scoped.ServiceProvider;
                return serviceProvider.GetRequiredService<T>();
            }
        }

        public static void UpdateDatabase()
        {
            using (var scoped = ServiceProvider.CreateScope())
            {
                var serviceProvider = scoped.ServiceProvider;

                using (var context = serviceProvider.GetRequiredService<AppDbContext>())
                {
                    context.Migration();
                }
            }
        }
    }
}
