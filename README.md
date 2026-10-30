# Karnel Travel Guide

Aptech eProject: a travel information website for **Karnel Travels**, served at `http://www.karneltravelguide.com`.

Built with **ASP.NET Core MVC 8 (C#)**, **Entity Framework Core** and **Microsoft SQL Server**.

## Folder layout

| Folder | Contents |
| --- | --- |
| `Docs/Assignment` | The original eProject brief and student guide |
| `Docs/Requirements` | PRD (Vietnamese) and Customer Requirement Specification |
| `Docs/Planning` | Workflow page, to-do checklist, Project Plan & Task Sheet |
| `Docs/Design` | ERD, database design, algorithms, GUI standards, interface design |
| `Docs/Testing` | Unit testing checklist |
| `Docs/Reports` | Status reports, review report, final checklist, setup guide |
| `scripts` | Windows hosts-file script for www.karneltravelguide.com |
| `Database` | SQL Server scripts: schema, then sample data |
| `src/KarnelTravelGuide` | The website source code |

## Quick start

1. Run `Database/01_create_schema.sql` then `Database/02_seed_data.sql` in SQL Server Management Studio.
2. Check the connection string in `src/KarnelTravelGuide/appsettings.json`.
3. Open `KarnelTravelGuide.sln` in Visual Studio 2022 and press F5, or run `dotnet run --project src/KarnelTravelGuide`.
4. To use the real domain locally, follow `Docs/Reports/Setup_Guide.md`.

Admin area: `/Admin` — demo login `admin` / `Admin@123`.

## Team

| Member | Role |
| --- | --- |
| A | Team leader, back-end lead |
| B | Database and content lead |
| C | Front-end and QA lead |
