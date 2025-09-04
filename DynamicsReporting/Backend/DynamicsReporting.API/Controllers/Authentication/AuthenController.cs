using DynamicsReporting.ExternalService.Service.Authentication.Interface;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Authen;
using DynamicsReporting.Models.Base;
using Microsoft.AspNetCore.Mvc;



namespace DynamicsReporting.API.Controllers.Authentication
{



    [Route("/api/[controller]/")]

    [ApiController]


    public class AuthenController : ControllerBase
    {

        private readonly IAuthenService _authenService;
        private readonly ILoggingRepository _logger;
        private readonly ExternalService.Utility.Utility _utility;



        public AuthenController(IAuthenService authenService, ILoggingRepository loggingRepository, ExternalService.Utility.Utility utility)
        {
            _authenService = authenService;
            _logger = loggingRepository;
            _utility = utility;
        }






        [HttpGet("BranchAll")]
        public async Task<IActionResult> GetBranchAsync()
        {
            var responseData = new ResponseDataModel<List<BranchModel>>();

            try
            {
                var listBranchModel = await _authenService.GetBranchAsync();

                if (listBranchModel != null && listBranchModel.Any())
                {
                    responseData.Data = listBranchModel;
                    responseData.ErrorCode = "0";
                    responseData.ErrorMessage = "Success";
                    responseData.Status = ResponseStatus.Success;
                    responseData.ErrorType = ResponseStatus.Success;
                    responseData.StatusCode = 200;

                    return StatusCode(HttpStatus.OK, responseData);

                }

                responseData.ErrorCode = "1";
                responseData.ErrorMessage = "No data found";
                responseData.Status = ResponseStatus.Failed;
                responseData.ErrorType = "DataNotFound";
                responseData.StatusCode = 404;
                return StatusCode(HttpStatus.NotFound, responseData);

            }
            catch (Exception ex)
            {
                AddLogModel addLogModel = new AddLogModel();
                addLogModel.IPAddress = _utility.GetLocalIPAddress();
                addLogModel.HostName = _utility.GetHost();
                addLogModel.ErrorMessages = "ErrorCode 500 " + ex.Message;
                addLogModel.FunctionName = "GetBranchALL";

                await _logger.AddLogAsync(addLogModel);

                responseData.ErrorCode = "500";
                responseData.ErrorMessage = ex.Message;
                responseData.Status = ResponseStatus.Error;
                responseData.ErrorType = ResponseErrorType.Exception;
                responseData.StatusCode = 500;

                return StatusCode(500, responseData);
            }



        }



        [HttpPost("Authen")]
        public async Task<IActionResult> AuthenAsync([FromBody] AuthenRequestModel authen)
        {
            var responseData = new AuthenResponseModel();

            try
            {
                responseData = await _authenService.AuthenAsync(authen);

                return StatusCode(200, responseData);
            }
            catch (Exception ex)
            {
                string ErrMessage = "ErrorCode 500 " + ex.Message + " | User : " + authen.Username + "| BranchCode :" + authen.BranchCode;
                AddLogModel addLogModel = new AddLogModel();
                addLogModel.IPAddress = _utility.GetLocalIPAddress();
                addLogModel.HostName = _utility.GetHost();
                addLogModel.ErrorMessages = ErrMessage;
                addLogModel.FunctionName = "Authen";
                await _logger.AddLogAsync(addLogModel);

                var errorResponse = new ResponseDataModel<AuthenResponseModel>
                {
                    ErrorCode = "500",
                    ErrorMessage = ErrMessage,
                    Status = ResponseStatus.Error,
                    ErrorType = ResponseErrorType.Exception,
                    StatusCode = 500
                };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("BranchByBranchCode")]
        public async Task<IActionResult> GetBranchByBranchCodeAsync([FromQuery] string branchCode)
        {


            var responseData = new ResponseDataModel<BranchModel>();

            try
            {
                var model = await _authenService.GetBranchByBranchCodeAsync(branchCode);

                if (model != null)
                {
                    responseData.Data = model;
                    responseData.ErrorCode = "0";
                    responseData.ErrorMessage = "Success";
                    responseData.Status = ResponseStatus.Success;
                    responseData.ErrorType = ResponseStatus.Success;
                    responseData.StatusCode = 200;

                    return StatusCode(HttpStatus.OK, responseData);
                }

                responseData.ErrorCode = "1";
                responseData.ErrorMessage = "No data found";
                responseData.Status = ResponseStatus.Failed;
                responseData.ErrorType = "DataNotFound";
                responseData.StatusCode = 404;

                return StatusCode(HttpStatus.NotFound, responseData);
            }
            catch (Exception ex)
            {
                AddLogModel addLogModel = new AddLogModel();
                addLogModel.IPAddress = _utility.GetLocalIPAddress();
                addLogModel.HostName = _utility.GetHost();
                addLogModel.ErrorMessages = "ErrorCode 500 " + ex.Message;
                addLogModel.FunctionName = "GetBranchALL";

                await _logger.AddLogAsync(addLogModel);

                responseData.ErrorCode = "500";
                responseData.ErrorMessage = ex.Message;
                responseData.Status = ResponseStatus.Error;
                responseData.ErrorType = ResponseErrorType.Exception;
                responseData.StatusCode = 500;
                return StatusCode(500, responseData);
            }



        }







    }
}
