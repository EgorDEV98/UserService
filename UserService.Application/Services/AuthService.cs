using CommonLib.Exceptions;
using CommonLib.Other.JwtProvider;
using CommonLib.Other.PasswordHasher;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Application.Models.Params;
using UserService.Contracts.Models.Response;
using UserService.Data;

namespace UserService.Application.Services;

public class AuthService : IAuthsService
{
    private readonly UserServiceDbContext _context;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(UserServiceDbContext context, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
    {
        _context = context;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }
    
    /// <summary>
    /// Авторизоваться
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<AuthResponse> AuthAsync(AuthParams param, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == param.Login, ct);
        if(user is null) UnauthorizedException.Throw("Invalid login or password");
        
        var isCorrectPassword = _passwordHasher.VerifyPassword(param.Password, user!.Password);
        if(!isCorrectPassword) UnauthorizedException.Throw("Invalid login or password");
        
        var jwt = _jwtProvider.GenerateJwtToken(new JwtModel()
        {
            UserId = user!.Id,
            Name = param.Password,
        });

        return new AuthResponse()
        {
            JwtToken = jwt
        };
    }
}