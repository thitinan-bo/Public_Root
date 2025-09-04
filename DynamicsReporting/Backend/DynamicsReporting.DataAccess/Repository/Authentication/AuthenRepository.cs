using Dapper;
using DynamicsReporting.DataAccess.Repository.Authentication.Interface;
using DynamicsReporting.Models;
using DynamicsReporting.Models.Authen;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using System.Data;

namespace DynamicsReporting.DataAccess.Repository.Authentication
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly IDbConnection _dbAuthenApp;
        //private readonly IDbConnection _dbCenter; // Added missing field declaration

        public AuthenRepository(IConfiguration config)
        {
            _dbAuthenApp = new SqlConnection(config.GetConnectionString("APP"));
        //    _dbCenter = new SqlConnection(config.GetConnectionString("CENTER"));
        }

        public async Task<List<BranchModel>> GetBranchAsync()
        {
            List<BranchModel> listBranches = new List<BranchModel>();

            try
            {
                var sql = "EXEC usp_GetAllBranch ";
                listBranches = (List<BranchModel>)await _dbAuthenApp.QueryAsync<BranchModel>(sql);
            }
            catch (Exception ex)
            {
                throw;
            }

            return listBranches;
        }

        public async Task<BranchModel> GetBranchByBranchCodeAsync(string branchCode)
        {
            BranchModel branch = null;

            try
            {
                var sql = "EXEC usp_GetBranchByBranchCode @i_BranchCode";
                var parameters = new DynamicParameters();
                parameters.Add("@i_BranchCode", branchCode);
                branch = await _dbAuthenApp.QueryFirstOrDefaultAsync<BranchModel>(sql, parameters); // Fixed _dbAuthen_App usage
            }
            catch (Exception ex)
            {
                throw;
            }

            return branch;
        }

        public async Task<int> AuthenAsync(string user, string pws, string dbAuthen)
        {
            int result = 0;

            try
            {
                //string connStr = String.Format(dbAuthen, branch, user, pws);

                var sql = "select result = case when count(0) > 0 then 1 else 0 end " +
                    " from master.sys.sql_logins a " +
                    " where a.name=@User and PWDCOMPARE(@Pws, a.password_hash) = 1 ";

                var parameters = new DynamicParameters();
                parameters.Add("User", user);
                parameters.Add("Pws", pws);

                SqlConnection _dbAuthen_Branch = new SqlConnection(dbAuthen);


                result = (int)await _dbAuthen_Branch.ExecuteScalarAsync(sql, parameters);
            }
            catch (Exception)
            {

            }

            return result;
        }




        public async Task<int> GetUserIDAsync(string UserName, string branchCode)
        {
            int userId = 0;

            try
            {
                var sql = "EXEC usp_GetUserByName @i_UserName, @i_BranchCode";
                var parameters = new DynamicParameters();
                parameters.Add("@i_UserName", UserName);
                parameters.Add("@i_BranchCode", branchCode);
                userId = await _dbAuthenApp.QueryFirstOrDefaultAsync<int>(sql, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }

            return userId;
        }



    }
}
