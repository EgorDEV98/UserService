namespace UserService.Application.Models.Params;

public class UpdateUserParams
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid Id { get; set; }
    
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