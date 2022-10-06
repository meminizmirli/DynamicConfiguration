using Mongo.Hub.Queries.Primitives;
using MongoDB.Driver;

namespace Mongo.Hub.Extensions.Querying
{
    public static class PagingQueryExtension
    {
        public static IFindFluent<T, T> QueryPaging<T>(this IFindFluent<T, T> query, MongoHubPagingQuery paging)
        {
            paging ??= new MongoHubPagingQuery(page: 1, itemsPerPage: 50);  

            paging.Total = (int)query.CountDocuments();
            paging.TotalPages = paging.Total / paging.ItemsPerPage;

            query = query
                .Skip(paging.ItemsPerPage * (paging.Page - 1))
                .Limit(paging.ItemsPerPage);

            return query;
        }
    }
}