using AutoMapper;
using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Data.Models;
using Dev.LeaveApplication.Data.Shared;
using Dev.LeaveApplication.Web.Managers.Interfaces;
using Dev.LeaveApplication.Web.Models;

namespace Dev.LeaveApplication.Web.Managers;

public class FormService : IFormService
{
	private readonly IFormManager _formManager;
	private readonly IEmployeeManager _employeeManager;
	private readonly IMapper _mapper;

	public FormService(IFormManager formManager,
		IEmployeeManager employeeManager,
		IMapper mapper)
	{
		_formManager = formManager;
		_employeeManager = employeeManager;
		_mapper = mapper;
	}

	public List<ApprovalEditViewModel> GetAllApplications()
	{
		var applications = _formManager.GetAllApplications();
		var employees = _employeeManager.GetAllEmployees();

		var list = (from application in applications
					join employee in employees on application.EmployeeId equals employee.EmployeeId
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

	public bool SubmitLeaveApplicationForm(FormEditViewModel model)
	{
		var formModel = _mapper.Map<FormModel>(model);

		formModel.ApplicationId = Guid.NewGuid();
		formModel.CreatedDate = DateTime.Now;
		formModel.CreatedBy = model.EmployeeId;
		formModel.LastModifiedDate = DateTime.Now;
		formModel.LastModifiedBy = model.EmployeeId;
		formModel.Status = LeaveStatus.Submitted;

		return _formManager.SubmitLeaveApplicationForm(formModel);
	}
}
