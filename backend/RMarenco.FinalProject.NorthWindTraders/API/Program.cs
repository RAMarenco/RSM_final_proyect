using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NorthWindTraders.Api.Middlewares;
using NorthWindTraders.Application;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Infra;
using NorthWindTraders.Infra.Persistence;
using QuestPDF.Infrastructure;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    // OR customize the response:
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        throw new BadRequestException("Validation errors occurred.", errors);
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();

        });
});

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

app.UseCors(MyAllowSpecificOrigins);
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }