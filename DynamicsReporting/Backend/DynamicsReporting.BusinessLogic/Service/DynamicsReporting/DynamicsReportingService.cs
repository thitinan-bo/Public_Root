using DynamicsReporting.Models;
using System.Dynamic;
using System.Text.Json;
using DynamicsReporting.ExternalService;
using DynamicsReporting.ExternalService.Utility;


public class DynamicsReportingService : IDynamicsReportingService
{
    private readonly IDynamicsReportingRepository _dynamicsReportingRepository;
    private readonly Utility _utility;
    private readonly string _jsonConfFilePath;
    private readonly string _branchConn;

    public DynamicsReportingService(IDynamicsReportingRepository dynamicsReportingRepository, Utility utility)
    {
        _dynamicsReportingRepository = dynamicsReportingRepository;
        _utility = utility;
        _branchConn = _utility.GetSection("BranchConn");
        _jsonConfFilePath = _utility.GetSection("ConfFilePath");
    }

    public async Task<PaginatedResult<ExpandoObject>> GetDynamicsReportingDataAsync(int inputPageId, int currentPage, int pageSize)
    {
        var response = new PaginatedResult<ExpandoObject>();
        List<ExpandoObject> allResults = new();

        PageConfig? selectedConfig = GetConfByPageId(inputPageId);

        if (selectedConfig != null)
        {
            foreach (var item in selectedConfig.PageConfigItem) // Corrected capitalization here
            {
                string connStr = String.Format(_branchConn, item.serverdb, item.dbName); //   $"Server={item.serverdb};Database={item.dbName};User Id=sa;Password=P@ssw0rd;";

                var data = await _dynamicsReportingRepository.ExecuteStoredProcedureAsync(connStr, item.sp, item.Parameter);
                allResults.AddRange(data);
            }

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
        }

        return response;
    }

    private PageConfig? GetConfByPageId(int inputPageId)
    {
        var json = File.ReadAllText(_jsonConfFilePath);

        var configData = JsonSerializer.Deserialize<List<PageConfig>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var result = configData?.FirstOrDefault(p => p.pageId == inputPageId);

        if (result != null)
        {
            Console.WriteLine($"Page ID: {result.pageId}");

            //foreach (var item in result.PageConfigItem) // Corrected capitalization here
            //{
            //    Console.WriteLine($"- DB: {item.dbName}");
            //    Console.WriteLine($"- SP: {item.sp}");

            //    foreach (var para in item.Parameter)
            //    {
            //        Console.WriteLine($"  Param: {para.Key} = {para.Value}");
            //    }
            //}
        }
        else
        {
            Console.WriteLine("PageConfig not found.");
        }

        return result;
    }


    public async Task<PaginatedResult<ExpandoObject>> UserGetReportingDataAsync(int userIdx, int inputPageId, int currentPage, int pageSize, Dictionary<string, object>? parameters)
    {
        var response = new PaginatedResult<ExpandoObject>();
        List<ExpandoObject> allResults = new();

        PageConfig? selectedConfig = GetConfByPageId(inputPageId);

        if (selectedConfig != null)
        {
            foreach (var item in selectedConfig.PageConfigItem)
            {
                string connStr = $"Server={item.serverdb};Database={item.dbName};Integrated Security=True;TrustServerCertificate=True;";

                var spParams = item.Parameter ?? parameters;

                var data = await _dynamicsReportingRepository.ExecuteStoredProcedureAsync(connStr, item.sp, spParams);

                allResults.AddRange(data);
            }

            var pagedData = allResults
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            response.Data = pagedData;
           // response.TotalCount = allResults.Count;
            response.Pagination = new Pagination
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalRecords = allResults.Count
            };
        }

        return response;
    }


}

