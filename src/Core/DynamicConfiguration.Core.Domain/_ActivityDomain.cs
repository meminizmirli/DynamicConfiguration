using System;
using Ardalis.GuardClauses;
using DynamicConfiguration.Core.Domain.Constants;

namespace DynamicConfiguration.Core.Domain
{
    public abstract class _ActivityDomain<TKey> : _Domain<TKey>
    {
        protected _ActivityDomain() { }

        #region Mapper
        protected _ActivityDomain(TKey id, bool isDeleted, DateTime createdAt, DateTime updatedAt, int status) : base(id, isDeleted)
        {
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.Status = Guard.Against.Negative(status, nameof(status));
        }
        #endregion

        public DateTime CreatedAt { get; protected set; } = DateTime.Now;
        public DateTime UpdatedAt { get; protected set; } = DateTime.Now;
        public virtual int Status { get; protected set; } = BaseStatus.Active;

        #region Markers
        public void MarkActive()
        {
            this.Status = BaseStatus.Active;
            MarkUpdated();
        }

        public void MarkPassive()
        {
            this.Status = BaseStatus.Passive;
            MarkUpdated();
        }

        public override void MarkDeleted()
        {
            this.IsDeleted = true;
            MarkUpdated();
        }

        public void MarkUpdated()
        {
            this.UpdatedAt = DateTime.Now;
        }
        #endregion
    }
}