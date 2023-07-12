namespace AllrideApiService.Services.Abstract.Mail
{
    public interface IMailService
    {
        public Task<bool> SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
        public Task<bool> SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        //public Task SendActivationAsync(string to, string subject, string body, int activationCode, bool isBodyHtml = true);
    }
}
