using AllrideApiCore.Entities;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract.Log;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.Log
{
    public class LogApiService : ILogApiService
    {
        protected readonly AllrideApiDbContext _context;
        private readonly ILogger<LogApiService> _logger;
        public LogApiService(AllrideApiDbContext context, ILogger<LogApiService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void LogApiSave(string _client_ip, string _service_name, string _request_param, int _response_status, string _responseText, int _api_type)
        {
            try
            {
                var newLog = new LogApi
                {
                    client_ip = _client_ip,
                    service_name = _service_name,
                    request_param = _request_param,
                    response_status = _response_status,
                    response = _responseText,
                    api_type = _api_type,
                    created_date = DateTime.UtcNow,
                    response_date = DateTime.UtcNow
                };
                _context.api_log.Add(newLog);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("database error: " + ex);
                _logger.LogError("database error: " + ex.InnerException);

            }
        }
    }
}
