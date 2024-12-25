using Dev.LeaveApplication.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dev.LeaveApplication.Web.Controllers;

public class UserController : Controller
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpGet]
	public IActionResult SignIn()
	{
		if(HttpContext.User.Identity.IsAuthenticated)
			return RedirectToAction("Index", "Home");

		return View();
	}

	[HttpPost]
	public IActionResult SignIn(string username)
	{
		if(_userService.SignIn(username, HttpContext))
			return RedirectToAction("Index", "Home");

		return View();
	}

	public IActionResult SignOut()
	{
		_userService.SignOut(HttpContext);

		return RedirectToAction("SignIn");
	}
}
