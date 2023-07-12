using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiCore.Dtos.RoutesDtos;
using AllrideApiCore.Entities.Here;
using AllrideApiCore.Entities.Routes;
using AllrideApiRepository.Repositories.Abstract;
using NetTopologySuite.Geometries;

namespace AllrideApiRepository.Repositories.Concrete
{
    public class RoutingRepository : IRoutingRepository
    {

        protected readonly AllrideApiDbContext _context;
        public RoutingRepository(AllrideApiDbContext context)
        {
            _context = context;
        }

        public RouteCalculate Add(List<List<RoutesEntities>> legs)
        {
            // leg = Line[]
            // line = Coordinate[]
            var line = new MultiLineString(
                legs.Select(legPoints =>
                    new LineString(

                        // line = Coordinate[]
                        legPoints.Select(point =>
                            new Coordinate(point.longitude, point.latitude)
                        ).ToArray()

                    )
                ).ToArray()
                );

            //string wkt = line.AsText();
            var routeCalculate = new RouteCalculate { geom = line };
            return _context.route_calculate.Add(routeCalculate).Entity;
        }

        public List<Route> GetLast3Routes(int UserId)
        {
            return _context.route.Where(x=>x.UserId == UserId).Select(x => new Route {
                Geoloc = x.Geoloc,
                Length = x.Length,
                Duration = x.Duration
            })
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();
        }
        public List<int> GetRecommendedRoute(int recommendedType)
        {
            return _context.route.Where(x => x.EditorAdvice == recommendedType).Select(rota => rota.Id).ToList();
        }
        public List<RouteDetail> GetRecommendedRouteDetail(int recommendedType)
        {
            List<RouteDetail> routeDetails = new();
            try
            {
                var result = _context.route.Where(x => x.EditorAdvice == recommendedType).Select(rota => rota.Id).ToList();

                foreach (var route in result)
                {
                    var routeDetail = _context.route_detail.Where(x => x.RouteId == route);
                    if (routeDetail != null)
                    {
                        routeDetails.AddRange(routeDetail);
                    }
                }
            }
            catch (Exception e)
            {
                // Hata işleme kodu
            }
            return routeDetails;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public RouteCalculate Update(RouteCalculate route)
        {
            throw new NotImplementedException();
        }
    }
}
//https://stackoverflow.com/questions/8835434/insert-data-using-entity-framework-model

//Debug.WriteLine("POİNT LİSTESİ:   " + pointList);
//_context.AddRange(pointList);
//Debug.WriteLine("Debug message. responseResult:" + pointList);
//foreach(var point in pointList) {
//    _context.Database.ExecuteSqlRawAsync($"insert into routecalculates (geometry) values(ST_AsText( ST_MakeLine(ST_Point({point.latitude}, {point.longitude})) ))");
//}
// _context.Database.ExecuteSqlRawAsync("insert into users1 (geometry) values(ST_AsText( ST_MakeLine(ST_Point(36.995624117136025, 34.31738383468312), ST_Point(36.814614, 34.651682)) ))");
//_context.Database.ExecuteSqlRawAsync("insert into users1 (geometry) values(ST_AsText( ST_MakeLine(ST_Point(36.995624117136025, 34.31738383468312), ST_Point(36.814614, 34.651682)) ))");
/* Func<double, String> doubleToString = (double num) => num.ToString().Replace(',', '.');

 String lineStringContent = String.Join(", ", pointList.Select(point => $"{doubleToString(point.latitude)} {doubleToString(point.longitude)}"));

 string sql = "INSERT INTO routecalculates (geom) VALUES ST_GeomFromText(@geom, 4326))";
 var parameters = new { geom = $"LINESTRING({lineStringContent})" };
 await _context.Database.ExecuteSqlRawAsync(sql, parameters);

 //RouteCalculate data = new RouteCalculate
 //{
 //    geom = Geometry.
 //};*/