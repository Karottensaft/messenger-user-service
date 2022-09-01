using Microsoft.EntityFrameworkCore;
using UserMessengerService.Domain.Models;
using UserMessengerService.Infrastructure.Data;

namespace UserMessengerService.Infrastructure.Repositories;

public class UserRepository : IUserRepository<UserModel>
{
    private readonly UserDbContext _context;

    private bool _disposed;

    public UserRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserModel>> GetEntityListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UserModel> GetEntityByIdAsync(int id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User was null");
        return user;
    }

    public void PostEntity(UserModel user)
    {
        _context.Users.Add(user);
    }

    public void UpdateEntity(UserModel user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public void DeleteEntity(string username)
    {
        var user = _context.Users.Find(username);
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User was null");
        _context.Users.Remove(user);
    }

    public async Task<UserModel> GetEntityByNameAsync(string username)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        return user!;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();
        _disposed = true;
    }
}