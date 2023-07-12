using AllrideApiCore.Dtos;
using AllrideApiService.Services.Abstract;
using AllrideApiService.Services.Abstract.Routes;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Concrete.TomtomApi
{
    public class NearBySearchService : ITomTomNearBySearchService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public NearBySearchService(HttpClient httpClient, IConfiguration config, IMapper mapper)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<TomTomNearbySearchResult> CreateNearBySearchService(Dictionary<string, dynamic> parameters)
        {

            // Get the base URL and append the endpoint path
            var url = _config.GetValue<string>("TomTomApiBaseUrl:NearBySearchUrl");
            var apiKey = _config.GetValue<string>("TomTomApiBaseUrl:ApiKey");

            // Build the URL
            if (parameters.ContainsKey("lat"))
            {
                url += $"lat={parameters["lat"].ToString().ToLower()}";
            }
            if (parameters.ContainsKey("lon"))
            {
                url += $"&lon={parameters["lon"].ToString().ToLower()}";
            }
            if (parameters.ContainsKey("limit"))
            {
                url += $"&limit={parameters["limit"].ToString().ToLower()}";
            }
            if (parameters.ContainsKey("radius"))
            {
                url += $"&radius={parameters["radius"].ToString().ToLower()}";
            }
            if (parameters.ContainsKey("countrySet"))
            {
                url += $"&countrySet={parameters["countrySet"].ToString().ToLower()}";
            }
            if (parameters.ContainsKey("language"))
            {
                url += $"&language={parameters["language"].ToString().ToLower()}";
            }
            if (parameters.ContainsKey("categorySet"))
            {
                url += $"&categorySet={parameters["categorySet"].ToString().ToLower()}";
            }

            if (parameters.ContainsKey("brandSet"))
            {
                url += $"&brandSet={parameters["brandSet"].ToString().ToLower()}";
            }
            url += "&key=" + apiKey;

            // Send the HTTP request
            var client = new HttpClient();
            Uri uri = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(uri);

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<TomTomNearbySearchResult>(result);
                return responseObject;
            }
            else
            {
                var errorMessage = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                return null;
            }
        }
    }
}
