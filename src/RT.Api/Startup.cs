using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RT.Api.AspNet.ContainerRegistries;

namespace RT.Api
{
    /// <summary>
    /// Classe respons�vel pelo boot do sistema
    /// </summary>
    public class Startup
    {
        readonly string CorsDefault = "_corsDefault";

        /// <summary>
        /// Construtor padr�o da classe de startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Cont�m todas as configura��es da aplica��o
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Metodo chamado pelo runtime para registrar as dependencias do container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsDefault,
                                  builder =>
                                  {
                                      builder
                                      .AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });


            services
                .RegisterMediator()
                .RegisterApplication(Configuration)
                .RegisterSwaggerGen()
                .AddControllers()
                .AddNewtonsoftJson(opts => { });
        }

        /// <summary>
        /// Configura o pipeline de requisi��o da aplica��o
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(CorsDefault);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1.0");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}
