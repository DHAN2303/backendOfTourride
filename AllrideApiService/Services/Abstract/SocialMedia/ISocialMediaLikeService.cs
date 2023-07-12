using AllrideApiCore.Dtos.SocialMedia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Abstract.SocialMedia
{
    public interface ISocialMediaLikeService
    {
        public string PostLike([FromForm] SocialMediaLikeDto socialMediaLike, int userId);
        public string PostUnLike([FromForm] SocialMediaUnLikeDto socialMediaUnLike, int userId);


    }
}
