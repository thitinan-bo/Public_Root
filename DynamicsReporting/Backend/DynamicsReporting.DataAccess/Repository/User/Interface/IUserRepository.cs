using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;

namespace DynamicsReporting.DataAccess.Repository.User.Interface
{
    public interface IUserRepository
    {
        Task<PaginatedResult<UserModel>> GetAllAsync(int currentPage, int pageSize);
        Task<UserModel> GetByUserNameAsync(string userName, string branch);

        Task<PaginatedResult<GroupReportUseModel>> GetGroupReportByUserIdAsync(ReqUserGroupReport reqUserGroup);

        Task<PaginatedResult<UserReportModel>> GetReportByUserIdAsync(ReqUserReport userReport);


        Task<List<ReportProc>> GetReportProcByReportIdAsync(int reportId);
        Task<List<ReportParam>> GetReportParamByReportProcIdAsync(int reportProcID);
        Task<IEnumerable<dynamic>> ExecuteReportAsync(int reportId, Dictionary<string, object> paramValues);

    }
}
