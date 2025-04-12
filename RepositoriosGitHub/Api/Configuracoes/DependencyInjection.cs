using Application.Interfaces;
using Application.Services;
using Infrastructure.Clients;
using Infrastructure.Configurations;
using Infrastructure.Storage;

namespace WebApi.Configuracoes
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioService, RepositorioService>();
            services.AddScoped<IFavoritosService, FavoritosService>();

            services.AddSingleton<IRelevanciaService, RelevanciaService>();
            services.AddSingleton<IFavoritosStorage, FavoritosStorage>();

            services.AddHttpClient<IGitHubClient, GitHubClient>(client =>
            {
                client.BaseAddress = new Uri(GitHubClientConfiguration.BaseUrl);
                client.DefaultRequestHeaders.UserAgent.ParseAdd(GitHubClientConfiguration.UserAgent);
            });
        }
    }
}
