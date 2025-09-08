using DynamicsReporting.DataAccess.Repository.Report.Interface;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsReporting.ExternalService.Service.Report.Interface
{
    public interface IReportService
    {
        Task<PaginatedResult<ReportModel>> GetAllAsync(int currentPage, int pageSize);


        Task<PaginatedResult<ReportModel>> GetReportByIdAsync(int groupId, int currentPage, int pageSize);



    }
}
