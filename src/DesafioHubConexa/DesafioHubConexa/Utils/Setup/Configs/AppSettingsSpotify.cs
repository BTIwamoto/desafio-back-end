namespace DesafioHubConexa.Utils.Setup.Configs
{
    public class AppSettingsSpotify
    {
        public static AppSettingsSpotify Instance { get; private set; }

        public string BaseURL { get; set; }

        public string BaseAutenticacaoURL { get; set; }

        public string ObterRecomendacaoPlaylistPorCategoria { get; set; }

        public string BasicToken { get; set; }

        public static void Initialize(AppSettingsSpotify appSettingsSpotify)
        {
            Instance = appSettingsSpotify;
        }
    }
}