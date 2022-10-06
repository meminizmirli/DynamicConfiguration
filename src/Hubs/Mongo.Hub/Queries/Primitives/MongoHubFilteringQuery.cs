namespace Mongo.Hub.Queries.Primitives
{
    public class MongoHubFilteringQuery
    {
        public MongoHubFilteringQuery() { }
        public MongoHubFilteringQuery(string field, string value)
        {
            Field = field;
            Value = value;
        }
        public MongoHubFilteringQuery(string @in, string field, string value)
        {
            In = @in;
            Field = field;
            Value = value;
        }

        public string In { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }
}