namespace DynamicsReporting.Models;

using System.Text.Json.Serialization;

public class BranchModel
{
    [JsonPropertyName("BranchCode")]
    public string branch_code { get; set; }
    [JsonPropertyName("BranchName")]
    public string branch_name { get; set; }
    [JsonPropertyName("DefaultServer")]
    public string default_server { get; set; }
}
