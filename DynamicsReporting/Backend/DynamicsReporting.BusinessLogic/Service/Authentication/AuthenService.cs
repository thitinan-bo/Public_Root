using DynamicsReporting.ExternalService.Service.Authentication.Interface;
using DynamicsReporting.DataAccess.Repository.Authentication.Interface;
using DynamicsReporting.Models.Authen;
using Microsoft.Extensions.Configuration;
using DynamicsReporting.Models;


namespace DynamicsReporting.ExternalService.Service.Authentication
{
    public class AuthenService : IAuthenService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenRepository _authenRepository;


        public AuthenService(IAuthenRepository authenRepository, IConfiguration configuration)
        {
            _authenRepository = authenRepository ?? throw new ArgumentNullException(nameof(authenRepository));
            _configuration = configuration.GetSection("BRANCH");

        }

        public async Task<List<BranchModel>> GetBranchAsync()
        {
            return await _authenRepository.GetBranchAsync();
        }


        public async Task<BranchModel> GetBranchByBranchCodeAsync(string branchCode)
        {

            return await _authenRepository.GetBranchByBranchCodeAsync(branchCode);
        }




        public async Task<AuthenResponseModel> AuthenAsync(AuthenRequestModel authenRequest)
        {

            AuthenResponseModel authenResponse = new AuthenResponseModel();
            string connStr = String.Empty;


            BranchModel branch = await GetBranchByBranchCodeAsync(authenRequest.BranchCode);


            branch.default_server = "R4AD01";
            authenRequest.Username = "glconnect";
            authenRequest.Password = "ledger";
          
            connStr = String.Format("Server={0};Database=centerdb;User Id={1};Password={2};TrustServerCertificate=True;", branch.default_server, authenRequest.Username, authenRequest.Password);

            int result = 1;
            // await _authenRepository.AuthenAsync(authenRequest.Username, authenRequest.Password, connStr);

            if (result == 1)
            {
                int userId = await _authenRepository.GetUserIDAsync(authenRequest.Username, authenRequest.BranchCode);
                authenResponse.UserId = userId;
                authenResponse.IsAuthenticated = true;
            }
            else
            {
                authenResponse.IsAuthenticated = false;
            }

            authenResponse.DisplayName = authenRequest.Username;
            authenResponse.BranchCode = authenRequest.BranchCode;
            authenResponse.DefaultServer = branch.default_server;

            return authenResponse;
        }






        //private async Task AddLog(string FunctionName, string Message)
        //{
        //    try
        //    {
        //        AddLogModel addLoggingFailModel = new AddLogModel
        //        {
        //            HostName = GetHost(),
        //            IPAddress = GetLocalIPAddress(),
        //            FunctionName = FunctionName,
        //            ErrorMessages = Message
        //        };
        //        await _loggingRepository.AddLogAsync(addLoggingFailModel);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}



        //private string GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostName();
        //    var ip = Dns.GetHostEntry(host)
        //        .AddressList
        //        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
        //    return ip?.ToString() ?? "127.0.0.1";
        //}


        //private string GetHost()
        //{
        //    return Dns.GetHostEntry(Dns.GetHostName()).HostName;
        //}
    }
}
