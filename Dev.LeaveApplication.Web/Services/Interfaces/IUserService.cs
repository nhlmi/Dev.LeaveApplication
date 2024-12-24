namespace Dev.LeaveApplication.Web.Services.Interfaces;

public interface IUserService
{
	bool SignIn(string username, HttpContext httpContext);
	void SignOut(HttpContext httpContext);
	bool IsSignedIn(HttpContext httpContext);
	string GetSignedInUser(HttpContext httpContext);
	bool IsManager(HttpContext httpContext);
	Guid GetSignedInId(HttpContext httpContext);
}
