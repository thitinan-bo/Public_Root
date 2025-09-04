using DynamicsReporting.ExternalService.Service.User.Interface;
using DynamicsReporting.Models;

namespace DynamicsReporting.ExternalService.Service.User
{


    using DynamicsReporting.DataAccess.Repository.User.Interface;
    using DynamicsReporting.Models.Request;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<PaginatedResult<UserModel>> GetAllAsync(int currentPage, int pageSize)
        {
            return await _userRepository.GetAllAsync(currentPage, pageSize);
        }

        public async Task<UserModel> GetByUserNameAsync(string userName)
        {
            return await _userRepository.GetByUserNameAsync(userName);
        }

        public async Task<PaginatedResult<GroupReportUseModel>> GetGroupReportByUserIdAsync(ReqUserGroupReport reqUserGroup)
        {
            return await _userRepository.GetGroupReportByUserIdAsync(reqUserGroup);
        }

        public async Task<PaginatedResult<UserReportModel>> GetReportByUserId(ReqUserReport userReport)
        {
            return await _userRepository.GetReportByUserIdAsync(userReport);
        }


    }





}
