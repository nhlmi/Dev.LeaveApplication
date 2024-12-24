using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dev.LeaveApplication.Web.Services;

public class EmployeeService : IEmployeeService
{
	private readonly IEmployeeManager _employeeManager;

	public EmployeeService(IEmployeeManager employeeManager)
	{
		_employeeManager = employeeManager;
	}

	public IEnumerable<SelectListItem> GetAllEmployees()
	{
		return _employeeManager.GetAllEmployees()
			.Select(x => new SelectListItem
			{
				Value = x.EmployeeId.ToString(),
				Text = x.EmployeeName
			});
	}

	public IEnumerable<SelectListItem> GetAllManagers()
	{
		return _employeeManager.GetAllManagers()
			.Select(x => new SelectListItem
			{
				Value = x.EmployeeId.ToString(),
				Text = x.EmployeeName
			});
	}
}
