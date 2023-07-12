using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AllrideApi.Controllers.Version_1.TomtomApi;
using AllrideApiCore.Dtos.Here;
using AllrideApiService.Abstract;
using AllrideApiService.Concrete.HereApi;
using AllrideApiService.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace AllrideApiTests.Controller
{
    public class HereNearBySearchControllerTests
    {
        private Mock<IUsageTrackerService> _usageTrackerServiceMock;
        private Mock<IHereNearBySearchService> _hereNearBySearchServiceMock;
        private Mock<IConfiguration> _config;
        private HereNearBySearchController _controller;
        private readonly Mock<HttpContext> _httpContextMock;

        public HereNearBySearchControllerTests()
        {
            _usageTrackerServiceMock = new Mock<IUsageTrackerService>();
            _hereNearBySearchServiceMock = new Mock<IHereNearBySearchService>();
            _config = new Mock<IConfiguration>();
            _controller = new HereNearBySearchController(_hereNearBySearchServiceMock.Object,_usageTrackerServiceMock.Object, _config.Object);
            _httpContextMock = new Mock<HttpContext>();
        }

        [Fact]
        public async Task RequestHereNearBySearch_ValidRequest_ReturnsOk()
        {
            // Arrange
            var hereNearBySearchService = new Dictionary<string, dynamic>();
            var user_email = "ali_demir@yahoo.com";
            var service_id = 98;
            var result = "1";
            var response = new HereNearByRootobject { };
            var expectedJson = new HereNearByJson { status = true, limit = true, body = response };
            hereNearBySearchService.Add("lat", "36.98844"); // the search query
            hereNearBySearchService.Add("lon", "-121.97483");
            hereNearBySearchService.Add("radius", 100);
            hereNearBySearchService.Add("categorySet", "pizza,restaurant,gas_station"); // the search query

            // create a mock ClaimsPrincipal
            var claimsPrincipalMock = new Mock<ClaimsPrincipal>();

            // set up the ClaimsPrincipal to return the desired value for Claims
            claimsPrincipalMock.Setup(cp => cp.Claims)
                .Returns(new List<Claim>() { new Claim("email", user_email) });

            // create a mock HttpContext and set it up to return the ClaimsPrincipal
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(hc => hc.User)
                .Returns(claimsPrincipalMock.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "98"),
                new Claim(ClaimTypes.Email, "ali_demir@yahoo.com")
            }));
            //var controller = new HereNearBySearchController(_hereNearBySearchServiceMock.Object, _usageTrackerServiceMock.Object, _config.Object);
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            _config.Setup(c => c.GetSection("ServiceId").GetSection("here_nearby_limit").Value).Returns("1");
            _hereNearBySearchServiceMock.Setup(x => x.CreateHereNearBySearchService(hereNearBySearchService)).ReturnsAsync(response);

            // Act
            var results = await _controller.RequestHereNearBySearch(hereNearBySearchService);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var jsonResult = (OkObjectResult)results;
            var actualJson = (HereNearByJson)jsonResult.Value;
            Assert.Equal(expectedJson.status, actualJson.status);
            Assert.Equal(expectedJson.limit, actualJson.limit);
            Assert.Equal(expectedJson.body, actualJson.body);
        }
    }
}
