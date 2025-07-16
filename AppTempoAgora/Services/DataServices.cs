using AppTempoAgora.Helpers;
using AppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace AppTempoAgora.Services
{
    public class DataServices
    {

        private static string? _apiKey;
        private static string? _baseUrl;

        public static async Task<Weather?> GetWeather(string city)
        {
            Weather? weather = null;
            _apiKey = ConfigurationHelper.GetApiKey();
            _baseUrl = ConfigurationHelper.GetBaseUrl();

            string url = $"{_baseUrl}" + $"weather?q={city}&units=metric&appid={_apiKey}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();
                    var rascunho = JObject.Parse(json);
                    DateTime time = new();
                    DateTime Sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime Sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();
                    weather = new()
                    {
                        Lat = (double)rascunho["coord"]["lat"],
                        Lon = (double)rascunho["coord"]["lon"],
                        Description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        Min_temp = (double)rascunho["main"]["temp_min"],
                        Max_temp = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        Visibility = (int)rascunho["visibility"],
                        Sunrise = Sunrise.ToString(),
                        Sunset = Sunset.ToString()
                    };
                }
            }
            return weather;
        }
    }
}
