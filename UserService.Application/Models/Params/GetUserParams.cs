namespace UserService.Application.Models.Params;

/// <summary>
/// Получить пользователя
/// </summary>
public class GetUserParams
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid Id { get; set; }
}