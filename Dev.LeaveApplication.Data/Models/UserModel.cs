using System.ComponentModel.DataAnnotations;

namespace Dev.LeaveApplication.Data.Models;

public class UserModel
{
	[Key]
	public string Username { get; set; }
	public Guid EmployeeId { get; set; }
}
