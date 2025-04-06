namespace UserService.Contracts.Models.Request;

/// <summary>
/// Обновляемые параметры
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Город пользователя
    /// </summary>
    public string? City { get; set; }
}