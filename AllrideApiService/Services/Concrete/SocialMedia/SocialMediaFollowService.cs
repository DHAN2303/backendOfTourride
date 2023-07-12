
using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiCore.Entities.SocialMedia;
using AllrideApiRepository;
using AllrideApiRepository.Repositories.Abstract.SocailMedia;
using AllrideApiService.Response;
using AllrideApiService.Services.Abstract.SocialMedia;
using AllrideApiService.Services.Concrete.UserCommon;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AllrideApiService.Services.Concrete.SocialMedia
{
    public class SocialMediaFollowService : ISocialMediaFollowService
    {
        protected AllrideApiDbContext _context;
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserGeneralService> _logger;
        public SocialMediaFollowService(AllrideApiDbContext context, IMapper mapper, ISocialMediaRepository socialMediaRepository, ILogger<UserGeneralService> logger)
        {
            _socialMediaRepository= socialMediaRepository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        public string AddFollower(SocialMediaFollowDto socialMediaFollow)
        {
            var follower_id = socialMediaFollow.follower_id;
            var followee_id = socialMediaFollow.followed_id;
            var newFollower = new SocialMediaFollow
            {
                follower_id = follower_id,
                followed_id = followee_id,
                created_at = DateTime.UtcNow
            };
            try
            {
                _context.social_media_follows.Add(newFollower);
                _context.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }
        public string UnFollower(SocialMediaUnFollowDto socialMediaUnFollow)
        {
            var follower_id = socialMediaUnFollow.follower_id;
            var followee_id = socialMediaUnFollow.followee_id;
            var follow = _context.social_media_follows.FirstOrDefault(u => u.follower_id == follower_id && u.followed_id == followee_id);
            if (follow != null)
                try
                {
                    _context.Remove(follow);
                    _context.SaveChanges();
                    return null;
                }
                catch (Exception ex)
                {
                    return ex.InnerException.Message;
                }
            else return "false";
        }
        public CustomResponse<SocialMediaFollowDto> GetUserFriends(SocialMediaFollowDto socialMediaFollow)
        {

            List<ErrorEnumResponse> _enumErrorResponse = new();
            SocialMediaFollowDto _socialMediaFollowDto = new();
            try
            {
                var SocialMedia = _mapper.Map<SocialMediaFollow>(socialMediaFollow);
                if (SocialMedia == null)
                {
                    _enumErrorResponse.Add(ErrorEnumResponse.MappingFailed);
                    return CustomResponse<SocialMediaFollowDto>.Fail(_enumErrorResponse, false);
                }
                var result = _socialMediaRepository.GetUserFriends(SocialMedia);
                if(result == null)
                {
                    _enumErrorResponse.Add(ErrorEnumResponse.UserHasNoFollowers);
                    return CustomResponse<SocialMediaFollowDto>.Fail(_enumErrorResponse, false);
                }

                _socialMediaFollowDto = _mapper.Map<SocialMediaFollowDto>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(" SOCIAL MEDIA FOLLOW SERVICE ERROR IN GET USER FRIENDS METHOD " + ex.Message);
            }

            // NOT SADECE ID DEĞİL KULLANICININ RESİMLERİNİ DE DÖNECEĞİM  
            return CustomResponse<SocialMediaFollowDto>.Success(_socialMediaFollowDto.follower_id.ToString(), false);
        }
    }
}
