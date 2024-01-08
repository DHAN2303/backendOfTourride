using System.ComponentModel.DataAnnotations.Schema;

namespace AllrideApiCore.Entities.Users
{
    public class UserInvites
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int inveting_id { get; set; }
        public int invited_id { get; set; }
        public int where { get; set; }
        public int whereId { get; set; }
        public bool status { get; set; }
        public DateTime invitedDateTime { get; set; }
        public UserEntity UserInvited { get; set; }
        public UserEntity UserInviting { get; set; }
    } 
}
