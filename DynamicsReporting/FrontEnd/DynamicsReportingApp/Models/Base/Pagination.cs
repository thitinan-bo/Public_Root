using DynamicsReporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicsReporting.Models
{ 
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }


    public class PaginatedResult<T> : ErrorResponse
    {
        public List<T> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public Pagination Pagination { get; set; } = new();
    }





}
