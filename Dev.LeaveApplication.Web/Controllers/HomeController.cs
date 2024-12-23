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

		public IActionResult Approval()
		{
			ApprovalEditViewModels models = new();

			return View(models);
		}
	}
}
