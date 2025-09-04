using System.ComponentModel.DataAnnotations;

public class PageConfig
{
    public int pageId { get; set; }
    public List<PageConfigItem> PageConfigItem { get; set; }
}

public class PageConfigItem
{
    public string serverdb { get; set; }
    public string dbName { get; set; }
    public string sp { get; set; }
    public Dictionary<string, object> Parameter { get; set; }
}

//public class PageConfigItem
//{
//    public string serverdb { get; set; }
//    public string dbName { get; set; }
//    public string sp { get; set; }
//    public   Dictionary<string, object> parameters { get; set; }
//}


//public class PageConfig
//{
//    public int pageId { get; set; }
//    public List<PageConfigItem> pageConfigItem { get; set; }
//}





//public class PageConfigItem
//{
//    public required string Serverdb { get; set; }   // Branch / Instance
//    public required string DbName { get; set; }     // Database
//    public required string Sp { get; set; }         // Stored Procedure Name
//    public required Dictionary<string, object> Parameters { get; set; }
//}

//public class PageConfig
//{
//    public int PageId { get; set; }
//    public required List<PageConfigItem> PageConfigItem { get; set; }
//}
