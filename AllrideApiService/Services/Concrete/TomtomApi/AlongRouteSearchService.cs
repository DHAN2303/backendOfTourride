using AllrideApiCore.Dtos.TomTom;
using AllrideApiCore.Entities.TomTom;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace AllrideApiService.Services.Concrete.TomtomApi
{
    public class AlongRouteSearchService : IAlongRouteSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILogger<AlongRouteSearchService> _logger;
        public AlongRouteSearchService(ILogger<AlongRouteSearchService> logger, HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
        }

        public async Task<AlongRoot> CreateAlongRouteSearchService(string Latlong, Dictionary<string, dynamic> requestHeader)
        {
            var client = new HttpClient();
            var requestBody = convertToJsonFormat(Latlong);

            var alongRouteContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
            string url = CreateUrl(requestHeader);
            var response = await client.PostAsync(url, alongRouteContent);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var resultResponse = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(resultResponse))
            {
                return null;
            }

            var responseObject = JsonConvert.DeserializeObject<AlongRoot>(resultResponse);
            return responseObject;
        }

        ///////////////////////////////////////////////////////////   
        //create url for along api
        //////////////////////////////////////////////////////////
        private string CreateUrl(Dictionary<string, dynamic> parameters)
        {

            // Get the base URL and append the endpoint path
            var url = _config.GetValue<string>("TomTomApiBaseUrl:AlongRouteSearchUrl");
            var apiKey = _config.GetValue<string>("TomTomApiBaseUrl:ApiKey");
            // Add optional parameters if they exist
            if (parameters.ContainsKey("categorySet"))
            {
                string categorySet = parameters["categorySet"].GetType().IsArray ?
                                     string.Join(",", parameters["categorySet"]) :
                                     parameters["categorySet"].ToString();
                url += $"{categorySet}.json?maxDetourTime={parameters["maxDetourTime"]}";

            }

            if (parameters.ContainsKey("typehead"))
            {
                url += $"&typehead={parameters["typehead"].ToString().ToLower()}";
            }

            if (parameters.ContainsKey("limit"))
            {
                int limit = int.Parse(parameters["limit"].ToString());
                url += $"&limit={limit}";
            }
            else
            {
                url += $"&limit={_config.GetValue<string>("TomTomApiBaseUrl:alongLimit")}";
            }
            if (parameters.ContainsKey("brandSet"))
            {
                url += $"&brandSet={parameters["brandSet"]}";
            }

            if (parameters.ContainsKey("avoid"))
            {
                url += $"&avoid={parameters["avoid"]}";
            }

            if (parameters.ContainsKey("hilliness"))
            {
                url += $"&hilliness={parameters["hilliness"]}";
            }

            if (parameters.ContainsKey("windingness"))
            {
                url += $"&windingness={parameters["windingness"]}";
            }

            if (parameters.ContainsKey("language"))
            {
                url += $"&language={parameters["language"]}";
            }
            url += "&key=" + apiKey;

            return url;
        }
        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////   
        //string format convert to json format for tomtom along api
        //////////////////////////////////////////////////////////
        private string convertToJsonFormat(string latlon)
        {

            string[] points = latlon.Split(':');

            List<AlongPoint> routePoints = new List<AlongPoint>();

            foreach (string point in points)
            {
                string[] latLongValues = point.Split(',');

                if (latLongValues!=null && latLongValues.Length >1)
                {
                    double lat = getStringToDouble(latLongValues[0]);//try catch kontrolü koyulmalı
                    double lon = getStringToDouble(latLongValues[1]);
                    routePoints.Add(new AlongPoint { lat = lat, lon = lon });
                }
               
            }

            // Add the first point again as the last point to create a closed loop
            AlongPoint firstPoint = routePoints[0];
            routePoints.Add(firstPoint);

            AlongRoute route = new AlongRoute { points = routePoints };

            string json = JsonConvert.SerializeObject(new { route }, Formatting.Indented);

            return json;
        }

        private Double getStringToDouble(String value)
        {
            double tmpValue = 0.0;
            try
            {
                value = value.Trim();
                tmpValue = Convert.ToDouble(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return tmpValue;
        }
        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////
    }

   
}
