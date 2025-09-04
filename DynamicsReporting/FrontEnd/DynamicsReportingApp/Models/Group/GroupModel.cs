public class GroupModel
{
    public int GroupID { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public bool GroupStatus { get; set; }
}

public class GroupReportModel
{
    public int GroupReportID { get; set; }
    public int GroupID { get; set; }
    public int ReportID { get; set; }
    public bool GroupReportStatus { get; set; }
}


public class GroupReportUseModel
{
    public int UserGroupReportID { get; set; }
    public int GroupID { get; set; }
    public string UserID { get; set; }

}
