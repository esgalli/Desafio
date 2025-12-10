using Application.UseCases.GetTitulos;
using Domain.Services;

namespace Webapi.Modules
{
    public static class UseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IGetTitulosUseCase, GetTitulosUseCase>();
            services.AddTransient<ICalculadoraService, CalculadoraService>();
            return services;
        }
    }
}
