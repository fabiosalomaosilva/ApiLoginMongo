using ApiLoginMongo.Data;

namespace ApiLoginMongo.Services.Interfaces
{
    public interface IBaseService<T>
    {
        Task<SearchResult<T>> GetAllAsync(int page, int pageSize);
        Task<T> GetAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
    }
}
