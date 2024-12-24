using Dev.LeaveApplication.Web.Models;

namespace Dev.LeaveApplication.Web.Managers.Interfaces;

public interface IFormService
{
	bool SubmitLeaveApplicationForm(FormEditViewModel model);
	List<ApprovalEditViewModel> GetAllApplications();
	bool ApproveLeaveApplicationForm(Guid applicationId, Guid managerEmployeeId);
	bool RejectLeaveApplicationForm(Guid applicationId, Guid managerEmployeeId);
}
