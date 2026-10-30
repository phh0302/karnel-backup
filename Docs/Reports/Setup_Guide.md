# Karnel Travel Guide – Setup Guide and Login Credentials

## 1. What you need

| Software | Version |
| --- | --- |
| Windows | 10 or 11 |
| .NET SDK | 8.0 |
| SQL Server | 2019 or later (Express or Developer is fine) |
| SQL Server Management Studio | any recent version |
| Visual Studio 2022 | 17.8 or later, "ASP.NET and web development" workload (optional; `dotnet run` also works) |
| Internet connection | needed for Bootstrap, jQuery and Google Fonts, which load from CDNs |

## 2. Create the database

1. Open SQL Server Management Studio and connect to your server.
2. Open `Database/01_create_schema.sql` and press **Execute**. It creates `KarnelTravelDB` (and drops an old copy first).
3. Open `Database/02_seed_data.sql` and press **Execute**. It loads the sample data.

## 3. Connection string

Edit `src/KarnelTravelGuide/appsettings.json`:

```json
"ConnectionStrings": {
  "KarnelDb": "Server=localhost;Database=KarnelTravelDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
}
```

If you installed SQL Server Express, use `Server=.\\SQLEXPRESS` instead of `Server=localhost`.

## 4. Run the website

```
cd src/KarnelTravelGuide
dotnet run --launch-profile http
```

Open http://localhost:5080. In Visual Studio, open `KarnelTravelGuide.sln`, choose the **http** profile and press F5.

## 5. Use the real address www.karneltravelguide.com (CRS FR-01)

1. Open PowerShell **as Administrator** in the project folder and run:
   `powershell -ExecutionPolicy Bypass -File scripts\setup-hosts.ps1`
   This adds `127.0.0.1 www.karneltravelguide.com` to the Windows hosts file.
2. Start the site with the **karneltravelguide.com** profile, which listens on port 80:
   `dotnet run --launch-profile karneltravelguide.com` (run the terminal as Administrator if port 80 is refused).
3. Open http://www.karneltravelguide.com.

To undo step 1: `powershell -ExecutionPolicy Bypass -File scripts\setup-hosts.ps1 -Remove`.

## 6. Photos

The sample data points to photos such as `/images/hotels/rex-saigon.jpg`. Put the photo files in `src/KarnelTravelGuide/wwwroot/images/<folder>/` with those exact names. The full list is in `Docs/Design/Database_Design.md`, section 7. Until then, every card shows a placeholder picture.

## 7. Login credentials

| What | Value |
| --- | --- |
| Admin page | http://localhost:5080/Admin (or http://www.karneltravelguide.com/Admin) |
| Admin username | `admin` |
| Admin password | `Admin@123` |
| Database | `KarnelTravelDB` on your SQL Server, Windows authentication |

Change the admin password before any real use. The `Admins.PasswordHash` column stores an ASP.NET Core `PasswordHasher` hash, not the password.

## 8. Backup for the final submission

Create `C:\KarnelBackup`, then run `Database/03_backup_database.sql`. Put `KarnelTravelDB.bak` in the final ZIP under `Database/`.
