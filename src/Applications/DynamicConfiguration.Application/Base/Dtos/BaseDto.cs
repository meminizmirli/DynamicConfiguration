namespace DynamicConfiguration.Application.Base.Dtos
{
    public class BaseDto<TKey>
    {
        public BaseDto() { }

        public BaseDto(TKey id)
        {
            Id = id;
        }

        public TKey Id { get; set; }
    }
}