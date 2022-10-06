using System;

namespace DynamicConfiguration.Application.Base.Dtos
{
    public class BaseActivityDto<TKey> : BaseDto<TKey>
    {
        public BaseActivityDto() { }
        public BaseActivityDto(TKey id, int status, DateTime createdAt, DateTime updatedAt) : base(id)
        {
            Status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}