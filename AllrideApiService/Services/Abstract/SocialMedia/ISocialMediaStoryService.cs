using AllrideApiCore.Dtos.SocialMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Abstract.SocialMedia
{
    public interface ISocialMediaStoryService
    {
        public string UpdatePost(SocialMediaUpdateStoryDto socialMediaUpdate);

        public string DeletePost(SocialMediaDeleteStoryDto socialMediaDelete);

        public object FetchStory(int userId);

    }
}
