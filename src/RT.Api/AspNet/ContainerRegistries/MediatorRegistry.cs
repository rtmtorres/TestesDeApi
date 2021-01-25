using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RT.Application.Commands.ClienteFeatures;
using System.Reflection;

namespace RT.Api.AspNet.ContainerRegistries
{
    /// <summary>
    /// Contém a configuracão do mediatr
    /// </summary>
    public static class MediatorRegistry
    {
        /// <summary>
        /// Extensão para configurar o mediatr na aplicação
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterMediator(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CadastrarClienteCommand).GetTypeInfo().Assembly);

            return services;
        }
    }
}
