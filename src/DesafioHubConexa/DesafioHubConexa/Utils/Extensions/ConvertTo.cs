using DesafioHubConexa.Models;
using DesafioHubConexa.Models.Enums;
using DesafioHubConexa.Models.ValueObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioHubConexa.Utils.Extensions
{
    public static class ConvertTo
    {
        public static Cidade ConverterParaCidade(this Task<JObject> obj)
        {
            if (obj.Result == null)
                return null;

            if (obj.Result["message"] != null)
                throw new Exception($"{obj.Result["cod"]} - {obj.Result["message"]}");

            var latitude = obj.Result["coord"]["lat"].ToString();
            var longitude = obj.Result["coord"]["lon"].ToString();
            var cidadeNome = obj.Result["name"].ToString();
            var temperatura = float.Parse(obj.Result["main"]["temp"].ToString());

            var coordenadas = new Coordenadas(latitude, longitude);

            var result = new Cidade(cidadeNome, temperatura, coordenadas);

            return result;
        }

        public static void ConverterParaSpotifyToken(this Task<JObject> obj)
        {
            if (obj.Result == null)
                return;

            var acessToken = obj.Result["access_token"].ToString();
            var expiresIn = obj.Result["expires_in"].ToString();

            SpotifyToken.Initialize(acessToken, expiresIn);
        }

        public static Playlist ConverterParaPlaylist(this Task<JObject> obj, CategoriaPlaylist categoriaPlaylist)
        {
            if (obj.Result == null)
                return null;

            var resultListaMusicas = obj.Result["tracks"];

            var musicas = resultListaMusicas.Select(resultMusica => new Musica(resultMusica["name"].ToString())).ToList();

            var result = new Playlist(categoriaPlaylist, musicas);

            return result;
        }
    }
}