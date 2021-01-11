using DesafioHubConexa.Controllers;
using DesafioHubConexa.Models;
using DesafioHubConexa.Models.Enums;
using DesafioHubConexa.Models.ValueObjects;
using DesafioHubConexa.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using Newtonsoft.Json;
using Xunit;

namespace DesafioHubConexaTest.TestsController
{
    public class RecomendacaoPlaylistController_Test
    {
        private RecomendacaoPlaylistController _recomendacaoPlaylistController { get; set; }

        private List<Musica> musicas { get; set; }

        public RecomendacaoPlaylistController_Test()
        {
            musicas = new List<Musica>
            {
                new Musica("Radio/Video"),
                new Musica("Toxicity"),
                new Musica("Lost In Hollywood"),
                new Musica("Lonely Day"),
                new Musica("B.Y.O.B"),
                new Musica("Alive"),
                new Musica("Just Breathe"),
                new Musica("Yellow Ledbetter"),
                new Musica("Rockstar"),
            };

            var coordenadas = new Coordenadas("-16.6799", "-49.255");
            var cidade = new Cidade("Goiania", 14, coordenadas);
            var playlist = new Playlist(CategoriaPlaylist.rock, musicas);

            var moqRecomendacaoPlaylistService = new Mock<IRecomendacaoPlaylistService>();
            moqRecomendacaoPlaylistService.Setup(x => x.ObterRecomendacaoPlaylistPorCidade(cidade.Nome)).ReturnsAsync(playlist);
            moqRecomendacaoPlaylistService.Setup(x =>
                    x.ObterRecomendacaoPlaylistPorLatitudeELongitude(coordenadas.Latitude, coordenadas.Longitude))
                .ReturnsAsync(playlist);

            _recomendacaoPlaylistController = new RecomendacaoPlaylistController(moqRecomendacaoPlaylistService.Object);
        }

        [Fact(DisplayName = "Obter recomendação de Playlist pela temperatura de uma cidade pesquisando pelo nome da cidade")]
        public void Test_ObterRecomendacaoPlaylistPorCidade()
        {
            var json = _recomendacaoPlaylistController.ObterRecomendacaoPlaylistPorCidade("Goiania");
            var result = json as OkObjectResult;
            var playlist = (Playlist) result.Value;

            Assert.NotNull(playlist);
            Assert.Equal("rock", playlist.Categoria);
            Assert.Equal(true, playlist.Success);
            Assert.Same(musicas, playlist.Musicas);
            Assert.Equal(false, playlist.Musicas.Exists(x => x.Nome == "Even Flow"));
        }

        [Fact(DisplayName = "Obter recomendação de Playlist pela temperatura de uma cidade pesquisando por Latitude e Longitude")]
        public void Test_ObterRecomendacaoPlaylistPorLatitudeELongitude()
        {
            var json = _recomendacaoPlaylistController.ObterRecomendacaoPlaylistPorLatitudeELongitude("-16.6799", "-49.255");
            var result = json as OkObjectResult;
            var playlist = (Playlist)result.Value;

            Assert.NotNull(playlist.Musicas.Count);
            Assert.Equal("rock", playlist.Categoria);
            Assert.Equal(true, playlist.Success);
            Assert.Same(musicas, playlist.Musicas);
            Assert.Equal(false, playlist.Musicas.Exists(x => x.Nome == "Even Flow"));
        }

        [Fact(DisplayName = "Obter recomendação de Playlist pela temperatura de uma cidade pesquisando pelo nome da cidade - FAILED")]
        public void Test_ObterRecomendacaoPlaylistPorCidade_Failed()
        {
            var json = _recomendacaoPlaylistController.ObterRecomendacaoPlaylistPorCidade("São Paulo");
            var result = json as OkObjectResult;

            Assert.Null(result);
        }

        [Fact(DisplayName = "Obter recomendação de Playlist pela temperatura de uma cidade pesquisando por Latitude e Longitude - FAILED")]
        public void Test_ObterRecomendacaoPlaylistPorLatitudeELongitude_Failed()
        {
            var json = _recomendacaoPlaylistController.ObterRecomendacaoPlaylistPorLatitudeELongitude("0.18234", "-0.000");
            var result = json as OkObjectResult;

            Assert.Null(result);
        }
    }
}