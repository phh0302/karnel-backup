/* =====================================================================
   Karnel Travel Guide - Sample data
   Run after 01_create_schema.sql.
   Places are real destinations in Viet Nam. Prices, ratings, room counts
   and fares are ILLUSTRATIVE sample values for the student project, not
   live data from the businesses named.
   Provinces follow the provincial map in force from 1 July 2025.
   ===================================================================== */
USE KarnelTravelDB;
GO

SET NOCOUNT ON;

-- ---------------------------------------------------------------------
-- Locations (IDs 1-10 in this order)
-- ---------------------------------------------------------------------
INSERT INTO Locations (City, Province, Region, Description) VALUES
 (N'Hà Nội',          N'Hà Nội',          N'North',   N'The capital, known for its Old Quarter, lakes and French colonial architecture.'),   -- 1
 (N'Hạ Long',         N'Quảng Ninh',      N'North',   N'Gateway to Ha Long Bay, a UNESCO World Heritage site of limestone islands.'),         -- 2
 (N'Sa Pa',           N'Lào Cai',         N'North',   N'Mountain town of rice terraces, ethnic minority villages and cool weather.'),        -- 3
 (N'Huế',             N'Huế',             N'Central', N'Former imperial capital with the Citadel, royal tombs and pagodas.'),                -- 4
 (N'Đà Nẵng',         N'Đà Nẵng',         N'Central', N'Coastal city with long sandy beaches, bridges and the Ba Na Hills.'),               -- 5
 (N'Hội An',          N'Đà Nẵng',         N'Central', N'Lantern-lit ancient town on the Thu Bon River, a UNESCO World Heritage site.'),      -- 6
 (N'Nha Trang',       N'Khánh Hòa',       N'Central', N'Beach city with islands, diving spots and the Po Nagar Cham towers.'),             -- 7
 (N'Đà Lạt',          N'Lâm Đồng',        N'South',   N'Highland city of pine forests, lakes, flower gardens and mild climate.'),           -- 8
 (N'TP. Hồ Chí Minh', N'TP. Hồ Chí Minh', N'South',   N'The largest city, full of markets, history museums and street food.'),             -- 9
 (N'Phú Quốc',        N'An Giang',        N'South',   N'Island with white-sand beaches, night markets and resorts.');                       -- 10
GO

-- ---------------------------------------------------------------------
-- Tourist spots (IDs 1-20)
-- ---------------------------------------------------------------------
INSERT INTO TouristSpots (LocationId, Name, Category, Description, Address, EntryFee, BestTime, ImageUrl, IsFeatured) VALUES
 (1,  N'Hồ Hoàn Kiếm',               N'Nature',   N'Historic lake in the heart of Hanoi with the red Huc Bridge and Ngoc Son Temple.', N'Hoàn Kiếm, Hà Nội', 0, N'All year', N'/images/spots/hoan-kiem-lake.jpg', 0),          -- 1
 (1,  N'Văn Miếu – Quốc Tử Giám',    N'Monument', N'Temple of Literature, built in 1070 and home to Viet Nam''s first university.', N'58 Quốc Tử Giám, Đống Đa, Hà Nội', 70000, N'Oct - Apr', N'/images/spots/temple-of-literature.jpg', 1), -- 2
 (1,  N'Lăng Chủ tịch Hồ Chí Minh',  N'Monument', N'Mausoleum of President Ho Chi Minh on Ba Dinh Square.', N'2 Hùng Vương, Ba Đình, Hà Nội', 0, N'Oct - Apr', N'/images/spots/ho-chi-minh-mausoleum.jpg', 0), -- 3
 (2,  N'Vịnh Hạ Long',               N'Island',   N'Nearly 2,000 limestone islands and islets rising from emerald water; best seen on a cruise.', N'Hạ Long, Quảng Ninh', 290000, N'Apr - Jun, Sep - Nov', N'/images/spots/ha-long-bay.jpg', 1), -- 4
 (3,  N'Đỉnh Fansipan',              N'Mountain', N'The highest peak in Indochina at 3,143 m, reachable by cable car or a two-day trek.', N'Sa Pa, Lào Cai', 750000, N'Mar - May, Sep - Nov', N'/images/spots/fansipan.jpg', 1), -- 5
 (3,  N'Bản Cát Cát',                N'Cultural', N'H''Mong village with waterfalls, weaving and rice terraces, a short walk from town.', N'Sa Pa, Lào Cai', 150000, N'Sep - Nov', N'/images/spots/cat-cat-village.jpg', 0), -- 6
 (4,  N'Đại Nội Huế',                N'Monument', N'The Imperial City of the Nguyen dynasty, with palaces, gates and moats.', N'Phú Hậu, Huế', 200000, N'Jan - Apr', N'/images/spots/hue-imperial-city.jpg', 1), -- 7
 (4,  N'Chùa Thiên Mụ',              N'Temple',   N'Seven-storey pagoda on the Perfume River, built in 1601.', N'Kim Long, Huế', 0, N'Jan - Apr', N'/images/spots/thien-mu-pagoda.jpg', 0), -- 8
 (5,  N'Bãi biển Mỹ Khê',            N'Beach',    N'Long stretch of fine sand close to the city centre, good for swimming.', N'Sơn Trà, Đà Nẵng', 0, N'May - Aug', N'/images/spots/my-khe-beach.jpg', 1), -- 9
 (5,  N'Bà Nà Hills',                N'Mountain', N'Hill station reached by cable car, home to the Golden Bridge held by giant hands.', N'Hòa Vang, Đà Nẵng', 900000, N'Mar - Aug', N'/images/spots/ba-na-hills.jpg', 1), -- 10
 (5,  N'Ngũ Hành Sơn',               N'Nature',   N'Marble Mountains: five limestone hills with caves, pagodas and viewpoints.', N'Ngũ Hành Sơn, Đà Nẵng', 40000, N'Feb - Aug', N'/images/spots/marble-mountains.jpg', 0), -- 11
 (6,  N'Phố cổ Hội An',              N'Monument', N'Well-preserved trading port with Japanese Bridge, old houses and lanterns at night.', N'Hội An, Đà Nẵng', 120000, N'Feb - Apr', N'/images/spots/hoi-an-ancient-town.jpg', 1), -- 12
 (6,  N'Biển An Bàng',               N'Beach',    N'Relaxed beach with seafood shacks, 4 km from the ancient town.', N'Cẩm An, Hội An', 0, N'Apr - Aug', N'/images/spots/an-bang-beach.jpg', 0), -- 13
 (7,  N'Bãi biển Nha Trang',         N'Beach',    N'Six-kilometre city beach along Tran Phu street, with calm water and islands offshore.', N'Trần Phú, Nha Trang', 0, N'Feb - Aug', N'/images/spots/nha-trang-beach.jpg', 0), -- 14
 (7,  N'Tháp Bà Ponagar',            N'Temple',   N'Cham towers built between the 8th and 13th centuries on a hill by the Cai River.', N'2 Tháng 4, Nha Trang', 30000, N'Feb - Aug', N'/images/spots/po-nagar-towers.jpg', 0), -- 15
 (8,  N'Hồ Xuân Hương',              N'Nature',   N'Crescent-shaped lake in the centre of Da Lat, circled by pine trees and flower gardens.', N'Phường 1, Đà Lạt', 0, N'Dec - Mar', N'/images/spots/xuan-huong-lake.jpg', 0), -- 16
 (8,  N'Thung lũng Tình Yêu',        N'Nature',   N'Valley of Love: pine hills, a lake and gardens north of the city.', N'Phường 8, Đà Lạt', 250000, N'Dec - Mar', N'/images/spots/valley-of-love.jpg', 0), -- 17
 (9,  N'Dinh Độc Lập',               N'Monument', N'Independence Palace, where the war ended on 30 April 1975.', N'135 Nam Kỳ Khởi Nghĩa, Quận 1', 65000, N'Dec - Apr', N'/images/spots/independence-palace.jpg', 1), -- 18
 (9,  N'Địa đạo Củ Chi',             N'Monument', N'Over 200 km of wartime tunnels, partly open to visitors.', N'Phú Hiệp, Củ Chi', 125000, N'Dec - Apr', N'/images/spots/cu-chi-tunnels.jpg', 0), -- 19
 (10, N'Bãi Sao',                    N'Beach',    N'Star Beach: white sand and turquoise water on the south-east coast of the island.', N'An Thới, Phú Quốc', 0, N'Nov - Apr', N'/images/spots/sao-beach.jpg', 1); -- 20
GO

-- ---------------------------------------------------------------------
-- Hotels (IDs 1-12)
-- ---------------------------------------------------------------------
INSERT INTO Hotels (LocationId, Name, Stars, Address, Phone, PricePerNight, TotalRooms, RoomsAvailable, Rating, Description, ImageUrl, IsNew) VALUES
 (1,  N'Sofitel Legend Metropole Hanoi',           5, N'15 Ngô Quyền, Hoàn Kiếm, Hà Nội',        N'024 3826 6919', 6500000, 364, 40, 4.8, N'Historic 1901 hotel with a colonial wing and a modern Opera wing.', N'/images/hotels/metropole-hanoi.jpg', 0),
 (1,  N'Hanoi Old Quarter Garden Hotel',           3, N'22 Hàng Bè, Hoàn Kiếm, Hà Nội',           N'024 3926 0011', 750000, 40, 12, 4.1, N'Small family-run hotel a few minutes from Hoan Kiem Lake.', N'/images/hotels/old-quarter-garden.jpg', 1),
 (2,  N'Novotel Ha Long Bay',                      4, N'160 Hạ Long, Bãi Cháy, Quảng Ninh',       N'0203 3848 108', 1800000, 214, 35, 4.4, N'Bay-view rooms close to the cruise port and night market.', N'/images/hotels/novotel-ha-long.jpg', 0),
 (3,  N'Hotel de la Coupole – MGallery',           5, N'1 Hoàng Liên, Sa Pa, Lào Cai',            N'0214 3629 999', 3200000, 249, 30, 4.7, N'Colonial-style hotel in the centre of Sa Pa, near the Fansipan cable car station.', N'/images/hotels/de-la-coupole.jpg', 0),
 (3,  N'Sapa Valley View Homestay Hotel',          3, N'45 Mường Hoa, Sa Pa, Lào Cai',            N'0214 3871 222', 650000, 24, 10, 4.2, N'Simple rooms with balconies over the Muong Hoa valley.', N'/images/hotels/sapa-valley-view.jpg', 1),
 (4,  N'Silk Path Grand Hue Hotel',                5, N'2 Lê Lợi, Huế',                           N'0234 3855 666', 2300000, 166, 28, 4.6, N'Riverside hotel with a large pool, walking distance to Truong Tien Bridge.', N'/images/hotels/silk-path-hue.jpg', 0),
 (5,  N'Novotel Danang Premier Han River',         5, N'36 Bạch Đằng, Hải Châu, Đà Nẵng',         N'0236 3929 999', 2100000, 323, 60, 4.5, N'Tall riverside tower with views of the Han River bridges.', N'/images/hotels/novotel-danang.jpg', 0),
 (5,  N'My Khe Ocean Breeze Hotel',                3, N'88 Võ Nguyên Giáp, Sơn Trà, Đà Nẵng',     N'0236 3955 120', 850000, 60, 25, 4.0, N'Budget beachfront hotel opposite My Khe Beach.', N'/images/hotels/my-khe-ocean-breeze.jpg', 1),
 (6,  N'Hotel Royal Hoi An – MGallery',            5, N'39 Đào Duy Từ, Hội An',                   N'0235 3950 777', 2800000, 119, 18, 4.7, N'Japanese-inspired hotel on the river, a short walk from the ancient town.', N'/images/hotels/royal-hoi-an.jpg', 0),
 (7,  N'Sheraton Nha Trang Hotel & Spa',           5, N'26–28 Trần Phú, Nha Trang',               N'0258 3880 000', 2500000, 280, 50, 4.6, N'Every room faces the sea; rooftop bar and infinity pool.', N'/images/hotels/sheraton-nha-trang.jpg', 0),
 (8,  N'Dalat Palace Heritage Hotel',              5, N'2 Trần Phú, Đà Lạt',                      N'0263 3825 444', 3000000, 38, 8, 4.7, N'Grand 1922 hotel overlooking Xuan Huong Lake.', N'/images/hotels/dalat-palace.jpg', 0),
 (9,  N'Rex Hotel Saigon',                         5, N'141 Nguyễn Huệ, Quận 1',                  N'028 3829 2185', 2600000, 286, 45, 4.4, N'Landmark hotel on Nguyen Hue walking street with a famous rooftop bar.', N'/images/hotels/rex-saigon.jpg', 0);
GO

-- ---------------------------------------------------------------------
-- Restaurants (IDs 1-12)
-- ---------------------------------------------------------------------
INSERT INTO Restaurants (LocationId, Name, Cuisine, Address, Phone, PriceLevel, AvgPricePerPerson, TotalSeats, SeatsAvailable, Rating, OpenHours, Description, ImageUrl, IsNew) VALUES
 (1,  N'Chả Cá Thăng Long',        N'Vietnamese', N'31 Đường Thành, Hoàn Kiếm, Hà Nội',      N'024 3824 5115', 2, 180000, 80, 30, 4.4, N'10:00 - 21:00', N'Hanoi''s signature turmeric fish with dill, cooked at the table.', N'/images/restaurants/cha-ca-thang-long.jpg', 0),
 (1,  N'Bún Chả Hương Liên',       N'Vietnamese', N'24 Lê Văn Hưu, Hai Bà Trưng, Hà Nội',    N'024 3943 4106', 1, 70000, 120, 50, 4.3, N'08:00 - 20:30', N'Grilled pork with rice noodles; famous for a 2016 presidential visit.', N'/images/restaurants/bun-cha-huong-lien.jpg', 0),
 (2,  N'Hồng Hạnh Seafood',        N'Seafood',    N'50 Hạ Long, Bãi Cháy, Quảng Ninh',       N'0203 3812 333', 3, 450000, 200, 80, 4.2, N'09:00 - 22:00', N'Large seafood restaurant serving fresh catches from the bay.', N'/images/restaurants/hong-hanh.jpg', 0),
 (4,  N'Quán Hạnh',                N'Vietnamese', N'11 Phó Đức Chính, Huế',                  N'0234 3833 552', 1, 90000, 60, 25, 4.3, N'10:00 - 21:00', N'Hue specialities: banh khoai, banh beo and nem lui.', N'/images/restaurants/quan-hanh.jpg', 0),
 (5,  N'Bé Mặn',                   N'Seafood',    N'Võ Nguyên Giáp, Sơn Trà, Đà Nẵng',       N'0236 3946 789', 2, 300000, 250, 100, 4.1, N'10:00 - 23:00', N'Pick your seafood from the tanks and choose how it is cooked.', N'/images/restaurants/be-man.jpg', 0),
 (5,  N'Madame Lân',               N'Vietnamese', N'4 Bạch Đằng, Hải Châu, Đà Nẵng',         N'0236 3616 226', 2, 200000, 300, 120, 4.3, N'06:30 - 22:00', N'Central Vietnamese dishes in a large riverside house.', N'/images/restaurants/madame-lan.jpg', 1),
 (6,  N'Morning Glory',            N'Vietnamese', N'106 Nguyễn Thái Học, Hội An',            N'0235 2241 555', 2, 220000, 90, 30, 4.5, N'10:00 - 22:00', N'Hoi An street food classics such as cao lau and white rose dumplings.', N'/images/restaurants/morning-glory.jpg', 0),
 (7,  N'Lạc Cảnh',                 N'BBQ',        N'44 Nguyễn Bỉnh Khiêm, Nha Trang',        N'0258 3821 391', 2, 250000, 100, 40, 4.2, N'10:00 - 21:30', N'Grill-at-your-table beef and seafood, a Nha Trang institution.', N'/images/restaurants/lac-canh.jpg', 0),
 (8,  N'Le Rabelais',              N'French',     N'2 Trần Phú, Đà Lạt',                     N'0263 3825 444', 4, 1200000, 50, 20, 4.6, N'18:00 - 22:00', N'Fine French dining inside the Dalat Palace Heritage Hotel.', N'/images/restaurants/le-rabelais.jpg', 0),
 (9,  N'Cục Gạch Quán',            N'Vietnamese', N'10 Đặng Tất, Quận 1',                    N'028 3848 0144', 3, 400000, 120, 40, 4.5, N'09:00 - 23:00', N'Home-style southern cooking in a restored old house.', N'/images/restaurants/cuc-gach-quan.jpg', 0),
 (9,  N'Pizza 4P''s Bến Thành',    N'Italian',    N'8 Thủ Khoa Huân, Quận 1',                N'1900 6043', 3, 350000, 150, 60, 4.7, N'10:00 - 23:00', N'Wood-fired pizza with house-made cheese.', N'/images/restaurants/pizza-4ps.jpg', 1),
 (10, N'Xin Chào Seafood',         N'Seafood',    N'66 Trần Hưng Đạo, Dương Đông, Phú Quốc', N'0297 3999 919', 3, 500000, 180, 70, 4.3, N'10:00 - 22:30', N'Seafood on a pier over the sea, best at sunset.', N'/images/restaurants/xin-chao.jpg', 1);
GO

-- ---------------------------------------------------------------------
-- Resorts (IDs 1-8)
-- ---------------------------------------------------------------------
INSERT INTO Resorts (LocationId, Name, Stars, Address, Phone, PricePerNight, TotalUnits, UnitsAvailable, Rating, Facilities, Description, ImageUrl, IsNew) VALUES
 (3,  N'Topas Ecolodge',                          4, N'Thanh Bình, Sa Pa, Lào Cai',             N'0214 3872 404', 4500000, 33, 6, 4.6, N'Infinity pool, Spa, Trekking tours',          N'Stone bungalows on a hilltop facing rice terraces.', N'/images/resorts/topas-ecolodge.jpg', 0),
 (4,  N'Pilgrimage Village Boutique Resort',      4, N'130 Minh Mạng, Thủy Xuân, Huế',          N'0234 3885 461', 2200000, 99, 20, 4.5, N'Pools, Spa, Cooking class',                   N'Garden villas between Hue city and the royal tombs.', N'/images/resorts/pilgrimage-village.jpg', 0),
 (5,  N'InterContinental Danang Sun Peninsula Resort', 5, N'Bãi Bắc, Sơn Trà, Đà Nẵng',        N'0236 3938 888', 12000000, 197, 25, 4.9, N'Private beach, Spa, Fine dining, Kids club', N'Terraced resort on the Son Tra peninsula above a private bay.', N'/images/resorts/intercontinental-danang.jpg', 0),
 (6,  N'Four Seasons Resort The Nam Hai',         5, N'Điện Dương, Đà Nẵng',                    N'0235 3940 000', 15000000, 100, 12, 4.9, N'Private beach, Pool villas, Spa',             N'Villas on Ha My beach, 15 minutes from Hoi An.', N'/images/resorts/nam-hai.jpg', 0),
 (7,  N'Six Senses Ninh Van Bay',                 5, N'Ninh Vân, Ninh Hòa, Khánh Hòa',          N'0258 3524 268', 14000000, 62, 8, 4.8, N'Private pools, Spa, Boat transfer',           N'Pool villas on a bay reachable only by boat.', N'/images/resorts/six-senses-ninh-van.jpg', 0),
 (7,  N'Vinpearl Resort & Spa Nha Trang Bay',     5, N'Hòn Tre, Nha Trang',                     N'0258 3598 188', 4800000, 400, 90, 4.4, N'Beach, Water park access, Spa',              N'Island resort linked to VinWonders amusement park.', N'/images/resorts/vinpearl-nha-trang.jpg', 1),
 (8,  N'Ana Mandara Villas Dalat',                5, N'Lê Lai, Phường 5, Đà Lạt',               N'0263 3555 888', 3500000, 65, 15, 4.5, N'Heated pool, Spa, Garden',                    N'Restored 1920s French villas on a pine hill.', N'/images/resorts/ana-mandara-dalat.jpg', 0),
 (10, N'JW Marriott Phu Quoc Emerald Bay',        5, N'Bãi Khem, An Thới, Phú Quốc',            N'0297 3779 999', 9000000, 244, 40, 4.8, N'Private beach, Pools, Spa, Kids club',        N'Resort themed as a fictional university, on Khem Beach.', N'/images/resorts/jw-marriott-phu-quoc.jpg', 1);
GO

-- ---------------------------------------------------------------------
-- Transport routes (IDs 1-14)
-- ---------------------------------------------------------------------
INSERT INTO Transports (FromLocationId, ToLocationId, TransportType, Operator, Fare, DurationHours, DepartureTimes, SeatsAvailable, Description) VALUES
 (1, 2,  N'Bus',   N'Karnel Travels Limousine',  300000, 2.5, N'07:00, 09:00, 13:00, 16:00', 18, N'Door-to-door limousine van from the Old Quarter to Bai Chay.'),
 (1, 2,  N'Car',   N'Karnel Travels Private Car', 1800000, 2.5, N'Any time (book 1 day ahead)', 4, N'Private 4-seat car with driver, price per car.'),
 (1, 3,  N'Bus',   N'Sao Việt',                  350000, 5.5, N'06:30, 13:00, 22:00', 30, N'Sleeper bus on the Noi Bai - Lao Cai expressway.'),
 (1, 3,  N'Train', N'Đường sắt Việt Nam',        550000, 8.0, N'21:30', 40, N'Overnight sleeper train to Lao Cai, then 1 hour by bus to Sa Pa.'),
 (1, 4,  N'Train', N'Đường sắt Việt Nam',        950000, 13.0, N'06:00, 19:30', 60, N'Reunification Express, soft sleeper berth.'),
 (1, 5,  N'Plane', N'Vietnam Airlines',          1900000, 1.3, N'06:00, 09:30, 14:00, 19:45', 120, N'Direct flight Noi Bai - Da Nang.'),
 (1, 9,  N'Plane', N'Vietnam Airlines',          2300000, 2.2, N'Every hour 06:00 - 21:00', 150, N'Direct flight Noi Bai - Tan Son Nhat.'),
 (4, 5,  N'Car',   N'Karnel Travels Private Car', 1200000, 2.5, N'Any time (book 1 day ahead)', 4, N'Over the Hai Van Pass with photo stops, price per car.'),
 (5, 6,  N'Car',   N'Karnel Travels Shuttle',    120000, 0.8, N'08:00, 10:00, 14:00, 17:00', 12, N'Shuttle from Da Nang hotels to Hoi An ancient town.'),
 (5, 7,  N'Train', N'Đường sắt Việt Nam',        700000, 9.5, N'08:30, 22:00', 50, N'Coastal railway to Nha Trang.'),
 (7, 8,  N'Bus',   N'Phương Trang (FUTA)',       160000, 4.0, N'07:00, 10:00, 13:00, 15:00', 34, N'Mountain road over the Khanh Le pass.'),
 (9, 8,  N'Bus',   N'Phương Trang (FUTA)',       300000, 7.0, N'Every hour 06:00 - 23:00', 34, N'Sleeper bus to Da Lat.'),
 (9, 7,  N'Train', N'Đường sắt Việt Nam',        650000, 8.0, N'06:00, 20:30', 60, N'Overnight train to Nha Trang.'),
 (9, 10, N'Plane', N'Vietnam Airlines',          1500000, 1.0, N'07:00, 11:00, 15:30, 19:00', 100, N'Direct flight Tan Son Nhat - Phu Quoc.');
GO

-- ---------------------------------------------------------------------
-- Packages (IDs 1-5) and the spots each one visits
-- ---------------------------------------------------------------------
INSERT INTO Packages (LocationId, Title, Description, DurationDays, Price, DiscountPercent, ValidFrom, ValidUntil, ImageUrl, IsActive) VALUES
 (2,  N'Ha Long Bay Cruise 2 Days 1 Night', N'Limousine from Hanoi, overnight cruise, kayaking and cave visit, all meals on board.', 2, 3900000, 10, '2026-10-01', '2027-03-31', N'/images/packages/ha-long-cruise.jpg', 1),
 (3,  N'Sa Pa Trekking 3 Days 2 Nights',    N'Sleeper bus, homestay in a H''Mong village, Fansipan cable car.', 3, 3200000, 0, '2026-10-01', '2027-05-31', N'/images/packages/sapa-trekking.jpg', 1),
 (5,  N'Central Heritage 4 Days 3 Nights',  N'Da Nang, Ba Na Hills, Hoi An ancient town and the Hue Imperial City with a private car.', 4, 7500000, 15, '2026-11-01', '2027-04-30', N'/images/packages/central-heritage.jpg', 1),
 (8,  N'Da Lat Highland Escape 3 Days 2 Nights', N'Lakes, flower gardens and pine valleys in the city of eternal spring.', 3, 2900000, 5, '2026-11-01', '2027-03-31', N'/images/packages/dalat-escape.jpg', 1),
 (10, N'Phu Quoc Beach Break 3 Days 2 Nights', N'Return flight from Ho Chi Minh City, beach resort and island-hopping boat trip.', 3, 6800000, 20, '2026-11-15', '2027-04-30', N'/images/packages/phu-quoc-break.jpg', 1);
GO

INSERT INTO PackageSpots (PackageId, SpotId, DayNumber) VALUES
 (1, 4, 1),
 (2, 6, 1), (2, 5, 2),
 (3, 9, 1), (3, 10, 2), (3, 11, 2), (3, 12, 3), (3, 7, 4),
 (4, 16, 1), (4, 17, 2),
 (5, 20, 2);
GO

-- ---------------------------------------------------------------------
-- Sample customers, bookings and feedback (for the admin pages and demo)
-- ---------------------------------------------------------------------
INSERT INTO Customers (FullName, Email, Phone) VALUES
 (N'Nguyễn Văn An',   N'an.nguyen@example.com',   N'0912345678'),
 (N'Trần Thị Bình',   N'binh.tran@example.com',   N'0987654321'),
 (N'Lê Minh Châu',    N'chau.le@example.com',     N'0905111222'),
 (N'Phạm Quốc Dũng',  N'dung.pham@example.com',   N'0933444555'),
 (N'John Smith',      N'john.smith@example.com',  N'0868777888');
GO

INSERT INTO Bookings (CustomerId, ServiceType, ServiceId, TravelDate, NumberOfPeople, Quantity, Note, Status) VALUES
 (1, N'Package',    1, '2026-12-12', 2, 1, N'Honeymoon, please arrange a double cabin.', N'New'),
 (2, N'Hotel',      7, '2026-12-20', 3, 2, N'Two rooms with river view if possible.',    N'Contacted'),
 (3, N'Transport',  6, '2026-12-05', 1, 1, NULL,                                         N'Completed'),
 (4, N'Resort',     8, '2027-01-02', 4, 1, N'Family with two children.',                N'New'),
 (5, N'Restaurant', 7, '2026-11-28', 2, 2, N'Dinner at 19:00.',                         N'Cancelled');
GO

INSERT INTO Feedbacks (CustomerId, Rating, Subject, Message, IsRead) VALUES
 (3, 5, N'Smooth flight booking', N'Booking was quick and the staff called me back within an hour.', 1),
 (2, 4, N'Good hotel suggestions', N'The hotel list was helpful. Please add more 3-star options in Da Nang.', 0),
 (5, 3, N'Restaurant booking',     N'I had to cancel. It would be nice to change the date online instead.', 0);
GO

-- ---------------------------------------------------------------------
-- Admin account (demo login: admin / Admin@123)
-- Hash format: ASP.NET Core PasswordHasher v3, PBKDF2-HMAC-SHA512,
-- 100,000 iterations. Verify with PasswordHasher<Admin>.VerifyHashedPassword.
-- ---------------------------------------------------------------------
INSERT INTO Admins (Username, PasswordHash, FullName) VALUES
 (N'admin', N'AQAAAAIAAYagAAAAED/D1y/oTFL7YtiqQkA++cEV3K9l/A2/aZHeb7bmzZZ0lJsYxqeWj26urLK0h5pRXw==', N'Karnel Administrator');
GO

PRINT N'Sample data inserted.';
GO
