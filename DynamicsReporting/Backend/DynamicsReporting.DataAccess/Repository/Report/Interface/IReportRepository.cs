using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;

namespace DynamicsReporting.DataAccess.Repository.Report.Interface
{
    public interface IReportRepository
    {
        Task<PaginatedResult<ReportModel>> GetAllAsync(int currentPage, int pageSize);
        Task<PaginatedResult<ReportModel>> GetReportByIdAsync(int reportId, int currentPage, int pageSize);

        //  Task<PaginatedResult<ReportModel>> GetReportByGroupIdAsync(ReqUserGroupReport groupReport);


        

    }
}