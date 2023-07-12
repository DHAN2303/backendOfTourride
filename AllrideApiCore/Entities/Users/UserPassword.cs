using System.ComponentModel.DataAnnotations.Schema;

namespace AllrideApiCore.Entities.Users
{
    public class UserPassword : BaseEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public byte[] SaltPass { get; set; }
        public byte[] HashPass { get; set; }

    }
}
