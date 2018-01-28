using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tax.Data;
using Tax.Repository;
using Tax.Tests.Common;

namespace Tax.Tests.FunctionalTests
{
    public class ServerFixture : IDisposable
    {
        private readonly IWebHost _host;
        private readonly string _baseAddress = "http://localhost:{0}";
        private static int _portNumber = 5410;
        protected const string Email = "ibraheem.osama@gmail.com";
        protected const string Password = "P@ssw0rd";

        protected IWebDriver Driver { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected UserManager<ApplicationUser> UserManager { get; }
        protected IUserTaxRepository UserTaxRepository { get; }
        protected ApplicationUser User => UserManager.Users.FirstOrDefault(x => x.Email == Email);

        public ServerFixture()
        {
            _baseAddress = string.Format(_baseAddress, _portNumber++);

            const string environemnt = "Development";
            var rootLocation =
                $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.FullName}\Tax.Web";

            _host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(_baseAddress)
                .UseEnvironment(environemnt)
                .UseContentRoot(rootLocation)
                .UseStartup<TestStartup>().Build();

            ServiceProvider = _host.Services;

            // fire and forget
            _host.RunAsync();

            UserManager = ServiceProvider.GetService<UserManager<ApplicationUser>>();
            UserTaxRepository = ServiceProvider.GetService<IUserTaxRepository>();

            // Selenium Tests
            Driver = new ChromeDriver(Environment.CurrentDirectory);
            Driver.Url = _baseAddress;
        }

        public virtual void Dispose()
        {
            _host.Dispose();
            Driver.Dispose();
        }
    }
}
