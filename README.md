# Dev.LeaveApplication.Web

### Tech
* .Net8 C#
*  Notification (Email): [Ethereal Email](https://ethereal.email), [MailKit](https://github.com/jstedfast/MailKit), [MimeKit](https://github.com/jstedfast/MimeKit)
* [Bootstrap](https://getbootstrap.com)

---
* Database/Tables Schemes: [Dev.LeaveApplication.Data/SQL](https://github.com/nhlmi/Dev.LeaveApplication/tree/master/Dev.LeaveApplication.Data/SQL) > 20241227_InitScript.sql
  - Includes dummy data for **Employees** and **Users**
* Email Templates: [Email Templates](Dev.LeaveApplication.Web/wwwroot/templates/email)
* Set **Dev.LeaveApplication.Web** as Startup, Run as usual.
* To check the **Email Notification**:
  - Go to [Ethreal Email (Login)](https://ethereal.email/login)
  - Use the credentials in [appsettings.json](Dev.LeaveApplication.Web/appsettings.json) to login
  - Click on [Messages](https://ethereal.email/messages)
* Pages/Views
  - Sign In (**_only username_**)
  - My Leave
    - Leave balance
    - All leave application submitted by the employee
  - Leave Application Form
    - To apply the leave, then send an email to Manager once Submitted
    - Withdraw the leave application, if not Rejected
  - Leave Application List
    - Only for Manager (Table Users: IsManager is True)
    - Approve or Reject the leave application, then send an email to the Employee

## References
1. https://jasonwatmore.com/post/2022/03/11/net-6-send-an-email-via-smtp-with-mailkit
