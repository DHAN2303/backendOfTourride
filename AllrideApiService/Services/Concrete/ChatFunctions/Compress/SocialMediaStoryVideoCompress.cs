using AllrideApiChat.Functions.Files;
using AllrideApiCore.Dtos.SocialMedia;
using AllrideApiService.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllrideApiChat.Functions.Compress
{
    public class SocialMediaStoryVideoCompress
    {
        private readonly IPosts _posts;

        public SocialMediaStoryVideoCompress(IPosts post)
        {
            _posts = post;
        }


        public String CompressVideo(SocialMediaStoryDto socialStory, double outputFileSizePercentage)
        {

            //rename file object
            renameFiles renamefiles = new renameFiles();


            //file features
            var tempFilePath = Path.GetTempFileName();
            var file = socialStory.file;
            var user_id = socialStory.user_id;
            var caption = socialStory.caption;
            var location = socialStory.location;
            var rename = renamefiles.RenameFile(Path.GetFileNameWithoutExtension(file.FileName), Path.GetExtension(file.FileName));
            var filePath = "/var/www/videos/" + rename;


            //create file 
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            //

            //compress operation
            try
            {
                // Get input video file info
                var inputFileSize = new FileInfo(tempFilePath).Length;
                var inputVideoFormat = Path.GetExtension(tempFilePath).ToLower();

                // Calculate video bitrate based on desired output file size
                var outputFileSizeBytes = (long)(inputFileSize * outputFileSizePercentage);
                var videoLengthInSeconds = (int)GetVideoLength(tempFilePath).TotalSeconds;
                var videoBitrate = (int)((outputFileSizeBytes * 8.0) / videoLengthInSeconds);

                // Run FFmpeg to compress video
                var ffmpegProcess = new Process();
                ffmpegProcess.StartInfo.FileName = "ffmpeg";
                ffmpegProcess.StartInfo.Arguments = $"-i \"{tempFilePath}\" -b:v {videoBitrate} \"{filePath}\"";
                ffmpegProcess.StartInfo.UseShellExecute = false;
                ffmpegProcess.StartInfo.RedirectStandardOutput = true;
                ffmpegProcess.Start();
                ffmpegProcess.WaitForExit();

                if (ffmpegProcess.ExitCode != 0)
                {
                    throw new Exception($"FFmpeg exited with code {ffmpegProcess.ExitCode}");
                }
            }
            finally
            {
                // Delete temporary file
                File.Delete(tempFilePath);

                //save database
                _ = _posts.PostSocialMediaStory(user_id, caption, location, rename);

            }
            //
            //return file location
            return rename;
        }

        TimeSpan GetVideoLength(string filePath)
        {
            var ffmpegProcess = new Process();
            ffmpegProcess.StartInfo.FileName = "ffmpeg";
            ffmpegProcess.StartInfo.Arguments = $"-i \"{filePath}\" -f null -";
            ffmpegProcess.StartInfo.UseShellExecute = false;
            ffmpegProcess.StartInfo.RedirectStandardError = true;
            ffmpegProcess.Start();
            var output = ffmpegProcess.StandardError.ReadToEnd();
            ffmpegProcess.WaitForExit();

            var durationIndex = output.IndexOf("Duration: ") + 10;
            var durationString = output.Substring(durationIndex, 11);
            return TimeSpan.Parse(durationString);
        }
    }
}
