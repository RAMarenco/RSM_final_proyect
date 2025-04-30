# NorthWindTraders Database Initialization Guide

This guide explains how to initialize the database for the NorthWindTraders project using the `instnwnd.sql` file.

## Prerequisites

1. **SQL Server**: Ensure you have SQL Server installed and running.
2. **Connection String**: Verify that the connection string in the `appsettings.json` file of the API project is correctly configured to point to your SQL Server instance.
3. **SQL Script**: Ensure the `instnwnd.sql` file is available in the `Scripts` folder of the API project.

## Steps to Initialize the Database

### 1. Place the SQL Script
- Copy the `instnwnd.sql` file into the `Scripts` folder located in the API project directory:
  ```
  backend/API/Scripts/instnwnd.sql
  ```

### 2. Verify the Script Settings
- Open the `appsettings.json` file in the API project and ensure the `ScriptSettings` section is configured correctly:
  ```json
  {
    "ScriptSettings": {
      "FileName": "instnwnd.sql"
    }
  }
  ```

### 3. Run the Application
- Start the API project. The database initialization logic is built into the application startup process.
- During startup, the application will:
  - Check if the database exists.
  - Create the database if it does not exist.
  - Execute the `instnwnd.sql` script to populate the database with the required data.

### 4. Monitor Logs
- Check the application logs to verify the database initialization process. Look for messages such as:
  - "Database not found. Creating..."
  - "Executing SQL script..."
  - "Database created successfully."

### 5. Verify the Database
- Open SQL Server Management Studio (SSMS) or any SQL client.
- Connect to your SQL Server instance.
- Verify that the `Northwind` database and its tables have been created and populated with data.

## Troubleshooting

### SQL Script Not Found
- Ensure the `instnwnd.sql` file is in the correct location (`backend/API/Scripts/`).
- Verify the file name in the `ScriptSettings` configuration matches the actual file name.

### Database Connection Issues
- Check the connection string in `appsettings.json`.
- Ensure SQL Server is running and accessible.

### Missing Tables or Data
- Ensure the `instnwnd.sql` script is complete and correctly formatted.
- Check the application logs for errors during script execution.

## Additional Notes
- The database initialization logic is implemented in the `DatabaseInitializer` class located in:
  ```
  backend/Infra/NorthWindTraders.Infra/Persistence/DatabaseInitializer.cs
  ```
- If you need to reinitialize the database, delete the existing database manually and restart the application.

By following these steps, you should be able to successfully initialize the NorthWindTraders database.