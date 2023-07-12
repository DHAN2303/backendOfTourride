namespace AllrideApiCore.Entities.Newss
{
    public class NewsTags:BaseEntity
    {
        public int NewsId { get; set; }
        public int TagId { get; set; }
        public News News { get; set; }
        public Tags Tags { get; set; }
    }
}
