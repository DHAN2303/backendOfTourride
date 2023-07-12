using AllrideApiCore.Dtos;
using AllrideApiService.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AllrideApi.Filter
{
    public class ValidateFilterAttributeController:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x=>x.ErrorMessage).ToString();
            context.Result = new BadRequestObjectResult(CustomResponse<NoContentDto>.Fail(400, errors));
        }
    }
}
