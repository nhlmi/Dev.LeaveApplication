using Dev.LeaveApplication.Data.Models;

namespace Dev.LeaveApplication.Data.Managers.Interfaces;

public interface IUserManager
{
	UserModel SignIn(string username);
}
