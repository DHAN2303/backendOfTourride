using AllrideApiCore.Dtos.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Abstract.SocialMedia
{
    public interface ISocialMediaPostsService
    {
        public bool UpdatePost(SocialMediaUpdatePostDto socialMediaUpdate);

        public bool DeletePost(SocialMediaDeletePostDto socialMediaDelete);

        public object FetchPost(int userId);

    }
}
