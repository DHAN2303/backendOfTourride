using AllrideApiCore.Entities.Users;

namespace AllrideApiCore.Entities.ServiceLimit
{
    public class ServiceUsage
    {
        public int user_id { get; set; }
        public int service_id { get; set; }
        public int usage_count { get; set; }
        public UserEntity User { get; set; }
        public ServiceTypes ServiceTypes { get; set; }
    }
}
