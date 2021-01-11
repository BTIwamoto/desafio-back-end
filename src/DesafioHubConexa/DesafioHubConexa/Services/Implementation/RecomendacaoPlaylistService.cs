using DesafioHubConexa.Models;
using DesafioHubConexa.Models.ValueObjects;
using DesafioHubConexa.Providers;
using DesafioHubConexa.Services.Interfaces;
using DesafioHubConexa.Utils.ValidationError;
using System.Threading.Tasks;

namespace DesafioHubConexa.Services.Implementation
{
    public class RecomendacaoPlaylistService : IRecomendacaoPlaylistService
    {
        public async Task<Playlist> ObterRecomendacaoPlaylistPorCidade(string nomeCidade)
        {
            var cidade = new Cidade(nomeCidade);

            if (!cidade.IsValid())
                return ValidationBase.TratarMensagemErro<Playlist>(cidade.MensagensErro);

            cidade = await new OpenWeatherMapsProvider().ObterTemperaturaPorNomeCidade(cidade);

            var playlist = await new SpotifyProvider().ObterRecomendacaoPlaylistPorCategoria(cidade);

            return playlist;
        }

        public async Task<Playlist> ObterRecomendacaoPlaylistPorLatitudeELongitude(string latitude, string longitude)
        {
            var coordenadas = new Coordenadas(latitude, longitude);

            if (!coordenadas.IsValid())
                return ValidationBase.TratarMensagemErro<Playlist>(coordenadas.MensagensErro);

            var cidade = await new OpenWeatherMapsProvider().ObterTemperaturaPorLatitudeELongitude(coordenadas);

            if (!cidade.IsValid())
                return ValidationBase.TratarMensagemErro<Playlist>(cidade.MensagensErro);

            var playlist = await new SpotifyProvider().ObterRecomendacaoPlaylistPorCategoria(cidade);

            return playlist;
        }
    }
}