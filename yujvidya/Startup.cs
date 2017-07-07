using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.Dashboard;
using System.Diagnostics;
using yujvidya.Migrations;

namespace yujvidya
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add framework services.  
            var connectionString = ConfigStrings.DbConnectionString;
            services.AddDbContext<PersonContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddHangfire(config => config.UseStorage(new PostgreSqlStorage(connectionString)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();

            PersonContext personContext;
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                personContext = serviceScope.ServiceProvider.GetService<PersonContext>();
                personContext.Database.Migrate();
                InitialDataSeed.Update(personContext);
            }

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new List<IDashboardAuthorizationFilter>() { new HangfireNoAuthFilter() },
            });

            // Need to find some beetter way to start
            Managers.NotificationScheduleManager.StartSchecules();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World from Docker @ 2016-16-04 22:32!");
            });
        }

        public static void DailyCallback()
        {
            Console.WriteLine($"*********************** Hangfire update console {DateTime.Now} ***********************");
            Debug.WriteLine($"*********************** Hangfire update debug {DateTime.Now} ***********************");
        }
    }
}
