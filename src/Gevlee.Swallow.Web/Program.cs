using Gevlee.Swallow.Web.Services;
using Gevlee.Swallow.Web.Settings;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Web
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			ConfigureConfiguration(builder.Configuration, builder.HostEnvironment);
			ConfigureServices(builder.Services, builder.HostEnvironment);

			var host = builder.Build();

			await host.RunAsync();
		}

		private static void ConfigureServices(IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
		{
			services.AddTransient(provider =>
			{
				return provider.GetRequiredService<IConfiguration>().Get<AppConfiguration>();
			});
			services.AddApiService<ITasksService, ApiTaskService>();
		}

		private static void ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IWebAssemblyHostEnvironment hostEnvironment)
		{
			Assembly assembly = typeof(Program).Assembly;

			var appsettingsFiles = assembly.GetManifestResourceNames()
				.Where(f => f.Contains("appsettings.", StringComparison.OrdinalIgnoreCase) && f.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
				.ToList();

			foreach (var resourceName in appsettingsFiles)
			{
				configurationBuilder.AddJsonStream(assembly.GetManifestResourceStream(resourceName));
			}
		}
	}
}
