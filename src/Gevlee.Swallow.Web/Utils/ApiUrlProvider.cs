using Gevlee.Swallow.Web.Settings;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;

namespace Gevlee.Swallow.Web.Utils
{
    public class ApiUrlProvider : IApiUrlProvider
    {
        private readonly IWebAssemblyHostEnvironment hostEnvironment;
        private readonly AppConfiguration appConfiguration;

        public ApiUrlProvider(IWebAssemblyHostEnvironment hostEnvironment, AppConfiguration appConfiguration)
        {
            this.hostEnvironment = hostEnvironment;
            this.appConfiguration = appConfiguration;
        }

        public Uri BaseUrl => GetBaseUrl();

        private Uri GetBaseUrl()
        {
            var apiUrl = appConfiguration.Api.Url;

            if (string.IsNullOrWhiteSpace(apiUrl) || apiUrl.StartsWith('/'))
            {
                apiUrl = $"{hostEnvironment.BaseAddress}{apiUrl.TrimStart('/')}";
            }

            return new Uri(apiUrl.TrimEnd('/') + "/");
        }
    }
}
