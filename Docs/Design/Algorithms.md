# Karnel Travel Guide – Algorithms

These algorithms describe the logic behind each feature in the Customer Requirement Specification. Table and column names match `01_create_schema.sql`. Each algorithm lists its input, its output, and the steps in pseudo-code.

| ID | Algorithm | Requirement |
| --- | --- | --- |
| A1 | Show Information page (new places and packages) | FR-41, FR-42 |
| A2 | Search tourist spots | FR-30 |
| A3 | Search hotels, restaurants and resorts | FR-31 |
| A4 | Advanced search | FR-33, FR-34 |
| A5 | Place an order (booking) | FR-50, FR-51, FR-53, FR-54 |
| A6 | Send feedback | FR-52, FR-53, FR-54 |
| A7 | Admin login | FR-60 |
| A8 | Update booking status | FR-62 |

---

## A1. Show Information page

**Input:** none · **Output:** five section links, new places, active packages with their discounted price

```
BEGIN
  newHotels      ← SELECT TOP 4 FROM Hotels      WHERE IsNew = 1 ORDER BY CreatedAt DESC
  newRestaurants ← SELECT TOP 4 FROM Restaurants WHERE IsNew = 1 ORDER BY CreatedAt DESC
  newResorts     ← SELECT TOP 4 FROM Resorts     WHERE IsNew = 1 ORDER BY CreatedAt DESC
  packages       ← SELECT FROM Packages
                   WHERE IsActive = 1 AND ValidFrom ≤ TODAY AND ValidUntil ≥ TODAY
                   ORDER BY DiscountPercent DESC
  FOR EACH p IN packages
      p.FinalPrice ← p.Price × (100 − p.DiscountPercent) / 100
  END FOR
  DISPLAY links: Tourist Spots, Travel, Hotels, Restaurants, Resorts
  DISPLAY newHotels, newRestaurants, newResorts, packages
END
```

## A2. Search tourist spots

**Input:** category (optional), locationId (optional), keyword (optional) · **Output:** list of matching spots

```
BEGIN
  query ← all rows of TouristSpots joined with Locations
  IF category is not empty   THEN query ← query WHERE Category = category
  IF locationId is not empty THEN query ← query WHERE LocationId = locationId
  IF keyword is not empty    THEN
      keyword ← TRIM(keyword)
      query ← query WHERE Name CONTAINS keyword OR Description CONTAINS keyword
  END IF
  results ← query ORDER BY IsFeatured DESC, Name
  IF results is empty THEN DISPLAY "No tourist spots match. Try another category or location."
  ELSE DISPLAY results
END
```

## A3. Search hotels, restaurants and resorts

The assignment asks for search by **location, price, quality, quantity and availability**. One algorithm serves all three types; only the columns change.

| Criterion | Hotels | Restaurants | Resorts |
| --- | --- | --- | --- |
| price | PricePerNight | AvgPricePerPerson | PricePerNight |
| quality | Stars | Rating | Stars |
| quantity / availability | RoomsAvailable | SeatsAvailable | UnitsAvailable |

**Input:** type, locationId, minPrice, maxPrice, minQuality, quantity (default 1), onlyAvailable (default true) · **Output:** list of matching places

```
BEGIN
  IF minPrice > maxPrice THEN SWAP(minPrice, maxPrice)          // user typed them the wrong way round
  IF quantity < 1 THEN quantity ← 1

  SELECT table, priceCol, qualityCol, availCol BY type
  query ← all rows of table joined with Locations

  IF locationId is not empty THEN query ← query WHERE LocationId = locationId
  IF minPrice is not empty   THEN query ← query WHERE priceCol ≥ minPrice
  IF maxPrice is not empty   THEN query ← query WHERE priceCol ≤ maxPrice
  IF minQuality is not empty THEN query ← query WHERE qualityCol ≥ minQuality
  IF onlyAvailable = true    THEN query ← query WHERE availCol ≥ quantity

  results ← query ORDER BY priceCol ASC
  IF results is empty THEN DISPLAY "Nothing matches. Try a wider price range or another location."
  ELSE DISPLAY results with a "Book" button for each row
END
```

## A4. Advanced search

Advanced search runs A3 on **several types at once**, adds a keyword, sorts and pages the results.

**Input:** types[] (any of Hotel, Restaurant, Resort, Spot), the A3 criteria, keyword, sortBy (price | quality | name), sortDir (asc | desc), page (default 1), pageSize = 10 · **Output:** one page of results and the total count

```
BEGIN
  IF types[] is empty THEN types[] ← [Hotel, Restaurant, Resort, Spot]
  all ← empty list

  FOR EACH t IN types[]
      IF t = Spot THEN rows ← A2(category = empty, locationId, keyword)
      ELSE             rows ← A3(t, locationId, minPrice, maxPrice, minQuality, quantity, onlyAvailable)
                       IF keyword is not empty THEN rows ← rows WHERE Name CONTAINS keyword
      FOR EACH r IN rows
          ADD {Type: t, Id, Name, City, Price, Quality, ImageUrl} TO all
      END FOR
  END FOR

  SORT all BY sortBy IN sortDir order
  total      ← COUNT(all)
  totalPages ← CEILING(total / pageSize)
  IF page < 1 THEN page ← 1
  IF page > totalPages AND totalPages > 0 THEN page ← totalPages
  pageRows   ← all SKIP (page − 1) × pageSize TAKE pageSize

  DISPLAY pageRows, "Showing page page of totalPages (total results)"
END
```

## A5. Place an order (booking)

**Input:** fullName, email, phone, serviceType, serviceId, travelDate, numberOfPeople, quantity, note · **Output:** saved booking and a confirmation, or a list of errors

```
BEGIN
  errors ← empty list
  // 1. Validate the form
  IF fullName is empty                     THEN ADD "Please enter your full name."              TO errors
  IF email does not match name@domain.tld  THEN ADD "Please enter a valid email address."       TO errors
  IF phone is not 10 digits starting with 0 THEN ADD "Please enter a 10-digit phone number."   TO errors
  IF travelDate < TODAY                    THEN ADD "Travel date cannot be in the past."        TO errors
  IF numberOfPeople NOT BETWEEN 1 AND 50   THEN ADD "Number of people must be from 1 to 50."    TO errors
  IF errors is not empty THEN redisplay the form with errors; STOP

  // 2. Check that the chosen service exists (ServiceId has no foreign key)
  service ← find row serviceId in the table for serviceType
  IF service not found THEN redisplay the form with "This service is no longer available."; STOP

  // 3. Check availability
  IF serviceType = Hotel      AND service.RoomsAvailable  < quantity       THEN error "Not enough rooms"
  IF serviceType = Resort     AND service.UnitsAvailable  < quantity       THEN error "Not enough villas"
  IF serviceType = Restaurant AND service.SeatsAvailable  < numberOfPeople THEN error "Not enough seats"
  IF serviceType = Transport  AND service.SeatsAvailable  < numberOfPeople THEN error "Not enough seats"
  IF serviceType = Package    AND NOT (service.IsActive AND travelDate BETWEEN ValidFrom AND ValidUntil)
                                                                            THEN error "Package not offered on that date"
  IF any error THEN redisplay the form with it; STOP

  // 4. Find or create the customer (email identifies a returning customer)
  customer ← SELECT FROM Customers WHERE Email = LOWER(TRIM(email))
  IF customer not found THEN
      customer ← INSERT INTO Customers (fullName, LOWER(TRIM(email)), phone)
  ELSE
      UPDATE customer SET FullName = fullName, Phone = phone
  END IF

  // 5. Save the booking; staff confirm and adjust availability later (A8)
  INSERT INTO Bookings (customer.CustomerId, serviceType, serviceId, travelDate,
                        numberOfPeople, quantity, note, Status = 'New')
  DISPLAY "Thank you! Your booking number is #BookingId. We will contact you within 24 hours."
END
```

## A6. Send feedback

**Input:** fullName, email, phone, rating (1–5), subject, message · **Output:** saved feedback and a thank-you message

```
BEGIN
  validate fullName, email, phone as in A5 step 1
  IF rating NOT BETWEEN 1 AND 5 THEN error "Please choose a rating from 1 to 5 stars."
  IF LENGTH(TRIM(message)) < 10 THEN error "Please write at least 10 characters."
  IF any error THEN redisplay the form; STOP

  customer ← find or create by email (A5 step 4)
  INSERT INTO Feedbacks (customer.CustomerId, rating, subject, message, IsRead = 0)
  DISPLAY "Thank you for your feedback."
END
```

## A7. Admin login

**Input:** username, password · **Output:** signed-in admin session, or an error

```
BEGIN
  IF username or password is empty THEN error "Please enter username and password."; STOP
  admin ← SELECT FROM Admins WHERE Username = username
  IF admin not found
     OR PasswordHasher.VerifyHashedPassword(admin, admin.PasswordHash, password) = Failed THEN
      error "Username or password is incorrect."            // same message for both cases
      STOP
  END IF
  UPDATE admin SET LastLoginAt = NOW
  create authentication cookie with admin.AdminId and admin.FullName
  REDIRECT to /admin
END
```

Every page under `/admin` checks the cookie first. If there is no valid cookie, it redirects to the login page.

## A8. Update booking status

**Input:** bookingId, newStatus · **Output:** booking with its new status; availability adjusted

Allowed changes: New → Contacted → Completed, and New or Contacted → Cancelled.

```
BEGIN
  booking ← SELECT FROM Bookings WHERE BookingId = bookingId
  IF booking not found THEN error "Booking not found"; STOP
  IF (booking.Status, newStatus) is not an allowed change THEN error "This status change is not allowed"; STOP

  BEGIN TRANSACTION
    IF newStatus = 'Contacted' THEN            // staff confirmed with the customer: hold the capacity
        reduce the service's available rooms / seats / units by booking.Quantity
        (or by booking.NumberOfPeople for restaurants and transport;
         packages have no capacity column, so nothing is reduced)
        IF the result would go below 0 THEN ROLLBACK; error "Not enough availability left"; STOP
    IF newStatus = 'Cancelled' AND booking.Status = 'Contacted' THEN
        give the held rooms / seats / units back
    UPDATE booking SET Status = newStatus
  COMMIT
END
```
