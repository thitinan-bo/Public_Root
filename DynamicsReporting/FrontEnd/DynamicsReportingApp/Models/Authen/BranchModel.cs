using System.Text.Json.Serialization;

namespace DynamicsReportingApp.Model.Authen
{



    //public class BranchModel
    //{
    //    [JsonPropertyName("BranchCode")]
    //    public string branch_code { get; set; }
    //    [JsonPropertyName("BranchName")]
    //    public string branch_name { get; set; }
    //    [JsonPropertyName("DefaultServer")]
    //    public string default_server { get; set; }
    //}

    public class BranchModel
    {
        [JsonPropertyName("BranchCode")]
        public string BranchCode { get; set; }
        [JsonPropertyName("BranchName")]
        public string BranchName { get; set; }
        [JsonPropertyName("DefaultServer")]
        public string DefaultServer { get; set; }
    }




}
