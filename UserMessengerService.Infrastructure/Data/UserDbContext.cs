﻿using Microsoft.EntityFrameworkCore;
using UserMessengerService.Domain.Models;

namespace UserMessengerService.Infrastructure.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserModel> Users => Set<UserModel>();
}