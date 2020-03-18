using dotnet_g36.Models.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_g036.Filters
{
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class GebruikerFilter : ActionFilterAttribute
    {
        private readonly IUserRepository _gebruikerRepository;

        public GebruikerFilter(IUserRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.ActionArguments["gebruiker"] = context.HttpContext.User.Identity.IsAuthenticated ?
                    _gebruikerRepository.GetDeelnemerByUsername(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }
}