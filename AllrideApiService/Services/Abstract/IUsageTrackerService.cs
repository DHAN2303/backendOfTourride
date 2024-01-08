namespace AllrideApiService.Services.Abstract
{
    public interface IUsageTrackerService
    {
        public string CanUseService(string email, int serviceId);

    }
}
