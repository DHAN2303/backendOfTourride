using System.Diagnostics;
using AllrideApiChat.Functions.Files;
using AllrideApiCore.Dtos.Chat;
using AllrideApiService.Services.Abstract;

namespace AllrideApiChat.Functions.Compress
{
    public class VideoCompress
    {
        private readonly IPosts _posts;

        public VideoCompress(IPosts post)
        {
            _posts = post;
        }


        public String CompressVideo(FileModel fileModel, double outputFileSizePercentage)
        {
            //rename file object
            renameFiles renamefiles = new renameFiles();

            //file features
            var tempFilePath = Path.GetTempFileName();
            var file = fileModel.file;
            var fileName =fileModel.FileName;
            var senderid = fileModel.sender_id;
            var receiveid = fileModel.receive_id;
            var rename = renamefiles.RenameFile(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
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
                if (fileModel.chat_type == 0)
                _ = _posts.PostMessageAsync(senderid, Convert.ToInt32(receiveid), 1, rename);
                else if(fileModel.chat_type == 1)
                {
                    var groupId = fileModel.group_id;
                    _ = _posts.PostGroupMessageAsync(Convert.ToInt32(groupId), senderid, 1, rename);
                }
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
