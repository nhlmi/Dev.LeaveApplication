using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dev.LeaveApplication.Web.Models;

public class UserEditViewModel
{
	[Required]
	[DisplayName("Username")]
	public string Username { get; set; }
}
