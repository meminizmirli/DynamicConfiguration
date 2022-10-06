namespace Mongo.Hub.Queries.Primitives
{
    public class MongoHubPagingQuery
    {
        public MongoHubPagingQuery() { }
        public MongoHubPagingQuery(int page, int itemsPerPage)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;

        }
        public MongoHubPagingQuery(int page, int itemsPerPage, int total)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
            this.Total = total;

        }
        public MongoHubPagingQuery(int page, int itemsPerPage, int total, int totalPages)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
            this.Total = total;
            this.TotalPages = totalPages;

        }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}