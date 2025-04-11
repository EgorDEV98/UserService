using CommonLib.Exceptions;
using CommonLib.Other.DateTimeProvider;
using CommonLib.Other.JwtProvider;
using CommonLib.Other.PasswordHasher;
using FluentAssertions;
using FluentAssertions.Execution;
using UserService.Application.Models.Params;
using UserService.Application.Services;
using UserService.Data.Entities;
using UserService.Tests.Mock;

namespace UserService.Tests.ServiceTests;

public class AuthServiceTests
{
    private readonly UserServiceDbContextMock _context;
    private readonly AuthService _authService;
    private readonly IPasswordHasher _passwordHasher;
    public AuthServiceTests()
    {
        _context = UsersMock.Create();
        _passwordHasher = new PasswordHasher();
        _authService = new AuthService(_context, new JwtProvider(new DateTimeProvider()), _passwordHasher);

        Seed();
    }

    [Fact]
    public async Task Login_Success()
    {
        var result = await _authService.AuthAsync(new AuthParams()
        {
            Login = "TEST_LOGIN",
            Password = "TEST_PASSWORD"
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.JwtToken.Length.Should().BeGreaterThan(1);
        }
    }
    
    [Fact]
    public async Task Login_Unauthorized_Login()
    {
        await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _authService.AuthAsync(new AuthParams()
            {
                Login = String.Empty,
                Password = "TEST_PASSWORD"
            }, CancellationToken.None);
        });
    }
    
    [Fact]
    public async Task Login_Unauthorized_Password()
    {
        await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _authService.AuthAsync(new AuthParams()
            {
                Login = "TEST_LOGIN",
                Password = String.Empty
            }, CancellationToken.None);
        });
    }

    private void Seed()
    {
        var user = new User()
        {
            Id = new Guid("4F66943B-D74D-4BE4-BE65-17333D71F984"),
            Name = "TEST_NAME",
            City = "TEST_CITY",
            Password = _passwordHasher.HashPassword("TEST_PASSWORD"),
            Login = "TEST_LOGIN",
            CreatedDate = new DateTime(2025, 4, 1),
            LastUpdate = new DateTime(2025, 4, 1),
        };
        
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}