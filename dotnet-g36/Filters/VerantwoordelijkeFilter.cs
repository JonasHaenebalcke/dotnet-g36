using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g36.Filters
{
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class VerantwoordelijkeFilter : ActionFilterAttribute
    {
        private readonly IGebruikerRepository _gebruikerRepository;

        public VerantwoordelijkeFilter(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["verantwoordelijke"] = context.HttpContext.User.Identity.IsAuthenticated ?
                    _gebruikerRepository.GetVerantwoordelijkeByUsername(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }



}
