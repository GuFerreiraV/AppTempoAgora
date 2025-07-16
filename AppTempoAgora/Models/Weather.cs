namespace AppTempoAgora.Models
{
    public class Weather
    {
        public double Lon { get; set; } // Longitude 
        public double Lat { get; set; } // Latitude
        public int Visibility { get; set; } // Visibilidade em metros
        public double Min_temp { get; set; } // Temperatura mínima
        public double Max_temp { get; set; } // Temperatura máxima
        public int Sunrise { get; set; } // Hora do nascer do sol (timestamp)
        public int Sunset { get; set; } // Hora do pôr do sol (timestamp)
        public string Description { get; set; } // Descrição
        public string main { get; set; } // resumo das informações do tempo
        public double speed { get; set; } // velocidade do vento
    }
}
