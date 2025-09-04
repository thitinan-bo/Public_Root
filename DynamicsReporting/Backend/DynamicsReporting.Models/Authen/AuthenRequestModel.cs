

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicsReporting.Models.Authen
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
 
        //public IEnumerable<SelectListItem> Branches { get; set; } = new List<SelectListItem>();
    }
     
}
