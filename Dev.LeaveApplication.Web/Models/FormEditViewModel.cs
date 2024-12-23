using Dev.LeaveApplication.Data.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Dev.LeaveApplication.Web.Models;

public class FormEditViewModel
{
	public Guid FormId { get; set; }

	[Required]
	[Display(Name = "Employee Name")]
	public Guid EmployeeId { get; set; }

	public IEnumerable<SelectListItem> Employees { get; set; } = [];

	[Required]
	[Display(Name = "Start Date and Time")]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
	public DateTime StartDatetime { get; set; } = DateTime.Now;

	[Required]
	[Display(Name = "End Date and Time")]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
	public DateTime EndDatetime { get; set; } = DateTime.Now;

	[Required]
	[Display(Name = "Justification")]
	[MaxLength(100, ErrorMessage = "'{0}' is limited to {1} characters.")]
	public string Justification { get; set; }

	[Required]
	[Display(Name = "Manager Name")]
	public Guid ManagerEmployeeId { get; set; }

	public IEnumerable<SelectListItem> Managers { get; set; } = [];

	public LeaveStatus Status { get; set; }

	public string? StatusDescription { get; set; }

	public DateTime CreatedDate { get; set; }

	public Guid CreatedBy { get; set; }

	public DateTime LastModifiedDate { get; set; }

	public Guid LastModifiedBy { get; set; }
}
