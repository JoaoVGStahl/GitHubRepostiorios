using Infrastructure.Clients;
using Infrastructure.Configurations;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGitHubInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient(GitHubClientConfiguration.HttpClientName, client =>
            {
                client.BaseAddress = new Uri(GitHubClientConfiguration.BaseUrl);
                client.DefaultRequestHeaders.Add("User-Agent", GitHubClientConfiguration.UserAgent);
            });

            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IGitHubClient, GitHubClient>();

            return services;
        }
    }
} 