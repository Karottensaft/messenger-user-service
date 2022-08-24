using UserMessengerService.Domain.Models;

namespace UserMessengerService.Infrastructure.Repositories
{
    public interface IUserRepository<T> : IDisposable
        where T : class
    {
        Task<IEnumerable<T>> GetEntityListAsync();

        Task<T> GetEntityByIdAsync(int id);

        void PostEntity(T entity);

        void UpdateEntity(T entity);

        void DeleteEntity(int id);

        Task<UserModel> GetEntityByNameAsync(string username);
    }
}
