using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

namespace RT.Api.AspNet.ContainerRegistries
{
    /// <summary>
    /// Contém como configurar o swagger
    /// </summary>
    public static class SwaggerRegistry
    {

        /// <summary>
        /// Extensão para configurar o swagger na aplicação
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSwaggerGen(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.IncludeXmlComments(Path.ChangeExtension(Assembly.GetAssembly(typeof(Startup)).Location, "xml"), true);
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API - Cadastro de Clientes",
                    Version = "1.0",
                    Description = "Api para cadastro de clientes",
                    Contact = new OpenApiContact()
                    {
                        Name = "Renan Torres (renan.torres87@gmail.com)"
                    }
                });
            });

            return services;
        }
    }
}
