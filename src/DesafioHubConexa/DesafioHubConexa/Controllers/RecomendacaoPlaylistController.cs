using DesafioHubConexa.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DesafioHubConexa.Controllers
{
    public class RecomendacaoPlaylistController : Controller
    {
        private readonly IRecomendacaoPlaylistService _recomendacaoPlaylistService;

        public RecomendacaoPlaylistController(IRecomendacaoPlaylistService recomendacaoPlaylistService)
        {
            _recomendacaoPlaylistService = recomendacaoPlaylistService;
        }

        /// <summary>
        /// Recomenda uma playlist de acordo com a temperatura da cidade, a partir do nome da cidade
        /// </summary>
        /// <param name="nomeCidade">Ex: Goiânia</param>
        /// <returns>Recomendação da Playlist de acordo com a temperatura da cidade</returns>
        /// <returns>400 -> Retorna o erro que ocorreu no sistema. Ex.: O nome da cidade deve ser preenchido</returns>
        /// <returns>404 -> Cidade não encontrada</returns>
        /// <remarks></remarks>
        [HttpGet]
        [Route("ObterRecomendacaoPlaylistPorCidade/{nomeCidade}")]
        public IActionResult ObterRecomendacaoPlaylistPorCidade(string nomeCidade)
        {
            try
            {
                var playlist = _recomendacaoPlaylistService.ObterRecomendacaoPlaylistPorCidade(nomeCidade);

                if (playlist.Result.Success)
                    return Ok(playlist.Result);
                else
                    return BadRequest(playlist.Result.ValidationResult);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Recomenda uma playlist de acordo com a temperatura da cidade, a partir da latitude e longitude
        /// </summary>
        /// <param name="latitude">Ex.: -16.6799</param>
        /// <param name="longitude">Ex.: -49.255</param>
        /// <returns>Recomendação da Playlist de acordo com a temperatura da cidade</returns>
        [HttpGet]
        [Route("ObterRecomendacaoPlaylistPorLatitudeELongitude/{latitude}&{longitude}")]
        public IActionResult ObterRecomendacaoPlaylistPorLatitudeELongitude(string latitude, string longitude)
        {
            try
            {
                var playlist = _recomendacaoPlaylistService.ObterRecomendacaoPlaylistPorLatitudeELongitude(latitude, longitude);

                if (playlist.Result.Success)
                    return Ok(playlist.Result);
                else
                    return BadRequest(playlist.Result.ValidationResult);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
