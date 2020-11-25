using FluentValidation.AspNetCore;
using Gevlee.Swallow.Core;
using Gevlee.Swallow.Server.Extensions;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration
		{
			get;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<IISServerOptions>(options =>
			{
				options.AutomaticAuthentication = false;
			});

			services.AddControllers(config =>
			{
				config.UseCentralRoutePrefix(new RouteAttribute("api"));
			})
			.AddFluentValidation(options =>
			{
				options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
				options.RegisterValidatorsFromAssemblyContaining<Startup>();
			});
			services.AddLiteDbPersistance();
			services.AddLiteDbRepositories();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseCors(options =>
				{
					options.AllowAnyOrigin();
					options.AllowAnyMethod();
					options.AllowAnyHeader();
				});
				app.UseWebAssemblyDebugging();
			}

			//app.UseHttpsRedirection();

			app.Use((context, next) =>
			{
				if (context.Request.Path.StartsWithSegments("/api"))
				{
					return Task.Run(async () =>
					{
						var db = context.RequestServices.GetRequiredService<ILiteDatabase>();
						try
						{
							if (db.BeginTrans())
							{
								await next.Invoke();
								db.Commit();
							}
							else
							{
								throw new System.Exception("Cannot open transaction");
							}
						}
						catch (System.Exception)
						{
							db.Rollback();
							throw;
						}
					});
				}
				else
					return next.Invoke();
			});

			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
