using AllrideApiChat.Functions.Files;
using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Services.Abstract;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AllrideApiChat.Functions.Compress
{
    public class SocialMediaPostsImageCompress
    {
        private readonly IPosts _posts;

        public SocialMediaPostsImageCompress(IPosts posts)
        {
            _posts = posts;
        }

        public string CompressImage(SocialMediaPostsDto socialPosts, int quality, int userId)
        {
            //rename file object
            renameFiles renamefiles = new renameFiles();

            //file features
            var file = socialPosts.file;
            var user_id = userId;
            var caption = socialPosts.caption;
            var location = socialPosts.location;
            var rename = renamefiles.RenameFile(Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName));
            var filePath = "D:\\image\\" + rename;//"/var/www/images/" + rename;

            //compress operation
            using (var image = Image.Load(file.OpenReadStream()))
            {
                var encoder = new JpegEncoder
                {
                    Quality = quality
                };

                image.Save(filePath, encoder);
                //save database

                
               _ = _posts.PostSocialMediaPostsAsync(user_id, caption, location, rename);

                //return file location
                return rename;
            }
        }
    }
}
