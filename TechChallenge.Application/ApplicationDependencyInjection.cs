using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace TechChallenge.Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
        }
    }
}
