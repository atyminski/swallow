using Gevlee.Swallow.Web.Utils;
using Microsoft.Extensions.DependencyInjection;

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
				client.BaseAddress = provider.GetRequiredService<IApiUrlProvider>().BaseUrl;
			});

			return serviceCollection;
		}
	}
}
