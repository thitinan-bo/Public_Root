using DynamicsReporting.ExternalService.Service.User.Interface;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly DynamicsReporting.ExternalService.Utility.Utility _utility;
    private readonly ILoggingRepository _logger;

    public UserController(IUserService userService, ILoggingRepository loggingRepository, DynamicsReporting.ExternalService.Utility.Utility utility)
    {
        _userService = userService;
        _logger = loggingRepository;
        _utility = utility;
    }

    // GET: api/user/getAll
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAsync(int currentPage, int pageSize)
    {
        var result = new PaginatedResult<UserModel>();
        try
        {
            result = await _userService.GetAllAsync(currentPage, pageSize);

            if (result.Data.Count == 0)
            {
                result.StatusCode = 404;
                return NotFound(result);
            }

            result.StatusCode = 200;
            return Ok(result);
        }
        catch (Exception ex)
        {
            result.StatusCode = 500;
            await LogErrorAsync("GetAllAsync", ex);
            return StatusCode(500, result);
        }
    }

    // GET: api/user/by-username/{userName}
    [HttpGet("by-username/{userName}")]
    public async Task<IActionResult> GetUserByUserName(string userName, string branchCode)
    {
        var user = await _userService.GetByUserNameAsync(userName, branchCode);
        if (user == null) return NotFound();

        return Ok(user);
    }

    // POST: api/user/GroupReportByUserId
    [HttpPost("GroupReport")]
    public async Task<IActionResult> GetGroupReportByUserIdAsync([FromBody] ReqUserGroupReport req)
    {
        var responseData = new ResponseDataModel<PaginatedResult<GroupReportUseModel>>();

        try
        {
            var model = await _userService.GetGroupReportByUserIdAsync(req);

            if (model != null)
            {
                responseData.Data = model;
                responseData.ErrorCode = "0";
                responseData.ErrorMessage = "Success";
                responseData.Status = ResponseStatus.Success;
                responseData.ErrorType = ResponseStatus.Success;
                responseData.StatusCode = 200;

                return Ok(responseData);
            }

            responseData.ErrorCode = "1";
            responseData.ErrorMessage = "No data found";
            responseData.Status = ResponseStatus.Failed;
            responseData.ErrorType = "DataNotFound";
            responseData.StatusCode = 404;

            return NotFound(responseData);
        }
        catch (Exception ex)
        {
            string errMessage = $"ErrorCode 500 {ex.Message} Internal server error: {ex.Message}";
            await LogErrorAsync("GetGroupReportByUserIdAsync", ex);

            responseData.ErrorCode = "500";
            responseData.ErrorMessage = errMessage;
            responseData.Status = ResponseStatus.Error;
            responseData.ErrorType = ResponseErrorType.Exception;
            responseData.StatusCode = 500;

            return StatusCode(500, responseData);
        }
    }

    // GET: api/user/Report
    [HttpPost("Report")]
    public async Task<IActionResult> GetReportByUserIdAsync([FromBody] ReqUserReport userReport)

    {
        var responseData = new ResponseDataModel<PaginatedResult<UserReportModel>>();

        try
        {
            var result = await _userService.GetReportByUserId(userReport);
            if (result != null)
            {
                responseData.Data = result;
                responseData.ErrorCode = "0";
                responseData.ErrorMessage = "Success";
                responseData.Status = ResponseStatus.Success;
                responseData.ErrorType = ResponseStatus.Success;
                responseData.StatusCode = 200;

                return Ok(responseData);
            }

            responseData.ErrorCode = "1";
            responseData.ErrorMessage = "No data found";
            responseData.Status = ResponseStatus.Failed;
            responseData.ErrorType = "DataNotFound";
            responseData.StatusCode = 404;

            return NotFound(responseData);
        }
        catch (Exception ex)
        {
            string errMessage = $"ErrorCode 500 {ex.Message} Internal server error: {ex.Message}";
            await LogErrorAsync("GetReportByUserIdAsync", ex);

            responseData.ErrorCode = "500";
            responseData.ErrorMessage = errMessage;
            responseData.Status = ResponseStatus.Error;
            responseData.ErrorType = ResponseErrorType.Exception;
            responseData.StatusCode = 500;

            return StatusCode(500, responseData);
        }
    }

    [HttpPost("ConfigReport")]
    public async Task<IActionResult> GetReportConfigByReportIdAsync([FromBody] ReportViewRequest viewRequest)
    {
        var responseData = new ResponseDataModel<ReportConfigModel>();

        try
        {
            var result = await _userService.GetReportConfigByReportIdAsync(viewRequest.ReportId);

            if (result == null)
            {
                responseData.ErrorCode = "1";
                responseData.ErrorMessage = "No data found";
                responseData.Status = ResponseStatus.Failed;
                responseData.ErrorType = "DataNotFound";
                responseData.StatusCode = 404;
                return NotFound(responseData);
            }

            if (result.ReportProcs == null || !result.ReportProcs.Any())
            {
                responseData.Data = result;
                responseData.ErrorCode = "2";
                responseData.ErrorMessage = "ReportProcs not found";
                responseData.Status = ResponseStatus.Failed;
                responseData.ErrorType = "MissingConfig";
                responseData.StatusCode = 200; // หรือ 404 ก็ได้ถ้าอยากให้ชัดว่าไม่เจอ
                return Ok(responseData);
            }

            if (result.ReportParams == null || !result.ReportParams.Any())
            {
                responseData.Data = result;
                responseData.ErrorCode = "3";
                responseData.ErrorMessage = "ReportParams not found";
                responseData.Status = ResponseStatus.Failed;
                responseData.ErrorType = "MissingConfig";
                responseData.StatusCode = 200;
                return Ok(responseData);
            }


            responseData.Data = result;
            responseData.ErrorCode = "0";
            responseData.ErrorMessage = "Success";
            responseData.Status = ResponseStatus.Success;
            responseData.ErrorType = ResponseStatus.Success;
            responseData.StatusCode = 200;

            return Ok(responseData);
        }
        catch (Exception ex)
        {
            string errMessage = $"ErrorCode 500 {ex.Message} Internal server error: {ex.Message}";
            await LogErrorAsync("GetReportConfigByReportIdAsync", ex);

            responseData.ErrorCode = "500";
            responseData.ErrorMessage = errMessage;
            responseData.Status = ResponseStatus.Error;
            responseData.ErrorType = ResponseErrorType.Exception;
            responseData.StatusCode = 500;

            return StatusCode(500, responseData);
        }
    }



    [HttpPost("execute")]


    public async Task<IActionResult> ExecuteReport([FromBody] ReportRequest request)
    {
        var responseData = new ResponseDataModel<IEnumerable<dynamic>>();

        try
        {
            var data = await _userService.ExecuteReportAsync(request.ReportId, request.Parameters);

            if (data == null || !data.Any())
            {
                responseData.Data = null;
                responseData.ErrorCode = "1";
                responseData.ErrorMessage = "No data found";
                responseData.Status = ResponseStatus.Failed;
                responseData.ErrorType = "DataNotFound";
                responseData.StatusCode = 404;

                return NotFound(responseData);
            }

            responseData.Data = data;
            responseData.ErrorCode = "0";
            responseData.ErrorMessage = "Success";
            responseData.Status = ResponseStatus.Success;
            responseData.ErrorType = ResponseStatus.Success;
            responseData.StatusCode = 200;

            return Ok(responseData);
        }
        catch (Exception ex)
        {
            string errMessage = $"ErrorCode 500 {ex.Message} Internal server error: {ex.Message}";
            await LogErrorAsync("ExecuteReport", ex);

            responseData.ErrorCode = "500";
            responseData.ErrorMessage = errMessage;
            responseData.Status = ResponseStatus.Error;
            responseData.ErrorType = ResponseErrorType.Exception;
            responseData.StatusCode = 500;

            return StatusCode(500, responseData);
        }
    }



    private async Task LogErrorAsync(string functionName, Exception ex)
    {
        var addLogModel = new AddLogModel
        {
            IPAddress = _utility.GetLocalIPAddress(),
            HostName = _utility.GetHost(),
            ErrorMessages = ex.ToString(),
            FunctionName = functionName
        };
        await _logger.AddLogAsync(addLogModel);
    }
}
