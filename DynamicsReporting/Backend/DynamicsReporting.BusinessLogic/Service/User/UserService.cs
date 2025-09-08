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

        public async Task<UserModel> GetByUserNameAsync(string userName, string branchCode)

        {
            return await _userRepository.GetByUserNameAsync(userName, branchCode);
        }

        public async Task<PaginatedResult<GroupReportUseModel>> GetGroupReportByUserIdAsync(ReqUserGroupReport reqUserGroup)
        {
            return await _userRepository.GetGroupReportByUserIdAsync(reqUserGroup);
        }

        public async Task<PaginatedResult<UserReportModel>> GetReportByUserId(ReqUserReport userReport)
        {
            return await _userRepository.GetReportByUserIdAsync(userReport);
        }



        public async Task<ReportConfigModel> GetReportConfigByReportIdAsync(int reportId)
        {
            var reportConfigModel = new ReportConfigModel();

            // ดึง procs
            var reportProcs = await _userRepository.GetReportProcByReportIdAsync(reportId);

            if (reportProcs == null || !reportProcs.Any())
                return reportConfigModel;

            reportConfigModel.ReportProcs = reportProcs;

            // ✅ ประกาศ List เก็บผลลัพธ์
            var paramResults = new List<IEnumerable<ReportParam>>();

            // ✅ ดึง param ทีละ proc (ไม่ชนกัน ไม่ต้องใช้ MARS)
            foreach (var proc in reportProcs)
            {
                var result = await _userRepository.GetReportParamByReportProcIdAsync(proc.ReportProcId);
                if (result != null)
                {
                    paramResults.Add(result);
                }
            }

            // ✅ รวมทั้งหมดใส่ ReportParams
            reportConfigModel.ReportParams = paramResults.SelectMany(x => x).ToList();

            return reportConfigModel;
        }




        public async Task<IEnumerable<dynamic>> ExecuteReportAsync(int reportId, Dictionary<string, object> paramValues)
        {

            return await _userRepository.ExecuteReportAsync(reportId, paramValues);


        }







    }





}
