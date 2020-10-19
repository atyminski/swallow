using Gevlee.Swallow.Core.Persistence;
using Gevlee.Swallow.Core.Persistence.Repository;
using LiteDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Gevlee.Swallow.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteDbPersistance(this IServiceCollection services)
        {
            services.AddScoped<ILiteDatabase>((provider) => 
            {
                var filePath = Path.GetFullPath(provider.GetRequiredService<IConfiguration>().GetSection("Db:LiteDb:DbFile")?.Value ?? "swallow.db");
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
                return new LiteDatabase($"Filename={filePath};Connection=shared", LiteDbConfig.Mapper);
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
