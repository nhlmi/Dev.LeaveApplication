using Dev.LeaveApplication.Data.Managers.Interfaces;
using Dev.LeaveApplication.Data.Shared;
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

		public HomeController(ILogger<HomeController> logger, IEmployeeManager employeeManager)
        {
            _logger = logger;
			_employeeManager = employeeManager;
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

		public IActionResult Form()
		{
			FormEditViewModel model = new();

			model.Managers = _employeeManager.GetAllManagers()
				.Select(x => new SelectListItem
				{
					Value = x.EmployeeId.ToString(),
					Text = x.EmployeeName
				});

			return View(model);
		}

		[HttpPost]
		public IActionResult SubmitForm(FormEditViewModel model)
		{
			if (!ModelState.IsValid)
				return View("Form", model);


			var test = Guid.NewGuid();

			model.Status = LeaveStatus.Submitted;
			model.CreatedDate = DateTime.Now;
			model.CreatedBy = test;
			model.LastModifiedDate = DateTime.Now;
			model.LastModifiedBy = test;

			return View("Form");
		}

		public IActionResult Approval()
		{
			ApprovalEditViewModels models = new();

			return View(models);
		}
	}
}
