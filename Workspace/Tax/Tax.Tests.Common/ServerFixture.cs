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

namespace Tax.Tests.Common
{
    public class ServerFixture : IDisposable
    {
        private const string BaseAddress = "http://localhost:5310";
        private readonly TestServer _server;
        protected HttpClient Client { get; }
        protected IServiceProvider ServiceProvider { get; }

        public ServerFixture()
        {
            try
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
            catch (Exception ex)
            {
                Dispose();
            }
        }

        protected async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> formData)
        {
            var antiForgeryResponse = await GetAntiForgeryResponseAsync();
            var antiForgeryBodyToken = await ExtractAntiForgeryBodyTokenAsync(antiForgeryResponse);
            var antiForgeryCookieToken = GetAntiForgeryCookieToken(antiForgeryResponse);

            formData.Add("__RequestVerificationToken", antiForgeryBodyToken);

            var content = new FormUrlEncodedContent(formData);
            content.Headers.Add("Cookie", antiForgeryCookieToken);
            return await Client.PostAsync(url, content);
        }

        private string GetAntiForgeryCookieToken(HttpResponseMessage antiForgResponse)
        {
            var cookieValue = antiForgResponse.Headers.GetValues("Set-Cookie").First(x => x.Contains("Antiforgery"));
            return cookieValue.Substring(0, cookieValue.IndexOf(";", StringComparison.Ordinal) + 1);
        }

        private async Task<HttpResponseMessage> GetAntiForgeryResponseAsync(string path = null)
        {
            path = path ?? $"/Account/{nameof(AccountController.Login)}";
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            var antiForgResponse = await Client.SendAsync(request);
            return antiForgResponse;
        }

        private async Task<string> ExtractAntiForgeryBodyTokenAsync(HttpResponseMessage response)
        {
            var htmlResponseText = await response.Content.ReadAsStringAsync();
            var match = Regex.Match(htmlResponseText, @"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
            return match.Success ? match.Groups[1].Captures[0].Value : null;
        }

        public void Dispose()
        {
            Client?.Dispose();
            _server?.Dispose();
        }
    }
}
