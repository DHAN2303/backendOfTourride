namespace AllrideApiChat.Functions.Files
{
    public class renameFiles
    {

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
