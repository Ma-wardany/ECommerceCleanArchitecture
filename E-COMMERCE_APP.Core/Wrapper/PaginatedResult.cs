using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Wrapper
{
    public class PaginatedResult<T>
    {
        public List<T>? Items { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);


        public static async Task<PaginatedResult<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize = 10)
        {
            int totalCount = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            if (items.Any())
            {
                return new PaginatedResult<T>
                {
                    Items = items,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                };
            }
            return null;
        }
    }
}
