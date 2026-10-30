# PRD – Karnel Travel Guide

| Mục | Nội dung |
|---|---|
| Dự án | Karnel Travel Guide (Aptech eProject) |
| Khách hàng | Karnel Travels, công ty du lịch và lữ hành |
| URL | `http://www.Karneltravelguide.com` |
| Phiên bản | 1.0 (bản nháp) · 29/10/2026 |
| Nhóm | Batch: ______ · Group: ______ |

> **Cách đọc file này:** yêu cầu gắn nhãn **[ĐỀ]** lấy trực tiếp từ đề bài, bắt buộc phải có. Yêu cầu gắn nhãn **[ĐỀ XUẤT]** là do nhóm tự đặt thêm cho website hoàn chỉnh hơn, có thể bỏ nếu thiếu thời gian.

---

## 1. Bối cảnh bài toán

Karnel Travels là công ty du lịch và lữ hành. Họ cung cấp dịch vụ di chuyển giữa các thành phố và tới các điểm du lịch, đồng thời đặt chỗ ở khách sạn. Khách hàng cần xem và đặt được tour cũng như dịch vụ di chuyển qua mạng.

Công ty muốn có một **website gắn với tên miền riêng** để thu hút khách hàng. Website cung cấp thông tin về điểm du lịch, phương tiện di chuyển, khách sạn, nhà hàng và resort trong nước, và cho phép khách liên hệ đặt dịch vụ hoặc gửi góp ý.

## 2. Mục tiêu

| # | Mục tiêu | Cách đo |
|---|---|---|
| G1 | Giới thiệu công ty và dịch vụ | Trang About Us mô tả đủ các dịch vụ của công ty |
| G2 | Cung cấp thông tin du lịch trong nước | Có đủ 5 nhóm thông tin, mỗi nhóm có dữ liệu thật |
| G3 | Giúp khách tìm nhanh dịch vụ phù hợp | Search lọc được theo địa điểm, giá, chất lượng, số lượng, tình trạng còn chỗ |
| G4 | Nhận đơn đặt và góp ý từ khách | Form Contact và Feedback lưu được vào CSDL |
| G5 | Quảng bá ưu đãi | Hiển thị được gói tour, khuyến mãi, cơ sở mới |

## 3. Người dùng

| Vai trò | Mô tả | Nhu cầu chính |
|---|---|---|
| Khách truy cập | Bất kỳ ai vào website, không cần đăng nhập | Xem thông tin, tìm kiếm, đặt dịch vụ, gửi góp ý |
| Quản trị viên **[ĐỀ XUẤT]** | Nhân viên Karnel Travels | Thêm/sửa/xóa dữ liệu, xem đơn đặt và góp ý |

## 4. Phạm vi

**Trong phạm vi:** website thông tin du lịch gồm Home, About Us, Search (kèm Advanced Search), Information (5 trang con) và Contact Us (kèm Feedback). Có CSDL lưu dữ liệu và đơn đặt.

**Ngoài phạm vi:** thanh toán trực tuyến, ứng dụng di động, đa ngôn ngữ, dịch vụ du lịch nước ngoài (đề chỉ nói "trong nước").

---

## 5. Yêu cầu chức năng

### 5.1. Chung

| ID | Yêu cầu | Nguồn |
|---|---|---|
| FR-01 | Website chạy tại `http://www.Karneltravelguide.com`. Khi demo ở máy local, trỏ tên miền về máy bằng file `hosts`. | [ĐỀ] CRS 1 |
| FR-02 | Có **menu** hiển thị trên mọi trang để đi tới các mục dễ dàng | [ĐỀ] CRS 2 |
| FR-03 | Menu gồm: **Home Page, About Us, Search Page, Information Page** (5 mục con), **Contact Us** | [ĐỀ] CRS 3 |
| FR-04 | Footer có thông tin liên hệ của công ty và link tới các trang chính | [ĐỀ XUẤT] |

### 5.2. Home Page

| ID | Yêu cầu | Nguồn |
|---|---|---|
| FR-10 | Trang chủ là trang đầu tiên khách nhìn thấy, có lối vào tới mọi mục trong menu | [ĐỀ] CRS 3 |
| FR-11 | Có banner, một số điểm du lịch nổi bật và các gói tour đang khuyến mãi | [ĐỀ XUẤT] |
| FR-12 | Có ô tìm kiếm nhanh, dẫn sang trang Search | [ĐỀ XUẤT] |

### 5.3. About Us

| ID | Yêu cầu | Nguồn |
|---|---|---|
| FR-20 | Giới thiệu thông tin công ty Karnel Travels | [ĐỀ] CRS 4 |
| FR-21 | Mô tả các dịch vụ: **di chuyển**, **gói tour (packages)**, **di chuyển và chỗ ở tại các điểm du lịch** | [ĐỀ] CRS 4 |

### 5.4. Search Page

| ID | Yêu cầu | Nguồn |
|---|---|---|
| FR-30 | Tìm **điểm du lịch** theo loại: bãi biển, di tích, v.v. | [ĐỀ] CRS 5 |
| FR-31 | Tìm **khách sạn, nhà hàng, resort** theo các tiêu chí: | [ĐỀ] CRS 5 |
| | • **Địa điểm** (tỉnh/thành) | |
| | • **Giá** (khoảng giá từ–đến) | |
| | • **Chất lượng** (số sao hoặc điểm đánh giá) | |
| | • **Số lượng** (số phòng/chỗ cần, số người) | |
| | • **Tình trạng còn chỗ** | |
| FR-32 | Trang Search có **link sang trang Advanced Search** | [ĐỀ] CRS 5 |
| FR-33 | Advanced Search kết hợp nhiều tiêu chí cùng lúc, tìm được trên nhiều loại dịch vụ | [ĐỀ] CRS 5 (đề chỉ nêu tên trang) |
| FR-34 | Kết quả có thể sắp xếp theo giá hoặc chất lượng, có phân trang | [ĐỀ XUẤT] |
| FR-35 | Khi không có kết quả thì báo rõ và gợi ý nới lỏng tiêu chí | [ĐỀ XUẤT] |
| FR-36 | Bấm vào một kết quả thì mở trang chi tiết | [ĐỀ XUẤT] |

### 5.5. Information Page

| ID | Yêu cầu | Nguồn |
|---|---|---|
| FR-40 | Trang Information có **5 link**: Tourist Spots, Travel Information, Hotel Information, Restaurant Information, Resorts Information | [ĐỀ] CRS 3, 6 |
| FR-41 | Hiển thị thông tin **khách sạn, nhà hàng, resort mới** | [ĐỀ] CRS 6 |
| FR-42 | Hiển thị **gói tour hoặc khuyến mãi** công ty đang có, nếu có | [ĐỀ] CRS 6 |
| FR-43 | **Tourist Spots**: các điểm du lịch trong nước (tên, loại, địa điểm, mô tả, hình ảnh) | [ĐỀ] CRS 7 |
| FR-44 | **Travel Information**: các phương tiện di chuyển trong nước (loại xe, tuyến đi–đến, giá vé, lịch chạy) | [ĐỀ] CRS 8 |
| FR-45 | **Hotel Information**: danh sách khách sạn trong nước (tên, địa điểm, số sao, giá, phòng trống) | [ĐỀ] CRS 9 |
| FR-46 | **Restaurant Information**: danh sách nhà hàng trong nước (tên, địa điểm, loại món, mức giá, đánh giá) | [ĐỀ] CRS 9 |
| FR-47 | **Resorts Information**: danh sách resort trong nước (tên, địa điểm, số sao, giá, tình trạng còn chỗ) | [ĐỀ] CRS 9 |
| FR-48 | Mỗi mục có trang chi tiết riêng | [ĐỀ XUẤT] |

### 5.6. Contact Us

| ID | Yêu cầu | Nguồn |
|---|---|---|
| FR-50 | Khách liên hệ với công ty để **đặt dịch vụ** (place an order) | [ĐỀ] CRS 10 |
| FR-51 | Form đặt dịch vụ gồm: họ tên, email, số điện thoại, loại dịch vụ, dịch vụ cụ thể, ngày đi, số người, ghi chú | [ĐỀ XUẤT] (chi tiết hóa CRS 10) |
| FR-52 | Có form **Feedback** thu thập góp ý từ người dùng website | [ĐỀ] CRS 10 |
| FR-53 | Form Feedback gồm: họ tên, email, điểm đánh giá (1–5), nội dung | [ĐỀ XUẤT] |
| FR-54 | Kiểm tra dữ liệu nhập: không bỏ trống trường bắt buộc, email và số điện thoại đúng định dạng, ngày đi không ở quá khứ | [ĐỀ XUẤT] |
| FR-55 | Gửi xong thì báo thành công và lưu vào CSDL | [ĐỀ XUẤT] |
| FR-56 | Hiển thị địa chỉ, số điện thoại, email của công ty | [ĐỀ XUẤT] |

### 5.7. Quản trị [ĐỀ XUẤT]

| ID | Yêu cầu |
|---|---|
| FR-60 | Đăng nhập dành cho quản trị viên |
| FR-61 | Thêm/sửa/xóa: điểm du lịch, phương tiện, khách sạn, nhà hàng, resort, gói tour |
| FR-62 | Xem danh sách đơn đặt và cập nhật trạng thái (Mới → Đã liên hệ → Hoàn tất / Hủy) |
| FR-63 | Xem danh sách góp ý |

> Đề không yêu cầu trang quản trị. Tuy vậy, có trang này thì dữ liệu "mới" và "khuyến mãi" ở FR-41/42 mới cập nhật được, và tài liệu nộp cũng phải có "login credentials".

---

## 6. Yêu cầu phi chức năng

| ID | Yêu cầu | Nguồn |
|---|---|---|
| NFR-01 | **Mọi đoạn code đều có comment** | [ĐỀ] Standards 1 |
| NFR-02 | Logic chương trình được giải thích, tài liệu được duy trì đầy đủ | [ĐỀ] Standards 2 |
| NFR-03 | Dùng công nghệ trong danh sách đề cho phép: HTML/JavaScript, Java (JSP/Servlets/EJB/Struts/JDBC) hoặc .NET; CSDL SQL Server, Oracle hoặc MS Access | [ĐỀ] Software |
| NFR-04 | Backup dữ liệu mỗi ngày | [ĐỀ] Note |
| NFR-05 | Giao diện thống nhất theo GUI Standards Document | [ĐỀ] Documentation |
| NFR-06 | Hiển thị tốt trên máy tính và điện thoại (responsive) | [ĐỀ XUẤT] |
| NFR-07 | Mỗi trang tải xong trong khoảng 3 giây ở môi trường demo | [ĐỀ XUẤT] |
| NFR-08 | Chống SQL Injection (dùng tham số hóa truy vấn), mã hóa mật khẩu admin | [ĐỀ XUẤT] |

---

## 7. Dữ liệu

| Thực thể | Thuộc tính chính |
|---|---|
| Location | Id, City, Region |
| TouristSpot | Id, LocationId, Name, Category (beach/monument/…), Description, ImageUrl |
| Transport | Id, FromLocationId, ToLocationId, Type (bus/train/car/…), Fare, Schedule |
| Hotel | Id, LocationId, Name, Stars, PricePerNight, RoomsAvailable, IsNew |
| Restaurant | Id, LocationId, Name, Cuisine, PriceLevel, Rating, IsNew |
| Resort | Id, LocationId, Name, Stars, Price, Available, IsNew |
| Package | Id, Title, Description, Price, DiscountPercent, ValidUntil |
| Customer | Id, FullName, Email, Phone |
| Booking | Id, CustomerId, ServiceType, ServiceId, TravelDate, People, Note, Status, CreatedAt |
| Feedback | Id, CustomerId, Rating, Message, CreatedAt |
| Admin [ĐỀ XUẤT] | Id, Username, PasswordHash |

**Dữ liệu mẫu tối thiểu:** khoảng 10 điểm du lịch trải trên ít nhất 5 tỉnh/thành; khách sạn, nhà hàng, resort mỗi loại ít nhất 8 mục; ít nhất 10 tuyến di chuyển; ít nhất 3 gói tour.

---

## 8. Tiêu chí nghiệm thu

- [ ] Gõ `www.karneltravelguide.com` mở được trang chủ
- [ ] Menu có đủ 5 mục, mọi link đều chạy, không có trang lỗi 404
- [ ] About Us nêu đủ dịch vụ di chuyển, gói tour, chỗ ở
- [ ] Search lọc điểm du lịch theo loại ra đúng kết quả
- [ ] Search lọc khách sạn/nhà hàng/resort đúng theo cả 5 tiêu chí
- [ ] Từ trang Search vào được Advanced Search
- [ ] Information có 5 link, mỗi trang hiển thị dữ liệu lấy từ CSDL
- [ ] Information hiển thị cơ sở mới và gói khuyến mãi
- [ ] Đặt dịch vụ ở Contact Us lưu được vào bảng Booking
- [ ] Gửi Feedback lưu được vào bảng Feedback
- [ ] Nhập sai dữ liệu thì form báo lỗi rõ ràng
- [ ] Mọi file code đều có comment

---

## 9. Sản phẩm phải nộp

1. **2 status mail** (tiêu đề `STATUS:`): mail #1 khoảng ngày 10, mail #2 khoảng ngày 20. Dự án dưới 30 ngày thì mail #1 vào ngày 7–10, mail #2 trước hạn chót 3 ngày.
2. **Project Report** gồm: Certificate of Completion, Table of Contents, Problem Definition, Customer Requirement Specification, Project Plan, E-R Diagrams, Algorithms, GUI Standards Document, Interface Design Document, Task Sheet, Project Review and Monitoring Report, Unit Testing Check List, Final Check List.
3. **File ZIP duy nhất** đặt tên theo Batch_Group_TênDựÁn, gồm source code, tài liệu (có thông tin đăng nhập), file backup CSDL, Feedback Form. Gửi với tiêu đề `PROJECT SUBMISSION:`. **Chỉ được nộp 1 lần.**

## 10. Giả định & câu hỏi cần làm rõ

| # | Nội dung | Trạng thái |
|---|---|---|
| Q1 | Đề có yêu cầu đặt dịch vụ online, nhưng đơn đặt chỉ cần lưu lại (không thanh toán) đúng không? | Giả định: đúng |
| Q2 | Có cần tài khoản cho khách hàng không? | Giả định: không, khách đặt mà không cần đăng nhập |
| Q3 | Công nghệ sử dụng | Đã chốt: ASP.NET Core MVC + SQL Server (EF Core). Hỏi lại giảng viên để chắc chắn được chấp nhận |
| Q4 | Thời lượng chính xác của dự án? | Tạm tính 30 ngày (29/10 → 27/11) |
