using System.ComponentModel.DataAnnotations;

namespace Dev.LeaveApplication.Data.Models;

public class EmployeeModel
{
	[Key]
	public Guid EmployeeId { get; set; }
	public string EmployeeName { get; set; }
	public bool IsManager { get; set; }
	public string? Email { get; set; }
}
