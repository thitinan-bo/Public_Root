//public class ResponseDataModel<T>
//{
//    public T? Data { get; set; }
//    public string? ErrorCode { get; set; }
//    public string? ErrorMessage { get; set; }
//    public string? Status { get; set; }
//    public string? ErrorType { get; set; }
//    public int StatusCode { get; set; }
//}


public class ResponseDataModel<T>
{
    public T? Data { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}


//public class ApiResponse<T>
//{
//    public ResponseData<T> responseData { get; set; }
//}

//public class ResponseData<T>
//{
//    public T data { get; set; }
//    public string errorCode { get; set; }
//    public string errorMessage { get; set; }
//    public string status { get; set; }
//    public string errorType { get; set; }
//    public int statusCode { get; set; }
//}