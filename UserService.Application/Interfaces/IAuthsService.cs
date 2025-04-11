using UserService.Application.Models.Params;
using UserService.Contracts.Models.Response;

namespace UserService.Application.Interfaces;

public interface IAuthsService
{
    public Task<AuthResponse> AuthAsync(AuthParams param, CancellationToken ct);
}