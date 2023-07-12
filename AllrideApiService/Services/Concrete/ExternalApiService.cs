using AllrideApiService.Services.Abstract;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace AllrideApiService.Services.Concrete
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ExternalApiService> _logger;
        public ExternalApiService(HttpClient httpClient, ILogger<ExternalApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<string> Get(string key)
        {
            string apiUrl = $"?key={key}";
            try
            {
               var response = await _httpClient.GetAsync(apiUrl); // Program.cs e eklediğim urle Controllerdan gelen parametreyi ekleyen ve yeni bir http isteği oluşturur.
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Weather Servisi Log Error: " + ex.Message, ex);
                return null;
            }
            
        }

        public async Task Post(string url, string parameter)
        {
            var paramaterJson = new StringContent(
                    JsonSerializer.Serialize(parameter),
                    Encoding.UTF8,
                    Application.Json); // using static System.Net.Mime.MediaTypeNames;
          //  string apiUrl = $"?key={url}";
            var response = await _httpClient.PostAsync(url, paramaterJson);
            response.EnsureSuccessStatusCode(); ;
        }

    }
}
