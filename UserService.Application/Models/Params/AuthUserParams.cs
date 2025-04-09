namespace UserService.Application.Models.Params;

/// <summary>
/// Параметры авторизации
/// </summary>
public class AuthUserParams
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