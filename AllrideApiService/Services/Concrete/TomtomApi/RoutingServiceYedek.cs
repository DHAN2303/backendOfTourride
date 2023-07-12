//using AllrideApiRepository.Repositories.Abstract;
//using AllrideApiService.Abstract;
//using AutoMapper;
//using Microsoft.Extensions.Configuration;
//using RestSharp;

//namespace AllrideApiService.Concrete
//{
//    public class RoutingServiceYedek : IRoutingService
//    {

//        private readonly HttpClient _httpClient; // http isteği yapabilmek için http client nesnesi oluştuduk
//        private readonly IConfiguration _config; // Base url i oluşturabilmek için Configuration sınıfını kullandık
//        private readonly IRoutingRepository _routingRepo;
//        private readonly IMapper _mapper;
//        public RoutingServiceYedek(HttpClient httpClient, IConfiguration config, IRoutingRepository routingRepo, IMapper mapper)
//        {
//            _httpClient = httpClient;
//            _config = config;
//            _routingRepo = routingRepo;
//            _mapper = mapper;

//        }

//        public Task<string> CreateMultiLineGet()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<string> CreateMultiLinePost()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<string> GetAsync(string coors)
//        {
//            string[] points = coors.Split(":");
//            var lat = new List<double>();
//            var lon = new List<double>();
//            for (int i = 0; i < points.Length; i++)
//            {
//                var Latlon = points[i].Split(",");
//                if (Latlon.Length == 2)
//                {
//                    double latitude, longitude;
//                    bool isConverted1 = double.TryParse(Latlon[0], out latitude);
//                    bool isConverted2 = double.TryParse(Latlon[1], out longitude);
//                    if (isConverted1 && isConverted2)
//                    {
//                        lat.Add(latitude);
//                        lon.Add(longitude);
//                    }
//                }
//            }
//            var coorsGecerli = string.Join(":", lat.Zip(lon, (double latitude, double longitute) => $"{latitude},{longitute}"));


//            string apiKey = _config.GetSection("TomTomApiBaseUrl").GetSection("ApiKey").Value;
//            string endpoint = _config.GetSection("TomTomApiBaseUrl").GetSection("RoutingApiBaseUrl").Value.Replace("{coors}", coorsGecerli);
//            endpoint += "/json?key=" + apiKey;

//            // Create a JSON object with the route details
//            string requestBody = "{\"routeRequest\": {\"departAt\": \"2022-12-30T12:00:00Z\",\"travelMode\": \"car\",\"instructionType\": \"text\",\"routeType\": \"fastest\"," +
//               "\"computeBestOrder\": true,\"language\": \"en-US\",\"routeRepresentation\": \"summary\",\"avoid\": [\"unpavedRoads\"]," +
//               "\"legs\": [{\"origin\": {\"lat\": 52.216567,\"lon\": 5.963976},\"destination\": {\"lat\": 52.229672,\"lon\": 4.758964}}]}}";
//            string degisken = "'supportingPoints': [ { 'latitude': 52.5093, 'longitude': 13.42936 },  { 'latitude': 52.50844,'longitude': 13.42859  } ],  'avoidVignette': [    'AUS',    'CHE'  ]}";


//            /*
//             * 
//             {
//  'supportingPoints': [
//    {
//      'latitude': 52.5093,
//      'longitude': 13.42936
//    },
//    {
//      'latitude': 52.50844,
//      'longitude': 13.42859
//    }
//  ],
//  'avoidVignette': [
//    'AUS',
//    'CHE'
//  ]
//}
//             * */
//            var client = new RestClient("https://api.tomtom.com/routing/1/calculateRoute/52.50931,13.42936:52.50274,13.43872/json?instructionsType=text&language=en-US&vehicleHeading=90&sectionType=traffic&report=effectiveSettings&routeType=eco&traffic=true&avoid=unpavedRoads&travelMode=car&vehicleMaxSpeed=120&vehicleCommercial=false&vehicleEngineType=combustion&key=a65t9sOuZGv0O686Y90SfuAExYyczukF");
//            //client.Time = -1;
//            var request = new RestRequest("POST");
//            request.AddHeader("Content-Type", "application/json");
//            request.AddHeader("Accept", "*/*");
//            var body = "{\"supportingPoints\":[{\"latitude\":52.5093,\"longitude\":13.42936},{\"latitude\":52.50844,\"longitude\":13.42859}],\"avoidVignette\":[\"AUS\",\"CHE\"]}";
//            request.AddParameter("application/json", body, ParameterType.RequestBody);
//            RestResponse response = client.Execute(request);
//            Console.WriteLine(response.Content);
//            return "done";
//            // Set the content type to JSON
//            //_httpClient.DefaultRequestHeaders.Accept.Clear();
//            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//            // Send the POST request
//            //    HttpResponseMessage response = await _httpClient.PostAsync(endpoint, new StringContent(requestBody, Encoding.UTF8, "application/json")); //new StringContent(requestBody, Encoding.UTF8, "application/json")

//            //    // Check the status code and handle the response
//            //    if (response.IsSuccessStatusCode)
//            //    {
//            //        string responseBody = await response.Content.ReadAsStringAsync();
//            //        // Parse the response and process the route data
//            //        return responseBody;
//            //    }
//            //    else
//            //    {
//            //        // Handle error
//            //    }

//            //    return response.Content.ReadAsStringAsync().Result;
//            //}

//            //public static RouteCalculateRequestParameter Deserialize(object reqParams )
//            //{
//            //    var ConvertReqParam = JsonConvert.DeserializeObject<RouteCalculateRequestParameter>(reqParams.ToString());
//            //    return ConvertReqParam;
//            //}

//            //public static string ConvertJson(RouteCalculateRequestParameter ConvertReqParam)
//            //{
//            //    var jsonReqParam = JsonConvert.SerializeObject(ConvertReqParam);
//            //    return jsonReqParam.GetType().ToString();
//            //}

//        }

//    }
//}


////Debug.WriteLine("Debug message. responseResult:" + responseResult);
////if (!responseResult.IsSuccessStatusCode)
////{
////    //return null;
////    return false;
////}
////string routeResponseResult = await responseResult.Content.ReadAsStringAsync();
////var parsedData = JsonConvert.DeserializeObject<dynamic>(routeResponseResult);
////List<RoutePoint> points = parsedData.routes[0].legs[0].points.ToObject<List<RoutePoint>>();
////Debug.WriteLine("Debug message. responseResult:" + points);
//////RoutePointDto routePo = _mapper.Map<RoutePointDto>(points);
////RouteCalculateDto routeCalculDto = new()
////{
////    Points = points
////};
////RouteCalculate routeCalculate = _mapper.Map<RouteCalculate>(routeCalculDto);
////if (routeCalculate != null)
////{
////    routeCalculate.Status = true;
////    _routingRepo.Add(routeCalculate);
////}
////return true;