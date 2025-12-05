using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class AuthorizeFilter : ActionFilterAttribute {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("ids", out var idsObj) && idsObj is string[] ids)
        {
            string authenticated = context.HttpContext.User.Identity?.Name;
            var itemsRepository = context.HttpContext.RequestServices.GetRequiredKeyedService<IItemsRepository>("db");

            foreach (var id in ids)
            {
                if (int.TryParse(id, out int resturantId))
                {
                    Resturant resturant = itemsRepository.GetResturants().FirstOrDefault(r => r.Id == resturantId);
                    if (resturant == null || !resturant.GetValidators().Contains(authenticated))
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
                else if (Guid.TryParse(id, out Guid menuItemId))
                {
                    MenuItem menuItem = itemsRepository.GetMenuItems()
                        .FirstOrDefault(m => m.Id == menuItemId);

                    if (menuItem == null || !menuItem.GetValidators().Contains(authenticated))
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }
        }
    }
}