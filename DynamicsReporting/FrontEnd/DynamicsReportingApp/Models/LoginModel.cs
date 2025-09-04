using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace DynamicsReportingApp.Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "กรุณากรอก User")]
        [Display(Name = "User")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "กรุณากรอก Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "กรุณาเลือกสาขา")]
        [Display(Name = "สาขา")]
        public required string Branch { get; set; }
    }
}
