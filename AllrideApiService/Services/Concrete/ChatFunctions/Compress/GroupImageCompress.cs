using AllrideApiChat.Functions.Files;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AllrideApiChat.Functions.Compress
{
    public class GroupImageCompress
    {

        public string CompressGroupImage(IFormFile groupImage, int quality)
        {

            //rename file object
            renameFiles renamefiles = new renameFiles();

            if(groupImage != null)
            {
                //file features
                var file = groupImage;
                var fileName = groupImage.FileName;
                var rename = renamefiles.RenameFile(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
                var filePath = "/var/www/images/" + rename;//"/home/pardus/Pictures/" + fileName;

                //compress operation
                using (var image = Image.Load(file.OpenReadStream()))
                {
                    var encoder = new JpegEncoder
                    {
                        Quality = quality
                    };

                    image.Save(filePath, encoder);

                    //return file location
                    return rename;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
