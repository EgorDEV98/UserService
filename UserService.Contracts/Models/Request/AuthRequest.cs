namespace UserService.Contracts.Models.Request;

/// <summary>
/// Модель атворизации пользователя
/// </summary>
public class AuthRequest
{
    /// <summary>
    /// Логин
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
}