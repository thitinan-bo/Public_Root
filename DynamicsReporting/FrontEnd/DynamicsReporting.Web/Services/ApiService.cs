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

        public ApiService(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        private const string GroupReportByGroupId = "Report/groupId/{0}";

        private const string AuthenGetBranchByBranchCode = "Authen/BranchByBranchCode/{branchCode}";

        private const string GroupGetAll = "Group/GetAll";
        private const string GroupGetById = "Group/{GroupId}";




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


        //

        public async Task<ResponseDataModel<PaginatedResult<ReportModel>>> GetReportDetailsAsync(ReqUserReport userReport)
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


        #endregion

    }

}
