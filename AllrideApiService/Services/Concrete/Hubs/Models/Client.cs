using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Concrete.Hubs.Models
{
    public class ClientModel
    {
        public string ConnectionId { get; set; }
        public string NickName { get; set; }
    }

    public class ClientGroupModel
    {
        public string ConnectionGroupId { get; set; }
        public string NickName { get; set; }
    }
}
