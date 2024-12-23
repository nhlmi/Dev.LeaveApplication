using Dev.LeaveApplication.Data.Database;
using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Data.Models;

namespace Dev.LeaveApplication.Data.Managers;

public class EmployeeManager : IEmployeeManager
{
	private readonly LeaveApplicationDbContext _dbContext;

	public EmployeeManager(LeaveApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public List<EmployeeModel> GetAllManagers()
	{
		return _dbContext.Employees
			.Where(x => x.IsManager)
			.Select(x => new EmployeeModel()
			{
				EmployeeId = x.EmployeeId,
				EmployeeName = x.EmployeeName
			})
			.OrderBy(x => x.EmployeeName)
			.ToList();
	}
}
