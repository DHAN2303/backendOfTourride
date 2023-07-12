using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiService.Services.Abstract.SocialMedia
{
    public interface IDeleteExpiredStoriesService
    {
        public Task DeleteExpiredStories();
    }
}
