using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicsReportingApp.Model.Authen
{
    public class AuthenRequestModel
    {
        [Required(ErrorMessage = "กรุณากรอก Username")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณากรอก Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณาเลือกสาขา")]
        public string BranchCode { get; set; } = string.Empty;
 
        public List<SelectListItem> Branches { get; set; } = new List<SelectListItem>();
    }


}


//    public class BranchModel
//    {
//        [JsonPropertyName("BranchCode")]
//        public string BranchCode { get; set; }
//        [JsonPropertyName("BranchName")]
//        public string BranchName { get; set; }
//        [JsonPropertyName("DefaultServer")]
//        public string DefaultServer { get; set; }
//    }

//}
