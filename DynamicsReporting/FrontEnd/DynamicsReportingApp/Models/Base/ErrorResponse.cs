using System.Text.Json.Serialization;

 
public class ErrorResponse
{
    public int? StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
}
