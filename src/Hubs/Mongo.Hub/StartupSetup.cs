using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo.Hub.Context;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Mongo.Hub
{
    public static class StartupSetup
    {
        private static readonly MongoHubSettings _mongoHubSettings = new MongoHubSettings();

        public static IServiceCollection AddMongoHub(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind("MongoHubSettings", _mongoHubSettings);
            services.AddSingleton(_mongoHubSettings);
            services.AddSingleton<IMongoHubContext, MongoHubContext>(x => new MongoHubContext(_mongoHubSettings.ConnectionStrings));
            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local));
            services.AddHealthChecks().AddCheck<MongoHubHealthCheck>("Mongo.Hub");
            return services;
        }
    }
}