using Microsoft.EntityFrameworkCore;
using SucursalService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SucursalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SucursalDB")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SucursalDbContext>();
    DbInitializer.Initialize(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
});

app.UseAuthorization();
app.MapControllers();
app.Run();