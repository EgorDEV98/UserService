namespace UserService.Application.Models.Params;

public class AuthParams
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