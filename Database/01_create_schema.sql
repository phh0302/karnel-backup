/* =====================================================================
   Karnel Travel Guide - Database schema
   DBMS    : Microsoft SQL Server 2019 or later
   Purpose : Creates the KarnelTravelDB database and all tables used by
             the website (information pages, search, booking, feedback,
             administration).
   Run     : Execute this file first, then 02_seed_data.sql
   Notes   : - All text columns are NVARCHAR so Vietnamese is stored correctly.
             - Prices are stored in Vietnamese dong (VND), no decimals.
   ===================================================================== */

-- ---------------------------------------------------------------------
-- 1. Create the database (drop the old copy first when re-running)
-- ---------------------------------------------------------------------
USE master;
GO

IF DB_ID(N'KarnelTravelDB') IS NOT NULL
BEGIN
    -- Close other connections so the database can be dropped
    ALTER DATABASE KarnelTravelDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE KarnelTravelDB;
END
GO

CREATE DATABASE KarnelTravelDB;
GO

USE KarnelTravelDB;
GO

-- ---------------------------------------------------------------------
-- 2. Locations: the cities / provinces every place belongs to.
--    Search by location filters on this table.
-- ---------------------------------------------------------------------
CREATE TABLE Locations (
    LocationId   INT IDENTITY(1,1) CONSTRAINT PK_Locations PRIMARY KEY,
    City         NVARCHAR(100) NOT NULL,
    Province     NVARCHAR(100) NOT NULL,
    Region       NVARCHAR(20)  NOT NULL
                 CONSTRAINT CK_Locations_Region CHECK (Region IN (N'North', N'Central', N'South')),
    Description  NVARCHAR(500) NULL,
    CONSTRAINT UQ_Locations_City UNIQUE (City)
);
GO

-- ---------------------------------------------------------------------
-- 3. TouristSpots: beaches, monuments, mountains... (Tourist Spots page)
-- ---------------------------------------------------------------------
CREATE TABLE TouristSpots (
    SpotId       INT IDENTITY(1,1) CONSTRAINT PK_TouristSpots PRIMARY KEY,
    LocationId   INT NOT NULL
                 CONSTRAINT FK_TouristSpots_Locations REFERENCES Locations(LocationId),
    Name         NVARCHAR(150) NOT NULL,
    Category     NVARCHAR(30)  NOT NULL
                 CONSTRAINT CK_TouristSpots_Category
                 CHECK (Category IN (N'Beach', N'Monument', N'Mountain', N'Nature', N'Temple', N'Cultural', N'Island')),
    Description  NVARCHAR(1000) NULL,
    Address      NVARCHAR(250) NULL,
    EntryFee     DECIMAL(12,0) NOT NULL CONSTRAINT DF_TouristSpots_EntryFee DEFAULT 0
                 CONSTRAINT CK_TouristSpots_EntryFee CHECK (EntryFee >= 0),
    BestTime     NVARCHAR(100) NULL,          -- best season to visit
    ImageUrl     NVARCHAR(250) NULL,
    IsFeatured   BIT NOT NULL CONSTRAINT DF_TouristSpots_IsFeatured DEFAULT 0,  -- shown on Home Page
    CreatedAt    DATETIME2 NOT NULL CONSTRAINT DF_TouristSpots_CreatedAt DEFAULT SYSDATETIME()
);
GO

-- ---------------------------------------------------------------------
-- 4. Hotels (Hotel Information page + hotel search)
--    RoomsAvailable is used for the "quantity" and "availability" filters.
-- ---------------------------------------------------------------------
CREATE TABLE Hotels (
    HotelId        INT IDENTITY(1,1) CONSTRAINT PK_Hotels PRIMARY KEY,
    LocationId     INT NOT NULL
                   CONSTRAINT FK_Hotels_Locations REFERENCES Locations(LocationId),
    Name           NVARCHAR(150) NOT NULL,
    Stars          TINYINT NOT NULL CONSTRAINT CK_Hotels_Stars CHECK (Stars BETWEEN 1 AND 5),
    Address        NVARCHAR(250) NULL,
    Phone          NVARCHAR(20)  NULL,
    PricePerNight  DECIMAL(12,0) NOT NULL CONSTRAINT CK_Hotels_Price CHECK (PricePerNight > 0),
    TotalRooms     INT NOT NULL CONSTRAINT CK_Hotels_TotalRooms CHECK (TotalRooms > 0),
    RoomsAvailable INT NOT NULL,
    Rating         DECIMAL(2,1) NULL CONSTRAINT CK_Hotels_Rating CHECK (Rating BETWEEN 0 AND 5),
    Description    NVARCHAR(1000) NULL,
    ImageUrl       NVARCHAR(250) NULL,
    IsNew          BIT NOT NULL CONSTRAINT DF_Hotels_IsNew DEFAULT 0,      -- shown in "New" on Information page
    CreatedAt      DATETIME2 NOT NULL CONSTRAINT DF_Hotels_CreatedAt DEFAULT SYSDATETIME(),
    CONSTRAINT CK_Hotels_RoomsAvailable CHECK (RoomsAvailable BETWEEN 0 AND TotalRooms)
);
GO

-- ---------------------------------------------------------------------
-- 5. Restaurants (Restaurant Information page + restaurant search)
--    PriceLevel: 1 = budget ... 4 = fine dining. Seats = quantity filter.
-- ---------------------------------------------------------------------
CREATE TABLE Restaurants (
    RestaurantId     INT IDENTITY(1,1) CONSTRAINT PK_Restaurants PRIMARY KEY,
    LocationId       INT NOT NULL
                     CONSTRAINT FK_Restaurants_Locations REFERENCES Locations(LocationId),
    Name             NVARCHAR(150) NOT NULL,
    Cuisine          NVARCHAR(50)  NOT NULL,
    Address          NVARCHAR(250) NULL,
    Phone            NVARCHAR(20)  NULL,
    PriceLevel       TINYINT NOT NULL CONSTRAINT CK_Restaurants_PriceLevel CHECK (PriceLevel BETWEEN 1 AND 4),
    AvgPricePerPerson DECIMAL(12,0) NOT NULL CONSTRAINT CK_Restaurants_Price CHECK (AvgPricePerPerson > 0),
    TotalSeats       INT NOT NULL CONSTRAINT CK_Restaurants_TotalSeats CHECK (TotalSeats > 0),
    SeatsAvailable   INT NOT NULL,
    Rating           DECIMAL(2,1) NULL CONSTRAINT CK_Restaurants_Rating CHECK (Rating BETWEEN 0 AND 5),
    OpenHours        NVARCHAR(50) NULL,
    Description      NVARCHAR(1000) NULL,
    ImageUrl         NVARCHAR(250) NULL,
    IsNew            BIT NOT NULL CONSTRAINT DF_Restaurants_IsNew DEFAULT 0,
    CreatedAt        DATETIME2 NOT NULL CONSTRAINT DF_Restaurants_CreatedAt DEFAULT SYSDATETIME(),
    CONSTRAINT CK_Restaurants_SeatsAvailable CHECK (SeatsAvailable BETWEEN 0 AND TotalSeats)
);
GO

-- ---------------------------------------------------------------------
-- 6. Resorts (Resorts Information page + resort search)
-- ---------------------------------------------------------------------
CREATE TABLE Resorts (
    ResortId        INT IDENTITY(1,1) CONSTRAINT PK_Resorts PRIMARY KEY,
    LocationId      INT NOT NULL
                    CONSTRAINT FK_Resorts_Locations REFERENCES Locations(LocationId),
    Name            NVARCHAR(150) NOT NULL,
    Stars           TINYINT NOT NULL CONSTRAINT CK_Resorts_Stars CHECK (Stars BETWEEN 1 AND 5),
    Address         NVARCHAR(250) NULL,
    Phone           NVARCHAR(20)  NULL,
    PricePerNight   DECIMAL(12,0) NOT NULL CONSTRAINT CK_Resorts_Price CHECK (PricePerNight > 0),
    TotalUnits      INT NOT NULL CONSTRAINT CK_Resorts_TotalUnits CHECK (TotalUnits > 0),   -- villas / bungalows
    UnitsAvailable  INT NOT NULL,
    Rating          DECIMAL(2,1) NULL CONSTRAINT CK_Resorts_Rating CHECK (Rating BETWEEN 0 AND 5),
    Facilities      NVARCHAR(300) NULL,          -- e.g. "Private beach, Spa, Pool"
    Description     NVARCHAR(1000) NULL,
    ImageUrl        NVARCHAR(250) NULL,
    IsNew           BIT NOT NULL CONSTRAINT DF_Resorts_IsNew DEFAULT 0,
    CreatedAt       DATETIME2 NOT NULL CONSTRAINT DF_Resorts_CreatedAt DEFAULT SYSDATETIME(),
    CONSTRAINT CK_Resorts_UnitsAvailable CHECK (UnitsAvailable BETWEEN 0 AND TotalUnits)
);
GO

-- ---------------------------------------------------------------------
-- 7. Transports: routes between two locations (Travel Information page)
-- ---------------------------------------------------------------------
CREATE TABLE Transports (
    TransportId     INT IDENTITY(1,1) CONSTRAINT PK_Transports PRIMARY KEY,
    FromLocationId  INT NOT NULL
                    CONSTRAINT FK_Transports_FromLocation REFERENCES Locations(LocationId),
    ToLocationId    INT NOT NULL
                    CONSTRAINT FK_Transports_ToLocation REFERENCES Locations(LocationId),
    TransportType   NVARCHAR(20) NOT NULL
                    CONSTRAINT CK_Transports_Type CHECK (TransportType IN (N'Bus', N'Train', N'Car', N'Plane', N'Boat')),
    Operator        NVARCHAR(100) NOT NULL,
    Fare            DECIMAL(12,0) NOT NULL CONSTRAINT CK_Transports_Fare CHECK (Fare > 0),
    DurationHours   DECIMAL(4,1) NOT NULL CONSTRAINT CK_Transports_Duration CHECK (DurationHours > 0),
    DepartureTimes  NVARCHAR(150) NULL,          -- e.g. "06:00, 13:00, 21:00"
    SeatsAvailable  INT NOT NULL CONSTRAINT CK_Transports_Seats CHECK (SeatsAvailable >= 0),
    Description     NVARCHAR(500) NULL,
    CONSTRAINT CK_Transports_DifferentPlaces CHECK (FromLocationId <> ToLocationId)
);
GO

-- ---------------------------------------------------------------------
-- 8. Packages: tours / discounts shown on Home and Information pages
-- ---------------------------------------------------------------------
CREATE TABLE Packages (
    PackageId        INT IDENTITY(1,1) CONSTRAINT PK_Packages PRIMARY KEY,
    LocationId       INT NOT NULL
                     CONSTRAINT FK_Packages_Locations REFERENCES Locations(LocationId),
    Title            NVARCHAR(150) NOT NULL,
    Description      NVARCHAR(1000) NULL,
    DurationDays     TINYINT NOT NULL CONSTRAINT CK_Packages_Duration CHECK (DurationDays BETWEEN 1 AND 30),
    Price            DECIMAL(12,0) NOT NULL CONSTRAINT CK_Packages_Price CHECK (Price > 0),
    DiscountPercent  TINYINT NOT NULL CONSTRAINT DF_Packages_Discount DEFAULT 0
                     CONSTRAINT CK_Packages_Discount CHECK (DiscountPercent BETWEEN 0 AND 90),
    ValidFrom        DATE NOT NULL,
    ValidUntil       DATE NOT NULL,
    ImageUrl         NVARCHAR(250) NULL,
    IsActive         BIT NOT NULL CONSTRAINT DF_Packages_IsActive DEFAULT 1,
    CONSTRAINT CK_Packages_Dates CHECK (ValidUntil >= ValidFrom)
);
GO

-- Many-to-many: which tourist spots a package visits
CREATE TABLE PackageSpots (
    PackageId  INT NOT NULL CONSTRAINT FK_PackageSpots_Packages REFERENCES Packages(PackageId) ON DELETE CASCADE,
    SpotId     INT NOT NULL CONSTRAINT FK_PackageSpots_Spots    REFERENCES TouristSpots(SpotId) ON DELETE CASCADE,
    DayNumber  TINYINT NOT NULL CONSTRAINT DF_PackageSpots_Day DEFAULT 1,   -- which day of the tour
    CONSTRAINT PK_PackageSpots PRIMARY KEY (PackageId, SpotId)
);
GO

-- ---------------------------------------------------------------------
-- 9. Customers: created automatically when someone books or sends feedback
--    (no customer login; the email identifies a returning customer)
-- ---------------------------------------------------------------------
CREATE TABLE Customers (
    CustomerId  INT IDENTITY(1,1) CONSTRAINT PK_Customers PRIMARY KEY,
    FullName    NVARCHAR(100) NOT NULL,
    Email       NVARCHAR(150) NOT NULL,
    Phone       NVARCHAR(20)  NOT NULL,
    CreatedAt   DATETIME2 NOT NULL CONSTRAINT DF_Customers_CreatedAt DEFAULT SYSDATETIME(),
    CONSTRAINT UQ_Customers_Email UNIQUE (Email)
);
GO

-- ---------------------------------------------------------------------
-- 10. Bookings: orders placed from the Contact Us page.
--     ServiceType + ServiceId point to a row in Hotels / Restaurants /
--     Resorts / Transports / Packages. The application checks that the
--     row exists before saving (see Algorithms document, A5).
-- ---------------------------------------------------------------------
CREATE TABLE Bookings (
    BookingId       INT IDENTITY(1,1) CONSTRAINT PK_Bookings PRIMARY KEY,
    CustomerId      INT NOT NULL
                    CONSTRAINT FK_Bookings_Customers REFERENCES Customers(CustomerId),
    ServiceType     NVARCHAR(20) NOT NULL
                    CONSTRAINT CK_Bookings_ServiceType
                    CHECK (ServiceType IN (N'Hotel', N'Restaurant', N'Resort', N'Transport', N'Package')),
    ServiceId       INT NOT NULL,
    TravelDate      DATE NOT NULL,
    NumberOfPeople  INT NOT NULL CONSTRAINT CK_Bookings_People CHECK (NumberOfPeople BETWEEN 1 AND 50),
    Quantity        INT NOT NULL CONSTRAINT DF_Bookings_Quantity DEFAULT 1
                    CONSTRAINT CK_Bookings_Quantity CHECK (Quantity BETWEEN 1 AND 50),   -- rooms / seats / units
    Note            NVARCHAR(500) NULL,
    Status          NVARCHAR(20) NOT NULL CONSTRAINT DF_Bookings_Status DEFAULT N'New'
                    CONSTRAINT CK_Bookings_Status CHECK (Status IN (N'New', N'Contacted', N'Completed', N'Cancelled')),
    CreatedAt       DATETIME2 NOT NULL CONSTRAINT DF_Bookings_CreatedAt DEFAULT SYSDATETIME()
);
GO

-- ---------------------------------------------------------------------
-- 11. Feedbacks: sent from the feedback form on Contact Us
-- ---------------------------------------------------------------------
CREATE TABLE Feedbacks (
    FeedbackId  INT IDENTITY(1,1) CONSTRAINT PK_Feedbacks PRIMARY KEY,
    CustomerId  INT NOT NULL
                CONSTRAINT FK_Feedbacks_Customers REFERENCES Customers(CustomerId),
    Rating      TINYINT NOT NULL CONSTRAINT CK_Feedbacks_Rating CHECK (Rating BETWEEN 1 AND 5),
    Subject     NVARCHAR(150) NULL,
    Message     NVARCHAR(2000) NOT NULL,
    IsRead      BIT NOT NULL CONSTRAINT DF_Feedbacks_IsRead DEFAULT 0,   -- admin marks as read
    CreatedAt   DATETIME2 NOT NULL CONSTRAINT DF_Feedbacks_CreatedAt DEFAULT SYSDATETIME()
);
GO

-- ---------------------------------------------------------------------
-- 12. Admins: staff accounts for the administration area.
--     Passwords are stored as hashes made by ASP.NET Core
--     PasswordHasher (PBKDF2-HMAC-SHA512), never as plain text.
-- ---------------------------------------------------------------------
CREATE TABLE Admins (
    AdminId       INT IDENTITY(1,1) CONSTRAINT PK_Admins PRIMARY KEY,
    Username      NVARCHAR(50)  NOT NULL CONSTRAINT UQ_Admins_Username UNIQUE,
    PasswordHash  NVARCHAR(256) NOT NULL,
    FullName      NVARCHAR(100) NOT NULL,
    LastLoginAt   DATETIME2 NULL,
    CreatedAt     DATETIME2 NOT NULL CONSTRAINT DF_Admins_CreatedAt DEFAULT SYSDATETIME()
);
GO

-- ---------------------------------------------------------------------
-- 13. Indexes for the columns the Search page filters and sorts on
-- ---------------------------------------------------------------------
CREATE INDEX IX_TouristSpots_Location_Category ON TouristSpots (LocationId, Category);
CREATE INDEX IX_Hotels_Location_Price          ON Hotels (LocationId, PricePerNight);
CREATE INDEX IX_Restaurants_Location_Price     ON Restaurants (LocationId, AvgPricePerPerson);
CREATE INDEX IX_Resorts_Location_Price         ON Resorts (LocationId, PricePerNight);
CREATE INDEX IX_Transports_Route               ON Transports (FromLocationId, ToLocationId);
CREATE INDEX IX_Bookings_Status                ON Bookings (Status, CreatedAt);
CREATE INDEX IX_Bookings_Customer              ON Bookings (CustomerId);
CREATE INDEX IX_Feedbacks_Customer             ON Feedbacks (CustomerId);
GO

PRINT N'KarnelTravelDB schema created.';
GO
