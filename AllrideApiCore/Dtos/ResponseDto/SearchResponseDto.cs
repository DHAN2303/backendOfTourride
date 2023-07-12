namespace AllrideApiCore.Dtos.ResponseDto
{
    public class SearchResponseDto
    {
        public string ClubName { get; set; }
        public string ClubImagePath { get; set; }
        public string GroupName { get; set; }
        public string GroupImagePath { get; set; }
        public string UserNameLastName { get; set; }
        public string UserImagePath { get; set; }
        public string RouteName { get; set; }

        // NOT : ROTA ARAMA İÇİN DE FIELD YARATILACAK
    }
}
