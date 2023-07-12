using System.Text.Json.Serialization;

namespace AllrideApiCore.Dtos.Weather
{
    //public class WeatherDto
    //{
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //    public Geometry Geoloc { get; set; }
    //[   public double Temperature { get; set; }
    //[    public DateTime Date { get; set; }
    //[    public int Pressure { get; set; }
    //[    public int SeaLevel { get; set; }
    //[    public int GrndLevel { get; set; }
    //[    public int Humidity { get; set; }
    //[    public int StatusId { get; set; }
    //[    public double Predection3h { get; set; }
    //[    public int WindSpeed { get; set; }
    //[    public int WindDeg { get; set; }
    //[    public int WindGust { get; set; }
    //[}

    public class WeatherResultDto
    {
        [JsonPropertyName("cod")]
        public string Cod { get; set; }

        [JsonPropertyName("message")]
        public double Message { get; set; }

        [JsonPropertyName("cnt")]
        public int Count { get; set; }

        [JsonPropertyName("list")]
        public List<WeatherData> WeatherDataList { get; set; }

        [JsonPropertyName("city")]
        public City City { get; set; }
    }

    public class WeatherData
    {
        [JsonPropertyName("dt")]  // Saatlik hava durumunda verinin tahmin edilen zamanı
        public long Dt { get; set; }

        [JsonPropertyName("main")]
        public Main Main { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherInfo> WeatherInfo { get; set; }

        [JsonPropertyName("clouds")]
        public Clouds Clouds { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        [JsonPropertyName("pop")]
        public double Pop { get; set; }

        [JsonPropertyName("rain")]
        public Rain Rain { get; set; }

        [JsonPropertyName("sys")]
        public Sys Sys { get; set; }

        [JsonPropertyName("dt_txt")]
        public string DateTimeText { get; set; }
    }

    public class Clouds
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }


    public class Main
    {
        [JsonPropertyName("temp")]
        public float Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("temp_min")]
        public float MinimumTemperature { get; set; }

        [JsonPropertyName("temp_max")]
        public float MaximumTemperature { get; set; }

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("sea_level")]
        public int SeaLevel { get; set; }

        [JsonPropertyName("grnd_level")]
        public int GroundLevel { get; set; }

        [JsonPropertyName("temp_kf")]
        public double TemperatureKf { get; set; }
    }

    public class WeatherInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Main { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }

    public class Rain
    {
        [JsonPropertyName("3h")]
        public double VolumeLast3Hours { get; set; }
    }

    public class Sys
    {
        [JsonPropertyName("pod")]
        public string Pod { get; set; }
    }


    public class Wind
    {
        [JsonPropertyName("speed")]
        public float Speed { get; set; }

        [JsonPropertyName("deg")]
        public float Deg { get; set; }

        [JsonPropertyName("gust")]
        public float Gust { get; set; }
    }

    public class City
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("coord")]
        public Coord Coord { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("timezone")]
        public long Timezone { get; set; }  // UTC den saniye cinsinden kayma

        [JsonPropertyName("sunrise")]
        public long Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long Sunset { get; set; }

    }

    public class Coord
    {
        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        [JsonPropertyName("lat")]
        public double Lat { get; set; }
    }


}
