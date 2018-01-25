using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tax.Data;
using Tax.Web;

namespace Tax.Tests.Common
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void SetupDatabase(IServiceCollection services)
        {
            var connection = new SqliteConnection($"DataSource=file:Tax-Db{Guid.NewGuid()}?mode=memory");
            var options = new DbContextOptionsBuilder<TaxDbContext>()
                .UseSqlite(connection).Options;

            using (var context = new TaxDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
            }

            services.AddTransient(x => new TaxDbContext(options));
        }
    }
}
