using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
namespace DynamicsReporting.ExternalService.Service.User.Interface
{
    public interface IUserService
    {
        Task<PaginatedResult<UserModel>> GetAllAsync(int currentPage, int pageSize);
        Task<UserModel> GetByUserNameAsync(string userName);


        Task<PaginatedResult<GroupReportUseModel>> GetGroupReportByUserIdAsync(ReqUserGroupReport reqUserGroup);

            Task<PaginatedResult<UserReportModel>> GetReportByUserId(ReqUserReport userReport);
    }
}