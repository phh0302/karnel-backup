# Karnel Travel Guide – GUI Standards Document

Every page follows these rules so the site looks and behaves as one product. The values below are the ones defined in `src/KarnelTravelGuide/wwwroot/css/site.css`.

## 1. Colours

The palette takes its two main colours from Viet Nam's coast: the teal of Ha Long Bay water and the amber of Hoi An lanterns.

| Token | Hex | Use |
| --- | --- | --- |
| `--kt-primary` | #0B6E73 | Header, primary buttons, links, active tab |
| `--kt-primary-dark` | #084D51 | Hover and pressed state of primary items, footer |
| `--kt-accent` | #E3A12F | Discount badges, "New" badges, star ratings |
| `--kt-ink` | #10222B | Body text and headings |
| `--kt-muted` | #5A6B74 | Secondary text, captions, placeholders |
| `--kt-bg` | #F3F6F7 | Page background |
| `--kt-surface` | #FFFFFF | Cards, forms, tables |
| `--kt-border` | #D6DEE2 | Card borders, table lines, input borders |
| `--kt-success` | #2E7D4F | Success messages, "Available" |
| `--kt-danger` | #C0392B | Error messages, "Full", Delete buttons |

Text on `--kt-primary` is always white. Body text on `--kt-bg` keeps a contrast ratio above 4.5 : 1.

## 2. Typography

| Role | Font | Size | Weight |
| --- | --- | --- | --- |
| Page title (h1) | Lexend | 32 px (24 px on phones) | 700 |
| Section title (h2) | Lexend | 24 px | 600 |
| Card title (h3) | Lexend | 18 px | 600 |
| Body text | Be Vietnam Pro | 16 px, line height 1.6 | 400 |
| Small text, captions | Be Vietnam Pro | 14 px | 400 |
| Prices | Be Vietnam Pro, tabular figures | 18 px | 600 |

Both fonts load from Google Fonts and support Vietnamese. If they fail to load, the browser falls back to Segoe UI, then Arial.

## 3. Layout and spacing

- Content width: at most 1200 px, centred, with 16 px side padding on phones.
- Spacing scale: 4, 8, 16, 24, 32, 48 px. Gaps between sections are 48 px; between cards 24 px.
- Grid: Bootstrap 5 grid. Cards show 4 per row on large screens, 2 on tablets, 1 on phones.
- Breakpoints: 576, 768, 992, 1200 px (Bootstrap defaults).

## 4. Components

| Component | Rule |
| --- | --- |
| Header menu | Teal bar, white links. The active page is underlined in amber. Folds into ☰ below 992 px. |
| Primary button | Teal fill, white text, 8 px radius. One primary button per form. |
| Secondary button | White fill, teal border and text. |
| Delete button | Red outline; always asks for confirmation on a separate page. |
| Card | White, 1 px border, 12 px radius, 4:3 photo on top, title, city, then price at the bottom. |
| Badges | "New" and "-15%" badges in amber with dark text, top-left of the card photo. |
| Star rating | Amber ★ characters, number of stars equals `Stars`. |
| Price | Always "1.800.000 ₫" style: dot thousands separator, ₫ after the number. |
| Tables (admin) | Striped rows, header row in `--kt-bg`, actions in the last column. |
| Empty state | Grey icon, one sentence that says what to try next. |

## 5. Forms and messages

- Every input has a visible label above it; required fields end with a red `*`.
- Validation runs in the browser first and again on the server.
- Error text appears in red directly under the field, in plain words: "Please enter a valid email address."
- After a successful submit, the page shows a green message that names what happened: "Your booking #12 was sent."
- Placeholders show an example value, never the label.

## 6. Images

- Photos are JPG, 1200 × 900 px or close to 4:3, under 300 KB.
- Every image has `alt` text with the place name.
- Missing photos fall back to `/images/placeholder.svg`.

## 7. Naming in code

| Item | Convention | Example |
| --- | --- | --- |
| C# classes, methods | PascalCase | `SearchController.Hotels()` |
| C# variables | camelCase | `minPrice` |
| Views | Folder per controller | `Views/Information/Hotels.cshtml` |
| CSS classes | `kt-` prefix, kebab-case | `.kt-card`, `.kt-badge-new` |
| Images | kebab-case, folder per type | `images/hotels/rex-saigon.jpg` |
