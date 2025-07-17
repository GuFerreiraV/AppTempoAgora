using AppTempoAgora.Models;
using AppTempoAgora.Services;
using System.Diagnostics;

namespace AppTempoAgora
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Search(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_city.Text))
                {
                    Weather? weather = await DataServices.GetWeather(txt_city.Text);
                    if (weather != null)
                    {
                        string forecast = "";

                        forecast = $"Cidade: {txt_city.Text}\n" +
                                   $"Temperatura Mínima: {weather.Min_temp}°C\n" +
                                   $"Temperatura Máxima: {weather.Max_temp}°C\n" +
                                   $"Descrição: {weather.Description}\n" +
                                   $"Velocidade do Vento: {weather.speed} m/s\n" +
                                   $"Visibilidade: {weather.Visibility} metros\n" +
                                   $"Nascer do Sol: {weather.Sunrise}\n" +
                                   $"Pôr do Sol: {weather.Sunset}\n" +
                                   $"Latitude: {weather.Lat}\n" +
                                   $"Longitude: {weather.Lon}";

                        lbl_res.Text = forecast;
                        string map = $"https://embed.windy.com/embed.html?" +
                           $"type=map&location=coordinates&metricRain=mm&metricTemp=°C" +
                           $"&metricWind=km/h&zoom=10&overlay=wind&product=ecmwf&level=surface" +
                            $"&lat={weather.Lat.ToString().Replace(",", ".")}&lon={weather.Lon.ToString().Replace(",", ".")}";

                        wv_map.Source = map;
                        Debug.WriteLine(map);
                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void Button_Clicked_Location(object sender, EventArgs e)
        {

            try
            {
                // criando a requisição de geolocalização
                GeolocationRequest geoLocationRequest = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                // Pega a localização apartir do geoLocationRequest
                Location? local = await Geolocation.Default.GetLocationAsync(geoLocationRequest);

                if (local != null)
                {
                    string device_local = $"Latitude: {local.Latitude}\n" +
                        $"Longitude {local.Longitude}";

                    lbl_coords.Text = device_local;

                    // Pega a cidade apartir da latitude e longitude
                    GetCity(local.Latitude, local.Longitude);
                }

            } // criando as exceções
            catch (FeatureNotSupportedException fnsEx) // falta de suporte para permitir localização
            {
                await DisplayAlert("Erro: Dispositivo não suporta", fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fnEx) // localização desabilitada no dispositivo
            {
                await DisplayAlert("Erro: Localização desabilitada", fnEx.Message, "OK");
            }
            catch (PermissionException pEx) // falta de permissão para acessar a localização
            {
                await DisplayAlert("Erro: Permissão de Localização", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void GetCity(double lat, double lon)
        {
            try
            {
                // Retorna marcadores(referencias) a partir da latitude e longitude
                IEnumerable<Placemark> places = await Geocoding.Default.GetPlacemarksAsync(lat, lon);
                Placemark? place = places.FirstOrDefault();

                if (place != null && !string.IsNullOrEmpty(place.Locality))
                {
                    txt_city.Text = place.Locality;
                }
                else
                {
                    await DisplayAlert("Cidade não encontrada", "Não foi possível determinar o nome da cidade para a sua localização.", "OK");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro: Obtenção do nome da cidade", ex.Message, "OK");
            }
        }
    }
}
