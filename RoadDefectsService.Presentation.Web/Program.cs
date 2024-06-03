using RoadDefectsService.Presentation.Web;
using RoadDefectsService.Presentation.Web.Middlewares.Extensions;
using RoadDefectsService.Infrastructure.Identity;
using RoadDefectsService.Core.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Services
builder.Services.AddPresentationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Exceptions handler
app.UseExceptionsHandler();

// DataBase
app.Services.AddAutoMigration();
app.Services.AddDatabaseSeed();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
