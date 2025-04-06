using CommonLib.EFCore.Extensions;
using CommonLib.Exceptions;
using CommonLib.Other.DateTimeProvider;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Application.Mappers;
using UserService.Application.Models.Params;
using UserService.Contracts.Models.Response;
using UserService.Data;
using UserService.Data.Entities;

namespace UserService.Application.Services;

public class UsersService : IUsersService
{
    private readonly UserServiceDbContext _context;
    private readonly UsersServiceMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UsersService(UserServiceDbContext context, UsersServiceMapper mapper, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }
    
    /// <summary>
    /// Получить пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetUserResponse> GetUserAsync(GetUserParams param, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (user is null) NotFoundException.Throw("User is not found");
        return _mapper.Map(user!);
    }

    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<IReadOnlyCollection<GetUserResponse>> GetUsersAsync(GetUsersParams param, CancellationToken ct)
    {
        var users = await _context.Users
            .WhereIf(param.Ids is { Length: > 0 }, x => param.Ids!.Contains(x.Id))
            .WhereIf(param.Cities is { Length: > 0 }, x => param.Cities!.Contains(x.City))
            .WhereIf(param.CreatedFrom.HasValue, x => x.CreatedDate >= param.CreatedFrom)
            .WhereIf(param.CreatedTo.HasValue, x => x.CreatedDate <= param.CreatedTo)
            .WhereIf(param.LastUpdateFrom.HasValue, x => x.LastUpdate >= param.LastUpdateFrom)
            .WhereIf(param.LastUpdateTo.HasValue, x => x.LastUpdate <= param.LastUpdateTo)
            .Skip(param.Offset ?? 0)
            .Take(param.Limit ?? 100)
            .ToArrayAsync(ct);
        return _mapper.Map(users);
    }

    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetUserResponse> AddUserAsync(AddUserParams param, CancellationToken ct)
    {
        var currentDate = _dateTimeProvider.GetCurrent();
        var newUser = new User()
        {
            Name = param.Name,
            Login = param.Login,
            Password = param.Password,
            City = param.City,
            CreatedDate = currentDate,
            LastUpdate = currentDate
        };

        await _context.Users.AddAsync(newUser, ct);
        await _context.SaveChangesAsync(ct);

        return _mapper.Map(newUser);
    }

    /// <summary>
    /// Обновить данные пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<GetUserResponse> UpdateUserAsync(UpdateUserParams param, CancellationToken ct)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (user is null) NotFoundException.Throw("User is not found");

        user!.City = param.City ?? user.City;
        user.Password = param.Password ?? user.Password;
        user.Name = param.Name ?? user.Name;
        user.LastUpdate = _dateTimeProvider.GetCurrent();

        await _context.SaveChangesAsync(ct);
        return _mapper.Map(user);
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public async Task<bool> DeleteUserAsync(DeleteUserParams param, CancellationToken ct)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == param.Id, ct);
        if (user is null) NotFoundException.Throw("User is not found!");

        _context.Users.Remove(user!);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}