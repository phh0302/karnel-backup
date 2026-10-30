# Karnel Travel Guide – Customer Requirement Specification

Oct 29, 2026 · Ha Pham

## 1. Document Information

This CRS records what Karnel Travels expects from its new website and serves as the baseline for design, development and testing.

| Item | Detail |
| --- | --- |
| Project title | Karnel Travel Guide |
| Client | Karnel Travels (Tours and Travels Company) |
| Website URL | http://www.Karneltravelguide.com |
| Document | Customer Requirement Specification (CRS) |
| Version | 1.0 |
| Batch | \_\_\_\_\_\_\_\_ |
| Group | \_\_\_\_\_\_\_\_ |
| Prepared by | \_\_\_\_\_\_\_\_ |

## 2. Client Background and Problem Statement

Karnel Travels needs a URL-specific website that attracts customers and gives them travel information in one place.

Karnel Travels is a Tours and Travels company. It provides transportation between cities and to tourist spots, and arranges accommodation in hotels. Customers should be able to visit online, view and order tours as well as transportation.

Today the company has no single place where customers can find tourist spots, transport options, hotels, restaurants and resorts across the country. The company has asked us to build a website that provides these information services, lets customers search them, and lets customers contact the company to place orders and give feedback.

## 3. Business Objectives

The website must turn visitors into customers by informing them, helping them find services, and capturing their orders.

1. Present the company and its services: transportation, packages, and accommodation at tourist locations.
2. Publish information on tourist spots, travel options, hotels, restaurants and resorts within the country.
3. Let customers search services by location, price, quality, quantity and availability.
4. Promote new hotels, restaurants, resorts, packages and discounts.
5. Collect customer orders and feedback through the website.

## 4. Scope

The project covers a public information website with search, ordering enquiries and feedback, backed by a database.

**In scope**

- Home Page, About Us, Search Page with Advanced Search, Information Page with five sub-pages, Contact Us with Feedback
- A database holding tourist spots, transport, hotels, restaurants, resorts, packages, orders and feedback
- An administration area to maintain the data (recommended by the team, not stated by the client)

**Out of scope**

- Online payment
- Customer accounts and login
- Mobile apps
- Destinations outside the country
- Multi-language support

## 5. Users of the System

| User | Description | Main needs |
| --- | --- | --- |
| Visitor / Customer | Anyone browsing the website, no login required | View information, search services, place an order, send feedback |
| Administrator | Karnel Travels staff member, logs in to the admin area | Add, edit and delete data; view orders and feedback |

## 6. Functional Requirements

Each requirement traces to the numbered client requirement (CR) in the assignment; "Team" marks additions proposed by the team.

### 6.1 General

| ID | Requirement | Source |
| --- | --- | --- |
| FR-01 | The website is reachable at http://www.Karneltravelguide.com | CR 1 |
| FR-02 | A menu on every page lets users reach each topic with ease | CR 2 |
| FR-03 | The menu contains Home Page, About Us, Search Page, Information Page and Contact Us | CR 3 |

### 6.2 Home Page

| ID | Requirement | Source |
| --- | --- | --- |
| FR-10 | The Home Page is the entry point and links to every menu section | CR 3 |
| FR-11 | The Home Page highlights featured tourist spots and current packages | Team |

### 6.3 About Us

| ID | Requirement | Source |
| --- | --- | --- |
| FR-20 | Show details about the company | CR 4 |
| FR-21 | Describe the facilities provided: transportation, packages, and transportation and accommodation to tourist locations | CR 4 |

### 6.4 Search Page

| ID | Requirement | Source |
| --- | --- | --- |
| FR-30 | Search tourist spots by type, such as beaches and monuments | CR 5 |
| FR-31 | Search hotels, restaurants and resorts by location, price, quality, quantity and availability | CR 5 |
| FR-32 | Provide a link to the Advanced Search page | CR 5 |
| FR-33 | Advanced Search combines several criteria across service types | CR 5 |
| FR-34 | Results can be sorted by price or quality and are paginated | Team |
| FR-35 | A clear message appears when no result matches | Team |

### 6.5 Information Page

| ID | Requirement | Source |
| --- | --- | --- |
| FR-40 | The page contains five links: Tourist Spots, Travel Information, Hotel Information, Restaurant Information, Resorts Information | CR 3, CR 6 |
| FR-41 | Display information about new hotels, restaurants and resorts | CR 6 |
| FR-42 | Display packages or discounts the company offers | CR 6 |
| FR-43 | Tourist Spots lists tourist locations within the country | CR 7 |
| FR-44 | Travel Information lists transportation facilities within the country | CR 8 |
| FR-45 | Hotel Information lists hotels within the country | CR 9 |
| FR-46 | Restaurant Information lists restaurants within the country | CR 9 |
| FR-47 | Resorts Information lists resorts within the country | CR 9 |

### 6.6 Contact Us

| ID | Requirement | Source |
| --- | --- | --- |
| FR-50 | Customers can contact the company to place an order | CR 10 |
| FR-51 | The order form captures name, email, phone, service, travel date, number of people and notes | Team |
| FR-52 | A feedback form collects feedback from website users | CR 10 |
| FR-53 | Forms validate required fields, email and phone formats, and reject past travel dates | Team |
| FR-54 | Submitted orders and feedback are saved to the database and confirmed on screen | Team |

### 6.7 Administration

| ID | Requirement | Source |
| --- | --- | --- |
| FR-60 | Administrator login | Team |
| FR-61 | Add, edit and delete tourist spots, transport, hotels, restaurants, resorts and packages | Team |
| FR-62 | View orders and update their status | Team |
| FR-63 | View customer feedback | Team |

## 7. Non-Functional Requirements and Standards

| ID | Requirement | Source |
| --- | --- | --- |
| NFR-01 | Every code block must have comments | Standards plan 1 |
| NFR-02 | Program logic is explained and proper documentation is maintained | Standards plan 2 |
| NFR-03 | A complete Project Report with synopsis, code and documentation is prepared | Standards plan 3 |
| NFR-04 | Project data is backed up every day | Assignment note |
| NFR-05 | All pages follow one GUI Standards Document | Team |
| NFR-06 | Pages display correctly on desktop and mobile screens | Team |
| NFR-07 | Database queries are parameterised and the admin password is stored hashed | Team |

## 8. Inputs and Expected Outputs

**Inputs from the client**

- Company profile and description of services
- Lists of tourist spots, transport routes and fares, hotels, restaurants and resorts, with location, price, quality and availability
- Current packages and discounts
- Company contact details (address, phone, email)

**Inputs from customers**

- Search criteria: type, location, price range, quality, quantity, availability
- Order details: name, email, phone, service, travel date, number of people, notes
- Feedback: name, email, rating, message

**Expected outputs**

- Information pages showing data stored in the database
- Search results matching the selected criteria
- Highlights of new hotels, restaurants, resorts and current packages
- Confirmation messages after an order or feedback is submitted
- Order and feedback records stored for the company to follow up

## 9. Hardware and Software Requirements

The assignment lists a minimum PC and a set of permitted technologies; the team's chosen stack is filled in below.

| Category | Assignment minimum / permitted | Team choice |
| --- | --- | --- |
| Processor | Pentium 166 or better | Intel Core i3 / AMD Ryzen 3 or better |
| Memory | 64 MB RAM or better | 8 GB RAM or more |
| Operating system | Windows 98 or higher | Windows 10/11 |
| Runtime | Java Virtual Machine | .NET 8 SDK and runtime |
| Editor | Notepad / HTML editor / Java editor, Dreamweaver | Visual Studio 2022, SQL Server Management Studio |
| Front end | HTML, JavaScript | HTML, CSS, JavaScript |
| Back end | J2SDK 1.4.1 or later / .NET / J2EE, JSP / Servlets, EJB / Struts | ASP.NET Core MVC (C#) |
| Data access | JDBC and JDBC driver | Entity Framework Core (SQL Server provider) |
| Database | SQL Server 2000 or later / Oracle 9i / MS Access | Microsoft SQL Server 2019 or later |

## 10. Assumptions and Constraints

**Assumptions**

- Orders are stored as enquiries for staff to follow up; no online payment is taken.
- Customers order without creating an account.
- Sample data for real destinations in the country is prepared by the team where the client supplies none.
- During development and demonstration, the domain is pointed to the local machine through the hosts file.

**Constraints**

- Only the technologies permitted in the assignment may be used.
- Two status reports must be submitted during the project.
- The final project is submitted once only, as a single ZIP file named with Batch, Group and Project Title.

**Open questions**

- Confirm with faculty that ASP.NET Core MVC is accepted as the .NET option.
- What is the exact project start and end date?

## 11. Acceptance Criteria and Sign-off

The website is accepted when every item below passes.

- [ ] Entering www.karneltravelguide.com opens the Home Page
- [ ] The menu shows all five sections and every link works
- [ ] About Us describes transportation, packages and accommodation services
- [ ] Tourist spots can be searched by type
- [ ] Hotels, restaurants and resorts can be searched by location, price, quality, quantity and availability
- [ ] The Search page links to Advanced Search
- [ ] The Information page has five links, each showing data from the database
- [ ] New hotels, restaurants, resorts and current packages are displayed
- [ ] An order placed on Contact Us is saved
- [ ] Feedback submitted is saved
- [ ] Invalid form input shows a clear error message
- [ ] Every code block has comments

| Role | Name | Signature | Date |
| --- | --- | --- | --- |
| Client representative |  |  |  |
| Project leader |  |  |  |
| Faculty |  |  |  |
