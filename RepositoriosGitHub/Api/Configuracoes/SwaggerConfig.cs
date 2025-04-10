using Microsoft.OpenApi.Models;

namespace WebApi.Configuracoes
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de Repositórios",
                    Version = "v1",
                    Description = "API para pesquisar e favoritas repositórios públicos do GitHub."
                });
            });
        }

        public static void UseSwaggerConfig(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
