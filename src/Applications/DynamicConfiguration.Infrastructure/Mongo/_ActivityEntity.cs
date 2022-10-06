using System;

namespace DynamicConfiguration.Infrastructure.Mongo
{
    public abstract class _ActivityEntity<TKey> : _Entity<TKey>
    {
        protected _ActivityEntity() { }
        protected _ActivityEntity(DateTime createdAt, DateTime updatedAt, int status)
        {
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.Status = status;
        }
        protected _ActivityEntity(TKey id, bool isDeleted, DateTime createdAt, DateTime updatedAt, int status) : base(id, isDeleted)
        {
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.Status = status;
        }
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual DateTime UpdatedAt { get; set; } = DateTime.Now;
        public virtual int Status { get; set; }
    }
}