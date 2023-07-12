namespace AllrideApiService.Services.Abstract
{
    public interface IUsageTrackerService
    {
        public string CanUseService(int userId, int serviceId);

    }
}
