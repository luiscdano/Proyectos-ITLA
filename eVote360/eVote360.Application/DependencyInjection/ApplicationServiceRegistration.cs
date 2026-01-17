using Microsoft.Extensions.DependencyInjection;

namespace eVote360.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registrar aquí servicios puros de Application (casos de uso, validaciones, etc.)
            // NO DbContext, NO Repositorios EF, NO Infrastructure.
            return services;
        }
    }
}