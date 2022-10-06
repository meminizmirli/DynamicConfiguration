namespace Mongo.Hub.Queries.Primitives
{
    public class MongoHubSortingQuery
    {
        public MongoHubSortingQuery() { }
        public MongoHubSortingQuery(string @in, string field, string sort)
        {
            this.In = @in;
            this.Field = field;
            this.Sort = sort;

        }
        public string In { get; set; }
        public string Field { get; set; }
        public string Sort { get; set; }
    }
}