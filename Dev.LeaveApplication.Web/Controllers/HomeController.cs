using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Web.Managers.Interfaces;
using Dev.LeaveApplication.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Dev.LeaveApplication.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IEmployeeManager _employeeManager;
		private readonly IFormService _formService;

		public HomeController(ILogger<HomeController> logger,
			IEmployeeManager employeeManager,
			IFormService formService)
		{
			_logger = logger;
			_employeeManager = employeeManager;
			_formService = formService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Form(FormEditViewModel model)
		{
			var employees = _employeeManager.GetAllEmployees()
				.Select(x => new SelectListItem
				{
					Value = x.EmployeeId.ToString(),
					Text = x.EmployeeName
				});
			var managers = _employeeManager.GetAllManagers()
				.Select(x => new SelectListItem
				{
					Value = x.EmployeeId.ToString(),
					Text = x.EmployeeName
				});

			model.Employees = employees;
			model.Managers = managers;

			return View(model);
		}

		[HttpPost]
		public IActionResult SubmitForm(FormEditViewModel model)
		{
			if (!ModelState.IsValid)
				return RedirectToAction("Form", model);

			_formService.SubmitLeaveApplicationForm(model);

			return View("Index");
		}

		[HttpGet]
		public IActionResult Approval()
		{
			GetAllApplications();

			return View();
		}

		[HttpGet]
		private void GetAllApplications()
		{
			ViewBag.Applications = _formService.GetAllApplications();
		}

		[HttpPost]
		public IActionResult ApproveForm([FromBody] Guid applicationId)
		{
			//TODO: Get the manager employee id from the logged in user
			Guid managerEmployeeId = Guid.Empty;

			_formService.ApproveLeaveApplicationForm(applicationId, managerEmployeeId);

			GetAllApplications();

			return View("Approval");
		}

		[HttpPost]
		public IActionResult RejectForm([FromBody] Guid applicationId)
		{
			//TODO: Get the manager employee id from the logged in user
			Guid managerEmployeeId = Guid.Empty;

			_formService.RejectLeaveApplicationForm(applicationId, managerEmployeeId);

			GetAllApplications();

			return View("Approval");
		}
	}
}
