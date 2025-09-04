using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        //if (string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
        //    return RedirectToAction("Login", "Account");

        //// Mock เมนู
        //ViewBag.Menus = new List<MenuItem>
        //{
        //    new MenuItem { Title = "Dashboard", Url = "/dashboard" },
        //    new MenuItem { Title = "Reports", Url = "/report" },
        //    new MenuItem { Title = "Settings", Url = "/settings" }
        //};

        return View();
    }
}
