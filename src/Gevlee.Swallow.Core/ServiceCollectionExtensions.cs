using Gevlee.Swallow.Core.Persistence;
using Gevlee.Swallow.Core.Persistence.Repository;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gevlee.Swallow.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteDbPersistance(this IServiceCollection services)
        {
            services.AddScoped<ILiteDatabase>((provider) => 
            {
                var filename = provider.GetRequiredService<IConfiguration>().GetSection("Db:LiteDb:DbFile")?.Value ?? "swallow.db";
                return new LiteDatabase($"Filename={filename};Connection=shared", LiteDbConfig.Mapper);
            });
            return services;
        }

        public static IServiceCollection AddLiteDbRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ITaskActivityRepository, TaskActivityRepository>();
            return services;
        }
    }
}
