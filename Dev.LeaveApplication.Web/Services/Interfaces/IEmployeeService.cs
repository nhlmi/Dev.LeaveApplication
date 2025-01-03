﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dev.LeaveApplication.Web.Services.Interfaces;

public interface IEmployeeService
{
	IEnumerable<SelectListItem> GetAllManagers(Guid employeeId);
	IEnumerable<SelectListItem> GetAllEmployees();
}
