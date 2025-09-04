public class UserModel
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime? UserLastLogin { get; set; }
    public bool UserStatus { get; set; }
}

public class  UserGroupReportUseModel
{
    public int UserGroupReportID { get; set; }
    public int GroupReportID { get; set; }
    public string GroupReportName { get; set; }
    public string GroupReportDescription { get; set; }
    public int ReportCount { get; set; }
}

public class UserGroupReportModel
{
    public int UserGroupReportID { get; set; }
    public int GroupReportID { get; set; }
    public int UserID { get; set; }
}


public class UserReportModel
{
    public int UserGroupReportID { get; set; }
    public int GroupReportID { get; set; }
    public int ReportID { get; set; }
    public int UserID { get; set; }
    public string ReportName { get; set; }
    //public string ReportStatus { get; set; }

}

 