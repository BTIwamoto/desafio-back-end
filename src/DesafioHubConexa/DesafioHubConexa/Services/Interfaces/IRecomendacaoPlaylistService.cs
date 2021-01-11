using DesafioHubConexa.Models;
using System.Threading.Tasks;

namespace DesafioHubConexa.Services.Interfaces
{
    public interface IRecomendacaoPlaylistService
    {
        Task<Playlist> ObterRecomendacaoPlaylistPorCidade(string nomeCidade);

        Task<Playlist> ObterRecomendacaoPlaylistPorLatitudeELongitude(string latitude, string longitude);
    }
}