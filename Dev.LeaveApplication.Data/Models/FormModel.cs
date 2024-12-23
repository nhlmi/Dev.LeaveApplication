using Dev.LeaveApplication.Data.Shared;
using System.ComponentModel.DataAnnotations;

namespace Dev.LeaveApplication.Data.Models;

public class FormModel
{
	[Key]
	public Guid FormId { get; set; }
	public Guid EmployeeId { get; set; }
	public DateTime StartDatetime { get; set; }
	public DateTime EndDatetime { get; set; }
	public string Justification { get; set; }
	public LeaveStatus Status { get; set; }
	public DateTime CreatedDate { get; set; }
	public Guid CreatedBy { get; set; }
	public DateTime LastModifiedDate { get; set; }
	public Guid LastModifiedBy { get; set; }
}
