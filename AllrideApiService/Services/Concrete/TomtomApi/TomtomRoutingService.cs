using AllrideApiCore.Dtos.RoutesDtos;
using AllrideApiCore.Entities.Routes;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.TomtomApi;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;

namespace AllrideApiService.Services.Concrete.TomtomApi
{
    public class TomtomRoutingService : ITomtomRoutingService
    {

        private readonly IConfiguration _config; // Base url i oluşturabilmek için Configuration sınıfını kullandık
        private readonly IRoutingRepository _routingRepo;
        public TomtomRoutingService(IConfiguration config, IRoutingRepository routingRepo)
        {
            _config = config;
            _routingRepo = routingRepo;
        }
        public async Task<RouteResponse> CreateRouteCalculatePost(Dictionary<string, string> parameters)
        {
            using var client = new HttpClient();
            // Gelen pa al
            // noktalardan ve virgüllerden ayır
            // Dictionary dizisine al 
            // double a dönüştürmeye gerek var mı   b ilmiyorum
            // önce double a dönüştürüp sonra tekrar stringe dönüştürerek body de gönderebilirim
            string latLongStr = parameters["Latlong"];
            char[] separators = { ',', ':' };
            string[] latLongArray = latLongStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var requestBody = @"{
                          'supportingPoints': [
                            {
                              'latitude': 52.5093,
                              'longitude': 13.42936
                            },
                            {
                              'latitude': 52.50844,
                              'longitude': 13.42859
                            },
                            {
                              'latitude': 52.50944,
                              'longitude': 13.42659
                            }
                          ]
                          'avoidVignette': [
                            ""AUS"",
                            ""CHE""
                          ]
                        }";

            var content = new StringContent(requestBody, Encoding.UTF8, "application/json"); //post metodu için request body oluşturuldu

            var url = "https://api.tomtom.com/routing/1/calculateRoute/52.50931,13.42936:52.50274,13.43872/json?key=a65t9sOuZGv0O686Y90SfuAExYyczukF";

            // var url = CreateUrl(Latlong, parameters);

            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
                return new RouteFailedResponse(1);

            var resultResponse = await response.Content.ReadFromJsonAsync<RootDto>();

            List<List<RoutesEntities>> pointLists = new List<List<RoutesEntities>>();
            GetPointsFromResponse(resultResponse, out pointLists);
            if (pointLists.Count < 0)
                return new RouteFailedResponse(2);

            RouteCalculate routeCalculate = _routingRepo.Add(pointLists);
            _routingRepo.SaveChanges();
            return new RouteSuccessResponse(routeCalculate.Id);
        }

        public async Task<RouteResponse> CreateRouteCalculateGet(string Latlong, Dictionary<string, string> parameters)
        {
            using var client = new HttpClient();  // client isteği atmak için nesne oluştu

            string url = CreateUrl(Latlong, parameters);
            Debug.WriteLine($"TomTom Route Url: {url}");
            var response = await client.GetAsync(url);

            Debug.WriteLine("Debug message. responseResult:" + response);

            if (!response.IsSuccessStatusCode)
                return new RouteFailedResponse(1);

            List<List<RoutesEntities>> pointLists;
            try
            {
                var resultResponse = await response.Content.ReadFromJsonAsync<RootDto>();
                GetPointsFromResponse(resultResponse, out pointLists);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new RouteFailedResponse(3);
            }

            if (pointLists.Count < 0)
                return new RouteFailedResponse(2);

            RouteCalculate routeCalculate = _routingRepo.Add(pointLists);
            _routingRepo.SaveChanges();
            return new RouteSuccessResponse(routeCalculate.Id);
        }

        private static void GetPointsFromResponse(RootDto resultResponse, out List<List<RoutesEntities>> list)
        {
            list = new List<List<RoutesEntities>>();
            foreach (var routes in resultResponse.routes)
            {
                foreach (var legs in routes.legs)
                {
                    List<RoutesEntities> legPoints = new List<RoutesEntities>();
                    foreach (var point in legs.points)
                    {
                        legPoints.Add(new()
                        {
                            longitude = point.longitude,
                            latitude = point.latitude,
                        });
                    }
                    list.Add(legPoints);
                }
            }

        }

        private string CreateUrl(string Latlong, Dictionary<string, string> parameters)
        {
            var url = _config.GetSection("TomTomApiBaseUrl").GetSection("RoutingApiBaseUrl").Value.Replace("{coors}", Latlong) + "/json?";

            string[] boolStr = { "false", "true" };
            string[] level = { "low", "normal", "high" };
            int index;
            foreach (string key in parameters.Keys)
            {
                string value = parameters[key].Trim();
#nullable enable
                string? acceptedValue = null;

                switch (key)
                {
                    case "travelMode":
                        string[] travelModes = { "truck", "taxi", "bus", "van", "motorcycle", "bicycle", "pedestrian" };
                        index = Array.FindIndex(travelModes, x => x.Equals(value, StringComparison.OrdinalIgnoreCase));
                        if (index != -1) acceptedValue = travelModes[index];
                        break;

                    case "traffic":
                        index = Array.FindIndex(boolStr, x => x.Equals(value, StringComparison.OrdinalIgnoreCase));
                        if (index != -1) acceptedValue = boolStr[index];
                        break;

                    case "routeType":
                        string[] routeTypes = { "fastest", "shortest", "short", "eco", "thrilling" };
                        index = Array.FindIndex(routeTypes, x => x.Equals(value, StringComparison.OrdinalIgnoreCase));
                        if (index != -1) acceptedValue = routeTypes[index];
                        break;

                    case "avoid":
                        string[] avoids = { "tollRoads", "motorways", "ferries", "unpavedRoads", "carpools", "alreadyUsedRoads", "borderCrossings", "tunnels", "carTrains" };

                        // value= "tollRoads, carpools,ferries" => ["tolslRoads", " carpools", "ferries"]
                        List<string> values = value.Split(",").Select(val => val.Trim()).ToList();
                        for (int i = 0; i < values.Count; i++)
                        {
                            index = Array.FindIndex(avoids, x => x.Equals(values[i], StringComparison.OrdinalIgnoreCase));
                            if (index == -1)
                            {
                                values.RemoveAt(i);
                                --i;
                                continue;
                            }

                            values[i] = avoids[index];
                        }

                        if (values.Count > 0)
                        {
                            // avoid=motorways&avoid=ferries
                            acceptedValue = string.Join("&", values.Select(val => $"avoid={val}"));
                        }
                        break;

                    case "hilliness":
                        index = Array.FindIndex(level, x => x.Equals(value, StringComparison.OrdinalIgnoreCase));
                        if (index != -1) acceptedValue = level[index];
                        break;

                    case "windingness":
                        index = Array.FindIndex(level, x => x.Equals(value, StringComparison.OrdinalIgnoreCase));
                        if (index != -1) acceptedValue = level[index];
                        break;

                    case "language":
                        int lenOfVal = value.Length;
                        if (lenOfVal >= 2)
                        {
                            acceptedValue = value;
                        }

                        break;

                }

                if (acceptedValue != null)
                    parameters[key] = acceptedValue;
                else
                {
                    parameters.Remove(key);
                    Debug.WriteLine($"Uygun olmayan bir değer 'travelMode' için alındı: '{value}'");
                }
            }

            List<string> items = parameters.Select(kvp => $"{kvp.Key}={kvp.Value}").ToList();
            if (items.Count > 0)
            {
                string filterRouteUrl = string.Join("&", items);
                url += filterRouteUrl;
            }

            url += "&instructionsType=text&routeRepresentation=polyline&key=" + _config.GetSection("TomTomApiBaseUrl").GetSection("ApiKey").Value;

            return url;
        }


    }
}

//string latLongStr = parameters["Latlong"];
//char[] separators = { ',', ':' };
//string[] latLongArray = latLongStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);
// var latLongArr = new Dictionary<string, double>();
// Dictionary<string, double> dictionary = LatLong.ToDictionary(k => k, v => double.Parse(v));

//Dictionary<string, double> dictionary = latLongArray.ToDictionary(kvp => kvp + "latiude", kvp => double.Parse(kvp));
//foreach (var arr in dictionary)
//{
//    latLongArr[kvp.Key] = kvp.Value;
//}



//var requestBody = @"{
//          ""supportingPoints"": [
//            {
//              ""latitude"": 52.5093,
//              ""longitude"": 13.42936
//            },
//            {
//              ""latitude"": 52.50844,
//              ""longitude"": 13.42859
//            },

//            {
//              ""latitude"": 52.50844,
//              ""longitude"": 13.42859
//            }
//          ],
//          ""avoidVignette"": [
//            ""AUS"",
//            ""CHE""
//          ],
//          ""avoidAreas"": {
//            ""rectangles"": [
//              {
//                ""southWestCorner"": {
//                  ""latitude"": 48.81851,
//                  ""longitude"": 2.26593
//                },
//                ""northEastCorner"": {
//                  ""latitude"": 48.90309,
//                  ""longitude"": 2.41115
//                }
//              }
//            ]
//          }
//        }";


