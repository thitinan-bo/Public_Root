using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;

namespace DynamicsReporting.ExternalService.Service.Group.Interface
{
    public interface IGroupService
    {
        Task<PaginatedResult<GroupModel>> GetAllAsync(int currentPage, int pageSize);
        Task<PaginatedResult<GroupReportModel>> GetReportByGroupIdAsync(int groupId, int currentPage, int pageSize);
 
        //Task<PaginatedResult<GroupReportUseModel>> GetGroupReportByUserIdAsync(ReqUserGroup req);
    }
}