
namespace AllrideApiCore.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string ClientIp { get; set; }
        public string ServisName { get; set; }
        public string RequestData { get; set; }
        public string RequestParam { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
