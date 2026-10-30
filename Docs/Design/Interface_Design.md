# Karnel Travel Guide – Interface Design Document

For each screen: the address, the controller action that serves it, what it shows, what the user can enter, and where it leads. Requirement IDs refer to the CRS.

## Public pages

| Screen | URL | Controller / action | Shows | User input | Leads to | CRS |
| --- | --- | --- | --- | --- | --- | --- |
| Home | `/` | `HomeController.Index` | Hero with quick search, featured spots (`IsFeatured`), active packages, services | Location, type | Search results | FR-10, FR-11 |
| About Us | `/About` | `HomeController.About` | Company story, three facilities, key numbers | – | Contact Us | FR-20, FR-21 |
| Search | `/Search?type=Hotel&...` | `SearchController.Index` | Tabs for 4 types, filter form, result cards | Location, price from/to, quality, quantity, only available, category (spots) | Detail pages, Advanced Search | FR-30, FR-31, FR-32, FR-35 |
| Advanced Search | `/Search/Advanced` | `SearchController.Advanced` | Multi-type filter, sortable paged table | Keyword, types, all A3 criteria, sort, page | Detail pages | FR-33, FR-34 |
| Information | `/Information` | `InformationController.Index` | 5 links, new hotels/restaurants/resorts, packages with final price | – | 5 sub-pages, package detail | FR-40, FR-41, FR-42 |
| Tourist Spots | `/Information/TouristSpots` | `InformationController.TouristSpots` | Spot cards, filter by city and category | City, category | Spot detail | FR-43 |
| Travel Information | `/Information/Travel` | `InformationController.Travel` | Route table: from, to, type, operator, fare, duration, departures | From, to, type | Contact Us (book transport) | FR-44 |
| Hotel Information | `/Information/Hotels` | `InformationController.Hotels` | Hotel cards | City | Hotel detail | FR-45 |
| Restaurant Information | `/Information/Restaurants` | `InformationController.Restaurants` | Restaurant cards | City | Restaurant detail | FR-46 |
| Resorts Information | `/Information/Resorts` | `InformationController.Resorts` | Resort cards | City | Resort detail | FR-47 |
| Detail pages | `/Information/{Spot|Hotel|Restaurant|Resort|Package}/{id}` | matching action | Photo, facts, description, Book button | – | Contact Us pre-filled | FR-43 – FR-47 |
| Contact Us | `/Contact` | `ContactController.Index` (GET) | Booking form, feedback form, company details | See below | Thanks page | FR-50 – FR-54 |
| Booking submit | `/Contact/Book` (POST) | `ContactController.Book` | – | Booking form | `/Contact/Thanks` or form with errors | FR-50, FR-53, FR-54 |
| Feedback submit | `/Contact/Feedback` (POST) | `ContactController.Feedback` | – | Feedback form | `/Contact/Thanks` or form with errors | FR-52 – FR-54 |
| Thanks | `/Contact/Thanks` | `ContactController.Thanks` | Confirmation with booking number | – | Home | FR-54 |

## Form fields

### Booking form (Contact Us)

| Field | Type | Rule | Error message |
| --- | --- | --- | --- |
| Full name | text | required, 2–100 characters | Please enter your full name. |
| Email | email | required, valid email | Please enter a valid email address. |
| Phone | tel | required, 10 digits starting with 0 | Please enter a 10-digit phone number starting with 0. |
| Service type | select | Hotel, Restaurant, Resort, Transport, Package | Please choose a service type. |
| Service | select (filled by type) | must exist | This service is no longer available. |
| Travel date | date | today or later | Travel date cannot be in the past. |
| Number of people | number | 1–50 | Number of people must be from 1 to 50. |
| Quantity | number | 1–50 (rooms, seats or units) | Quantity must be from 1 to 50. |
| Note | textarea | optional, up to 500 characters | Note can be at most 500 characters. |

### Feedback form (Contact Us)

| Field | Type | Rule |
| --- | --- | --- |
| Full name, Email, Phone | as above | as above |
| Rating | 1–5 stars (radio) | required |
| Subject | text | optional, up to 150 characters |
| Message | textarea | required, 10–2000 characters |

## Admin pages

| Screen | URL | Action | Notes |
| --- | --- | --- | --- |
| Login | `/Admin/Account/Login` | `AccountController.Login` | Username + password; one error message for any wrong pair |
| Dashboard | `/Admin` | `DashboardController.Index` | Counts of new bookings, unread feedback, places; latest 5 bookings |
| Places (6 types) | `/Admin/{Hotels|Restaurants|Resorts|TouristSpots|Transports|Packages}` | `Index`, `Create`, `Edit`, `Delete` | Table, add/edit form, delete confirmation page |
| Bookings | `/Admin/Bookings` | `Index`, `UpdateStatus` | Filter by status; status buttons follow Algorithm A8 |
| Feedback | `/Admin/Feedbacks` | `Index`, `MarkRead` | Unread first |
| Logout | `/Admin/Account/Logout` (POST) | `AccountController.Logout` | Clears the cookie |

All admin pages require the admin cookie; without it the user is sent to the login page.
