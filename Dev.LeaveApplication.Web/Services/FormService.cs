using AutoMapper;
using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Data.Models;
using Dev.LeaveApplication.Data.Shared;
using Dev.LeaveApplication.Web.Managers.Interfaces;
using Dev.LeaveApplication.Web.Models;
using Dev.LeaveApplication.Web.Services.Interfaces;

namespace Dev.LeaveApplication.Web.Managers;

public class FormService : IFormService
{
	private readonly IFormManager _formManager;
	private readonly IEmployeeManager _employeeManager;
	private readonly IMapper _mapper;
	private readonly IEmailService _emailService;
	private readonly IUserService _userService;

	public FormService(IFormManager formManager,
		IEmployeeManager employeeManager,
		IMapper mapper,
		IEmailService emailService,
		IUserService userService)
	{
		_formManager = formManager;
		_employeeManager = employeeManager;
		_mapper = mapper;
		_emailService = emailService;
		_userService = userService;
	}

	public bool ApproveLeaveApplicationForm(Guid applicationId, Guid managerEmployeeId)
	{
		var formModel = _formManager.FindApplicationById(applicationId);
		if (formModel == null) return false;

		formModel.Status = LeaveStatus.Approved;
		formModel.LastModifiedDate = DateTime.Now;
		formModel.LastModifiedBy = managerEmployeeId;

		var result = UpdateLeaveApplicationFormStatus(formModel);
		if(!result) return false;

		//Send Email to Employee
		var employee = _employeeManager.FindEmployeeById(formModel.EmployeeId);
		if(employee == null) return false;

		_emailService.SendEmailAsync(employee.Email, "Leave Application Approved", "Your leave application has been approved.");

		return result;
	}

	public List<ApprovalEditViewModel> GetAllApplications(HttpContext httpContext)
	{
		var applications = _formManager.GetAllApplications();
		var employees = _employeeManager.GetAllEmployees();
		var managerEmployeeId = _userService.GetSignedInId(httpContext);

		var list = (from application in applications
					join employee in employees on application.EmployeeId equals employee.EmployeeId
					where application.ManagerEmployeeId.Equals(managerEmployeeId)
					select new ApprovalEditViewModel
					{
						ApplicationId = application.ApplicationId,
						EmployeeId = application.EmployeeId,
						EmployeeName = employee.EmployeeName,
						StartDatetime = application.StartDatetime,
						EndDatetime = application.EndDatetime,
						Justification = application.Justification,
						CreatedDate = application.CreatedDate,
						LastModifiedDate = application.LastModifiedDate,
						Status = application.Status
					})
					.OrderByDescending(x => x.CreatedDate)
					.ToList();

		return list;
	}

	public List<ApprovalEditViewModel> GetApplicationsByEmployee(HttpContext httpContext)
	{
		var applications = _formManager.GetAllApplications();
		var employees = _employeeManager.GetAllEmployees();
		var employeeId = _userService.GetSignedInId(httpContext);

		var list = (from application in applications
					join manager in employees on application.ManagerEmployeeId equals manager.EmployeeId
					where application.EmployeeId.Equals(employeeId)
					select new ApprovalEditViewModel
					{
						ApplicationId = application.ApplicationId,
						EmployeeId = application.EmployeeId,
						StartDatetime = application.StartDatetime,
						EndDatetime = application.EndDatetime,
						Justification = application.Justification,
						ManagerName = manager.EmployeeName,
						CreatedDate = application.CreatedDate,
						LastModifiedDate = application.LastModifiedDate,
						Status = application.Status
					})
					.OrderByDescending(x => x.CreatedDate)
					.ToList();

		return list;
	}

	public bool RejectLeaveApplicationForm(Guid applicationId, Guid managerEmployeeId)
	{
		var formModel = _formManager.FindApplicationById(applicationId);
		if (formModel == null) return false;

		formModel.Status = LeaveStatus.Rejected;
		formModel.LastModifiedDate = DateTime.Now;
		formModel.LastModifiedBy = managerEmployeeId;

		var result = UpdateLeaveApplicationFormStatus(formModel);
		if(!result) return false;

		//Send Email to Employee
		var employee = _employeeManager.FindEmployeeById(formModel.EmployeeId);
		if (employee == null) return false;

		_emailService.SendEmailAsync(employee.Email, "Leave Application Rejected", "Your leave application has been rejected.");

		return result;
	}

	public bool SubmitLeaveApplicationForm(FormEditViewModel model)
	{
		var formModel = _mapper.Map<FormModel>(model);

		formModel.ApplicationId = Guid.NewGuid();
		formModel.CreatedDate = DateTime.Now;
		formModel.CreatedBy = model.EmployeeId;
		formModel.LastModifiedDate = DateTime.Now;
		formModel.LastModifiedBy = model.EmployeeId;
		formModel.Status = LeaveStatus.Submitted;

		var result = _formManager.SubmitLeaveApplicationForm(formModel);

		//Send Email to Manager
		var manager = _employeeManager.FindEmployeeById(model.ManagerEmployeeId);
		if (manager == null) return false;

		_emailService.SendEmailAsync(manager.Email, "Leave Application Submitted", "A new leave application has been submitted.");

		return result;
	}

	public bool WithdrawLeaveApplicationForm(Guid applicationId, Guid employeeId)
	{
		var formModel = _formManager.FindApplicationById(applicationId);
		if (formModel == null) return false;

		formModel.Status = LeaveStatus.Withdrawn;
		formModel.LastModifiedDate = DateTime.Now;
		formModel.LastModifiedBy = employeeId;

		var result = UpdateLeaveApplicationFormStatus(formModel);
		if (!result) return false;

		//Send Email to Employee
		var employee = _employeeManager.FindEmployeeById(formModel.EmployeeId);
		if (employee == null) return false;

		_emailService.SendEmailAsync(employee.Email, "Leave Application Withdraw", "You have withdraw your leave application.");

		return result;
	}

	private bool UpdateLeaveApplicationFormStatus(FormModel model)
	{
		return _formManager.UpdateLeaveApplicationForm(model);
	}
}
