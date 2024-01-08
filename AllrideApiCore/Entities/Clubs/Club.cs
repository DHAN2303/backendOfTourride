using System.ComponentModel.DataAnnotations;

namespace AllrideApiCore.Entities.Clubs
{
    public class Club 
    {
        public int Id { get; set; }
        public string name { get; set; }    
        public string image_path { get; set; }
        public string description { get; set; }
        public int club_rank { get; set; }
        public int type { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set;}

    }
}
