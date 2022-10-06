using System.Threading.Tasks;

namespace DynamicConfiguration.Core.Infrastructure.Data
{

    public interface IRepository<T, TUniqueIdentifier> where T : IEntity<TUniqueIdentifier>
    {
        Task<T> GetByIdAsync(TUniqueIdentifier id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}