namespace CustomTagBuilder.Models
{
    public class PagingInfo
    {
        public int TotalItemsCount { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get => (int)Math.Ceiling((double)TotalItemsCount / ItemsPerPage); }
    }
}
