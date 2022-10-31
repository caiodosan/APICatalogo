using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevius => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;

        public PagedList(List<T>items, int count, int pageNumber,int pageSize)
        {
            TotalCount =count;
            CurrentPage =pageNumber;
            PageSize =pageSize;
            TotalPage = (int)Math.Ceiling(count/(double)pageSize);
            AddRange(items);
        }

        public async static Task<PagedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = await source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

    }
}
