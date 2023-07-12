using AllrideApiCore.Dtos.RequestDto;
using AllrideApiService.Services.Abstract;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AllrideApiService.Compress
{
    public class ClubImageProfileOrBackgroundCompress
    {
        private readonly IPosts _posts;
        public ClubImageProfileOrBackgroundCompress(IPosts posts)
        {
           _posts = posts;
        }

        public string CompressImage(ClubUpdateChangePhotoDto changePhoto, int quality)
        {
            //file features
            var file = changePhoto.File;
            var clubId = changePhoto.ClubId;
            var location = changePhoto.Location;
            var rename = RenameFile(Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName));
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


                _ = _posts.ClubMediaPostsAsync(clubId, location);

                //return file location
                return rename;
            }
        }

        public string RenameFile(string filename, string extens)
        {
            DateTime datetime = DateTime.Now;
            string datetimeString = datetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTimeOffset datetimeOffset = new DateTimeOffset(Convert.ToDateTime(datetimeString));
            long milliseconds = datetimeOffset.ToUnixTimeMilliseconds();

            return filename + "_" + milliseconds.ToString() + extens;
        }
    }
}
