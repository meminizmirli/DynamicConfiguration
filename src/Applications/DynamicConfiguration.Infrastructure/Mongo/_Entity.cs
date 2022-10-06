using System.Runtime.Serialization;
using DynamicConfiguration.Core.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DynamicConfiguration.Infrastructure.Mongo
{
    public class _Entity<TKey> : IEntity<TKey>
    {
        #region Creator
        protected _Entity() { }
        #endregion

        #region Mapper
        protected _Entity(TKey id, bool isDeleted)
        {
            this.Id = id;
            this.IsDeleted = isDeleted;
        }
        #endregion

        [BsonId]
        [DataMember]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual TKey Id { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}