namespace DynamicsReporting.ExternalService.Service.Report
{
    using Dapper;
    using DynamicsReporting.DataAccess.Repository.Report.Interface;
    using DynamicsReporting.ExternalService.Service.Report.Interface;
    using DynamicsReporting.Models;
    using DynamicsReporting.Models.Request;
    using Microsoft.Data.SqlClient;
    using System.Data;

    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public Task<PaginatedResult<ReportModel>> GetAllAsync(int currentPage, int pageSize)
        {
            return _reportRepository.GetAllAsync(currentPage, pageSize);
        }

        public Task<PaginatedResult<ReportModel>> GetReportByIdAsync(int reportId, int currentPage, int pageSize)
        {
            return _reportRepository.GetReportByIdAsync(reportId, currentPage, pageSize);
        }



     


    }
}
