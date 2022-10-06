using Mongo.Hub.Constants;
using Mongo.Hub.Queries.Primitives;
using MongoDB.Driver;

namespace Mongo.Hub.Extensions.Querying
{
    public static class SortingQueryExtension
    {
        public static IFindFluent<T, T> QuerySorting<T>(this IFindFluent<T, T> query, MongoHubSortingQuery sorting)
        {
            if (sorting != null && !string.IsNullOrWhiteSpace(sorting.Field))
            {
                sorting.Field = sorting.Field;
                sorting.Sort = sorting.Sort.ToLower();

                var sortDefinition = sorting.Sort == SortingTypes.SORT_ASC
                    ? new SortDefinitionBuilder<T>().Ascending(sorting.Field)
                    : new SortDefinitionBuilder<T>().Descending(sorting.Field);

                query = query.Sort(sortDefinition);
            }

            return query;
        }
    }
}