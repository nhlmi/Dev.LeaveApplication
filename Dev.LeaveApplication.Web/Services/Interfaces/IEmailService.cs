using Dev.LeaveApplication.Data.Models;
using Dev.LeaveApplication.Data.Shared;

namespace Dev.LeaveApplication.Web.Services.Interfaces;

public interface IEmailService
{
	Task SendEmailAsync(string toEmail,string subject, string message);
	Task SendEmailAsync(string toEmail, LeaveStatus leaveStatus, FormModel model);
}
