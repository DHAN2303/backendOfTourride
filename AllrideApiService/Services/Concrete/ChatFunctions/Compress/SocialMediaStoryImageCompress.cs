using AllrideApiChat.Functions.Files;
using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Services.Abstract;
using SixLabors.ImageSharp.Formats.Jpeg;


namespace AllrideApiChat.Functions.Compress
{
    public class SocialMediaStoryImageCompress
    {
        private readonly IPosts _posts;

        public SocialMediaStoryImageCompress(IPosts posts)
        {
            _posts = posts;
        }

        public string CompressImage(SocialMediaStoryDto socialStory, int quality)
        {

            //rename file object
            renameFiles renamefiles = new renameFiles();

            //file features
            var file = socialStory.file;
            var user_id = socialStory.user_id;
            var caption = socialStory.caption;
            var location = socialStory.location;
            var rename = renamefiles.RenameFile(Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName));
            var filePath = "/var/www/images/" + rename;

            //compress operation
            using (var image = Image.Load(file.OpenReadStream()))
            {
                var encoder = new JpegEncoder
                {
                    Quality = quality
                };

                image.Save(filePath, encoder);
                //save database


                _ = _posts.PostSocialMediaStory(user_id, caption, location, rename);

                //return file location
                return rename;
            }
        }
    }
}
