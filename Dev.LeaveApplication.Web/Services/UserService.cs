using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Dev.LeaveApplication.Web.Services;

public class UserService : IUserService
{
	private readonly IUserManager _userManager;
	private readonly IEmployeeManager _employeeManager;

	public UserService(IUserManager userManager, IEmployeeManager employeeManager)
	{
		_userManager = userManager;
		_employeeManager = employeeManager;
	}

	public string GetSignedInUser(HttpContext httpContext)
	{
		return httpContext.User.Identity.Name;
	}

	public bool IsManager(HttpContext httpContext)
	{
		var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "IsManager");
		return claim != null && bool.Parse(claim.Value);
	}

	public bool IsSignedIn(HttpContext httpContext)
	{
		return httpContext.User.Identity.IsAuthenticated;
	}

	public bool SignIn(string username, HttpContext httpContext)
	{
		//Get user details
		var user = _userManager.SignIn(username);
		if (user == null) return false;

		//Get employee details
		var employee = _employeeManager.FindEmployeeById(user.EmployeeId);
		if (employee == null) return false;

		//Claims
		var claims = new List<Claim>
		{
			new (ClaimTypes.Name, employee.EmployeeName),
			new ("IsManager", employee.IsManager.ToString())
		};

		var claimsIdentity = new ClaimsIdentity(claims, "LeaveAppAuth");
		var authProperties = new AuthenticationProperties { IsPersistent = true };

		httpContext.SignInAsync("LeaveAppAuth", new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

		return true;
	}

	public void SignOut(HttpContext httpContext)
	{
		httpContext.SignOutAsync("LeaveAppAuth").Wait();
	}
}
