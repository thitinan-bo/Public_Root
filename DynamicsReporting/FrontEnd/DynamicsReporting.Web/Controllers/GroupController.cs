using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using DynamicsReporting.Web.Services;

using Microsoft.AspNetCore.Mvc;

namespace DynamicsReporting.Web.Controllers
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
                    UserId = userId ?? 0,
                    CurrentPage = page,
                    PageSize = pageSize
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




        // แสดงหน้า ViewReport
        public async Task<IActionResult> ViewReport(int reportId)
        {
            var config = await _apiService.GetConfigReport(reportId);

            var responseData = new ResponseDataModel<ReportConfigModel>
            {
                Data = config.Data,
                ErrorCode = "0",
                Status = ResponseStatus.Success,
                ErrorType = ResponseStatus.Success,
                StatusCode = 200
            };

            return View(responseData);
        }

        // Execute Report → Return JSON
        [HttpPost]
        public async Task<IActionResult> ExecuteReport([FromBody] ReportRequest request)
        {
            try
            {
                var data = await _apiService.ExecuteReportPage(request);
                return Json(new { success = true, data });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }



    }
}
