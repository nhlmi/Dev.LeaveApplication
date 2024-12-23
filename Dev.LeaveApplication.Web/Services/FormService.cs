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
	private readonly IMapper _mapper;

	public FormService(IFormManager formManager, IMapper mapper)
	{
		_formManager = formManager;
		_mapper = mapper;
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
