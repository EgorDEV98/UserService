using Microsoft.EntityFrameworkCore;
using UserService.Data.Entities;
using UserService.Data.EntityConfigurations;

namespace UserService.Data;

public class UserServiceDbContext : DbContext
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}