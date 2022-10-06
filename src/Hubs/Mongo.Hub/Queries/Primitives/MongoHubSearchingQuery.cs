namespace Mongo.Hub.Queries.Primitives
{
    public class MongoHubSearchingQuery
    {
        public MongoHubSearchingQuery() { }
        public MongoHubSearchingQuery(string @in, string field, string keyword)
        {
            In = @in;
            Field = field;
            Keyword = keyword;
        }

        public string In { get; set; }
        public string Field { get; set; }
        public string Keyword { get; set; }
    }
}