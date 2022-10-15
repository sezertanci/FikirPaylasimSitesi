namespace Common.Models.Page
{
    public class BasePagedQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public BasePagedQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
        }
    }
}
