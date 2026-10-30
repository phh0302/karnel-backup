# Karnel Travel Guide – Sitemap and Wireframes

## 1. Sitemap

Every public page shows the same header menu with the five sections the client asked for.

```
www.karneltravelguide.com
│
├── /                              Home Page
├── /About                         About Us
├── /Search                        Search Page (tabs: Tourist spots · Hotels · Restaurants · Resorts)
│   └── /Search/Advanced           Advanced Search
├── /Information                   Information Page (5 links + new places + packages)
│   ├── /Information/TouristSpots  Tourist Spots          → /Information/Spot/{id}
│   ├── /Information/Travel        Travel Information
│   ├── /Information/Hotels        Hotel Information      → /Information/Hotel/{id}
│   ├── /Information/Restaurants   Restaurant Information → /Information/Restaurant/{id}
│   ├── /Information/Resorts       Resorts Information    → /Information/Resort/{id}
│   └── /Information/Package/{id}  Package detail
├── /Contact                       Contact Us (booking form + feedback form)
│   └── /Contact/Thanks            Confirmation
│
└── /Admin                         Administration (login required)
    ├── /Admin/Account/Login
    ├── /Admin                     Dashboard
    ├── /Admin/TouristSpots · /Admin/Hotels · /Admin/Restaurants · /Admin/Resorts
    ├── /Admin/Transports · /Admin/Packages
    ├── /Admin/Bookings
    └── /Admin/Feedbacks
```

## 2. Shared frame

```
┌──────────────────────────────────────────────────────────────────────┐
│ [Logo] Karnel Travel Guide    Home  About Us  Search  Information ▾  Contact Us │
├──────────────────────────────────────────────────────────────────────┤
│                                                                      │
│                         page content                                 │
│                                                                      │
├──────────────────────────────────────────────────────────────────────┤
│ About · Links · Contact details                 © 2026 Karnel Travels│
└──────────────────────────────────────────────────────────────────────┘
```

On screens narrower than 992 px the menu folds into a ☰ button.

## 3. Wireframes

### Home Page

```
┌──────────────────────────────────────────────────────────────────────┐
│  HERO: photo of Ha Long Bay                                          │
│  "Discover Viet Nam with Karnel Travels"                              │
│  [ Where to? (location ▾) ] [ What? (Hotels ▾) ] [ Search ]          │
├──────────────────────────────────────────────────────────────────────┤
│  Featured destinations                                    See all →  │
│  [card] [card] [card] [card]                                          │
├──────────────────────────────────────────────────────────────────────┤
│  Packages and discounts                                              │
│  [package -10%] [package -15%] [package -20%]                         │
├──────────────────────────────────────────────────────────────────────┤
│  Our services: Transport · Packages · Accommodation   [About us →]   │
└──────────────────────────────────────────────────────────────────────┘
```

### About Us

```
┌──────────────────────────────────────────────────────────────────────┐
│  Page title: About Karnel Travels                                    │
│  Company story (2 paragraphs)                  [photo]               │
├──────────────────────────────────────────────────────────────────────┤
│  Our facilities                                                       │
│  [Transportation] [Tour packages] [Transport + accommodation]        │
├──────────────────────────────────────────────────────────────────────┤
│  Numbers: 10 destinations · 12 hotels · 14 routes   [Contact us →]   │
└──────────────────────────────────────────────────────────────────────┘
```

### Search Page

```
┌──────────────────────────────────────────────────────────────────────┐
│  Search                                      Advanced search →       │
│  ( Tourist spots | Hotels | Restaurants | Resorts )   ← tabs         │
│  Location [▾]  Price from [  ] to [  ]  Quality [▾]  Quantity [ 1 ]  │
│  [x] Only show available                        [ Search ]           │
├──────────────────────────────────────────────────────────────────────┤
│  12 results                                                           │
│  [card: image | name, city, stars | price | Book]                     │
│  [card ...]                                                           │
│  (no result → "Nothing matches. Try a wider price range…")           │
└──────────────────────────────────────────────────────────────────────┘
```

### Advanced Search

```
┌──────────────────────────────────────────────────────────────────────┐
│  Advanced search                                                     │
│  Keyword [               ]                                           │
│  Types [x] Spots [x] Hotels [x] Restaurants [x] Resorts               │
│  Location [▾]  Price [ ]–[ ]  Min quality [▾]  Quantity [ ]          │
│  Sort by [Price ▾] [Ascending ▾]                     [ Search ]       │
├──────────────────────────────────────────────────────────────────────┤
│  Type | Name | City | Quality | Price | (link)                       │
│  ...                                                                  │
│  « 1 2 3 »   Showing page 1 of 3 (27 results)                         │
└──────────────────────────────────────────────────────────────────────┘
```

### Information Page

```
┌──────────────────────────────────────────────────────────────────────┐
│  Information                                                         │
│  [Tourist Spots] [Travel] [Hotels] [Restaurants] [Resorts]  ← 5 tiles│
├──────────────────────────────────────────────────────────────────────┤
│  New on Karnel: hotels | restaurants | resorts (cards with NEW badge)│
├──────────────────────────────────────────────────────────────────────┤
│  Packages and discounts (price struck through, final price)          │
└──────────────────────────────────────────────────────────────────────┘
```

### Information sub-page (Hotels as the example)

```
┌──────────────────────────────────────────────────────────────────────┐
│  Information › Hotels                                                │
│  Filter by city [▾]                                                   │
│  [card] [card] [card]                                                 │
│  [card] [card] [card]                                                 │
└──────────────────────────────────────────────────────────────────────┘
Detail page: photo · name · stars · address · phone · price · rooms left
             · description · [ Book this hotel ] → Contact Us, pre-filled
```

### Contact Us

```
┌──────────────────────────────────────────────────────────────────────┐
│  Contact Us                                                          │
│  ┌────────────── Book a service ───────────┐ ┌── Company contact ──┐ │
│  │ Full name [        ] Email [          ] │ │ Address             │ │
│  │ Phone [        ]                        │ │ Hotline             │ │
│  │ Service type [▾]  Service [▾]           │ │ Email               │ │
│  │ Travel date [  ]  People [ ]  Qty [ ]   │ │ Opening hours       │ │
│  │ Note [                               ]  │ └─────────────────────┘ │
│  │                         [ Send booking ]│                         │
│  └─────────────────────────────────────────┘                         │
│  ┌────────────── Feedback ─────────────────┐                         │
│  │ Name, Email, Phone, Rating ★★★★★,        │                         │
│  │ Subject, Message          [ Send ]      │                         │
│  └─────────────────────────────────────────┘                         │
└──────────────────────────────────────────────────────────────────────┘
```

### Admin

```
┌───────────────┬──────────────────────────────────────────────────────┐
│ Karnel Admin  │  Dashboard                                           │
│ Dashboard     │  [New bookings 2] [Unread feedback 2] [Hotels 12]    │
│ Tourist spots │  Latest bookings table                               │
│ Hotels        │                                                      │
│ Restaurants   │  List pages: table + [Add] + Edit / Delete per row    │
│ Resorts       │  Edit pages: one form, Save / Cancel                 │
│ Transports    │                                                      │
│ Packages      │                                                      │
│ Bookings      │                                                      │
│ Feedback      │                                                      │
│ Log out       │                                                      │
└───────────────┴──────────────────────────────────────────────────────┘
```
