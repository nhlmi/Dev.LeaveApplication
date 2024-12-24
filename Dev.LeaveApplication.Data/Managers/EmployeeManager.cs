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

	public EmployeeModel FindEmployeeById(Guid employeeId)
	{
		return _dbContext.Employees.Find(employeeId);
	}

	public List<EmployeeModel> GetAllEmployees()
	{
		return _dbContext.Employees
			.Select(x => new EmployeeModel()
			{
				EmployeeId = x.EmployeeId,
				EmployeeName = x.EmployeeName
			})
			.OrderBy(x => x.EmployeeName)
			.ToList();
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
