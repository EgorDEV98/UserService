using Riok.Mapperly.Abstractions;
using UserService.Contracts.Models.Response;
using UserService.Data.Entities;

namespace UserService.Application.Mappers;

[Mapper]
public partial class UsersServiceMapper
{
    public partial GetUserResponse Map(User entity);
    public partial GetUserResponse[] Map(User[] entities);
}