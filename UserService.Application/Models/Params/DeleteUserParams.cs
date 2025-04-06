namespace UserService.Application.Models.Params;

/// <summary>
/// Удалить пользователя
/// </summary>
public class DeleteUserParams
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required Guid Id { get; set; }
}