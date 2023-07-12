using AllrideApiChat.Functions.Compress;
using AllrideApiCore.Dtos.RequestDto;
using AllrideApiCore.Entities;
using AllrideApiCore.Entities.Chat;
using AllrideApiCore.Entities.Clubs;
using AllrideApiCore.Entities.Groups;
using AllrideApiCore.Entities.SocialMedia;
using AllrideApiRepository;
using AllrideApiService.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AllrideApiChat.DataBase
{
    public class Posts : IPosts
    {
        protected readonly AllrideApiDbContext _context;

        public Posts(AllrideApiDbContext context)
        {
            _context = context;
        }

        //specific messages
        public async Task PostMessageAsync(int senderId, int recipientId, int content_type,string message_content)
        {
            try
            {
                var newMessage = new Message { sender_id = senderId, recipient_id = recipientId, content_type = content_type, message_content = message_content, created_at = DateTime.UtcNow };
                _context.Entry(newMessage).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("database error: " + ex.Message + " " + ex.InnerException);
            }
        }
        //


        //group messages
        public async Task PostGroupMessageAsync(int groupId, int senderId, int content_type, string message_content)
        {
            try
            {
                var newGroupMessage = new GroupMessage { sender_id = senderId, group_id = groupId, content_type = content_type, message_content = message_content, created_at = DateTime.UtcNow };
                _context.Entry(newGroupMessage).State = EntityState.Added;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("database error: " + ex);
            }
        }
        //

        public string PostSocialMediaStory(int userId, string caption, string location, string mediaUrl)
        {
            try
            {
                var newStory = new SocialMediaStory
                {
                    user_id = userId,
                    caption = caption,
                    location_info = location,
                    media_url = mediaUrl,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                _context.social_media_story.Add(newStory);
                _context.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("database error: " + ex);
                return ex.Message;
            }
        }

        public async Task PostLogAsync(string _client_ip, string _service_name, string _request_param, int _response_status, string _responseText, int _api_type)
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
                Console.WriteLine("database error: " + ex);
                Console.WriteLine("database error: " + ex.InnerException);

            }

        }

        public async Task PostSocialMediaPostsAsync(int userId, string caption, string location,string mediaUrl)
        {
            try
            {
                var newPost = new SocialMediaPosts
                {
                   user_id = userId,
                   caption = caption,
                   location_info = location,
                   media_url = mediaUrl,
                   comments_count = 0,
                   likes_count=0,
                   created_at = DateTime.UtcNow,
                   updated_at = DateTime.UtcNow
                };
                _context.social_media_posts.Add(newPost);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("database error: " + ex);
            }
        }


        public async Task PostNewGroup(string groupName, IFormFile groupImage, string description, List<string> groupMember, int admin)
        {
            try
            {
                GroupImageCompress imageCompress = new GroupImageCompress();

                var savedPath = imageCompress.CompressGroupImage(groupImage, 25);
                var newGroup = new Group
                {
                    name = groupName,
                    image_path = savedPath,
                    description = description,
                    created_date = DateTime.UtcNow,
                    updated_date = DateTime.UtcNow,
                };

                _context.Entry(newGroup).State = EntityState.Added;

                _context.SaveChanges(); // Save changes to the database

                // Retrieve the last inserted group ID
                var lastGroupId = newGroup.id;

                var groupMembers = groupMember;

                //for gorup creator
                var newGroupAdmin = new GroupMember
                {
                    user_id = admin,
                    group_id = lastGroupId, // Use the last group ID
                    role = 0,
                    joined_date = DateTime.UtcNow,
                };
                _context.Entry(newGroupAdmin).State = EntityState.Added;
                //

                if(groupMembers != null)
                {
                    //other member
                    foreach (var member in groupMembers)
                    {
                        var newGroupMember = new GroupMember
                        {
                            user_id = Convert.ToInt32(member.Split("_")[0]),
                            group_id = lastGroupId, // Use the last group ID
                            role = Convert.ToInt32(member.Split("_")[1]),
                            joined_date = DateTime.UtcNow,
                        };
                        _context.Entry(newGroupMember).State = EntityState.Added;
                    }
                    //
                }

                _context.SaveChanges(); // Save changes to the database
            }
            catch (Exception ex)
            {
                Console.WriteLine("database error: " + ex);
            }
        }

        public async Task CreateNewClub(ClubRequestDto clubRequestDto, int admin)
        {
            try
            {
                GroupImageCompress imageCompress = new GroupImageCompress();

                var savedPath = imageCompress.CompressGroupImage(clubRequestDto.Image, 25);
                var newClub = new Club
                {
                    name = clubRequestDto.Name,
                    image_path = savedPath,
                    description = clubRequestDto.Description,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };

                _context.Entry(newClub).State = EntityState.Added;

                _context.SaveChanges(); // Save changes to the database

                // Retrieve the last inserted group ID
                var lastGroupId = newClub.Id;

                var clubMembers = clubRequestDto.ClubMembers;

                //for gorup creator
                var newClubAdmin = new ClubMember
                {
                    user_id = admin,
                    club_id = lastGroupId, // Use the last group ID
                    role = 0,
                    joined_date = DateTime.UtcNow,
                };
                _context.Entry(newClubAdmin).State = EntityState.Added;
                //

                if (clubMembers != null)
                {
                    //other member
                    foreach (var member in clubMembers)
                    {
                        var newGroupMember = new ClubMember
                        {
                            user_id = Convert.ToInt32(member.Split("_")[0]),
                            club_id = lastGroupId, // Use the last group ID
                            role = Convert.ToInt32(member.Split("_")[1]),
                            joined_date = DateTime.UtcNow,
                        };
                        _context.Entry(newGroupMember).State = EntityState.Added;
                    }
                    //
                }

                _context.SaveChanges(); // Save changes to the database
            }
            catch (Exception ex)
            {
                Console.WriteLine("database error: " + ex);
            }
        }

        // 27 Mayıs
        public string ClubMediaPostsAsync(int clubId, string location)
        {
            try
            {
                var newClubImage = new Club
                {
                    Id = clubId,
                    profile_path = location,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                _context.club.Add(newClubImage);
                _context.SaveChanges();
                var result = _context.club.Where(x => x.Id == clubId).Select(x => x.profile_path).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        // 27 Mayıs
        public string GroupMediaPostsAsync(int groupId, string location)
        {
            try
            {
                var newGroupImage = new Group
                {
                    id = groupId,
                    image_path = location,
                    created_date = DateTime.UtcNow,
                    updated_date = DateTime.UtcNow
                };

                _context.groups.Add(newGroupImage);
                _context.SaveChanges();
                var result = _context.club.Where(x => x.Id == groupId).Select(x => x.profile_path).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }



}

