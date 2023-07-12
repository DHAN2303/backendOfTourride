using AllrideApiCore.Dtos.Here;
using AllrideApiService.Services.Abstract.Routes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace AllrideApiService.Services.Concrete.HereApi
{
    public class HereNearBySearchService : IHereNearBySearchService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILogger<HereNearBySearchService> _logger;
        public HereNearBySearchService(HttpClient httpClient, IConfiguration config, ILogger<HereNearBySearchService> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
        }
        public async Task<HereNearByRootobject> CreateHereNearBySearchService(Dictionary<string, dynamic> hereNearBySearchParam)
        {
            // Get the base URL and append the endpoint path
            var url = _config.GetValue<string>("Here:HereNearByService");
            var apiKey = _config.GetValue<string>("Here:apikey");

            // Build the URL
            if (hereNearBySearchParam.ContainsKey("lat"))
            {
                url += $"at={hereNearBySearchParam["lat"].ToString().ToLower()}";
            }
            if (hereNearBySearchParam.ContainsKey("lon"))
            {
                url += $",{hereNearBySearchParam["lon"].ToString().ToLower()}";
            }
            if (hereNearBySearchParam.ContainsKey("limit"))
            {
                url += $"&size={hereNearBySearchParam["limit"].ToString().ToLower()}";
            }
            if (hereNearBySearchParam.ContainsKey("radius"))
            {
                url += $"&radius={hereNearBySearchParam["radius"].ToString().ToLower()}";
            }
            if (hereNearBySearchParam.ContainsKey("categorySet"))
            {
                url += $"&q={hereNearBySearchParam["categorySet"].ToString().ToLower()}";
            }
            if (hereNearBySearchParam.ContainsKey("brandSet"))
            {
                url += $"&q={hereNearBySearchParam["brandSet"].ToString().ToLower()}";
            }
            url += "&apiKey=" + apiKey;


            // Send the HTTP request
            var client = new HttpClient();
            Uri uri = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(uri);

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<HereNearByRootobject>(result);
                return responseObject;
            }
            else
            {
               _logger.LogError($"Error: {response.StatusCode} - {response.ReasonPhrase}");
               return null;
            }
        }
    }
}
