namespace CityInfo.API.Services
{
    public class PaginationMetaData
    {
        public int TotalItemCount { get; set; }
        public int TotalPageSize { get; set; }
        public int pageSize { get; set; }
        public int CurrentPage { get; set; }

        public PaginationMetaData(
            int totalItemCount,
            int pageSize,
            int currentPage
        )
        {
            TotalItemCount = totalItemCount;
            this.pageSize = pageSize;
            CurrentPage = currentPage;
            TotalPageSize = (int)Math.Ceiling((double)totalItemCount / pageSize);
        }
    }
}
