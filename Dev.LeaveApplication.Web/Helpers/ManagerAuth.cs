using Dev.LeaveApplication.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dev.LeaveApplication.Web.Helpers;

public class ManagerAuth : ActionFilterAttribute
{
	public override void OnActionExecuting(ActionExecutingContext context)
	{
		var userService = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
		if (userService == null || !userService.IsSignedIn(context.HttpContext) || !userService.IsManager(context.HttpContext))
		{
			context.Result = new RedirectToActionResult("SignIn", "User", null);
		}
	}
}
