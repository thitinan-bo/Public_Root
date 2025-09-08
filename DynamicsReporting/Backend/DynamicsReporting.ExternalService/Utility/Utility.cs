using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace DynamicsReporting.ExternalService.Utility;

public class Utility
{
    private readonly IConfiguration _configuration;
    //private readonly IHostEnvironment _hostEnvironment;


    public Utility(IConfiguration configuration) //, IHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        //_hostEnvironment = hostEnvironment;
        //_httpContextAccessor = httpContextAccessor;
    }

    public string GetConnectionString(string sectionName)
    {
        return _configuration.GetConnectionString(sectionName) ?? "";
    }


    public string GetProjectName()
    {
        return _configuration.GetSection("ProjectName").Value ?? "";
    }

    public string GetProjectName(string sectionName)
    {
        return _configuration.GetSection(sectionName)?.Value ?? "";

    }

    public string GetSection(string sectionName)
    {
        return _configuration.GetSection(sectionName)?.Value ?? "";

    }

    public double GetCacheAbsoluteExpiration()
    {
        var cacheSection = _configuration.GetSection("CacheAbsoluteExpiration");
        if (cacheSection == null || cacheSection["Houre"] == null)
        {
            throw new InvalidOperationException("CacheAbsoluteExpiration or its 'Houre' value is not configured properly.");
        }

        return double.Parse(cacheSection["Houre"]);
    }

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostName();
        var ip = Dns.GetHostEntry(host)
            .AddressList
            .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
        return ip?.ToString() ?? "127.0.0.1";
    }


    public string GetHost()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).HostName;
    }



    public object? ConvertJsonElementToClrObject(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.String:
                return element.GetString();

            case JsonValueKind.Number:
                if (element.TryGetInt32(out var intVal)) return intVal;
                if (element.TryGetInt64(out var longVal)) return longVal;
                if (element.TryGetDecimal(out var decVal)) return decVal;
                if (element.TryGetDouble(out var dblVal)) return dblVal;
                return element.ToString();

            case JsonValueKind.True:
            case JsonValueKind.False:
                return element.GetBoolean();

            case JsonValueKind.Null:
            case JsonValueKind.Undefined:
                return null;

            case JsonValueKind.Array:
                return element.EnumerateArray()
                              .Select(ConvertJsonElementToClrObject)
                              .ToList();

            case JsonValueKind.Object:
                return element.EnumerateObject()
                              .ToDictionary(
                                  prop => prop.Name,
                                  prop => ConvertJsonElementToClrObject(prop.Value)
                              );

            default:
                return element.ToString();
        }
    }


    //public object ConvertJsonElement(JsonElement element)
    //{
    //    switch (element.ValueKind)
    //    {
    //        case JsonValueKind.String:
    //            // ถ้าเป็น DateTime หรือ Guid แปลงด้วย
    //            if (element.TryGetDateTime(out DateTime dt)) return dt;
    //            if (element.TryGetGuid(out Guid guid)) return guid;
    //            return element.GetString();

    //        case JsonValueKind.Number:
    //            if (element.TryGetInt32(out int i)) return i;
    //            if (element.TryGetInt64(out long l)) return l;
    //            if (element.TryGetDecimal(out decimal d)) return d;
    //            if (element.TryGetDouble(out double dbl)) return dbl;
    //            return element.GetRawText();

    //        case JsonValueKind.True:
    //        case JsonValueKind.False:
    //            return element.GetBoolean();

    //        case JsonValueKind.Null:
    //        case JsonValueKind.Undefined:
    //            return null;

    //        default:
    //            return element.GetRawText(); // กรณีเป็น Object/Array → return JSON string
    //    }
    //}



}