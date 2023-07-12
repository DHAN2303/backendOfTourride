using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiCore.Entities
{
    public class LogApi
    {
        public int id { get; set; }
        public string client_ip { get; set; }
        public string service_name { get; set; }
        public string request_param { get; set; }
        public int response_status { get; set; }
        public string response { get; set; }
        public int api_type { get; set; }
        public DateTime created_date { get; set; }
        public DateTime response_date { get; set; }

    }
}
