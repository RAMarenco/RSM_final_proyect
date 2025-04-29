using Microsoft.Extensions.Options;
using NorthWindTraders.Api.Middlewares;
using NorthWindTraders.Application;
using NorthWindTraders.Infra;
using NorthWindTraders.Infra.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ScriptSettings>(
    builder.Configuration.GetSection("ScriptSettings"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    var scriptSettings = services.GetRequiredService<IOptions<ScriptSettings>>();
    var logger = services.GetRequiredService<ILogger<DatabaseInitializer>>();
    var databaseInitializer = new DatabaseInitializer(scriptSettings, logger);

    await databaseInitializer.InitializeAsync(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
