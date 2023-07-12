using AllrideApiCore.Entities.Here;

namespace AllrideApiCore.Entities.Routes
{
    public class RouteDetail 
    {
        public int Id { get; set; } 
        public int  RouteId { get; set; }
        public string ImagePath { get; set; }
        public string ImageLocation { get; set; }
        public double AverageUphillSlope{ get; set; }
        public double AverageDownhillSlope{ get; set; }
        public int FavoriteCounter { get; set; }
        public Route Route { get; set; }

    }
}
