using AppTempoAgora.Models;
using AppTempoAgora.Services;

namespace AppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
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
                                   $"Pôr do Sol: {weather.Sunset}";

                        lbl_res.Text = forecast;
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
    }

}
