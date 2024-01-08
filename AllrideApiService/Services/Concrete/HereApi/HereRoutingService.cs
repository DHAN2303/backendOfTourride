using AllrideApiCore.Dtos.Here;
using AllrideApiCore.Entities.Here;
using AllrideApiRepository.Repositories.Abstract;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.HereApi;
using HERE.FlexiblePolyline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.Json;
using Point = NetTopologySuite.Geometries.Point;
//using Point = NetTopologySuite.Geometries.Point;

namespace AllrideApiService.Services.Concrete.HereApi
{
    public class HereRoutingService : IHereRoutingService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<HereRoutingService> _logger;
        private readonly IHereRoutingRepository _hereRepository;
        private readonly IRouteTransportModeRepository _routeTransportModeRepository;
        private readonly IRouteInstructionDetailRepository _routeInstructionDetailRepository;

        public HereRoutingService(IConfiguration config,
            HttpClient httpClient, ILogger<HereRoutingService> logger,
            IHereRoutingRepository hereRepository,
            IRouteTransportModeRepository routeTransportModeRepository, IRouteInstructionDetailRepository routeInstructionDetailRepository)
        {
            _config = config;
            _httpClient = httpClient;
            _logger = logger;
            _hereRepository = hereRepository;
            _routeTransportModeRepository = routeTransportModeRepository;
            _routeInstructionDetailRepository = routeInstructionDetailRepository;
        }

        public async Task<CustomResponse<HereUIResultResponseDto>> SendRequestHere(Dictionary<string, string> param)
        {
            // Url i oluştur
           
            var newRoute = new Route();
            var routeInstructionDetail = new RouteInstructionDetail();
            List<List<LatLngZ>> polyLineGeoloc = new List<List<LatLngZ>>();
            List<double> viaList = new();
            List<ErrorEnumResponse> ErrorEnums = new();
            try
            {
                if (param.TryGetValue("public", out string pub))
                    newRoute.Public = pub;
                if (param.TryGetValue("via", out string vias))
                {
                    string[] viaLatLong = vias.Split(',');
                    if (viaLatLong.Length < 2)
                    {
                        viaList = null;
                    }
                    foreach (var l in viaLatLong)
                    {
                        bool response1 = double.TryParse(l, out var result);
                        if (response1)
                        {
                            viaList.Add(double.Parse(l));
                        }
                        else
                        {
                            viaList = null;
                        }
                    };
                }

                var hereRouteUrl = CreateUrl(param);

                if (hereRouteUrl.IsNullOrEmpty())
                {
                    ErrorEnums.Add(ErrorEnumResponse.CouldNotCreateRouteUrl);
                    return CustomResponse<HereUIResultResponseDto>.Fail(ErrorEnums, false);

                }

                var response = await _httpClient.GetAsync(hereRouteUrl);

                if (!response.IsSuccessStatusCode)
                    return CustomResponse<HereUIResultResponseDto>.Fail("Başarısız");

                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                string responseJson = await response.Content.ReadAsStringAsync();
                Root resultResponseRootDto = new();
                resultResponseRootDto = JsonSerializer.Deserialize<Root>(responseJson, jsonOptions);
                if (resultResponseRootDto == null)
                {
                    // Eğer Json değeri Deserialize olamamışsa burada bir hata döndürmem gerekiyor
                    ErrorEnums.Add(ErrorEnumResponse.NoResponseReturnedFromHereApi);
                    CustomResponse<HereUIResultResponseDto>.Fail(ErrorEnums, false);
                }

                foreach (RouteDto route in resultResponseRootDto.routes) // Bir tane route nesnesi geldi
                {
                    int i = 0;
                    int j = 0;
                    foreach (Section section in route.sections)
                    {
                        routeInstructionDetail.Language = section.language;
                        if (i == 0)
                        {
                            int resize = section.actions.Count * route.sections.Count;
                            routeInstructionDetail.ResizeArrays(resize);
                        }
                        foreach (Action action in section.actions)
                        {
                            if (double.TryParse(action.duration.ToString(), out double durationDouble))
                            {
                                newRoute.Duration += durationDouble;
                            }
                            else
                                newRoute.Duration = 0;
                            if (double.TryParse(action.length.ToString(), out double lengthDouble))
                            {

                                newRoute.Length += lengthDouble;
                            }
                            else
                                newRoute.Length = 0;

                            routeInstructionDetail.Duration[j] = action.duration;
                            routeInstructionDetail.Direction[j] = action.direction != null ? action.direction : "ReturnedNull";
                            routeInstructionDetail.Leng[j] = action.length;
                            routeInstructionDetail.Instruction[j] = action.instruction != null ? action.instruction : "ReturnedNull";
                            routeInstructionDetail.Action[j] = action.action != null ? action.action : "ReturnedNull";
                            routeInstructionDetail.Offset[j] = action.offset;
                            j++;
                        }
                        i++;
                        polyLineGeoloc.Add(PolylineEncoderDecoder.Decode(section.polyline));
                    }
                }
                SaveParametersFromHereToTable(param, newRoute, routeInstructionDetail, polyLineGeoloc, viaList);

            }
            catch (Exception ex)
            {
                _logger.LogError("Here Rotalama Servisi Log Error: " + ex.Message, ex);
            }
            HereUIResultResponseDto uıResponseDto = new()
            {
                polyline = polyLineGeoloc,
                duration = routeInstructionDetail.Duration,
                length = routeInstructionDetail.Leng
            };
            return CustomResponse<HereUIResultResponseDto>.Success(uıResponseDto, true);
        }

        private string CreateUrl(Dictionary<string, string> parameters)
        {
            if (parameters.Keys.Contains("return") == false)
                parameters.Add("return", "summary,polyline,elevation,actions,instructions,turnByTurnActions,summary,travelSummary,incidents");
            //parameters.Add("return", "polyline,summary,actions,instructions,travelSummary,turnByTurnActions,mlDuration,typicalDuration,elevation,routeHandle,passthrough,incidents,routingZones,truckRoadTypes,tolls,routeLabels");

            var url = _config.GetSection("Here").GetSection("HereRoutingApiBaseUrl").Value;
            url += "/routes?";
            // Gelen parametreleri kontrol edeceğim
            int index;
            try
            {
                if (parameters.Keys.Contains("transportMode") && parameters.Keys.Contains("origin") && parameters.Keys.Contains("origin"))
                {
                    foreach (string key in parameters.Keys)
                    {
                        string value = parameters[key].Trim();
                        string acceptedValue = null;

                        switch (key)
                        {
                            case "transportMode": // required
                                string[] transportMode = { "car", "truck", "pedestrian", "bicycle", "scooter", "taxi", "bus", "privateBus" };
                                index = Array.FindIndex(transportMode, x => x.Equals(value, StringComparison.OrdinalIgnoreCase));
                                if (index != -1) acceptedValue = transportMode[index];
                                break;

                            case "origin": // required
                                string[] latLong = value.Split(',');
                                if (latLong.Length < 2)
                                {
                                    acceptedValue = "0,0";
                                }
                                int count = 0;
                                foreach (var l in latLong)
                                {
                                    bool response = double.TryParse(l, out var result);
                                    count = response == false ? count++ : count;
                                }
                                acceptedValue = count == 0 ? value : "0,0";
                                break;

                            case "destination": // required

                                string[] latLon = value.Split(',');
                                if (latLon.Length < 2)
                                {
                                    acceptedValue = "0,0";
                                }
                                int _count = 0;
                                foreach (var l in latLon)
                                {
                                    bool response = double.TryParse(l, out var result);
                                    count = response == false ? _count++ : _count;
                                }
                                acceptedValue = _count == 0 ? value : "0,0";
                                break;

                            case "via":
                                string[] viaLatLong = value.Split(',');
                                if (viaLatLong.Length < 2)
                                {
                                    acceptedValue = "0,0";
                                }
                                int countVia = 0;

                                for (int i = 0; i < viaLatLong.Length; i++)
                                {
                                    bool response = double.TryParse(viaLatLong[i], out var result);
                                    count = response == false ? countVia++ : countVia;
                                    if (i % 2 != 0 && i != 0 && i != viaLatLong.Length - 1)
                                    {
                                        viaLatLong[i] += "&via=";
                                    }
                                    else if (i != viaLatLong.Length - 1)
                                    {
                                        viaLatLong[i] += ",";
                                    }
                                    else
                                    {
                                        continue;
                                    }

                                }

                                acceptedValue = countVia == 0 ? string.Join("", viaLatLong) : "0,0";
                                //string.Join("",viaLatLong)
                                //foreach (var l in viaLatLong)
                                //{
                                //    bool response = double.TryParse(l, out var result);
                                //    count = response == false ? countVia++ : countVia;
                                //}
                                break;
                            case "routingMode":
                                acceptedValue = parameters["routingMode"] == "fast" ? "fast" : "short";
                                break;

                            case "passthrough":
                                acceptedValue = parameters["passthrough"] == "true" ? "!passthrough=true" : "!passthrough=false";
                                break;
                            case "departureTime":
                                if (value!= null && value is DateTime)
                                {
                                    acceptedValue = value.ToString();
                                }
                                break;
                            case "arrivalTime":
                                if (value != null && value is DateTime)
                                {
                                    acceptedValue = value.ToString();
                                }
                                break;
                            case "alternatives":
                                acceptedValue = Convert.ToInt16(value) <= 1 || Convert.ToInt16(value) >= 6 ? null : value.ToString();
                                break;
                            case "return":
                                acceptedValue = value.ToString();
                                break;
                        }

                        if (acceptedValue != null)
                            parameters[key] = acceptedValue;
                        else
                        {
                            parameters.Remove(key);
                            _logger.LogError($"Uygun olmayan bir değer 'transportMode' için alındı: '{value}'");
                        }
                    }
                }
                List<string> items = parameters.Select(kvp => $"{kvp.Key}={kvp.Value}").ToList();

                if (items.Count > 0)
                {
                    string filterRouteUrl = string.Join("&", items);
                    url += filterRouteUrl;
                }

                url += "&apiKey=" + _config.GetSection("Here").GetSection("apikey").Value;
            }
            catch (Exception ex)
            {
                _logger.LogError("Here API URL oluşturma: " + ex.Message);
            }

            return url;
        }

        public void SaveParametersFromHereToTable(Dictionary<string, string> param,
            Route newRoute, RouteInstructionDetail routeInstructionDetail,
            List<List<LatLngZ>> polyLine, List<double> viaList = null)
        {
            try
            {
                double org = 0;
                double dest = 0;
                string newDest = string.Empty;

                if (param.TryGetValue("origin", out string origin) &&
                    param.TryGetValue("destination", out string destination) &&
                    param.TryGetValue("transportMode", out string transportMode))
                {
                    string[] originArr = origin.Split(",");
                    string[] destinationArr = destination.Split(",");
                    double[] originPoint = new double[originArr.Length];
                    double[] destinationPoint = new double[2 * destinationArr.Length];

                    for (int i = 0; i < originArr.Length; i++)
                    {
                        if (double.TryParse(originArr[i], out org))
                        {
                            originPoint[i] = org;
                        }
                        if (i == originArr.Length - 1)
                            break;
                        newRoute.OriginPoint = new Point(originPoint[i + 1], originPoint[i]);
                    }

                    for (int j = 0; j < destinationArr.Length; j++)
                    {
                        if (double.TryParse(destinationArr[j], out dest))
                        {
                            destinationPoint[j] = dest;
                        }
                        if (j == destinationArr.Length - 1)
                            break;
                        newRoute.DestinationPoint = new Point(destinationPoint[j + 1], destinationPoint[j]);
                    }

                    // Ara Noktaları Kayıt Etme

                    List<Coordinate> coordinates = new();
                    for (int i = 0; i < viaList.Count; i++)
                    {
                        coordinates.Add(new Coordinate(viaList[i + 1], viaList[i]));
                    }

                    // ROTA ÇİZDİRME
                    var lineGeoloc = new MultiLineString(
                     polyLine.Select(legPoints =>
                         new LineString(
                             legPoints.Select(point =>
                                 new Coordinate(point.Lng, point.Lat)
                             ).ToArray()

                         )
                     ).ToArray()
                     );

                    // Rota Kaydı
                    newRoute.Geoloc = lineGeoloc;
                    newRoute.Waypoints = new LineString(coordinates.ToArray());
                    var tMode = _routeTransportModeRepository.Get(transportMode);
                    newRoute.RouteTransportModeId = tMode.Type;
                    newRoute.UserId = 49;  // BURASI TOKEN DAN GELİCEK
                    _hereRepository.Add(newRoute);
                    _hereRepository.SaveChanges();
                    var lastRegister = _hereRepository.GetLastByRoute();
                    if (lastRegister == null)
                    {
                        routeInstructionDetail.RouteId = 0;
                    }
                    routeInstructionDetail.RouteId = lastRegister.Id;
                    _routeInstructionDetailRepository.Add(routeInstructionDetail);
                    _routeInstructionDetailRepository.Save();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("ROTA KAYDETME HATASI:" + ex.Message);
            }
        }

    }
}


//https://router.hereapi.com/v8/routes?transportMode=car&origin=36.788405691567654,34.52082110568881&destination=36.95423059486147,34.56788683310151&via=36.79136004614785,34.542901050299406&via=36.81186798197976,34.55410564318299&via=36.85390801774949,34.57913165912032&routingMode=fast&return=polyline,elevation,actions,instructions,turnByTurnActions,summary,travelSummary,incidents&apiKey=RLe-nFj46bfdzlvvlBCFuLrj3ZHYkJoxgjDrPnnY_-Q

//https://router.hereapi.com/v8/routes?transportMode=car&origin=36.79082630598165,34.53437564894557&destination=39.82125825017974,32.77551256120205&via=36.95934184253092,34.899179711937904&via=37.29139486141986,34.82552692294121&via=37.846853051855305,34.71925478428602&routingMode=fast&return=polyline,elevation,actions,instructions,turnByTurnActions,summary,travelSummary,incidents&apiKey=RLe-nFj46bfdzlvvlBCFuLrj3ZHYkJoxgjDrPnnY_-Q
//case "return":
//    // Gelen returndeki string içerisinde , var mı kontrol et 
//    string[] returnType = { "polyline", "actions", "instructions", "summary", "travelSummary", "turnByTurnActions", "mlDuration", "typicalDuration", "elevation",
//    "routeHandle", "passthrough", "incidents",  "routingZones", "truckRoadTypes", "tolls", "routeLabels" };

//    bool isExistComma = value.Contains(",");
//    if (isExistComma)
//    {
//        string[] words = value.Split(',');
//        foreach (string word in words)
//        {
//            index = Array.FindIndex(returnType, x => x.Equals(word, StringComparison.OrdinalIgnoreCase));
//            if (index != -1) acceptedValue = string.Join(",", returnType[index]);
//        }

//    }
//    index = Array.FindIndex(returnType, x => x.Equals(value, StringComparison.OrdinalIgnoreCase));
//    if (index != -1) acceptedValue = returnType[index];
//    break;


//     case "destination": // required

//                                //queryParams["via"] = string.Join("&via=", wayPoints.Select(wayPoint => PointToStrForApi(wayPoint)));
//                                // destination=52.52332,13.42800&via=52.52426,13.43000
//                                //if (desLatLong.Length < 2)
//                                //{
//                                //    acceptedValue = "0,0";
//                                //}
//                                string[] desLatLong = value.Split(',');
//acceptedValue = desLatLong.Length < 2 ? "0,0" : acceptedValue;

//if (desLatLong.Length > 2 && desLatLong.Length % 2 != 0)
//{
//    Array.Resize(ref desLatLong, desLatLong.Length - 1);
//}
//int counter = 0;
//foreach (var l in desLatLong)
//{
//    bool response = double.TryParse(l, out var result);
//    counter = response == false ? counter++ : counter;
//}

//acceptedValue = counter == 0 ? value : "0,0";

//break;