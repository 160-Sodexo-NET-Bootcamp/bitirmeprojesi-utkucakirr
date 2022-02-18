using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Filters
{
    /// <summary>
    /// Adding OffererId to offer using Jwt tokens claim.
    /// </summary>
    public class OfferAddFilter : BaseFilter, IActionFilter
    {
        private readonly IMapper _mapper;

        public OfferAddFilter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var x = context.ActionArguments["entity"] as OfferModel;
            var item = _mapper.Map<OfferAddModel>(x);
            context.ActionArguments.Remove("entity");

            item.OffererId = base.GetToken(context);
            context.ActionArguments.Add("entity", item);
        }
    }
}
