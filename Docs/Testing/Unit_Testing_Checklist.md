# Karnel Travel Guide – Unit Testing Check List

Run each test on the sample data from `02_seed_data.sql`. Write **Pass** or **Fail** in the Result column, with the date and the tester's initials. A failed test gets a short note and is re-tested after the fix.

Tester: ________  Browser: ________  Date: ________

## 1. Navigation and layout

| ID | Test | Steps | Expected result | Result |
| --- | --- | --- | --- | --- |
| UT-01 | Domain | Run the hosts script, start the site, open http://www.karneltravelguide.com | Home Page opens | |
| UT-02 | Menu on every page | Open each page from the menu | Menu shows Home, About Us, Search, Information, Contact Us; current page underlined | |
| UT-03 | Information dropdown | Open the Information menu | 6 links: All information + the 5 sections | |
| UT-04 | Mobile menu | Narrow the window below 992 px | Menu folds into ☰ and opens on click | |
| UT-05 | Missing page | Open /abc | Friendly "We could not find that page" with a link home | |
| UT-06 | Missing photo | Open a page whose photo file is absent | Placeholder picture, no broken-image icon | |

## 2. Home and About Us

| ID | Test | Steps | Expected result | Result |
| --- | --- | --- | --- | --- |
| UT-10 | Featured spots | Open Home | 8 spot cards, featured ones first | |
| UT-11 | Packages | Open Home | Up to 3 active packages, biggest discount first, old price struck through | |
| UT-12 | Quick search | Choose Đà Nẵng + Hotels, click Search | Search page, Hotels tab, Đà Nẵng selected, 2 results | |
| UT-20 | About Us content | Open About Us | Company text and the three facilities: Transportation, Packages, Transport and accommodation | |
| UT-21 | About Us numbers | Compare with the database | 10 destinations, 12 hotels, 8 resorts, 14 routes, 5 packages | |

## 3. Search

| ID | Test | Steps | Expected result | Result |
| --- | --- | --- | --- | --- |
| UT-30 | Spots by type | Tourist spots tab, Type = Beach | 4 beaches | |
| UT-31 | Hotels by location | Hotels tab, Location = Hà Nội | 2 hotels | |
| UT-32 | Hotels by price | 500,000 to 1,000,000 | Only hotels in that range (3 budget hotels) | |
| UT-33 | Prices reversed | From 2,500,000 to 500,000 | Same as 500,000 to 2,500,000 (values swapped) | |
| UT-34 | Quality | Hotels, 5 stars+ | Only 5-star hotels | |
| UT-35 | Quantity | Hotels, rooms needed = 20, Only available ticked | Hotels with fewer than 20 rooms left disappear | |
| UT-36 | Availability off | Untick Only available | Full hotels also shown | |
| UT-37 | Restaurants rating | Restaurants, Rated 4.5+ | Only restaurants rated 4.5 or more | |
| UT-38 | No result | Resorts, price to 100,000 | Message "Nothing matches…" | |
| UT-39 | Advanced link | On Search click "Advanced search" | Advanced Search page opens | |
| UT-40 | Advanced multi-type | Keyword "beach", all types | Spots and places containing "beach" | |
| UT-41 | Advanced sort | Sort by Price, High to low | Most expensive first | |
| UT-42 | Advanced paging | No filters | Page 1 of 6 (52 results), Next goes to page 2, filters kept | |

## 4. Information pages

| ID | Test | Steps | Expected result | Result |
| --- | --- | --- | --- | --- |
| UT-50 | Five links | Open Information | 5 tiles with counts | |
| UT-51 | New places | Open Information | "New hotels / restaurants / resorts" show items marked New | |
| UT-52 | Packages | Open Information | Active packages with final price | |
| UT-53 | Tourist Spots filter | City = Huế | 2 spots | |
| UT-54 | Travel filter | From Hà Nội, To Hạ Long | 2 routes (bus and private car) | |
| UT-55 | Hotel detail | Click a hotel card | Detail with price, rooms, address, phone, Book button | |
| UT-56 | Package detail | Open "Central Heritage" | Shows 5 spots with Day 1–4 badges | |
| UT-57 | Bad id | Open /Information/Hotel/999 | Not-found page | |

## 5. Contact Us

| ID | Test | Steps | Expected result | Result |
| --- | --- | --- | --- | --- |
| UT-60 | Pre-filled booking | From a hotel detail click Book | Service type Hotel and that hotel selected | |
| UT-61 | Required fields | Click Send booking on an empty form | Red messages under name, email, phone, type, service | |
| UT-62 | Email format | Email "abc" | "Please enter a valid email address." | |
| UT-63 | Phone format | Phone "12345" | "Please enter a 10-digit phone number starting with 0." | |
| UT-64 | Past date | Travel date yesterday | "Travel date cannot be in the past." | |
| UT-65 | Too many rooms | Hotel with 8 rooms left, quantity 9 | "Only 8 rooms are left at …" | |
| UT-66 | Package date | Package, date after Valid until | Message with the valid period | |
| UT-67 | Booking saved | Valid booking | Thank-you page with booking number; row in Bookings with status New | |
| UT-68 | Returning customer | Book again with the same email | No new row in Customers; name/phone updated | |
| UT-69 | Refresh after send | Press F5 on the thank-you page | No second booking is created | |
| UT-70 | Feedback required | Send empty feedback | Messages for name, email, phone, rating, message | |
| UT-71 | Feedback saved | Valid feedback with 4 stars | Thank-you page; row in Feedbacks, IsRead = 0 | |

## 6. Admin

| ID | Test | Steps | Expected result | Result |
| --- | --- | --- | --- | --- |
| UT-80 | Protected pages | Log out, open /Admin/Hotels | Redirect to the login page | |
| UT-81 | Wrong login | admin / wrong | "Username or password is incorrect." | |
| UT-82 | Correct login | admin / Admin@123 | Dashboard with counts | |
| UT-83 | Add hotel | Hotels → Add, fill all fields | "Added hotel …", appears on the public Hotels page | |
| UT-84 | Validation | Rooms available > total rooms | Error under Rooms available | |
| UT-85 | Edit hotel | Change the price | New price on the public page; CreatedAt unchanged | |
| UT-86 | Delete | Delete the test hotel, confirm | "Deleted hotel …" | |
| UT-87 | Route same city | Transport From = To | "The route must end in a different city." | |
| UT-88 | Booking Contacted | New hotel booking → Contacted | Status Contacted; hotel rooms left reduced by quantity | |
| UT-89 | Cancel contacted | Contacted → Cancelled | Rooms given back | |
| UT-90 | Not allowed | Completed booking | No status buttons shown | |
| UT-91 | Feedback read | Mark as read | Moves below unread items | |
| UT-92 | Log out | Click Log out | Back to login; /Admin redirects to login | |

## 7. Code standards

| ID | Test | Expected result | Result |
| --- | --- | --- | --- |
| UT-95 | Comments | Every controller, action and view has a comment explaining its purpose | |
| UT-96 | SQL injection | Search keyword `' OR 1=1 --` returns no extra rows (queries are parameterised) | |
