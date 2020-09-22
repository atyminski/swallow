using Gevlee.Swallow.Core.Persistence;
using Gevlee.Swallow.Core.Persistence.Repository;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace Gevlee.Swallow.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLiteDbPersistance(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ILiteDatabase>((provider) => new LiteDatabase(connectionString, LiteDbConfig.Mapper));
            return services;
        }

        public static IServiceCollection AddLiteDbRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
            return services;
        }
    }
}
