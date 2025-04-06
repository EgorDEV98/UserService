using CommonLib.EFCore.Interfaces;

namespace UserService.Data.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Зашифрованный пароль пользователя
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// город пользователя
    /// </summary>
    public string? City { get; set; }
    
    /// <summary>
    /// Дата создания объекта
    /// </summary>
    public DateTime CreatedDate { get; set; }
    
    /// <summary>
    /// Дата последнего обновления
    /// </summary>
    public DateTime LastUpdate { get; set; }
}