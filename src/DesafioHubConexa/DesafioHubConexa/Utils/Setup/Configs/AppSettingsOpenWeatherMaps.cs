namespace DesafioHubConexa.Utils.Setup.Configs
{
    public class AppSettingsOpenWeatherMaps
    {
        public static AppSettingsOpenWeatherMaps Instance { get; private set; }

        public string BaseURL { get; set; }

        public string ObterTemperaturaPorNomeCidade { get; set; }

        public string ObterTemperaturaPorLatitudeELongitude { get; set; }

        public string API_KEY { get; set; }

        public static void Initialize(AppSettingsOpenWeatherMaps appSettingsOpenWeatherMaps)
        {
            Instance = appSettingsOpenWeatherMaps;
        }
    }
}