﻿using Dev.LeaveApplication.Data.Models;

namespace Dev.LeaveApplication.Data.Managers.Interfaces;

public interface IEmployeeManager
{
	List<EmployeeModel> GetAllManagers();
	List<EmployeeModel> GetAllEmployees();
	EmployeeModel FindEmployeeById(Guid employeeId);
}
