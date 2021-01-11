using DesafioHubConexa.Models;
using DesafioHubConexa.Models.Enums;
using DesafioHubConexa.Models.ValueObjects;
using DesafioHubConexa.Utils;
using DesafioHubConexa.Utils.Extensions;
using DesafioHubConexa.Utils.Setup.Configs;
using Microsoft.OpenApi.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DesafioHubConexa.Providers
{
    public class SpotifyProvider : Service
    {
        #region Privados
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri(AppSettingsSpotify.Instance.BaseURL)
        };
        #endregion

        public async Task<Playlist> ObterRecomendacaoPlaylistPorCategoria(Cidade cidade)
        {
            await ValidarToken();

            ConfigurarHttpClient();

            var categoriaPlaylist = ObterCategoriaPlaylistPorTemperatura(cidade.Temperatura);

            var response = await ObterRecomendacaoPlaylist(categoriaPlaylist);

            TratarErrosResponse(response);

            var result = DeserializarObjetoResponse(response).ConverterParaPlaylist(categoriaPlaylist);

            return result;
        }

        #region Privados
        private static async Task<HttpResponseMessage> ObterRecomendacaoPlaylist(CategoriaPlaylist categoriaPlaylist)
        {
            return await _httpClient.GetAsync(string.Format(AppSettingsSpotify.Instance.ObterRecomendacaoPlaylistPorCategoria, categoriaPlaylist.GetDisplayName()));
        }

        private static void ConfigurarHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SpotifyToken.Instance.Bearer);
        }

        private async Task ValidarToken()
        {
            if (SpotifyToken.Instance == null || SpotifyToken.Instance.TokenValidation < DateTime.Now)
                await Autenticar();
        }

        private async Task Autenticar()
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri(AppSettingsSpotify.Instance.BaseAutenticacaoURL) };

            using var request = CreateRequest();

            var response = await httpClient.SendAsync(request);

            DeserializarObjetoResponse(response).ConverterParaSpotifyToken();
        }

        private static HttpRequestMessage CreateRequest()
        {
            var request = new HttpRequestMessage(new HttpMethod("POST"), AppSettingsSpotify.Instance.BaseAutenticacaoURL);

            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {AppSettingsSpotify.Instance.BasicToken}");
            request.Content = new StringContent("grant_type=client_credentials");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            return request;
        }

        private static CategoriaPlaylist ObterCategoriaPlaylistPorTemperatura(float temperatura)
        {
            if (temperatura > 30)
                return CategoriaPlaylist.party;
            if (temperatura > 14 && temperatura < 31)
                return CategoriaPlaylist.pop;
            if (temperatura > 9 && temperatura < 15)
                return CategoriaPlaylist.rock;
            else
                return CategoriaPlaylist.classical;
        }
        #endregion Privados
    }
}