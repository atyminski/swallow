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
			serviceCollection.AddHttpClient<T, TInstance>(client =>
			{
				client.BaseAddress = new Uri("http://localhost:5000");
			});
			return serviceCollection;
		}
	}
}
