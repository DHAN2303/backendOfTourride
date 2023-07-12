namespace AllrideApiCore.Entities.ServiceLimit
{
    public class ServiceTypes
    {
        public int service_id { get; set; }
        public string service_name { get; set; }
        public IEnumerable<ServiceUsage> service_usages { get; set; }
    }
}
