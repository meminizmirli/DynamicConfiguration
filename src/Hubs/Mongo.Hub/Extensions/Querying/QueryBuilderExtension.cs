using Mongo.Hub.Queries;
using MongoDB.Driver;

namespace Mongo.Hub.Extensions.Querying
{
    public static class QueryBuilderExtension
    {
        public static IFindFluent<T, T> QueryBuild<T>(this IFindFluent<T, T> query, MongoHubQuery mongoHubQuery)
        {
            query = query
                .QueryFilter(mongoHubQuery.Filter)
                .QuerySearch(mongoHubQuery.Search)
                .QuerySorting(mongoHubQuery.Sort)
                .QueryPaging(mongoHubQuery.Paging);

            return query;
        }
    }
}