﻿namespace AllrideApiCore.Dtos.RequestDto.RoutePlanner
{
    public class CreateRoutePlannerDto
    {
        public int RouteId { get; set; }
        public string RoutePlannerTitle { get; set; }
        public string RouteName { get; set; }
        public string ColorCodeHex { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public string AlertTime { get; set; }
    }
}

