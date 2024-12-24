using Dev.LeaveApplication.Data.Database;
using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Data.Models;

namespace Dev.LeaveApplication.Data.Managers;

public class UserManager : IUserManager
{
	private readonly LeaveApplicationDbContext _dbContext;

	public UserManager(LeaveApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public UserModel SignIn(string username)
	{
		return _dbContext.Users.Find(username);
	}
}
