using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;

namespace DesafioHubConexa.Utils.Setup
{
    public static class SwaggerConfiguration
    {
        public static void SwaggerConfigurationService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Desafio Hub Conexa - API",
                    Version = "v1",
                    Description = "Recomendação de Playlist de acordo com a temperatura em graus celsius de uma determinada cidade",
                    Contact = new OpenApiContact
                    {
                        Name = "Bruno Ivamoto",
                        Email = "bruno.ivamoto@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/bruno-ivamoto/")
                    }

                });
                c.CustomSchemaIds(x => x.FullName); //Essa linha deve ser inserida em casos que há classes com mesmo nome em namespaces diferentes

                //Obtendo o diretório e depois o nome do arquivo .xml de comentários
                var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                //Caso exista arquivo então adiciona-lo
                if (File.Exists(caminhoXmlDoc))
                {
                    c.IncludeXmlComments(caminhoXmlDoc);
                }
            });
        }

        public static void SwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Hub Conexa - API");
            });
        }
    }
}