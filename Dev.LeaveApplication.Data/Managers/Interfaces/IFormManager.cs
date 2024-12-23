using Dev.LeaveApplication.Data.Models;

namespace Dev.LeaveApplication.Data.Managers.Interfaces;

public interface IFormManager
{
	bool SubmitLeaveApplicationForm(FormModel model);
	bool UpdateLeaveApplicationForm(FormModel model);
	bool WithdrawLeaveApplicationForm(Guid formId, Guid employeeId);
}
