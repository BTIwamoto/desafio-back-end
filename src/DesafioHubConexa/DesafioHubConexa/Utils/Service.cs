using DesafioHubConexa.Utils.Extensions;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesafioHubConexa.Utils
{
    public abstract class Service
    {
        protected async Task<JObject> DeserializarObjetoResponse(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JObject.Parse(await responseMessage.Content.ReadAsStringAsync());

            return result;
        }

        protected bool TratarErrosResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}