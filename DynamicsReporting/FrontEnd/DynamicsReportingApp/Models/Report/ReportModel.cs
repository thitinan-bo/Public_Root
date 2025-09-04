
public class ReportModel
{
    public int ReportID { get; set; }
    public string ReportName { get; set; }
    public bool ReportStatus { get; set; }

}


public class ReportConfigModel
{
    public int ReportID { get; set; }
    public string ServerName { get; set; }
    public string DatabaseName { get; set; }
    public string StoredProcedure { get; set; }
}


