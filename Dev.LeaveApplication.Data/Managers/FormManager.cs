using Dev.LeaveApplication.Data.Database;
using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Data.Models;
using Dev.LeaveApplication.Data.Shared;

namespace Dev.LeaveApplication.Data.Managers;

public class FormManager : IFormManager
{
	private readonly LeaveApplicationDbContext _dbContext;

	public FormManager(LeaveApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public List<FormModel> GetAllApplications()
	{
		return _dbContext.Applications.ToList();
	}

	public bool SubmitLeaveApplicationForm(FormModel model)
	{
		_dbContext.Applications.Add(model);
		_dbContext.SaveChanges();

		return true;
	}

	public bool UpdateLeaveApplicationForm(FormModel model)
	{
		_dbContext.Applications.Update(model);
		_dbContext.SaveChanges();

		return true;
	}

	public bool WithdrawLeaveApplicationForm(Guid formId, Guid employeeId)
	{
		var leaveApplication = _dbContext.Applications.Find(formId);
		if (leaveApplication == null)
			return false;

		leaveApplication.Status = LeaveStatus.Withdrawn;
		leaveApplication.LastModifiedDate = DateTime.Now;
		leaveApplication.LastModifiedBy = employeeId;

		_dbContext.Applications.Update(leaveApplication);
		_dbContext.SaveChanges();

		return true;
	}
}
