using AllrideApiService.Services.Concrete.Hubs.Models;

namespace AllrideApiService.Services.Concrete.Hubs.Data
{
    public static class ClientSource
    {
        public static List<ClientModel> Clients { get; } = new List<ClientModel>();
        public static List<ClientModel> ClientsGroup { get; } = new List<ClientModel>();

    }
}
