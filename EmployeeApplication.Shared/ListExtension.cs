namespace EmployeeApplication.Shared
{
    public static class ListExtension
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> list, int page, int pageSize, long count)
        {
            var items = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPage = (int)Math.Ceiling((decimal)count / pageSize);

            return new PagedList<T>(items, page, totalPage, pageSize, count);
        }
    }
}