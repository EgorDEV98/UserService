namespace UserService.Contracts.Models.Response;

/// <summary>
/// Пользователь
/// </summary>
public class GetUserResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Город пользователя
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