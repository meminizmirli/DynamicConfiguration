using System.Collections.Generic;
using Mongo.Hub.Queries.Primitives;
using MongoDB.Driver;

namespace Mongo.Hub.Extensions.Querying
{
    public static class SearchingQueryExtension
    {
        public static IFindFluent<T, T> QuerySearch<T>(this IFindFluent<T, T> query, List<MongoHubSearchingQuery> searchs)
        {
            if (searchs == null)
                return query;

            var builder = Builders<T>.Filter;
            var filter = query.Filter;
            foreach (var searchingQuery in searchs)
            {
                if (string.IsNullOrWhiteSpace(searchingQuery.Field))
                    continue;

                var addingFilter = builder.Regex(searchingQuery.Field, searchingQuery.Keyword);
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