using NetTopologySuite.Geometries;

namespace AllrideApiCore.Entities.Routes
{
    public class RouteCalculate
    {
        public int Id { get; set; }
        public Geometry geom { get; set; }
        public Geometry[] geom2 { get; set; }

        // public int srid { get; set; }
        // public bool status { get; set; }
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
        //public DateTime UpdatedDate { get; set; }

    }
}
