namespace ApiLoginMongo.Data
{
    public class SearchResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
