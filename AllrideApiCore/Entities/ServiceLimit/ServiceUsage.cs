using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiCore.Entities.ServiceLimit
{
    public class ServiceUsage
    {
        public string user_id { get; set; }
        public int service_id { get; set; }
        public int usage_count { get; set; }
    }
}
