using RoadDefectsService.Presentation.Web;
using RoadDefectsService.Presentation.Web.Middlewares.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddPresentationServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Exceptions handler
app.UseExceptionsHandler();

app.UseCors();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
