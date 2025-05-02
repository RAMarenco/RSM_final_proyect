# NorthWindTraders

NorthWindTraders is a full-stack application that demonstrates core enterprise functionality using clean architecture. This guide provides instructions to initialize the backend database and configure the frontend environment.

---

## 📁 Project Structure

```
northwind-traders/
│
├── backend/
│   └── RMarenco.FinalProject.NorthWindTraders/
│       └──API
│          └── Scripts/
│              └── instnwnd.sql
│
├── frontend/
│   └── northwind-traders/
│       └── .env
```

---

## 🗄️ Backend Database Initialization Guide

### ✅ Prerequisites

1. **SQL Server**: Ensure SQL Server (2019 or later) is installed and running.
2. **Connection String**: The `appsettings.json` file must have a correct connection string, for example:

  ```json
  "ConnectionStrings": {
    "NorthwindDbConnection": "Server=localhost;Database=RMarenco-Northwind;Trusted_Connection=true;Trust Server Certificate=true;MultipleActiveResultSets=true"
  }
  ```
---

### ⚙️ Steps to Initialize the Database

#### 1. Place the SQL Script

Place the `instnwnd.sql` file in the following location:

```
backend/RMarenco.FinalProject.NorthWindTraders/API/Scripts/instnwnd.sql
```

#### 2. Configure Script Settings

Verify the `ScriptSettings` section in `appsettings.json`:

```json
"ScriptSettings": {
  "FileName": "instnwnd.sql"
}
```

#### 3. Run the Application

Navigate to the API directory and run the backend:

```bash
cd backend/RMarenco.FinalProject.NorthWindTraders/API
dotnet run
```

This will:

- Check if the `Northwind` database exists.
- Create it if it does not exist.
- Execute the `instnwnd.sql` script to populate data.

#### 4. Monitor Logs

Watch the application logs for output like:

- `Database not found. Creating...`
- `Executing SQL script...`
- `Database created successfully.`

#### 5. Verify the Database

Open **SQL Server Management Studio (SSMS)** or any other SQL client and:

- Connect to your SQL Server instance.
- Verify the `Northwind` database and its tables exist with proper data.

---

### ❗ Troubleshooting

| Problem | Solution |
|---------|----------|
| `SQL script not found` | Ensure the file exists in `backend/API/Scripts/` and is correctly referenced in `appsettings.json`. |
| `Database connection error` | Verify SQL Server is running and the connection string is valid. |
| `Missing tables or data` | Check application logs for SQL execution errors and ensure the SQL script is correct. |

---

### 🔧 Developer Note

Database initialization logic is located in:

```
backend/Infra/NorthWindTraders.Infra/Persistence/DatabaseInitializer.cs
```

To reinitialize the database, manually delete it using SSMS or SQL commands and restart the backend.

---

## 🌐 Frontend Environment Setup Guide

### 1. Create a `.env` File

In the frontend directory (`frontend/northwind-traders`), create a `.env` file using the example format below:

```env
NEXT_PUBLIC_API_URL=http://localhost:5000
NEXT_PUBLIC_GOOGLE_MAPS_API_KEY=your-google-maps-api-key
NEXT_PUBLIC_GOOGLE_MAP_ID=your-google-map-id
```

Or copy from `.env.example` and update the values.

---

### 2. Required Environment Variables

| Variable | Description |
|----------|-------------|
| `NEXT_PUBLIC_API_URL` | URL of your backend API (e.g., `http://localhost:5000`) |
| `NEXT_PUBLIC_GOOGLE_MAPS_API_KEY` | API Key for Google Maps JavaScript API |
| `NEXT_PUBLIC_GOOGLE_MAP_ID` | Custom Map ID from Google Cloud Console |

---

### 3. Obtain Google Maps API Key

1. Go to [Google Cloud Console](https://console.cloud.google.com/).
2. Create a new project or select an existing one.
3. Navigate to **APIs & Services > Credentials**.
4. Click **Create Credentials > API Key**.
5. Enable **Maps JavaScript API**.
6. Restrict the key to authorized domains for security.
7. Copy the key and place it in `.env` as shown above.

---

### 4. Load Environment Variables

Next.js automatically loads `.env` files in the project root.

After updating `.env`, restart the development server:

```bash
cd frontend/northwind-traders
npm run dev
```

---

## ✅ Final Checks

- [ ] Database is initialized with data in SQL Server.
- [ ] `.env` file is correctly configured in the frontend.
- [ ] Both backend and frontend servers are running.

---

## 📎 Useful Links

- [SQL Server Download](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Google Cloud Console](https://console.cloud.google.com/)
- [Next.js Documentation](https://nextjs.org/docs)

---

## 🧑‍💻 Authors

**Author**: Roberto Marenco