using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;

namespace DynamicsReporting.DataAccess.Repository.User.Interface
{
    public interface IUserRepository
    {
        Task<PaginatedResult<UserModel>> GetAllAsync(int currentPage, int pageSize);
        Task<UserModel> GetByUserNameAsync(string userName);

        Task<PaginatedResult<GroupReportUseModel>> GetGroupReportByUserIdAsync(ReqUserGroupReport reqUserGroup);

        Task<PaginatedResult<UserReportModel>> GetReportByUserIdAsync(ReqUserReport userReport);

    }
}
