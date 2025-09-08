using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsReporting.Models.Request
{
    public class ReqUserGroup
    {
        [Required(ErrorMessage = "UserID is required")]
        public int UserId { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 100;

    }
}
