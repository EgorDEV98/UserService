namespace UserService.Contracts.Models.Request;

/// <summary>
/// Модель добавления пользователя
/// </summary>
public class AddUserRequest
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Город пользователя
    /// </summary>
    public string? City { get; set; }
}