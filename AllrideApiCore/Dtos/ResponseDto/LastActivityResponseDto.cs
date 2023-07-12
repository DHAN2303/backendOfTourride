using NetTopologySuite.Geometries;

namespace AllrideApiCore.Dtos.ResponseDtos
{
    public class LastActivityResponseDto
    {
        public string ActivityName { get; set;}
        public double Distance { get; set;}
        public double Duration { get; set;}
        public DateTime CreatedDate { get; set;}
        public DateTime StartedDate { get; set;}
        public string CreaterName { get; set;}
        public Geometry Geoloc { get; set;}
        public string NameLastName { get; set;}
        public string UserProfileImagePath { get; set;}

        // Rotaya ait detaylarda alınacak 

    }
}
