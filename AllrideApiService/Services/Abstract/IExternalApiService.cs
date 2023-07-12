namespace AllrideApiService.Services.Abstract
{
    public interface IExternalApiService
    {
        Task<string> Get(string key);
        Task Post(string url, string parameter);
    }
}
