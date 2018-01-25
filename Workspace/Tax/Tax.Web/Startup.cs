using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tax.Core;
using Tax.Data;
using Tax.Repository;

namespace Tax.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void SetupDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<TaxDbContext>();
            options.UseSqlServer(connectionString);
            using (var context = new TaxDbContext(options.Options))
            {
                context.Database.Migrate();
            }
            services.AddDbContext<TaxDbContext>(o => o.UseSqlServer(connectionString));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            SetupDatabase(services);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TaxDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddTransient<IUserTaxRepository, UserTaxRepository>();
            services.AddTransient<ITaxService, TaxService>();
            services.AddTransient<IYearService, YearService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Run(async (context) => {
            //    await context.Response.WriteAsync(
            //       "Hello World! This ASP.NET Core Application");
            //});
        }
    }
}
