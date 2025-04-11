using Application.Interfaces;
using Application.Services;
using Infrastructure.Clients;
using Infrastructure.Storage;

namespace WebApi.Configuracoes
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IRepositorioService, RepositorioService>();
            services.AddTransient<IFavoritosService, FavoritosService>();

            services.AddSingleton<IFavoritosStorage, FavoritosStorage>();

            services.AddHttpClient<IGitHubClient, GitHubClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubRepositorios");
            });
        }
    }
}
