using Microsoft.EntityFrameworkCore;
using UserMessengerService.Domain.Models;
using UserMessengerService.Infrastructure.Data;

namespace UserMessengerService.Infrastructure.Repositories;

public class UnitOfWork : IDisposable
{
    private readonly UserDbContext _context;

    private bool _disposed;

    public UnitOfWork(DbContextOptions<UserDbContext> options)
    {
        _context = new UserDbContext(options);
        UserRepository = new UserRepository(_context);
    }

    public UserRepository UserRepository { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();
        _disposed = true;
    }
}