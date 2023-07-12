using AllrideApiCore.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiCore.Entities.Commons
{
    public class UserBlock
    {
        public int Id { get; set; }
        public int BlockedUserId { get; set; }
        public int BlockingUserId { get; set; }
        public UserEntity UserBlocked { get; set; }
        public UserEntity UserBlocking { get; set; }

    }
}
