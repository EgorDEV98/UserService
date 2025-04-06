namespace UserService.Application.Models.Params;

/// <summary>
/// Добавить пользователя
/// </summary>
public class AddUserParams
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public required string Login { get; set; }
    
    /// <summary>
    /// Зишифрованный пароль пользователя
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