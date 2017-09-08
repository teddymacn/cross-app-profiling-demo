using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using CoreProfiler.Data;
using CoreProfiler;
using CoreProfiler.Web;
using Core.Api.Models;

namespace Core.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite()
                .AddDbContext<WebsiteDbContext>(
                    options => options.UseSqlite(GetConnection()));

            services.AddMvc();
        }

        private static DbConnection GetConnection()
        {
            return new ProfiledDbConnection(new SqliteConnection("Data Source=./mvcefsample.sqlite"), () =>
            {
                if (ProfilingSession.Current == null)
                    return null;

                return new DbProfiler(ProfilingSession.Current.Profiler);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
			
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCoreProfiler(true);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "child",
                    template: "{controller=Home}/{action=Child}");

                // InitializeDatabase is executed in async thread on app startup
                // to profile its performance, we need to call ProfilingSession.Start()/Stop() explicitly
                try
                {
                    ProfilingSession.Start("InitializeDatabase");
                    InitializeDatabase(app);
                }
                finally
                {
                    ProfilingSession.Stop();
                }
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<WebsiteDbContext>();

                if (db.Database.EnsureCreated())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        var article = new Article
                        {
                            Title = string.Format("Article {0}", i + 1),
                            Content = string.Format("Article {0} content blabla blabla", i + 1),
                            CreatedTime = DateTime.Now,
                            UpdatedTime = DateTime.Now
                        };

                        db.Articles.Add(article);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
