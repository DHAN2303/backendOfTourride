using AllrideApiCore.Entities.ServiceLimit;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete
{
    public class UsageTrackerService : IUsageTrackerService
    {
        private readonly AllrideApiDbContext _context;
        private readonly ILogger<UsageTrackerService> _logger;
        public UsageTrackerService(AllrideApiDbContext context, ILogger<UsageTrackerService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public string CanUseService(int userId, int serviceId)
        {

            try
            {
                var user = _context.user.FirstOrDefault(u => u.Id == userId);
                var user_type = user.user_type;
                var service = _context.service_types.FirstOrDefault(s => s.service_id == serviceId);
                var service_name = service.service_name;

                var user_types = _context.user_types.FirstOrDefault(ut => ut.type == user_type);
                var limit = serviceId == 0 ? user_types.tomtom_nearby_limit : serviceId == 1 ? user_types.tomtom_along_limit : serviceId == 2 ? user_types.here_nearby_limit : serviceId == 3 ? user_types.tomtom_routing_limit : serviceId == 4 ? user_types.weather_limit : user_types.here_route_limit;

                var usage = _context.service_usage
                    .FirstOrDefault(su => su.user_id == userId && su.service_id == serviceId);
                if (usage == null)
                {
                    usage = new ServiceUsage { user_id = userId, service_id = serviceId, usage_count = 0 };
                    _context.service_usage.Add(usage);
                }

                usage.usage_count++;
                if (usage.usage_count <= limit)
                    _context.SaveChanges();
                if (usage.usage_count <= limit)
                    return "1";
                else return "0";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " "+ex.InnerException);
                return "0";
            }
        }
    }
}
