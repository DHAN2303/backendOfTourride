using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Newss;

namespace AllrideApiCore
{
    public class Tags:BaseEntity
    {
        public string Name { get; set; }
        public  IEnumerable<NewsTags> NewsTagss { get; set; }
    }
}
