using AllrideApiCore.Dtos;
using AllrideApiService.Response;

namespace AllrideApiService.Configuration
{
    public static class ValidationVehicleType
    {
        public static CustomResponse<NoContentDto> VehicleTypeValidation(string vehicleType)
        {
            List<string> vehiclerList = new List<string> { "car", "truck"};
            List<ErrorEnumResponse> errorEnumResponses = new();
            if (!vehiclerList.Contains(vehicleType))
            {
                errorEnumResponses.Add(ErrorEnumResponse.UnsupportedVehicleType);
                return CustomResponse<NoContentDto>.Fail(errorEnumResponses, false);
            }
            return CustomResponse<NoContentDto>.Success(vehicleType, true);
        }
    }
}
