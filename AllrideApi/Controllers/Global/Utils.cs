using AllrideApiService.Response;
using Microsoft.AspNetCore.Mvc;

namespace AllrideApi.Controllers.Global
{
    //  return Utils.GetInstance().getGlobalResponse(respBody);
    //  return Ok(CustomResponse<object>.Success(savedPath, true));
    // private readonly ILogger<GroupsController> _logger;
    //_logger.LogError(ex.Message + " " + ex.InnerException.ToString());
    public class Utils: ControllerBase
    {
        private static Utils _instance = null;
        public IActionResult getGlobalResponse(Object respBody)
        {
            if (respBody == null)
            {
                try
                {
                    return Ok(CustomResponse<object>.Success(respBody, true));
                }
                catch
                {
                    return Ok(CustomResponse<object>.Success(ErrorEnumResponse.BadRequest, false));
                }
            }
            else
            {
                return Ok(CustomResponse<object>.Success(ErrorEnumResponse.NullData, false));
            }
        }

        public static Utils GetInstance()
        {
            if (_instance != null) return _instance;
            return _instance = new Utils();
        }
    }


}
