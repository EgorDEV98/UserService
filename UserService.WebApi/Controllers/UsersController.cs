using CommonLib.Response;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;
using UserService.Application.Models.Params;
using UserService.Contracts.Clients;
using UserService.Contracts.Models.Request;
using UserService.Contracts.Models.Response;

namespace UserService.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase, IUsersClient
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    /// <summary>
    /// Получить конкретного пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<BaseResponse<GetUserResponse>> GetUserAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await _usersService.GetUserAsync(new GetUserParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<IReadOnlyCollection<GetUserResponse>>> GetUsersAsync([FromQuery] GetUsersRequest request, CancellationToken ct)
    {
        var result = await _usersService.GetUsersAsync(new GetUsersParams()
        {
            Ids = request.Ids,
            Cities = request.Cities,
            CreatedFrom = request.CreatedFrom,
            CreatedTo = request.CreatedTo,
            LastUpdateFrom = request.LastUpdateFrom,
            LastUpdateTo = request.LastUpdateTo,
            Offset = request.Offset,
            Limit = request.Limit
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<BaseResponse<GetUserResponse>> AddUserAsync([FromBody] AddUserRequest request, CancellationToken ct)
    {
        var result = await _usersService.AddUserAsync(new AddUserParams()
        {
            Login = request.Login,
            Name = request.Name,
            Password = request.Password,
            City = request.City
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Обновить пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<BaseResponse<GetUserResponse>> UpdateUserAsync([FromRoute] Guid id,[FromBody] UpdateUserRequest request, CancellationToken ct)
    {
        var result = await _usersService.UpdateUserAsync(new UpdateUserParams()
        {
            Id = id,
            City = request.City,
            Password = request.Password,
            Name = request.Name
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="id">Идентификатоор пользователя</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<BaseResponse> DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var result = await _usersService.DeleteUserAsync(new DeleteUserParams()
        {
            Id = id
        }, ct);
        return AppResponse.Create();
    }
}