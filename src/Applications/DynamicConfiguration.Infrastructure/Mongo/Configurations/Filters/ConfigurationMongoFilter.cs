using DynamicConfiguration.Infrastructure.Mongo.Configurations.Entities;
using MongoDB.Driver;

namespace DynamicConfiguration.Infrastructure.Mongo.Configurations.Filters
{
    public class ConfigurationMongoFilter
    {
        private FilterDefinitionBuilder<ConfigurationEntity> builder = Builders<ConfigurationEntity>.Filter;
        
        public FilterDefinition<ConfigurationEntity> _BaseFilter()
        {
            var notDeletedFilter = builder.Eq(x => x.IsDeleted, false);

            return notDeletedFilter;
        }

        public FilterDefinition<ConfigurationEntity> _IdFilter(string id)
        {
            var idFilter = builder.Eq(q => q.Id, id);

            return idFilter
                   & _BaseFilter();
        }

        public FilterDefinition<ConfigurationEntity> _StatusFilter(int status)
        {
            var statusFilter = builder.Eq(q => q.Status, status);

            return statusFilter
                   & _BaseFilter();
        }

        public FilterDefinition<ConfigurationEntity> _ApplicationNameFilter(string aplicationName, int status)
        {
            var aplicationNameFilter = builder.Eq(q => q.ApplicationName, aplicationName);

            return aplicationNameFilter
                & _StatusFilter(status);
        }
    }
}
