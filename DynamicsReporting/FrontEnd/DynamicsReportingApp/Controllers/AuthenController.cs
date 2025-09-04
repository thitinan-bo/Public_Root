using DynamicsReporting.Models.Authen;
using DynamicsReportingApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
public class AuthenController : Controller
{
    private readonly IApiService _apiService;
    private const string SESSION_BRANCH_CODE = "BranchCode";
    private const string SESSION_BRANCH_NAME = "BranchName";
    private const string SESSION_DEFAULT_SERVER = "DefaultServer";
    public AuthenController(IApiService apiService)
    {
        _apiService = apiService;
    }


    [HttpGet]
    public async Task<IActionResult> Login()
    {
        var model = new AuthenRequestModel();
        List<SelectListItem> items = new List<SelectListItem>();

        try
        {
            var result = await _apiService.BranchAll(); // คืนค่า List<BranchModel>

            ViewBag.ddlBranches = result
               .Select(b => new SelectListItem
               {
                   Value = b.branch_code,
                   Text = b.branch_name
               })
               .ToList();

            //foreach (var item in result.ToList())
            //{
            //    //model.Branches.Add(new SelectListItem { Text = item.branch_name, Value = item.branch_code });

            //    items.Add(new SelectListItem { Text = item.branch_name, Value = item.branch_code });

            //}

            //ViewBag.ddlBranches = items;


        }
        catch
        {
            ModelState.AddModelError("", "เกิดข้อผิดพลาดในการเชื่อมต่อ กรุณาลองใหม่อีกครั้ง");
        }


        return View(model);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return RedirectToAction(nameof(Login));
    }
 
    [HttpPost]
    public async Task<IActionResult> Login(AuthenRequestModel model)
    {
        //if (!ModelState.IsValid)
        //{
        //    await PopulateBranches(model);
        //    return View(model);
        //}

        try
        {
            model.Username = model.Username?.Trim();


            var response = await _apiService.Authen(model);
            if (response?.Data != null && response.Data.IsAuthenticated)
            {
                // Store session data
                HttpContext.Session.SetString("BranchCode", response.Data.BranchCode ?? "");
                HttpContext.Session.SetString("BranchName", response.Data.BranchName ?? "");
                HttpContext.Session.SetString("DefaultServer", response.Data.DefaultServer ?? "");

                HttpContext.Session.SetString("Username", model.Username ?? "");
                HttpContext.Session.SetInt32("UserId", response.Data.UserId ?? 0);

                return RedirectToAction("Index", "Group");
            }

            ModelState.AddModelError("", "User หรือ Password ไม่ถูกต้อง");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "เกิดข้อผิดพลาดในการเชื่อมต่อ กรุณาลองใหม่อีกครั้ง" + ex.Message.ToString());
            // Log the exception
        }

        // await PopulateBranches(model);
        return View(model);
    }
}
