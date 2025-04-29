namespace NorthWindTraders.Domain.Interfaces
{
    public interface IPaginationHelper
    {
        Task<(IEnumerable<T> result, int TotalPages, int CurrentPage, int TotalItems)> PaginateAsync<T>(int pageNumber, int pageSize, IQueryable<T> orderQuery);
    }
}
