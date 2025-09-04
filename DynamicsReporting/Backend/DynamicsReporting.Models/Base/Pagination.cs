namespace DynamicsReporting.Models
{

    //public class Pagination
    //{
    //    public int CurrentPage { get; set; }
    //    public int PageSize { get; set; }
    //    public int TotalRecords { get; set; }
    //    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    //}


    //public class PaginatedResult<T> : ErrorResponse
    //{
    //    public List<T> Data { get; set; } = new();
    //    public int TotalCount { get; set; }
    //    public Pagination Pagination { get; set; } = new();
    //}

    public class Pagination
    {
        public int CurrentPage { get; set; }      // หน้าที่กำลังดูอยู่
        public int PageSize { get; set; }         // จำนวน record ต่อหน้า
        public int TotalRecords { get; set; }     // จำนวน record ทั้งหมด
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    }

    public class PaginatedResult<T> : ErrorResponse
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; } = new();   // ข้อมูลที่ใช้แสดงในหน้านี้
        public Pagination Pagination { get; set; } = new();
    }
}