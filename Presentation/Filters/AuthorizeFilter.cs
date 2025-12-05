using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthorizeFilter : ActionFilterAttribute {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("ids", out var idsObj) && idsObj is string[] ids)
        {
            string authenticated = context.HttpContext.User.Identity?.Name;

            foreach (var id in ids)
            {
                if (int.TryParse(id, out int resturantId))
                {
                    Resturant resturant = new Resturant();
                    if (!resturant.GetValidators().Contains(authenticated))
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
                else if (Guid.TryParse(id, out Guid menuItemId))
                {
                    MenuItem menuItem = new MenuItem();

                    if (!menuItem.GetValidators().Contains(authenticated))
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            }
        }
    }
}