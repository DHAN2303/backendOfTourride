namespace AllrideApiCore.Dtos.Weather
{
    public class WeatherResponseDto
    {
        public double Temperature { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int  WindSpeed { get; set; }  // Rüzgar hızı
        public int WindDeg { get; set; }    // Rüzgar yönü, dereceler (meteorolojik)
        public int WindGust { get; set; }   // Rüzgar esintileri. Birimler – varsayılan: metre/sn, metrik: metre/sn, İngiliz ölçü birimi: mil/saat.
        public int WeatherId { get; set; }
        public string WeatherParameter { get; set; }
        public string WeatherDescription { get; set; }





    }
}
