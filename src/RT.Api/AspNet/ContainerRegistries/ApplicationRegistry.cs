using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using RT.Application.Abstractions.Validators;
using RT.Application.Commands.ClienteFeatures;
using RT.Application.Queries;
using RT.Application.Queries.Abstractions;
using RT.Domain;
using RT.Domain.Repositories;
using RT.Infra.Dapper.Finders;
using RT.Infra.Ef.Contexts;
using RT.Infra.Ef.Repositories;
using System.Data;

namespace RT.Api.AspNet.ContainerRegistries
{
    /// <summary>
    /// Contém a configuracão de container da aplicação
    /// </summary>
    public static class ApplicationRegistry
    {

        /// <summary>
        /// Extensão para configurar todas as dependencias da aplicação
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterApplication(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration["MySqlConnection"];
          
            services.AddScoped<IDbConnection>(c => new MySqlConnection(connectionString));


            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseMySQL(connectionString);
            });


            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IUnitOfWork>(e => e.GetService<AppDbContext>());

            services.AddScoped<IAtualizarClienteCommandValidator, AtualizarClienteCommandValidator>();
            services.AddScoped<ICadastrarClienteCommandValidator, CadastrarClienteCommandValidator>();
            services.AddScoped<IPesquisarClienteRequestValidator, PesquisarClienteRequestValidator>();

            services.AddScoped<IPesquisarClienteFinder, PesquisarClienteFinder>();
            services.AddScoped<IListarCidadesFinder, ListarCidadesFinder>();


            return services;
        }
    }
}
