using CommonLib.Exceptions;
using CommonLib.Other.DateTimeProvider;
using CommonLib.Other.JwtProvider;
using CommonLib.Other.PasswordHasher;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Mappers;
using UserService.Application.Models.Params;
using UserService.Application.Services;
using UserService.Data.Entities;
using UserService.Tests.Mock;

namespace UserService.Tests.ServiceTests;

public class UserServiceTests
{
    private readonly UserServiceDbContextMock _context;
    private readonly UsersService _userService;
    private readonly IPasswordHasher _passwordHasher;
    public UserServiceTests()
    {
        _context = UsersMock.Create();
        _passwordHasher = new PasswordHasher();
        _userService = new UsersService(_context, new UsersServiceMapper(), new DateTimeProvider(), _passwordHasher, new JwtProvider(new DateTimeProvider()));

        Seed();
    }

    #region Auth

    [Fact]
    public async Task Auth_Success()
    {
        var result = await _userService.AuthUserAsync(new AuthUserParams()
        {
            Login = "TEST_LOGIN",
            Password = "TEST_PASSWORD",
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            result.JwtToken.Should().NotBeNullOrEmpty();
        }
    }
    
    [Fact]
    public async Task Auth_Unauthorized()
    {
        await Assert.ThrowsAsync<UnauthorizedException>(async () =>
        {
            await _userService.AuthUserAsync(new AuthUserParams()
            {
                Login = string.Empty,
                Password = string.Empty,
            }, CancellationToken.None);
        });
    }

    #endregion

    #region GetUser

    [Fact]
    public async Task GetUser_Success()
    {
        var user = await _userService.GetUserAsync(new GetUserParams()
        {
            Id = new Guid("4F66943B-D74D-4BE4-BE65-17333D71F984")
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            user.Id.Should().Be(new Guid("4F66943B-D74D-4BE4-BE65-17333D71F984"));
            user.City.Should().Be("TEST_CITY");
            user.Name.Should().Be("TEST_NAME");
            user.CreatedDate.Should().Be(new DateTime(2025, 4, 1));
            user.LastUpdate.Should().Be(new DateTime(2025, 4, 1));
        }
    }
    
    [Fact]
    public async Task GetUser_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _userService.GetUserAsync(new GetUserParams()
            {
                Id = Guid.NewGuid()
            }, CancellationToken.None);
        });
    }

    #endregion

    #region GetUser

    [Fact]
    public async Task GetUsers_Success()
    {
        var users = await _userService.GetUsersAsync(new GetUsersParams(), CancellationToken.None);

        using (new AssertionScope())
        {
            users.Count.Should().Be(2);
        }
    }
    
    [Fact]
    public async Task GetUsers_WithFiltersSuccess()
    {
        var users = await _userService.GetUsersAsync(new GetUsersParams()
        {
            LastUpdateFrom = new DateTime(2025, 4, 2),
            LastUpdateTo = new DateTime(2025, 4, 10)
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            users.Count.Should().Be(1);
        }
    }
    
    [Fact]
    public async Task GetUsers_EmptySuccess()
    {
        var users = await _userService.GetUsersAsync(new GetUsersParams()
        {
            LastUpdateFrom = new DateTime(2025, 4, 4),
            LastUpdateTo = new DateTime(2025, 4, 10)
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            users.Count.Should().Be(0);
        }
    }

    #endregion

    #region AddUser

    [Fact]
    public async Task AddUser_Success()
    {
        var user = await _userService.AddUserAsync(new AddUserParams()
        {
            Login = "TEST",
            Name = "TEST",
            City = "TEST",
            Password = "TEST"
        }, CancellationToken.None);

        var allEntitiesAfterAdd = await _context.Users.CountAsync();

        using (new AssertionScope())
        {
            user.City.Should().Be("TEST");
            allEntitiesAfterAdd.Should().BeGreaterThan(2);
        }
    }

    #endregion

    #region UpdateUser

    [Fact]
    public async Task UpdateUser_Success()
    {
        var user = await _userService.UpdateUserAsync(new UpdateUserParams()
        {
            Id = new Guid("4F66943B-D74D-4BE4-BE65-17333D71F984"),
            Name = "NEW_TEST_NAME"
        }, CancellationToken.None);

        using (new AssertionScope())
        {
            user.City.Should().Be("TEST_CITY");
            user.Name.Should().Be("NEW_TEST_NAME");
            user.LastUpdate.Should().BeAfter(user.CreatedDate);
        }
    }
    
    [Fact]
    public async Task UpdateUser_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _userService.UpdateUserAsync(new UpdateUserParams()
            {
                Id = Guid.NewGuid(),
                Name = "NEW_TEST_NAME"
            }, CancellationToken.None);
        });
    }

    #endregion
    
    #region DeleteUser

    [Fact]
    public async Task DeleteUser_Success()
    {
        var user = await _userService.DeleteUserAsync(new DeleteUserParams()
        {
            Id = new Guid("4F66943B-D74D-4BE4-BE65-17333D71F984"),
        }, CancellationToken.None);

        var currentUser = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == new Guid("4F66943B-D74D-4BE4-BE65-17333D71F984"));

        using (new AssertionScope())
        {
            user.Should().Be(true);
            currentUser.Should().Be(null);
        }
    }
    
    [Fact]
    public async Task DeleteUser_NotFound()
    {
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _userService.DeleteUserAsync(new DeleteUserParams()
            {
                Id = Guid.NewGuid(),
            }, CancellationToken.None);
        });
    }

    #endregion
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
        
        var user2 = new User()
        {
            Id = new Guid("52F7D87D-0610-4F12-BB25-BAF76EA0E276"),
            Name = "TEST_NAME_2",
            City = "TEST_CITY_2",
            Password = _passwordHasher.HashPassword("TEST_PASSWORD_2"),
            Login = "TEST_LOGIN_2",
            CreatedDate = new DateTime(2025, 4, 1),
            LastUpdate = new DateTime(2025, 4, 2),
        };
        
        _context.Users.AddRange(user, user2);
        _context.SaveChanges();
    }
}