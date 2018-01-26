using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Tax.Web.Controllers;
using Tax.Web.Models.AccountViewModels;

namespace Tax.Tests.Common
{
    public class ServerFixture : IDisposable
    {
        private const string BaseAddress = "http://localhost:5310";
        private const string RequestVerificationToken = "__RequestVerificationToken";
        private readonly TestServer _server;
        protected HttpClient Client { get; }
        protected IServiceProvider ServiceProvider { get; }

        public ServerFixture()
        {
            const string environemnt = "Development";

            var rootLocation =
                $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.Parent?.FullName}\Tax.Web";

            var builder = new WebHostBuilder()
                .UseEnvironment(environemnt)
                .UseContentRoot(rootLocation)
                .UseStartup<TestStartup>();

            _server = new TestServer(builder);
            Client = _server.CreateClient();

            Client.BaseAddress = new Uri(BaseAddress);
            ServiceProvider = _server.Host.Services;
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> formData, string email = null, string password = null)
        {
            IList<string> cookies = new List<string>();
            if (email != null && password != null)
            {
                cookies = (await GetAuthTokenCookiesAsync(email, password)).ToList();
            }

            var antiForgeryResponse = await GetAntiForgeryResponseAsync(url, cookies);
            var antiForgeryBodyToken = await ExtractAntiForgeryBodyTokenAsync(antiForgeryResponse);
            var antiForgeryCookieToken = GetAntiForgeryCookieToken(antiForgeryResponse);

            formData.Add(RequestVerificationToken, antiForgeryBodyToken);

            var content = new FormUrlEncodedContent(formData);
            cookies.Add(antiForgeryCookieToken);
            content.Headers.Add("Cookie", cookies);

            return await Client.PostAsync(url, content);
        }

        private async Task<IEnumerable<string>> GetAuthTokenCookiesAsync(string email, string password)
        {
            const string authTokenCookieName = ".AspNetCore.Identity.Application";
            var payload = new LoginViewModel
            {
                Email = email,
                Password = password
            }.ToDictionary();
            var response = await PostAsync("/Account/Login", payload);

            var setCookies = response
                .Headers
                .GetValues("Set-Cookie");
            return setCookies.Where(x => x.Contains(authTokenCookieName));
        }

        private async Task<HttpResponseMessage> GetAntiForgeryResponseAsync(string path = null, IList<string> cookies = null)
        {
            path = path ?? "/Account/login";
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            if (cookies != null && cookies.Any())
                request.Headers.Add("Cookie", cookies);
            var antiForgResponse = await Client.SendAsync(request);
            return antiForgResponse;
        }

        private static string GetAntiForgeryCookieToken(HttpResponseMessage antiForgResponse)
        {
            var cookieValue = antiForgResponse.Headers.GetValues("Set-Cookie").First(x => x.Contains("Antiforgery"));
            return cookieValue.Substring(0, cookieValue.IndexOf(";", StringComparison.Ordinal) + 1);
        }

        private static async Task<string> ExtractAntiForgeryBodyTokenAsync(HttpResponseMessage response)
        {
            var htmlResponseText = await response.Content.ReadAsStringAsync();
            var match = Regex.Match(htmlResponseText, $@"\<input name=""{RequestVerificationToken}"" type=""hidden"" value=""([^""]+)"" \/\>");
            return match.Success ? match.Groups[1].Captures[0].Value : null;
        }

        public virtual void Dispose()
        {
            Client?.Dispose();
            _server?.Dispose();
        }
    }
}
