namespace UserService.Contracts.Models.Request;

/// <summary>
/// Модель авторизации пользователя
/// </summary>
public class AuthRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string Password { get; set; }
}