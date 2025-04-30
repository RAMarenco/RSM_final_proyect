using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            _logger.LogInformation("Verifying Database...");

            // 1. Ensure database exists without creating tables
            await EnsureDatabaseExistsAsync(dbContext);

            // 2. Check if tables exist (your existing code)
            _logger.LogInformation("Verifying Data...");
            var connection = dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Customers'";
            var result = (int)await command.ExecuteScalarAsync();

            if (result == 0)
            {
                _logger.LogInformation("Northwind data not found. Executing SQL script...");
                var filePath = Path.Combine("Scripts", _scriptSettings.FileName);

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

        private async Task EnsureDatabaseExistsAsync(AppDbContext dbContext)
        {
            var connection = dbContext.Database.GetDbConnection();
            var databaseName = connection.Database;
            var originalConnectionString = connection.ConnectionString;

            var masterConnectionString = new SqlConnectionStringBuilder(originalConnectionString)
            {
                InitialCatalog = "master"
            }.ToString();

            await using var masterConnection = new SqlConnection(masterConnectionString);
            await masterConnection.OpenAsync();

            // Verificar si la base ya existe
            await using var checkCommand = masterConnection.CreateCommand();
            checkCommand.CommandText = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{databaseName}'";
            var exists = (int)await checkCommand.ExecuteScalarAsync() > 0;

            if (!exists)
            {
                _logger.LogInformation("Database not found. Creating...");

                await using var createCommand = masterConnection.CreateCommand();
                createCommand.CommandText = $"CREATE DATABASE [{databaseName}]";
                await createCommand.ExecuteNonQueryAsync();

                _logger.LogInformation("Database created successfully.");
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
