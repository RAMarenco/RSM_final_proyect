namespace NorthWindTraders.Application.DTOs
{
    public class PaginatedDto<T>
    {
        public T Data { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
}
