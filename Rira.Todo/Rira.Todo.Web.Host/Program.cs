using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();


builder.Services.AddHostServices<Guid>(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}

app.UseAppExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddleware<Guid>>();

app.MapHealthChecks("/health");
app.MapControllers();

await app.RunAsync();