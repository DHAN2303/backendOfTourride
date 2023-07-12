namespace AllrideApiService.Response
{

    public abstract class RouteResponse
    {
        public bool status { get; private set; }
        public RouteResponse(bool status)
        {
            this.status = status;
        }
    }

    public class RouteSuccessResponse: RouteResponse
    {
        public int routeId { get; private set; }

        public RouteSuccessResponse(int routeId) : base(true) {
            this.routeId = routeId;
        }
    }

    public class RouteFailedResponse: RouteResponse
    {
        public int errorCode { get; set; }
        public RouteFailedResponse(int errorCode) : base(false)
        {
            this.errorCode = errorCode;
        }
    }

}

