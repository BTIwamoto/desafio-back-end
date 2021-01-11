using DesafioHubConexa.Services.Implementation;
using DesafioHubConexa.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioHubConexa.Utils.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRecomendacaoPlaylistService, RecomendacaoPlaylistService>();
        }
    }
}