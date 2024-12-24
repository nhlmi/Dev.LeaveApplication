using Dev.LeaveApplication.Web.Services.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;

namespace Dev.LeaveApplication.Web.Services;

public class EmailService : IEmailService
{
	private readonly IConfiguration _configuration;

	public EmailService(IConfiguration configuration)
	{
		_configuration = configuration;
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
}
