using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiRepository.Repositories.Abstract.Messages.GroupsChats;

namespace AllrideApiRepository.Repositories.Concrete.Messages.GroupsChats
{
    public class ClubChatRepository:IClubChatRepository
    {
        protected readonly AllrideApiDbContext _context;
        public ClubChatRepository(AllrideApiDbContext context)
        {
            _context= context;  
        }

        // 29 MAYIS
        public List<ClubChatListResponseDto> GetUserClubsLastMessage(int UserId)
        {
            var clubMessageList = (
                from gm in _context.group_member
                join u in _context.user on gm.user_id equals u.Id  // Burada 
                join g in _context.groups on gm.group_id equals g.id
                join gmes in _context.group_messages on g.id equals gmes.group_id
                join ud in _context.user_detail on u.Id equals ud.UserId
                where (u.Id == UserId)
                orderby gmes.created_at descending
                select new ClubChatListResponseDto
                {
                    UserName = ud.Name,
                    ClubName = g.name,
                    MessageContent = gmes.message_content,
                    ClubProfilePhoto = g.image_path,
                    LastDateTime = gmes.created_at,


                }).ToList();
            return clubMessageList;
        }


    }
}
