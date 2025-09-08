using DynamicsReporting.Models;
using DynamicsReporting.Models.Authen;
using DynamicsReporting.Models.Request;

namespace DynamicsReporting.Web.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;


        private const string AuthenGetBranch = "/Authen/BranchAll";
        private const string AuthenUser = "/Authen/Authen";
        private const string GroupReportByUserId = "/User/GroupReport";
        private const string ReportByUserId = "/User/Report";

        private const string ConfigReport = "/User/ConfigReport";
        private const string ExecuteReport = "/User/execute";

        public ApiService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }
 



        #region Authentication
        public async Task<List<BranchModel>> BranchAll()
        {
            var responseData = new ResponseDataModel<List<BranchModel>>();
            try
            {
                var apiUrl = _config.GetValue<string>("ApiBaseUrl") + AuthenGetBranch;
                responseData = await _http.GetFromJsonAsync<ResponseDataModel<List<BranchModel>>>(apiUrl);

                //if (responseData == null)
                //{
                //    responseData = new ResponseDataModel<List<BranchModel>>
                //    {
                //        ErrorCode = "1",
                //        ErrorMessage = "No data found",
                //        Data = new List<BranchModel>()
                //    };
                //}
                //else if (responseData.ErrorCode != "0")
                //{
                //    responseData.Data = new List<BranchModel>();
                //}

            }
            catch (Exception ex)
            {

            }
            return responseData.Data;

        }
        public async Task<ResponseDataModel<AuthenResponseModel>> Authen(AuthenRequestModel model)
        {
            var responseData = new ResponseDataModel<AuthenResponseModel>();

            try
            {
                var apiUrl = _config.GetValue<string>("ApiBaseUrl") + AuthenUser;
                var response = await _http.PostAsJsonAsync(apiUrl, model);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<AuthenResponseModel>();

                if (result != null)
                {
                    responseData.Data = result;
                    responseData.ErrorCode = "0";
                    responseData.ErrorMessage = "Success";

                }
                else
                {
                    responseData.ErrorCode = "1";
                    responseData.ErrorMessage = "No data found";

                }

                return responseData;
            }
            catch (Exception ex)
            {
                responseData.ErrorCode = "500";
                responseData.ErrorMessage = ex.Message;

                return responseData;
            }
        }

        #endregion



        #region Group Report
        public async Task<ResponseDataModel<PaginatedResult<GroupReportUseModel>>> GetGroupReportByUserIdAsync(ReqUserGroup reqUserGroup)
        {

            var responseData = new ResponseDataModel<PaginatedResult<GroupReportUseModel>>();

            try
            {
                var apiUrl = _config.GetValue<string>("ApiBaseUrl") + GroupReportByUserId;
                var response = await _http.PostAsJsonAsync(apiUrl, reqUserGroup);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<ResponseDataModel<PaginatedResult<GroupReportUseModel>>>();

                if (result != null)
                {
                    responseData.Data = result.Data;
                    responseData.ErrorCode = result.ErrorCode;
                    responseData.ErrorMessage = result.ErrorMessage;
                }
                else
                {
                    responseData.ErrorCode = "1";
                    responseData.ErrorMessage = "No data found";
                }
            }
            catch (Exception ex)
            {
                responseData.ErrorCode = "500";
                responseData.ErrorMessage = ex.Message;
            }

            return responseData;
        }
        #endregion



        #region Report
        public async Task<ResponseDataModel<PaginatedResult<ReportModel>>> GetReportByUserId(ReqUserReport userReport)
        {
            var responseData = new ResponseDataModel<PaginatedResult<ReportModel>>();

            try
            {
                var apiUrl = _config.GetValue<string>("ApiBaseUrl") + ReportByUserId;
                var response = await _http.PostAsJsonAsync(apiUrl, userReport);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<ResponseDataModel<PaginatedResult<ReportModel>>>();

                if (result != null)
                {
                    responseData.Data = result.Data;
                    responseData.ErrorCode = result.ErrorCode;
                    responseData.ErrorMessage = result.ErrorMessage;
                }
                else
                {
                    responseData.ErrorCode = "1";
                    responseData.ErrorMessage = "No data found";
                }
            }
            catch (Exception ex)
            {
                responseData.ErrorCode = "500";
                responseData.ErrorMessage = ex.Message;
            }

            return responseData;

        }


        public async Task<ResponseDataModel<ReportConfigModel>> GetConfigReport(int reportId)
        {
            var responseData = new ResponseDataModel<ReportConfigModel>();

            try
            {
                var apiUrl = _config.GetValue<string>("ApiBaseUrl") + ConfigReport;
               
                var response = await _http.PostAsJsonAsync(apiUrl, new ReportViewRequest { ReportId = reportId });
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ResponseDataModel<ReportConfigModel>>();

                if (result != null)
                {
                    responseData.Data = result.Data;
                    responseData.ErrorCode = result.ErrorCode;
                    responseData.ErrorMessage = result.ErrorMessage;
                    responseData.Status = result.Status;
                    responseData.StatusCode = result.StatusCode;
                }
                else
                {
                    responseData.ErrorCode = "1";
                    responseData.ErrorMessage = "No data found";
                    responseData.Status = ResponseStatus.Failed;
                }
            }
            catch (Exception ex)
            {
                responseData.ErrorCode = "500";
                responseData.ErrorMessage = ex.Message;
                responseData.Status = ResponseStatus.Error;
            }

            return responseData;
        }


        public async Task<ResponseDataModel<IEnumerable<dynamic>>> ExecuteReportPage(ReportRequest reportRequest)
        {
       
            var responseData = new ResponseDataModel<IEnumerable<dynamic>>();

            try
            {
                var apiUrl = _config.GetValue<string>("ApiBaseUrl") + ExecuteReport;
                var response = await _http.PostAsJsonAsync(apiUrl, reportRequest);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ResponseDataModel<IEnumerable<dynamic>>>();

                if (result != null)
                {
                    responseData.Data = result.Data;
                    responseData.ErrorCode = result.ErrorCode;
                    responseData.ErrorMessage = result.ErrorMessage;
                    responseData.Status = result.Status;
                    responseData.StatusCode = result.StatusCode;
                }
                else
                {
                    responseData.ErrorCode = "1";
                    responseData.ErrorMessage = "No data found";
                    responseData.Status = ResponseStatus.Failed;
                }
            }
            catch (Exception ex)
            {
                responseData.ErrorCode = "500";
                responseData.ErrorMessage = ex.Message;
                responseData.Status = ResponseStatus.Error;
            }

            return responseData;
        }


        #endregion
    }

}
