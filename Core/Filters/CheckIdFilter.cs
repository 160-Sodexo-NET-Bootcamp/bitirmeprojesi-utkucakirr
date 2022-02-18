using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Core.Filters
{
    /// <summary>
    /// Checking UserId using Jwt tokens claim.
    /// </summary>
    public class CheckIdFilter : BaseFilter, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = GetToken(context);
            var x = context.ActionArguments["UserId"] as int?;
            if(x!=id)
            {
                throw new Exception(message: "The entered id is not your id. Your id is: " + id + ". Try again.");
            }
        }
    }
}
