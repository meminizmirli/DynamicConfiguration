using System.Collections.Generic;
using Mongo.Hub.Queries.Primitives;
using MongoDB.Driver;

namespace Mongo.Hub.Extensions.Querying
{
    public static class FilteringQueryExtension
    {
        public static IFindFluent<T, T> QueryFilter<T>(this IFindFluent<T, T> query, List<MongoHubFilteringQuery> filters)
        {
            if (filters == null)
                return query;

            var builder = Builders<T>.Filter;
            var filter = query.Filter;
            foreach (var filteringQuery in filters)
            {
                if (string.IsNullOrWhiteSpace(filteringQuery.Field))
                    continue;

                var addingFilter = builder.Eq(filteringQuery.Field, filteringQuery.Value);
                filter = filter == null
                    ? addingFilter
                    : builder.And(filter, addingFilter);
            }

            if (filter != null)
                query.Filter = filter;

            return query;
        }
    }
}