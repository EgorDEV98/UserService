using CommonLib;
using CommonLib.EFCore.Extensions;
using CommonLib.Middlewares;
using UserService.Data;
using UserService.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Custom
builder.Services.AddPostgresDbContext<UserServiceDbContext>(builder.Configuration);
builder.Services.AddCommon();
builder.Services.AddServices();
builder.Services.AddMappers();

var app = builder.Build();
await app.Services.ApplyMigrationAsync<UserServiceDbContext>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();
app.MapControllers();
app.Run();