namespace UserService.Contracts.Models.Request;

/// <summary>
/// Запрос списка пользователей
/// </summary>
public class GetUsersRequest
{
    /// <summary>
    /// Массив Id пользователей
    /// </summary>
    public Guid[]? Ids { get; set; }
    
    /// <summary>
    /// Список городов
    /// </summary>
    public string[]? Cities { get; set; }
    
    /// <summary>
    /// Созданы с какой даты
    /// </summary>
    public DateTime? CreatedFrom { get; set; }
    
    /// <summary>
    /// Созданы до какой даты
    /// </summary>
    public DateTime? CreatedTo { get; set; }
    
    /// <summary>
    /// Последнее обновление С
    /// </summary>
    public DateTime? LastUpdateFrom { get; set; }
    
    /// <summary>
    /// Последнее обновление До
    /// </summary>
    public DateTime? LastUpdateTo { get; set; }
    
    /// <summary>
    /// Отступ
    /// </summary>
    public int? Offset { get; set; }
    
    /// <summary>
    /// Лимит в выборке
    /// </summary>
    public int? Limit { get; set; }
}