namespace DynamicsReporting.Models;

//public class ReportModel
//{
//    public int ReportID { get; set; }
//    public string ReportName { get; set; }
//    //public bool ReportStatus { get; set; }

//}


public class ReportModel
{
    public int ReportID { get; set; }
    public string ReportName { get; set; }
    public string ReportStatus { get; set; }
}

public class ReportConfigModel
{
    public List<ReportProc> ReportProcs { get; set; } = new();
    public List<ReportParam> ReportParams { get; set; } = new();
}

public class ReportProc
{
    public int ReportProcId { get; set; }
    public int ReportId { get; set; }
    public string ServerName { get; set; }
    public string DatabaseName { get; set; }
    public string StoredProcedure { get; set; }
}

public class ReportParam
{
    public int ReportParaId { get; set; }
    public int ReportProcId { get; set; }
    public string ParameterName { get; set; }
    public string ParameterDirection { get; set; }
    public string ParameterDbType { get; set; }
}

public class ReportViewRequest
{
    public int ReportId { get; set; }
}

public class ReportRequest
{
    public int ReportId { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}
