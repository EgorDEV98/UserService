namespace UserService.Tests.Mock;

public class UsersMock
{
    public static UserServiceDbContextMock Create()
    {
        var chargePointDbContextMock = new UserServiceDbContextMock();
        chargePointDbContextMock.Database.EnsureDeleted();
        chargePointDbContextMock.Database.EnsureCreated();
        chargePointDbContextMock.SaveChanges();
        return chargePointDbContextMock;
    }
}