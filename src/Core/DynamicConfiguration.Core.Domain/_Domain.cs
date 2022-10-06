using System;
using DynamicConfiguration.Core.Domain.Abstractions.Data;

namespace DynamicConfiguration.Core.Domain
{
    public abstract class _Domain<TKey> : IDomain<TKey>
    {
        protected _Domain() { }

        protected _Domain(TKey id)
        {
            this.Id = id;
        }
        protected _Domain(TKey id, bool isDeleted) : this(id)
        {
            this.IsDeleted = isDeleted;
        }
        public TKey Id { get; protected set; }
        public bool IsDeleted { get; protected set; }

        #region Actions
        public virtual void MarkDeleted() => this.IsDeleted = true;
        public virtual void UndoDeleted() => this.IsDeleted = false;

        public void SetChanges(TKey id)
        {
            this.Id = id;
        }

        public virtual bool HasKey()
        {

            if (this.Id is int intId)
                return intId != 0;
            else if (this.Id is string stringId)
                return !string.IsNullOrEmpty(stringId);
            else if (this.Id == null)
                return false;

            throw new Exception("Undefined Key.");
            //throw new DomainKeyCannotBeDefinedException();
        }
        #endregion
    }
}