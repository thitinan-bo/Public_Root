using Dapper;
using DynamicsReporting.DataAccess.Repository.Report.Interface;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DynamicsReporting.DataAccess.Repository.Report
{
    internal class ReportRepository : IReportRepository
    {
        private readonly IDbConnection _db;

        public ReportRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("APPConn"));
        }

        public async Task<PaginatedResult<ReportModel>> GetAllAsync(int currentPage, int pageSize)
        {
            var response = new PaginatedResult<ReportModel>();
            List<ReportModel> allResults = new();

            var sql = "EXEC usp_GetAllReport";
            allResults = (await _db.QueryAsync<ReportModel>(sql)).ToList();
            var pagedData = allResults
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            response.Data = pagedData;
            //    response.TotalCount = allResults.Count;
            response.Pagination = new Pagination
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalRecords = allResults.Count
            };

            return response;
        }

        public async Task<PaginatedResult<ReportModel>> GetReportByIdAsync(int reportId, int currentPage, int pageSize)
        {
            var response = new PaginatedResult<ReportModel>();
            List<ReportModel> allResults = new();

            var sql = "EXEC usp_GetReportById @i_ReportID";
            allResults = (await _db.QueryAsync<ReportModel>(sql, new { i_ReportID = reportId })).ToList();
            var pagedData = allResults
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            response.Data = pagedData;

            response.Pagination = new Pagination
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalRecords = allResults.Count
            };

            return response;
        }


        ////public async Task<PaginatedResult<ReportModel>> GetReportByGroupIdAsync(ReqUserGroupReport groupReport)
        //{
        //    var response = new PaginatedResult<ReportModel>();
        //    List<ReportModel> allResults = new();

        //    var sql = "EXEC usp_GetReportByGroupId @i_GroupID";
        //    allResults = (await _db.QueryAsync<ReportModel>(sql, new { i_GroupID = groupReport.GroupID })).ToList();
        //    var pagedData = allResults
        //        .Skip((groupReport.currentPage - 1) * groupReport.pageSize)
        //        .Take(groupReport.pageSize)
        //        .ToList();

        //    response.Data = pagedData;

        //    response.Pagination = new Pagination
        //    {
        //        CurrentPage = groupReport.currentPage,
        //        PageSize = groupReport.pageSize,
        //        TotalRecords = allResults.Count
        //    };

        //    return response;
        //}





    }
}
