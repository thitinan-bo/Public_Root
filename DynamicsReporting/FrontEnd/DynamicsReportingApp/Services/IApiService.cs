using DynamicsReporting.Models;
using DynamicsReporting.Models.Authen;
using DynamicsReporting.Models.Request;

using Microsoft.AspNetCore.Mvc;



namespace DynamicsReportingApp.Services
{
    public interface IApiService
    {

        Task<List<BranchModel>> BranchAll();

        Task<ResponseDataModel<AuthenResponseModel>> Authen(AuthenRequestModel model);

        Task<ResponseDataModel<PaginatedResult<GroupReportUseModel>>> GetGroupReportByUserIdAsync(ReqUserGroup reqUserGroup);

        Task<ResponseDataModel<PaginatedResult<GroupReportUseModel>>> GetReportByGroupIdAsync(ReqUserGroupReport reqUserGroup);

  
    }

}