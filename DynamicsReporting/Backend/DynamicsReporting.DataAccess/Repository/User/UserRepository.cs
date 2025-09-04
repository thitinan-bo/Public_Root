using Dapper;
using DynamicsReporting.DataAccess.Repository.User.Interface;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DynamicsReporting.DataAccess.Repository.User
{
    internal class UserRepository : IUserRepository
    {

        private readonly IDbConnection _db;

        public UserRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("APP"));
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

        public async Task<UserModel> GetByUserNameAsync(string userName)
        {
            var sql = "EXEC usp_GetUserByName @i_UserName";
            return await _db.QueryFirstOrDefaultAsync<UserModel>(sql, new { i_UserName = userName });
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


    }








}

