
using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using DynamicsReporting.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

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

        // แสดงหน้า View Report
        public async Task<IActionResult> ViewReport(int reportId)
        {
            var config = await _apiService.GetConfigReport(reportId);

            if (config == null)
            {
                config = new ResponseDataModel<ReportConfigModel>
                {
                    Data = new ReportConfigModel(),
                    ErrorMessage = "No config data found",
                    Status = ResponseStatus.Failed
                };
            }

            return View(config);
        }

        [HttpPost("execute")]
        public async Task<IActionResult> ExecuteReport([FromBody] ReportRequest request)
        {
            try
            {
             
                var response = await _apiService.ExecuteReportPage(request);

                if (response != null && response.Data != null && response.Data.Any())
                {
                    return Json(new { success = true, data = response.Data });
                }
                else
                {
                    return Json(new { success = false, error = "No data found" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }





        [HttpPost]
        public async Task<IActionResult> LoadReportData([FromBody] ReportRequest request)
        {
            var result = await _apiService.ExecuteReportPage(request);

            if (result.Status == ResponseStatus.Success && result.Data != null)
            {
                return Json(new
                {
                    // draw = request.Draw, // DataTables draw counter
                    recordsTotal = result.Data.Count(),
                    recordsFiltered = result.Data.Count(),
                    data = result.Data
                });
            }

            return Json(new
            {
                //  draw = request.Draw,
                recordsTotal = 0,
                recordsFiltered = 0,
                data = new List<object>(),
                error = result.ErrorMessage
            });
        }
    }
}