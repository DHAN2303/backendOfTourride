using AllrideApiCore.Dtos.ResponseDto;
using AllrideApiRepository.Repositories.Abstract.Messages.GroupsChats;

namespace AllrideApiRepository.Repositories.Concrete.Messages.GroupsChats
{
    public class GroupChatRepository:IGroupChatRepository
    {
        protected readonly AllrideApiDbContext _context;
        public GroupChatRepository(AllrideApiDbContext context)
        {
            _context= context;  
        }
        // 26 Mayıs
        public List<GroupChatListResponseDto> GetUserGroupsLastMessage(int UserId)
        {
            var groupMessageList = (
                from gm in _context.group_member
                join u in _context.user on gm.user_id equals u.Id  // Burada 
                join g in _context.groups on gm.group_id equals g.id
                join gmes in _context.group_messages on g.id equals gmes.group_id
                join ud in _context.user_detail on u.Id equals ud.UserId
                where (u.Id == UserId)
                orderby gmes.created_at descending 
                select new GroupChatListResponseDto
                {
                    UserName = ud.Name,
                    GroupName = g.name,
                    MessageContent = gmes.message_content,
                    GroupProfilePhoto = g.image_path,
                    LastDateTime = gmes.created_at,


                }).ToList();
            return groupMessageList;


            //return _context.group_messages.OrderByDescending(m => m.created_at).Take(UserId).ToList();
            // return _context.group_messages.Where(x=>x.sender_id==UserId).OrderByDescending(m=>m.created_at).ToList();
            // Grup Chat tablosunda son gönderilen mesajı alarak ve bu son gönderilen mesajın sender id sini alarak user_detail tablosunda o senderın profile foroğarfına ulaşabilirim
            //var groupChatList = (
            //    from gm in _context.group_messages
            //    join gmem in _context.group_member on  gm.sender_id equals gmem.user_id
            //    join g in _context.groups on gm.group_id equals g.id
            //    join u in _context.user on gmem.user_id equals u.Id
            //    join ud in _context.user_detail on u.Id equals ud.UserId
            //    where (u.Id == UserId)
            //    orderby gm.created_at descending
            //    select new GroupChatListResponseDto
            //    {
            //        UserName=ud.Name,
            //        GroupName = g.name,
            //        MessageContent = gm.message_content,
            //        GroupProfilePhoto = g.image_path,
            //        LastDateTime = gm.created_at,

            //    }).ToList();
            //return groupChatList;
        }
    }
}
