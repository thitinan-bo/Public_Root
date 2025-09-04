using DynamicsReporting.Models;
using DynamicsReporting.Models.Authen;

namespace DynamicsReporting.DataAccess.Repository.Authentication.Interface
{
    public interface IAuthenRepository
    {
        Task<List<BranchModel>> GetBranchAsync();
        Task<BranchModel> GetBranchByBranchCodeAsync(string branchCode);
        Task<int> AuthenAsync(string user, string pws, string connStr);
        Task<int> GetUserIDAsync(string UserName, string branchCode);

    }
}