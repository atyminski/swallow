using FluentValidation.AspNetCore;
using Gevlee.Swallow.Core;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Gevlee.Swallow.Api
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

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
			.AddFluentValidation(options =>
			{
				options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
				options.RegisterValidatorsFromAssemblyContaining<Startup>();
			});
			services.AddLiteDbPersistance("Filename=swallow.db;Connection=shared");
			services.AddLiteDbRepositories();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
			}

			//app.UseHttpsRedirection();

			app.Use((context, next) =>
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
					catch (System.Exception e)
					{
						db.Rollback();
						throw;
					}
				});
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
