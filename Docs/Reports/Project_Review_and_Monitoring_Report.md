# Karnel Travel Guide – Project Review and Monitoring Report

**Batch:** ______ · **Group:** ______ · **Review date:** 24/11/2026

## 1. Schedule: planned against actual

Fill the Actual column from the Task Sheet before the review meeting.

| Phase | Planned | Actual | Note |
| --- | --- | --- | --- |
| 1. Analysis and planning | 29 – 31 Oct | | |
| 2. Design | 1 – 5 Nov | | |
| 3. Build: layout and information | 6 – 13 Nov | | |
| 4. Build: search, contact, admin | 14 – 20 Nov | | |
| 5. Testing | 21 – 24 Nov | | |
| 6. Documents and ZIP | 25 – 27 Nov | | |

## 2. Requirements coverage

| Requirement | Where it is met | Tested by |
| --- | --- | --- |
| FR-01 URL | `scripts/setup-hosts.ps1`, launch profile `karneltravelguide.com` | UT-01 |
| FR-02, FR-03 Menu | `Views/Shared/_Layout.cshtml` | UT-02 – UT-04 |
| FR-10, FR-11 Home | `HomeController.Index` | UT-10 – UT-12 |
| FR-20, FR-21 About Us | `HomeController.About` | UT-20, UT-21 |
| FR-30 – FR-35 Search | `SearchController` | UT-30 – UT-42 |
| FR-40 – FR-47 Information | `InformationController` | UT-50 – UT-57 |
| FR-50 – FR-54 Contact Us | `ContactController` | UT-60 – UT-71 |
| FR-60 – FR-63 Admin | `Areas/Admin` | UT-80 – UT-92 |
| NFR-01 Comments | all source files | UT-95 |
| NFR-07 Parameterised queries, hashed password | EF Core, `PasswordHasher` | UT-96, UT-81 |

## 3. Test summary

| Area | Tests | Passed | Failed | Fixed and re-tested |
| --- | --- | --- | --- | --- |
| Navigation and layout | 6 | | | |
| Home and About Us | 5 | | | |
| Search | 13 | | | |
| Information | 8 | | | |
| Contact Us | 12 | | | |
| Admin | 13 | | | |
| Code standards | 2 | | | |

## 4. Issues and decisions

| # | Issue | Decision | Owner |
| --- | --- | --- | --- |
| 1 | The assignment lists old versions (J2SDK 1.4, SQL Server 2000) | Use ASP.NET Core 8 and SQL Server 2019 as the permitted .NET and SQL Server options | A |
| 2 | One booking can point to five service tables | Store ServiceType + ServiceId; check in code (A5) | B |
| 3 | When to reduce availability | Only when staff mark a booking Contacted (A8) | A |
| 4 | Photo licences | Use free-licence photos; placeholder image when missing | C |
| 5 | | | |

## 5. Lessons learned

- 
- 
- 

## 6. Sign-off

| Role | Name | Signature | Date |
| --- | --- | --- | --- |
| Team leader (A) | | | |
| Member B | | | |
| Member C | | | |
