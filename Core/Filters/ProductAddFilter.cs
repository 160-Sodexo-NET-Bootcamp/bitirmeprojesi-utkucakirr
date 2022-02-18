using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Filters
{
    /// <summary>
    /// Adding OwnerId to product using Jwt tokens claim.
    /// </summary>
    public class ProductAddFilter : BaseFilter,IActionFilter
    {
        private readonly IMapper _mapper;

        public ProductAddFilter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var x = context.ActionArguments["entity"] as ProductModel;
            var item = _mapper.Map<ProductAddModel>(x);
            context.ActionArguments.Remove("entity");

            item.UserId = base.GetToken(context);
            context.ActionArguments.Add("entity", item);
            
        }
    }
}
