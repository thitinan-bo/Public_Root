using Dapper;
using DynamicsReporting.DataAccess.Repository.User.Interface;
using DynamicsReporting.ExternalService.Utility;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace DynamicsReporting.DataAccess.Repository.User
{
    internal class UserRepository : IUserRepository
    {

        private readonly IDbConnection _db;
        private readonly IConfiguration _configuration;
        private readonly Utility _utility;

        public UserRepository(IConfiguration config, Utility utility)
        {
            _db = new SqlConnection(config.GetConnectionString("APP"));
            _utility = utility;
        }


        public async Task<PaginatedResult<UserModel>> GetAllAsync(int currentPage, int pageSize)
        {
            var response = new PaginatedResult<UserModel>();
            List<UserModel> allResults = new();



            var sql = "EXEC usp_GetAllUsers ";
            allResults = (await _db.QueryAsync<UserModel>(sql)).ToList();
            var pagedData = allResults
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            response.Data = pagedData;
            //  response.TotalCount = allResults.Count;
            response.Pagination = new Pagination
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalRecords = allResults.Count
            };


            return response;
        }

        public async Task<UserModel> GetByUserNameAsync(string userName, string branchCode)
        {
            return await _db.QueryFirstOrDefaultAsync<UserModel>(
        "usp_GetUserByName",
        new { i_UserName = userName, i_BranchCode = branchCode },
        commandType: CommandType.StoredProcedure
    );
        }
         
        public async Task<PaginatedResult<GroupReportUseModel>> GetGroupReportByUserIdAsync(ReqUserGroupReport reqUserGroup)
        {
            var response = new PaginatedResult<GroupReportUseModel>();

            try
            {
                var sql = "EXEC usp_UserGroupReportByUserId @i_UserID";
                var allResults = (await _db.QueryAsync<GroupReportUseModel>(
                    sql,
                    new { i_UserID = reqUserGroup.UserID }
                )).ToList();

                if (allResults == null || allResults.Count == 0)
                {
                    response.Data = new List<GroupReportUseModel>();
                    response.TotalCount = 0;
                    response.Pagination = new Pagination
                    {
                        CurrentPage = reqUserGroup.currentPage,
                        PageSize = reqUserGroup.pageSize,
                        TotalRecords = 0
                    };
                    return response;
                }

                var pagedData = allResults
                    .Skip((reqUserGroup.currentPage - 1) * reqUserGroup.pageSize)
                    .Take(reqUserGroup.pageSize)
                    .ToList();

                response.Data = pagedData;
                response.TotalCount = allResults.Count; // ✅ อย่าลืมใส่
                response.Pagination = new Pagination
                {
                    CurrentPage = reqUserGroup.currentPage,
                    PageSize = reqUserGroup.pageSize,
                    TotalRecords = allResults.Count
                };

                return response;
            }
            catch (Exception ex)
            {
                // TODO: คุณอาจจะ log error ที่นี่
                throw new Exception("Error fetching group report by user id", ex);
            }
        }
         
        public async Task<PaginatedResult<UserReportModel>> GetReportByUserIdAsync(ReqUserReport reqUserReport)
        {
            var response = new PaginatedResult<UserReportModel>();

            try
            {
                var sql = "EXEC usp_UserReportByUserId @i_UserID, @i_GroupID";

                var allResults = (await _db.QueryAsync<UserReportModel>(
                    sql,
                    new { i_UserID = reqUserReport.UserID, i_GroupID = reqUserReport.GroupID }
                )).ToList();

                if (allResults == null || allResults.Count == 0)
                {
                    response.Data = new List<UserReportModel>();
                    response.TotalCount = 0;
                    response.Pagination = new Pagination
                    {
                        CurrentPage = reqUserReport.currentPage,
                        PageSize = reqUserReport.pageSize,
                        TotalRecords = 0
                    };
                    return response;
                }

                var pagedData = allResults
                    .Skip((reqUserReport.currentPage - 1) * reqUserReport.pageSize)
                    .Take(reqUserReport.pageSize)
                    .ToList();

                response.Data = pagedData;
                //   response.TotalCount = allResults.Count; // ✅ อย่าลืมใส่
                response.Pagination = new Pagination
                {
                    CurrentPage = reqUserReport.currentPage,
                    PageSize = reqUserReport.pageSize,
                    TotalRecords = allResults.Count
                };

                return response;
            }
            catch (Exception ex)
            {
                // TODO: log error
                throw new Exception("Error fetching report by user id", ex);
            }
        }


        public async Task<PaginatedResult<UserReportModel>> GetReportDetailByUserIdAsync(ReqUserReport reqUserReport)
        {
            var response = new PaginatedResult<UserReportModel>();

            try
            {
                var sql = "EXEC usp_UserReportDetailByUserId @i_UserID, @i_GroupID";

                var allResults = (await _db.QueryAsync<UserReportModel>(
                    sql,
                    new { i_UserID = reqUserReport.UserID, i_GroupID = reqUserReport.GroupID }
                )).ToList();

                if (allResults == null || allResults.Count == 0)
                {
                    response.Data = new List<UserReportModel>();
                    response.TotalCount = 0;
                    response.Pagination = new Pagination
                    {
                        CurrentPage = reqUserReport.currentPage,
                        PageSize = reqUserReport.pageSize,
                        TotalRecords = 0
                    };
                    return response;
                }

                var pagedData = allResults
                    .Skip((reqUserReport.currentPage - 1) * reqUserReport.pageSize)
                    .Take(reqUserReport.pageSize)
                    .ToList();

                response.Data = pagedData;
                response.Pagination = new Pagination
                {
                    CurrentPage = reqUserReport.currentPage,
                    PageSize = reqUserReport.pageSize,
                    TotalRecords = allResults.Count
                };

                return response;
            }
            catch (Exception ex)
            {
                // TODO: log error
                throw new Exception("Error fetching report detail by user id", ex);
            }
        }


        //////////////////////////
         
        public async Task<List<ReportProc>> GetReportProcByReportIdAsync(int reportId)
        {
            try
            {
                var sql = "EXEC usp_GetReportProcByReportId @i_ReportID";
                var result = await _db.QueryAsync<ReportProc>(
                    sql,
                    new { i_ReportID = reportId }
                );

                return result.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error while getting ReportProc for ReportID {ReportId}", reportId);
                throw; // rethrow หลัง log
            }
        }

        public async Task<List<ReportParam>> GetReportParamByReportProcIdAsync(int reportProcID)
        {
            try
            {
                var sql = "EXEC usp_GetReportParamByReportProcID @i_ReportProcID";
                var result = await _db.QueryAsync<ReportParam>(
                    sql,
                    new { i_ReportProcID = reportProcID }
                );

                return result.ToList();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error while getting ReportParam for ReportProcID {ReportProcID}", reportProcID);
                throw; // Rethrow หลัง log
            }
        }


        public async Task<IEnumerable<dynamic>> ExecuteReportAsync(int reportId, Dictionary<string, object> paramValues)
        {
            var reportProcs = await GetReportProcByReportIdAsync(reportId);

            var allResults = new List<dynamic>();

            foreach (var proc in reportProcs)
            {
                // connection ไปยัง DB ปลายทางที่ต้องรัน Stored Procedure
                var targetConn = $"Server={proc.ServerName};Database={proc.DatabaseName};User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";
                //Server=R18AM660107;Database=DynamicsReporting;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;

                using (var conn = new SqlConnection(targetConn))
                {
                    var parameters = await GetReportParamByReportProcIdAsync(proc.ReportProcId);

                    var dParams = new DynamicParameters();

                    foreach (var p in parameters)
                    {
                        object? value = null;

                        if (paramValues.TryGetValue(p.ParameterName, out var rawValue))
                        {
                            if (rawValue is JsonElement jsonElement)
                            {
                                value = _utility.ConvertJsonElementToClrObject(jsonElement);
                            }
                            else
                            {
                                value = rawValue;
                            }
                        }

                        var dbType = ParseDbType(p.ParameterDbType);
                        var direction = ParseDirection(p.ParameterDirection);

                        dParams.Add(
                            name: p.ParameterName,
                            value: value,
                            dbType: dbType,
                            direction: direction
                        );
                    }

                    var result = await conn.QueryAsync(
                        sql: proc.StoredProcedure,
                        param: dParams,
                        commandType: CommandType.StoredProcedure
                    );

                    allResults.AddRange(result);
                }
            }

            return allResults;
        }



        private DbType ParseDbType(string dbTypeStr)
        {
            if (string.IsNullOrWhiteSpace(dbTypeStr))
                return DbType.String;

            // Trim และ upper ให้ชัดเจน
            var normalized = dbTypeStr.Trim().ToUpperInvariant();

            return normalized switch
            {
                "INT" or "INTEGER" => DbType.Int32,
                "BIGINT" => DbType.Int64,
                "SMALLINT" => DbType.Int16,
                "TINYINT" => DbType.Byte,
                "BIT" => DbType.Boolean,

                "DECIMAL" or "NUMERIC" => DbType.Decimal,
                "FLOAT" => DbType.Double,
                "REAL" => DbType.Single,

                "DATE" => DbType.Date,
                "DATETIME" => DbType.DateTime,
                "DATETIME2" => DbType.DateTime2,
                "SMALLDATETIME" => DbType.DateTime,
                "TIME" => DbType.Time,

                "CHAR" or "NCHAR" or "VARCHAR" or "NVARCHAR" or "TEXT" or "NTEXT"
                    => DbType.String,

                "UNIQUEIDENTIFIER" => DbType.Guid,

                "VARBINARY" or "BINARY" or "IMAGE"
                    => DbType.Binary,

                _ => DbType.String // fallback ป้องกัน error
            };
        }


        private ParameterDirection ParseDirection(string directionStr)
        {
            return directionStr?.ToLower() switch
            {
                "output" => ParameterDirection.Output,
                "return" => ParameterDirection.ReturnValue,
                _ => ParameterDirection.Input
            };
        }





    }





}
