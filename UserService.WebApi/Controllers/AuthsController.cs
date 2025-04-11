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
public class AuthsController : ControllerBase, IAuthsClient
{
    private readonly IAuthsService _authsService;

    public AuthsController(IAuthsService authsService)
    {
        _authsService = authsService;
    }
    
    /// <summary>
    /// Авторизоваться
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct">Токен</param>
    /// <returns></returns>
    public async Task<BaseResponse<AuthResponse>> AuthAsync(AuthRequest request, CancellationToken ct)
    {
        var result = await _authsService.AuthAsync(new AuthParams()
        {
            Login = request.Login,
            Password = request.Password,
        }, ct);
        return AppResponse.Create(result);
    }
}