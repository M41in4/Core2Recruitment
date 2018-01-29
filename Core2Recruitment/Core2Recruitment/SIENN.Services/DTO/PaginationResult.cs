using System.Collections.Generic;

namespace SIENN.Services.DTO
{
    public class PaginationResult<TEntity>
    {
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Results { get; set; }
    }
}