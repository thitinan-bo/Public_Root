using Microsoft.AspNetCore.Mvc;

namespace DynamicsReportingApp.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
