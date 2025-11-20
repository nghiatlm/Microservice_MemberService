
namespace MemberService.BO.Common
{
    public class PageResult<T>
    {
        public PageResult(List<T> items, int totalItems, int totalPages, int currentPage, int pageSize)
        {

            TotalItems = totalItems;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Items = items;
        }

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}