using Microsoft.EntityFrameworkCore;
using NorthWindTraders.Domain.Interfaces;

namespace NorthWindTraders.Infra.Helpers
{
    public class PaginationHelper : IPaginationHelper
    {
        public async Task<(IEnumerable<T> result, int TotalPages, int CurrentPage, int TotalItems)> PaginateAsync<T>(int pageNumber, int pageSize, IQueryable<T> orderQuery)
        {
            pageNumber = Math.Abs(pageNumber);

            var totalItems = await orderQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages == 0 ? 1 : totalPages;
            }

            var pagedOrders = await orderQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (pagedOrders, totalPages, pageNumber, totalItems);
        }
    }
}
