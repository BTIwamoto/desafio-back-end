using DesafioHubConexa.Models.Enums;
using DesafioHubConexa.Models.ValueObjects;
using DesafioHubConexa.Utils.ValidationError;
using Microsoft.OpenApi.Extensions;
using System.Collections.Generic;

namespace DesafioHubConexa.Models
{
    public class Playlist : ValidationBase
    {
        public string Categoria { get; }

        public List<Musica> Musicas { get; }

        public Playlist()
        { }

        public Playlist(CategoriaPlaylist categoria, List<Musica> musicas)
        {
            Categoria = categoria.GetDisplayName();
            Musicas = musicas;

            Success = true;
        }
    }
}