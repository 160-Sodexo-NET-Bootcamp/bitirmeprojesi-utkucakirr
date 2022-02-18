using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Filters
{
    /// <summary>
    /// Adding OffererId to offer using Jwt token.
    /// </summary>
    public class OfferAddFilterP : BaseFilter, IActionFilter
    {
        private readonly IMapper _mapper;

        public OfferAddFilterP(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var x = context.ActionArguments["entity"] as OfferPercentageModel;
            var item = _mapper.Map<OfferAddModelP>(x);
            context.ActionArguments.Remove("entity");

            item.OffererId = base.GetToken(context);
            context.ActionArguments.Add("entity", item);
        }
    }
}
