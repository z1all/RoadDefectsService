using RoadDefectsService.Presentation.Web;
using RoadDefectsService.Presentation.Web.Middlewares.Extensions;
using RoadDefectsService.Core.Application;
using RoadDefectsService.Infrastructure.Identity;
using RoadDefectsService.Infrastructure.SMTP;
using RoadDefectsService.Infrastructure.Itext7;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Services
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPresentationServices();
builder.Services.AddSmtpServices();
builder.Services.AddItext7Services();
builder.Services.AddDatabaseSeed();

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
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
