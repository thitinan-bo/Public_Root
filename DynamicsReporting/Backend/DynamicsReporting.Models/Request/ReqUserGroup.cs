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
        public int UserID { get; set; }
        public int currentPage { get; set; } = 1;
        public int pageSize { get; set; } = 100;

    }
}
