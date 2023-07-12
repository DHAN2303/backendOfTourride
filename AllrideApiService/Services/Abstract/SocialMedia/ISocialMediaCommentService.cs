using AllrideApiCore.Dtos.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Abstract.SocialMedia
{
    public interface ISocialMediaCommentService
    {
        public string SendCommentToPost(SocialMediaCommentsDto socialMediaComments);
        public string EditComment(SocialMediaEditCommensDto socialMediaEdit);
        public string DeleteComment(SocialMediaDeleteCommentsDto socialMediaDelete);

    }
}
