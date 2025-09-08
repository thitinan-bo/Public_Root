
using DynamicsReporting.Models;
using DynamicsReporting.Models.Authen;
using DynamicsReporting.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DynamicsReporting.Web.Services
{
    public interface IApiService
    {

        Task<List<BranchModel>> BranchAll();

        Task<ResponseDataModel<AuthenResponseModel>> Authen(AuthenRequestModel model);

        Task<ResponseDataModel<PaginatedResult<GroupReportUseModel>>> GetGroupReportByUserIdAsync(ReqUserGroup reqUserGroup);


        Task<ResponseDataModel<PaginatedResult<ReportModel>>> GetReportByUserId(ReqUserReport userReport);

        Task<ResponseDataModel<ReportConfigModel>> GetConfigReport(int reportId);



        Task<ResponseDataModel<IEnumerable<dynamic>>> ExecuteReportPage(ReportRequest reportRequest);
    }
}
