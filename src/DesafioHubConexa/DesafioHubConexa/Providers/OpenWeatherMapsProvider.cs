using DesafioHubConexa.Models.ValueObjects;
using DesafioHubConexa.Utils;
using DesafioHubConexa.Utils.Extensions;
using DesafioHubConexa.Utils.Setup.Configs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesafioHubConexa.Providers
{
    public class OpenWeatherMapsProvider : Service
    {
        #region Privados
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri(AppSettingsOpenWeatherMaps.Instance.BaseURL)
        };
        #endregion

        public OpenWeatherMapsProvider()
        {
        }

        public async Task<Cidade> ObterTemperaturaPorNomeCidade(Cidade cidade)
        {
            var response = await _httpClient.GetAsync(string.Format(AppSettingsOpenWeatherMaps.Instance.ObterTemperaturaPorNomeCidade, cidade.Nome, AppSettingsOpenWeatherMaps.Instance.API_KEY));

            var result = DeserializarObjetoResponse(response).ConverterParaCidade();

            return result;
        }

        public async Task<Cidade> ObterTemperaturaPorLatitudeELongitude(Coordenadas coordenadas)
        {
            var response = await _httpClient.GetAsync(string.Format(AppSettingsOpenWeatherMaps.Instance.ObterTemperaturaPorLatitudeELongitude, coordenadas.Latitude, coordenadas.Longitude, AppSettingsOpenWeatherMaps.Instance.API_KEY));

            var result = DeserializarObjetoResponse(response).ConverterParaCidade();

            return result;
        }

    }
}