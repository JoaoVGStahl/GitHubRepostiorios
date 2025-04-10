using Application.Interfaces;
using Application.Services;
using Infrastructure.Storage;

namespace WebApi.Configuracoes
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            //services.AddTransient<IRepositorioService, RepositorioService>();

            services.AddSingleton<IFavoritoStorage, FavoritoStorage>();
        }
    }
}
