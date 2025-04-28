using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace NorthWindTraders.Infra.Persistence
{
    public partial class DatabaseInitializer(IOptions<ScriptSettings> scriptSettings, ILogger logger)
    {
        private readonly ScriptSettings _scriptSettings = scriptSettings.Value;
        private readonly ILogger _logger = logger;
        public async Task InitializeAsync(AppDbContext dbContext)
        {
            // Ensure the database is created
            await dbContext.Database.EnsureCreatedAsync();

            // Check if Northwind already exists
            var connection = dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customers'";
            var result = (int)await command.ExecuteScalarAsync();

            if (result == 0)
            {
                _logger.LogInformation("Northwind data not found. Executing SQL script...");

                var filePath = Path.Combine("Scripts", _scriptSettings.FileName);

                // Check if the file exists before reading
                if (!File.Exists(filePath))
                {
                    _logger.LogError($"SQL script file not found: {filePath}");
                    throw new FileNotFoundException($"SQL script file not found: {filePath}");
                }

                var sqlScript = await File.ReadAllTextAsync(filePath);
                await ExecuteSqlScriptAsync(dbContext, sqlScript);
            }
            else
            {
                _logger.LogInformation("Northwind data already exists. Skipping SQL script execution.");
            }
        }

        private async Task ExecuteSqlScriptAsync(AppDbContext dbContext, string sqlScript)
        {
            var sqlCommands = ScriptRgex().Split(sqlScript)
                .Where(command => !string.IsNullOrWhiteSpace(command))
                .ToArray();

            foreach (var command in sqlCommands)
            {
                if (!string.IsNullOrWhiteSpace(command))
                {
                    _logger.LogInformation($"Executing command: {command}");
                    await dbContext.Database.ExecuteSqlRawAsync(command);
                }
            }
        }

        [GeneratedRegex(@"(?<=\r?\n)GO[\s\r\n]*(?=\r?\n)", RegexOptions.IgnoreCase, "es-SV")]
        private static partial Regex ScriptRgex();
    }

    public class ScriptSettings
    {
        public required string FileName { get; set; }
    }
}
