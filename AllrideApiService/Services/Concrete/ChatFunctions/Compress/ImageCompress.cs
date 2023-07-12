using AllrideApiChat.Functions.Files;
using AllrideApiCore.Dtos.Chat;
using AllrideApiService.Services.Abstract;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace AllrideApiChat.Functions.Compress
{
    public class ImageCompress
    {

        private readonly IPosts _posts;

        public ImageCompress(IPosts post) 
        {
            _posts = post;
        }
        
        public string CompressImage(FileModel fileModel, int quality)
        {
            //rename file object
            renameFiles renamefiles = new renameFiles();

            //file features
            var file = fileModel.file;
            var fileName = fileModel.FileName;
            var senderid = fileModel.sender_id;
            var receiveid = fileModel.receive_id;
            var rename = renamefiles.RenameFile(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
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


                    if (fileModel.chat_type == 0)
                        _ = _posts.PostMessageAsync(senderid, Convert.ToInt32(receiveid), 2, rename);
                    else if (fileModel.chat_type == 1)
                    {
                        var groupId = fileModel.group_id;
                        _ = _posts.PostGroupMessageAsync(Convert.ToInt32(groupId), senderid, 2, rename);
                    }



                //return file location
                return rename;
            }
        }


    }
}
