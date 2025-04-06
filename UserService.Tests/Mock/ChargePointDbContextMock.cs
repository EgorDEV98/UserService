using Microsoft.EntityFrameworkCore;
using UserService.Data;

namespace UserService.Tests.Mock;

public class UserServiceDbContextMock : UserServiceDbContext
{
    public UserServiceDbContextMock()
        : base(new DbContextOptions<UserServiceDbContext>()) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}