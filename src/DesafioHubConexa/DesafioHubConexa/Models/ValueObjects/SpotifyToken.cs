using System;

namespace DesafioHubConexa.Models.ValueObjects
{
    public class SpotifyToken
    {
        public static SpotifyToken Instance { get; private set; }

        public string Bearer { get; set; }

        public DateTime TokenValidation { get; set; }

        public static void Initialize(string bearer, string tempoValidadeToken)
        {
            Instance = new SpotifyToken(bearer, tempoValidadeToken);
        }

        private SpotifyToken(string bearer, string tempoValidadeToken)
        {
            Bearer = bearer;
            TokenValidation = DateTime.Now.AddSeconds(double.Parse(tempoValidadeToken));
        }
    }
}