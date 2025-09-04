using DynamicsReporting.ExternalService.Service.Report.Interface;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Base;
using DynamicsReporting.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace DynamicsReporting.API.Controllers.Report
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private readonly IReportService _reportingService;
        private readonly ExternalService.Utility.Utility _utility;
        private readonly ILoggingRepository _logger;
        public ReportController(IReportService reportingService, ExternalService.Utility.Utility utility, ILoggingRepository logger)
        {
            _reportingService = reportingService;
            _utility = utility;
            _logger = logger;
        }



        [HttpPost("getAll")]
        public async Task<IActionResult> GetDynamicsReportingDataAsync(int currentPage, int pageSize)
        {

            var result = new PaginatedResult<ReportModel>();
            try
            {
                result = await _reportingService.GetAllAsync(currentPage, pageSize);

                if (result.Data.Count == 0)
                {
                    result.StatusCode = 400;
                    return StatusCode(400, result);
                }

                result.StatusCode = 200;
                return StatusCode(200, result);
            }
            catch
            {
                result.StatusCode = 500;
                return StatusCode(500, result);
            }
        }


        [HttpPost("reportId/{reportId}")]
        public async Task<IActionResult> GetReportByIdAsync(int reportId, int currentPage, int pageSize)
        {

            var result = new PaginatedResult<ReportModel>();
            try
            {
                result = await _reportingService.GetReportByIdAsync(reportId, currentPage, pageSize);

                if (result.Data.Count == 0)
                {
                    result.StatusCode = 400;
                    return StatusCode(400, result);
                }

                result.StatusCode = 200;
                return StatusCode(200, result);
            }
            catch
            {
                result.StatusCode = 500;
                return StatusCode(500, result);
            }


        }


        //[HttpPost("groupId/{groupId}")]
        //public async Task<IActionResult> GetReportByGroupIdAsync(ReqUserGroupReport groupReport)
        //{

        //    var responseData = new ResponseDataModel<PaginatedResult<ReportModel>>();
        //    var model = new PaginatedResult<ReportModel>();


        //    try
        //    {
        //        model = await _reportingService.GetReportByGroupIdAsync(groupReport);



        //        if (model != null)
        //        {
        //            responseData.Data = model;
        //            responseData.ErrorCode = "0";
        //            responseData.ErrorMessage = "Success";
        //            responseData.Status = ResponseStatus.Success;
        //            responseData.ErrorType = ResponseStatus.Success;
        //            responseData.StatusCode = 200;

        //            return StatusCode(HttpStatus.OK, responseData);
        //        }

        //        responseData.ErrorCode = "1";
        //        responseData.ErrorMessage = "No data found";
        //        responseData.Status = ResponseStatus.Failed;
        //        responseData.ErrorType = "DataNotFound";
        //        responseData.StatusCode = 404;

        //        return StatusCode(HttpStatus.NotFound, responseData);
        //    }
        //    catch (Exception ex)
        //    {


        //        string ErrMessage = "ErrorCode 500 " + ex.Message + " Internal server error: " + ex.Message;

        //        AddLogModel addLogModel = new AddLogModel();
        //        addLogModel.IPAddress = _utility.GetLocalIPAddress();
        //        addLogModel.HostName = _utility.GetHost();
        //        addLogModel.ErrorMessages = ErrMessage;
        //        addLogModel.FunctionName = "Authen";

        //        await _logger.AddLogAsync(addLogModel);

        //        responseData.ErrorCode = "500";
        //        responseData.ErrorMessage = ErrMessage;
        //        responseData.Status = ResponseStatus.Error;
        //        responseData.ErrorType = ResponseErrorType.Exception;
        //        responseData.StatusCode = 500;

        //        return StatusCode(500, responseData);


        //    }


        //}




    }
}
