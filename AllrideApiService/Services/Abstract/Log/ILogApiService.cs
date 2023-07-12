using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Abstract.Log
{
    public interface ILogApiService
    {
        public void LogApiSave(string _client_ip, string _service_name, string _request_param, int _response_status, string _responseText, int _api_type);

    }
}
