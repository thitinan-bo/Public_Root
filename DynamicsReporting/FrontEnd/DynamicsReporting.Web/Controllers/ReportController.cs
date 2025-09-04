using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using DynamicsReporting.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace DynamicsReporting.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IApiService _apiService;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IApiService apiService, ILogger<ReportController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? groupId = null, int page = 1, int pageSize = 10)
        {
            // ตรวจสอบ Session
            var username = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Authen");
            }

            // ส่งข้อมูลไปให้ View
            ViewBag.BranchCode = HttpContext.Session.GetString("BranchCode");
            ViewBag.BranchName = HttpContext.Session.GetString("BranchName");
            ViewBag.Username = username;
            ViewBag.UserId = userId;
            ViewBag.GroupId = groupId;

            try
            {
                // เรียก API เพื่อดึงข้อมูล Reports
                var request = new ReqUserReport
                {
                    UserID = userId ?? 0,
                    GroupID = (int)groupId,  
                    currentPage = page,
                    pageSize = pageSize
                };

                var reports = await _apiService.GetReportByUserId(request);

       
                //_logger.LogInformation("Retrieved {Count} reports for User {UserId}, Group {GroupId}",
                //    reports?.Data?.Data?.Count ?? 0, userId, groupId);

                return View(reports);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error loading Reports for UserId {UserId}, GroupId {GroupId}", userId, groupId);
                //TempData["ErrorMessage"] = "ไม่สามารถโหลดข้อมูล Reports ได้ กรุณาลองใหม่ภายหลัง";

                // Return empty model with error
                var emptyModel = new ResponseDataModel<PaginatedResult<ReportModel>>
                {
                    Data = new PaginatedResult<ReportModel>
                    {
                        Data = new List<ReportModel>(),
                        Pagination = new Pagination { CurrentPage = 1, PageSize = pageSize, TotalRecords = 0 }
                    },
                    ErrorMessage = ex.Message,
                    Status = "Error"
                };

                return View(emptyModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewReport(int reportId)
        {
            // ตรวจสอบ Session
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Authen");
            }

            try
            {
                // Logic สำหรับแสดง Report Details
                // var reportDetails = await _apiService.GetReportDetailsAsync(reportId);

                ViewBag.ReportId = reportId;
                return View(); // สร้าง View สำหรับแสดง report details
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing report {ReportId}", reportId);
                TempData["ErrorMessage"] = "ไม่สามารถโหลดรายงานได้";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GenerateReport(int reportId)
        {
            // ตรวจสอบ Session
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Authen");
            }

            try
            {
                // Logic สำหรับ generate report (PDF, Excel, etc.)
                // var reportData = await _apiService.GenerateReportAsync(reportId);

                TempData["SuccessMessage"] = $"Report {reportId} has been generated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating report {ReportId}", reportId);
                TempData["ErrorMessage"] = "ไม่สามารถสร้างรายงานได้";
                return RedirectToAction("Index");
            }
        }
    }
}