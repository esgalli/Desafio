using Domain.Repositories;
using Infrastructure.Repositories;

namespace Webapi.Modules
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITituloRepository, TituloRepository>();
            services.AddScoped<IFinanceRepository, FinanceRepository>();

            return services;
        }
    }
}
