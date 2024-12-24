namespace Dev.LeaveApplication.Web.Services.Interfaces;

public interface IEmailService
{
	Task SendEmailAsync(string toEmail,string subject, string message);
}
