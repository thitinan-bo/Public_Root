using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Data;

namespace DynamicsReporting.DataAccess.Repository.Logging
{

 
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IDbConnection _db;
    

        public LoggingRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("APP"));

        
        }

        public async Task AddLogAsync(AddLogModel log)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@i_HostName", log.HostName);
                parameters.Add("@i_IPAddress", log.IPAddress);
                parameters.Add("@i_FunctionName", log.FunctionName);
                parameters.Add("@i_ErrorMessages", log.ErrorMessages);

                await _db.ExecuteAsync(
                    "usp_AddLog",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch
            {
                throw;
            }
        }

    }

}
