using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace AllrideApi.Filter
{
    public class HttpOnlyActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isHttps = context.HttpContext.Request.Scheme == Uri.UriSchemeHttps;
            if (isHttps == false && HttpsNotSupported(context))
            {
                context.Result = new BadRequestResult();
            }
        }

        private bool HttpsNotSupported(ActionContext context)
        {
            return context.ActionDescriptor is ControllerActionDescriptor x &&
            !x.ControllerTypeInfo.GetCustomAttributes<AllowHttpsAttribute>().Any() &&
            !x.MethodInfo.GetCustomAttributes<AllowHttpsAttribute>().Any();
        }
    }
}