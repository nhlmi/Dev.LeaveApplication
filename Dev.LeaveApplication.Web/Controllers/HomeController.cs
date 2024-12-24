using Dev.LeaveApplication.Web.Helpers;
using Dev.LeaveApplication.Web.Managers.Interfaces;
using Dev.LeaveApplication.Web.Models;
using Dev.LeaveApplication.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dev.LeaveApplication.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IEmployeeService _employeeService;
		private readonly IFormService _formService;
		private readonly IUserService _userService;

		public HomeController(ILogger<HomeController> logger,
			IEmployeeService employeeService,
			IFormService formService,
			IUserService userService)
		{
			_logger = logger;
			_employeeService = employeeService;
			_formService = formService;
			_userService = userService;
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
			model.EmployeeId = _userService.GetSignedInId(HttpContext);
			model.Employees = _employeeService.GetAllEmployees();
			model.Managers = _employeeService.GetAllManagers();

			return View(model);
		}

		[HttpPost]
		public IActionResult SubmitForm(FormEditViewModel model)
		{
			model.EmployeeId = _userService.GetSignedInId(HttpContext);

			if (!ModelState.IsValid)
				return RedirectToAction("Form", model);

			_formService.SubmitLeaveApplicationForm(model);

			return View("Index");
		}

		[ManagerAuth]
		[HttpGet]
		public IActionResult Approval()
		{
			GetAllApplications();

			return View();
		}

		[HttpGet]
		private void GetAllApplications()
		{
			ViewBag.Applications = _formService.GetAllApplications(HttpContext);
		}

		[HttpPost]
		public IActionResult ApproveForm([FromBody] Guid applicationId)
		{
			//Get the manager employee id from the logged in user
			Guid managerEmployeeId = _userService.GetSignedInId(HttpContext);

			_formService.ApproveLeaveApplicationForm(applicationId, managerEmployeeId);

			GetAllApplications();

			return View("Approval");
		}

		[HttpPost]
		public IActionResult RejectForm([FromBody] Guid applicationId)
		{
			//Get the manager employee id from the logged in user
			Guid managerEmployeeId = _userService.GetSignedInId(HttpContext);

			_formService.RejectLeaveApplicationForm(applicationId, managerEmployeeId);

			GetAllApplications();

			return View("Approval");
		}
	}
}
