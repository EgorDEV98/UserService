using UserService.Application.Models.Params;
using UserService.Contracts.Models.Response;

namespace UserService.Application.Interfaces;

public interface IUsersService
{
    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public Task<AuthResponse> AuthUserAsync(AuthUserParams param, CancellationToken ct);
    
    /// <summary>
    /// Получить конкретного пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<GetUserResponse> GetUserAsync(GetUserParams param, CancellationToken ct);

    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<GetUserResponse>> GetUsersAsync(GetUsersParams param, CancellationToken ct);

    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<GetUserResponse> AddUserAsync(AddUserParams param, CancellationToken ct);

    /// <summary>
    /// Обновить данные пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<GetUserResponse> UpdateUserAsync(UpdateUserParams param, CancellationToken ct);

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="param">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    public Task<bool> DeleteUserAsync(DeleteUserParams param, CancellationToken ct);
}