using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Data.Models;
using Dev.LeaveApplication.Data.Shared;
using Dev.LeaveApplication.Web.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Dev.LeaveApplication.Web.Services;

public class EmailService : IEmailService
{
	private readonly IConfiguration _configuration;
	private readonly IWebHostEnvironment _env;
	private readonly IEmployeeManager _employeeManager;

	public EmailService(IConfiguration configuration, 
		IWebHostEnvironment env,
		IEmployeeManager employeeManager)
	{
		_configuration = configuration;
		_env = env;
		_employeeManager = employeeManager;
	}

	public async Task SendEmailAsync(string toEmail, string subject, string message)
	{
		var emailConfig = _configuration.GetSection("Notification:Email");
		var emailMessage = new MimeMessage();
		emailMessage.From.Add(new MailboxAddress("No Reply", emailConfig["FromEmail"]));
		emailMessage.To.Add(new MailboxAddress("", toEmail));
		emailMessage.Subject = subject;
		emailMessage.Body = new TextPart("plain") { Text = message };

		using var client = new SmtpClient();
		await client.ConnectAsync(emailConfig["SmtpServer"], int.Parse(emailConfig["SmtpPort"]), false);
		await client.AuthenticateAsync(emailConfig["SmtpUsername"], emailConfig["SmtpPassword"]);
		await client.SendAsync(emailMessage);
		await client.DisconnectAsync(true);
	}

	public async Task SendEmailAsync(string toEmail, LeaveStatus leaveStatus, FormModel model)
	{
		var emailConfig = _configuration.GetSection("Notification:Email");
		var emailMessage = new MimeMessage();
		emailMessage.From.Add(new MailboxAddress("No Reply", emailConfig["FromEmail"]));
		emailMessage.To.Add(new MailboxAddress("", toEmail));
		emailMessage.Subject = GetEmailSubject(leaveStatus);
		emailMessage.Body = GetEmailMessage(leaveStatus, model);

		using var client = new SmtpClient();
		await client.ConnectAsync(emailConfig["SmtpServer"], int.Parse(emailConfig["SmtpPort"]), false);
		await client.AuthenticateAsync(emailConfig["SmtpUsername"], emailConfig["SmtpPassword"]);
		await client.SendAsync(emailMessage);
		await client.DisconnectAsync(true);
	}

	private static string GetEmailSubject(LeaveStatus leaveStatus)
	{
		return leaveStatus switch
		{
			LeaveStatus.Submitted => "Leave Application Submitted",
			LeaveStatus.Approved => "Leave Application Approved",
			LeaveStatus.Rejected => "Leave Application Rejected",
			LeaveStatus.Withdrawn => "Leave Application Withdrawn",
			_ => "Leave Application Status",
		};
	}

	private MimeEntity GetEmailMessage(LeaveStatus leaveStatus, FormModel model)
	{
		var path = _env.WebRootPath;
		var template = GetEmailTemplate(leaveStatus);
		var templatePath = Path.Combine(path, "templates/email", template);
		var templateHtml = File.ReadAllText(templatePath);

		templateHtml = ReplaceEmailPlaceholders(templateHtml, model);

		var builder = new BodyBuilder
		{
			HtmlBody = templateHtml
		};

		return builder.ToMessageBody();
	}

	private  static string GetEmailTemplate(LeaveStatus leaveStatus)
	{
		return leaveStatus switch
		{
			LeaveStatus.Submitted => "submitted.html",
			LeaveStatus.Approved => "approved.html",
			LeaveStatus.Rejected => "rejected.html",
			LeaveStatus.Withdrawn => "withdrawn.html",
			_ => string.Empty
		};
	}

	private string ReplaceEmailPlaceholders(string templateHtml, FormModel model)
	{
		var employeeName = _employeeManager.FindEmployeeById(model.EmployeeId)?.EmployeeName;
		var managerName = _employeeManager.FindEmployeeById(model.ManagerEmployeeId)?.EmployeeName;

		templateHtml = templateHtml.Replace("{{startDatetime}}", model.StartDatetime.ToString("dd/MM/yyyy hh:mm tt"));
		templateHtml = templateHtml.Replace("{{endDatetime}}", model.EndDatetime.ToString("dd/MM/yyyy hh:mm tt"));
		templateHtml = templateHtml.Replace("{{managerName}}", managerName);
		templateHtml = templateHtml.Replace("{{employeeName}}", employeeName);
		templateHtml = templateHtml.Replace("{{justification}}", model.Justification);

		return templateHtml;
	}

}
