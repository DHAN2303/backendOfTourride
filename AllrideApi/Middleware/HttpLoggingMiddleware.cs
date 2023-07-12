using Microsoft.IO;
using System.Text;

namespace Serilog
{

    public class HttpLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<HttpLoggingMiddleware> logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        public HttpLoggingMiddleware(ILogger<HttpLoggingMiddleware> logger)
        {
            this.logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            // Orjinal Streamin yedeğini al
            //var originalBodyStream = context.Response.Body;

            // Request
            // QueryStringden gelen verileri aldı
            // Bir sonraki adım txt dosyasına ve veritabanına bu bilgileri kaydetme de

            string requestText = await GetRequestBody(context);

            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            var value = context.Request.QueryString.ToString();


            if (!string.IsNullOrEmpty(value))
            {
                logger.LogInformation($"Query Keys: {context.Request.QueryString}");
            }
            logger.LogInformation($"Request Host Value: {context.Request.Host.Value}");
            logger.LogInformation($"Request Host Method: {context.Request.Method}");
            logger.LogInformation($"Request Is Https: {context.Request.IsHttps}");
            logger.LogInformation($"Request Path: {context.Request.Path}");
            logger.LogInformation("Client IP: {ClientIP}", context.Connection.RemoteIpAddress);
            logger.LogInformation("Request Scheme: " + context.Request.Scheme);

            await next.Invoke(context);


            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            await responseBody.CopyToAsync(originalBodyStream);

            logger.LogInformation($"Request: {requestText}");
            logger.LogInformation($"Response: {responseText}");
            logger.LogInformation($"Response Body:{context.Response.Body}");
            logger.LogInformation($"Response Status Code:{context.Response.StatusCode}");
        }


        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream, Encoding.UTF8);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            { 
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private async Task<string> GetRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            string reqBody = ReadStreamInChunks(requestStream);

            context.Request.Body.Seek(0, SeekOrigin.Begin);

            return reqBody;
        }
    }
}

/*
 * 
            logger.LogInformation($"Request Host Port: {context.Request.Host.Port}");
            logger.LogInformation($"Request Header: {context.Request.Headers}");
            logger.LogInformation($"Request Content Type: {context.Request.ContentType}");
            logger.LogInformation($"Request Body: {context.Request.Body}");
            logger.LogInformation($"Request Body: {context.Request.Body.Flush}");
 */