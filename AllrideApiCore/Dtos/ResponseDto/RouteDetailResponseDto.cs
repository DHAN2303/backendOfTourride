namespace AllrideApiCore.Dtos.ResponseDto
{
    public class RouteDetailResponseDto
    {
        public string ImagePath { get; set; }
        public string ImageLocation { get; set; }
        public double AverageUphillSlope { get; set; }
        public double AverageDownhillSlope { get; set; }
        public int FavoriteCounter { get; set; }
    }
}
