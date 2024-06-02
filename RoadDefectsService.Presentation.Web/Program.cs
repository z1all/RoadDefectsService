using RoadDefectsService.Presentation.Web;
using RoadDefectsService.Presentation.Web.Middlewares.Extensions;
using RoadDefectsService.Infrastructure.Identity;
using RoadDefectsService.Core.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddPresentationServices();
builder.Services.AddIdentityServices();
builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Exceptions handler
app.UseExceptionsHandler();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
