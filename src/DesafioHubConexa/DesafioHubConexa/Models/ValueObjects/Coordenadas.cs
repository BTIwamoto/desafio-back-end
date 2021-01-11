using System.Text.RegularExpressions;

namespace DesafioHubConexa.Models.ValueObjects
{
    public class Coordenadas : ValueObjectBase
    {
        public string Latitude { get; private set; }

        public string Longitude { get; private set; }

        public Coordenadas(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;

            Validar();
        }

        private void Validar()
        {
            var reg = new Regex(@"^-?[0-9][0-9\.]+$");

            if (string.IsNullOrEmpty(Latitude))
                AddError("A Latitude deve ser preenchida");
            else if (!reg.Match(Latitude).Success)
                AddError("A Latitude deve ser preenchida apenas com números e pontos");

            if (string.IsNullOrEmpty(Longitude))
                AddError("A Longitude deve ser preenchida");
            else if (!reg.Match(Longitude).Success)
                AddError("A Longitude deve ser preenchida apenas com números e pontos");
        }
    }
}