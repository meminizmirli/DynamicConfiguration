using System.Collections.Generic;
using Mongo.Hub.Queries.Primitives;

namespace Mongo.Hub.Queries
{
    public class MongoHubQuery
    {
        public List<MongoHubFilteringQuery> Filter { get; set; }
        public List<MongoHubFilteringQuery> FilterContains { get; set; }
        public List<MongoHubSearchingQuery> Search { get; set; }
        public MongoHubSortingQuery Sort { get; set; }
        public MongoHubPagingQuery Paging { get; set; }
    }
}