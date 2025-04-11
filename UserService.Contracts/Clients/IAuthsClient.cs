using CommonLib.Response;
using Refit;
using UserService.Contracts.Models.Request;
using UserService.Contracts.Models.Response;

namespace UserService.Contracts.Clients;

public interface IAuthsClient
{
    /// <summary>
    /// Получить токен авторизации
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [Post("/Auths/Auth")]
    public Task<BaseResponse<AuthResponse>> AuthAsync(AuthRequest request, CancellationToken ct);
}