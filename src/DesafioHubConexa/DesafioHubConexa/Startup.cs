using DesafioHubConexa.Utils.Setup;
using DesafioHubConexa.Utils.Setup.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DesafioHubConexa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AppSettingsOpenWeatherMaps.Initialize(configuration.GetSection("OpenWeatherMapsConfigs").Get<AppSettingsOpenWeatherMaps>());
            AppSettingsSpotify.Initialize(configuration.GetSection("SpotifyConfigs").Get<AppSettingsSpotify>());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.RegisterServices();

            services.SwaggerConfigurationService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.SwaggerConfig();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
