namespace TaggerApi.Pagination;

public interface IPagedList
{
   Task<PagedResult<T>> CreatePagedGenericResults<T>(
       IQueryable<T> queryable,
       int page,
       int pageSize,
       string orderBy,
       bool ascending
   );
}