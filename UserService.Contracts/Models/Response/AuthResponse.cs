namespace UserService.Contracts.Models.Response;

/// <summary>
/// Модель ответа авторизации
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string JwtToken { get; set; }
}