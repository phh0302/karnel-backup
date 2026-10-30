# STATUS: Karnel Travel Guide – Status Report #2

**Batch:** ______ · **Group:** ______ · **Project:** Karnel Travel Guide · **Report date:** 17/11/2026 (day 20)

> Email subject: `STATUS: <Batch> <Group> Karnel Travel Guide – Report 2`. Attach this report and a ZIP with the latest source code, database scripts and screenshots.

## 1. Overall progress

All features in the CRS are built. The team is finishing the admin pages and starts formal testing on 21 Nov.

| Phase | Planned | Status |
| --- | --- | --- |
| 1 – 3 | 29 Oct – 13 Nov | Done |
| 4. Build: search, contact, admin | 14 – 20 Nov | Nearly done |
| 5. Testing | 21 – 24 Nov | Not started |
| 6. Documents and ZIP | 25 – 27 Nov | Not started |

## 2. Tasks completed since report #1

- Search page: tourist spots by type; hotels, restaurants and resorts by location, price, quality, quantity and availability
- Advanced Search across several types with keyword, sorting and paging
- Contact Us: booking form and feedback form with validation, saved to the database
- Admin: login, dashboard, add / edit / delete for all places, bookings with status changes, feedback list
- Setup script that points www.karneltravelguide.com to the local machine

## 3. Challenges

- Bookings can point to five different tables, so one foreign key cannot cover them. The application checks the service exists before saving (Algorithm A5).
- Availability is reduced only when staff confirm a booking, so unconfirmed requests do not block rooms.

## 4. Next steps

Run the Unit Testing Check List, fix defects, review comments in the code, write the Project Review and Monitoring Report, assemble the Project Report.

## 5. Files attached

`Status2_<Batch>_<Group>.zip`: `src/`, `Database/`, screenshots of Search, Advanced Search, Contact Us, Admin dashboard and Bookings.
