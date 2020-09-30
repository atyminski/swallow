using Gevlee.Swallow.Web.Settings;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gevlee.Swallow.Web
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApiService<T, TInstance>(this IServiceCollection serviceCollection)
			where T : class
			where TInstance : class, T
		{
			serviceCollection.AddHttpClient<T, TInstance>((provider, client) =>
			{
				var environment = provider.GetRequiredService<IWebAssemblyHostEnvironment>();
				var apiUrl = provider.GetRequiredService<AppConfiguration>().Api.Url;
				
				if (string.IsNullOrWhiteSpace(apiUrl) || apiUrl.StartsWith('/'))
				{
					apiUrl = $"{environment.BaseAddress}{apiUrl.TrimStart('/')}";
				}

				apiUrl = apiUrl.TrimEnd('/') + "/";
				client.BaseAddress = new Uri(apiUrl);
			});
			return serviceCollection;
		}
	}
}
