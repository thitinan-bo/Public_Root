using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using DynamicsReportingWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicsReportingWeb.Controllers
{
    public class GroupController : Controller
    {
        private readonly IApiService _apiService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IApiService apiService, ILogger<GroupController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            // ตรวจสอบ Session
            var username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Authen");
            }

            // ส่งข้อมูล Branch ไปให้ View
            ViewBag.BranchCode = HttpContext.Session.GetString("BranchCode");
            ViewBag.BranchName = HttpContext.Session.GetString("BranchName");
            ViewBag.Username = username;
            ViewBag.UserId = userId;

            try
            {
                // เรียก API เพื่อดึงข้อมูล Group Reports ตามสิทธิ์ของ User
                var reqUserGroup = new ReqUserGroup
                {
                    UserID = userId ?? 0,
                    currentPage = page,
                    pageSize = pageSize
                };

                var groupReports = await _apiService.GetGroupReportByUserIdAsync(reqUserGroup);

                return View(groupReports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Group Reports for UserId {UserId}", userId);
                TempData["ErrorMessage"] = "ไม่สามารถโหลดข้อมูล Group Reports ได้ กรุณาลองใหม่ภายหลัง";
                return View(new ResponseDataModel<PaginatedResult<GroupReportUseModel>>());
            }
        }
    }
}
