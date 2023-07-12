
using AllrideApiService.Services.Abstract.Log;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AllrideApi.Middleware
{
    public class HttpLoggingApiMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<HttpLoggingApiMiddleware> _logger;
        private readonly ILogApiService _logApi;
        public HttpLoggingApiMiddleware(RequestDelegate next, ILogger<HttpLoggingApiMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogApiService _logApi)
        {

            if (httpContext.Request.Path.StartsWithSegments("/chatHubs"))
            {
                // Skip logging for SignalR requests
                await _next.Invoke(httpContext);
                string ip = httpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string service = "/chathub";
                _logApi.LogApiSave(ip, service, "", 200, "", 0);
                return;
            }
            var originalBodyStream = httpContext.Response.Body;

            //_logger.LogInformation($"Client IP: {httpContext.Request.HttpContext.Connection.RemoteIpAddress}");
            //_logger.LogInformation($"Query Keys: {httpContext.Request.QueryString}");

            httpContext.Request.EnableBuffering(); // enable request buffering
            //httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

            //var form = await new StreamReader(httpContext.Request.Body, Encoding.UTF8).ReadToEndAsync(); //await httpContext.Request.ReadFormAsync();                       
            //var requestText = JsonSerializer.Serialize(form); // serialize form data to JSON

            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;

            await _next.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            int response_status = httpContext.Response.StatusCode;
            string client_ip = httpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
   
            if (string.IsNullOrEmpty(client_ip))
            {
                string localIP;
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("127.0.0.1", 65530);
                    var endPoint = socket.LocalEndPoint as IPEndPoint;
                    if (endPoint != null)
                    {
                        localIP = endPoint.Address.ToString();
                    }
                    else
                    {
                        // Yerel IP adresi al�namad�
                        localIP = "Belirtilmedi";
                    }
                }
            }
            int serviceName_length = httpContext.GetEndpoint().DisplayName.Split(".").Length;
            string service_name = httpContext.GetEndpoint().DisplayName.Split(".")[serviceName_length - 1];
            string request_param = httpContext.Request.Method;
            int api_type = service_name == "nearBy (netcoreSignal)" ? 1 : service_name == "RequestAlongRouteSearch (netcoreSignal)" ? 2 : service_name == "hereNearBy (netcoreSignal)" ? 3 : service_name == "TomTomRouting (netcoreSignal)" ? 4 : 0;
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            await httpContext.Response.Body.CopyToAsync(originalBodyStream);

            if (response_status == 200) responseText = "";

            _logApi.LogApiSave(client_ip, service_name, request_param, response_status, responseText, api_type);

            //_logger.LogInformation($"resp Keys: {requestText}");
            //_logger.LogInformation($"req Keys: {Newtonsoft.Json.JsonConvert.DeserializeObject(responseText)}");
        }
    }
}