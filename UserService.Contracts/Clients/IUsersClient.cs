using CommonLib.Response;
using Refit;
using UserService.Contracts.Models.Request;
using UserService.Contracts.Models.Response;

namespace UserService.Contracts.Clients;

public interface IUsersClient
{
    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    [Post("/auth")]
    public Task<BaseResponse<AuthResponse>> GetAuthAsync(AuthRequest request, CancellationToken ct);
    
    /// <summary>
    /// Получить конкретного пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [Get("/Users/{id}")]
    public Task<BaseResponse<GetUserResponse>> GetUserAsync(Guid id, CancellationToken ct);

    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [Get("/Users")]
    public Task<BaseResponse<IReadOnlyCollection<GetUserResponse>>> GetUsersAsync([Query(CollectionFormat.Multi)] GetUsersRequest request, CancellationToken ct);

    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [Post("/Users")]
    public Task<BaseResponse<GetUserResponse>> AddUserAsync([Body] AddUserRequest request, CancellationToken ct);

    /// <summary>
    /// Обновить данные пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [Patch("/Users/{id}")]
    public Task<BaseResponse<GetUserResponse>> UpdateUserAsync(Guid id, [Body] UpdateUserRequest request,
        CancellationToken ct);

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns></returns>
    [Delete("/Users/{id}")]
    public Task<BaseResponse> DeleteUserAsync(Guid id, CancellationToken ct);
}