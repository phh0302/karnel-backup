# Karnel Travel Guide – Checklist eProject

> **Ngày bắt đầu:** 29/10/2026 · **Thời lượng:** 30 ngày · **Hạn nộp:** 27/11/2026
> *(Nếu lịch thực tế khác thì sửa lại các ngày ở đây)*
>
> Cách tick: đổi `- [ ]` thành `- [x]`. Trên GitHub hoặc VS Code (bật Markdown Preview) có thể bấm thẳng vào ô.

**Thành viên nhóm:** _______________________ · **Batch:** _______ · **Group:** _______

---

## Mốc quan trọng

- [ ] **Status mail #1** – ngày 10 (07/11) – tiêu đề `STATUS:`
- [ ] **Status mail #2** – ngày 20 (17/11) – tiêu đề `STATUS:`
- [ ] **Nộp bài cuối** – ngày 30 (27/11) – tiêu đề `PROJECT SUBMISSION:` – ⚠️ **chỉ được nộp 1 lần**

> Dự án dưới 30 ngày: mail #1 gửi vào khoảng ngày 7–10, mail #2 gửi trước hạn chót 3 ngày.

---

## Giai đoạn 1 · Phân tích & lập kế hoạch (ngày 1–3 · 29/10 → 31/10)

- [ ] Cả nhóm đọc đề, gạch ra từng yêu cầu (URL, menu, 5 trang Information, Search, Contact)
- [ ] Viết **Problem Definition**
- [ ] Viết **Customer Requirement Specification**
- [x] Chọn công nghệ: **ASP.NET Core MVC + SQL Server** (Entity Framework Core)
- [ ] Tạo repo Git, thống nhất cách đặt tên và cấu trúc thư mục
- [ ] Lập **Project Plan**
- [ ] Lập **Task Sheet**: chia việc, giao người phụ trách, đặt hạn cho từng việc

## Giai đoạn 2 · Thiết kế (ngày 4–8 · 01/11 → 05/11)

- [ ] Vẽ **ERD** (Location, TouristSpot, Hotel, Restaurant, Resort, Transport, Package, Customer, Booking, Feedback)
- [ ] Chuẩn hóa bảng, chốt kiểu dữ liệu và khóa
- [ ] Viết script SQL tạo bảng (`CREATE TABLE`)
- [ ] Viết script dữ liệu mẫu (`INSERT`): khoảng 10 điểm du lịch, mỗi loại khách sạn/nhà hàng/resort ít nhất 8 mục
- [ ] Chốt sitemap và menu
- [ ] Vẽ wireframe cho từng trang
- [ ] Viết **GUI Standards Document**: màu, font, nút, form, cách báo lỗi
- [ ] Viết **Interface Design Document**
- [ ] Viết **Algorithms** cho tìm kiếm và đặt dịch vụ

## Giai đoạn 3 · Code giai đoạn 1: khung & thông tin (ngày 9–16 · 06/11 → 13/11)

- [ ] Khởi tạo project, kết nối CSDL
- [ ] Làm layout chung: header, **menu**, footer
- [ ] **Home Page**: banner, điểm đến nổi bật, gói tour đang khuyến mãi
- [ ] **About Us**: giới thiệu công ty, các dịch vụ di chuyển, tour trọn gói, chỗ ở
- [ ] **Information Page**: trang chính, có link tới 5 mục
  - [ ] Tourist Spots
  - [ ] Travel Information
  - [ ] Hotel Information
  - [ ] Restaurant Information
  - [ ] Resorts Information
- [ ] Trên Information Page hiển thị khách sạn/nhà hàng/resort **mới** và **khuyến mãi**
- [ ] Cấu hình file `hosts` để chạy được tại `www.karneltravelguide.com`
- [ ] 📧 **Gửi Status mail #1** (ZIP gồm code, script CSDL, ảnh chụp màn hình; báo tiến độ, việc đã xong, khó khăn)

## Giai đoạn 4 · Code giai đoạn 2: tìm kiếm & liên hệ (ngày 17–23 · 14/11 → 20/11)

- [ ] **Search**: tìm điểm du lịch theo loại (biển, di tích…)
- [ ] **Search**: tìm khách sạn/nhà hàng/resort theo **địa điểm, giá, chất lượng, số lượng, tình trạng còn chỗ**
- [ ] Đặt link sang trang Advanced Search ngay trên trang Search
- [ ] **Advanced Search**: kết hợp nhiều tiêu chí, sắp xếp kết quả, phân trang
- [ ] **Contact Us**: form đặt dịch vụ (lưu vào bảng Booking), có kiểm tra dữ liệu nhập
- [ ] Form **Feedback** lưu vào CSDL
- [ ] *(Khuyến nghị)* Trang **Admin** có đăng nhập, để thêm/sửa/xóa dữ liệu
- [ ] 📧 **Gửi Status mail #2** (ngày 20; cập nhật thêm phần Search và Contact)

## Giai đoạn 5 · Kiểm thử & hoàn thiện (ngày 24–27 · 21/11 → 24/11)

- [ ] Lập **Unit Testing Check List**
- [ ] Test từng trang, từng link trong menu
- [ ] Test từng form (Search, Advanced Search, Contact, Feedback)
- [ ] Test các trường hợp biên: ô tìm kiếm để trống, nhập sai định dạng, không có kết quả
- [ ] Kiểm tra giao diện trên điện thoại và nhiều trình duyệt
- [ ] Sửa lỗi
- [ ] Rà lại: **mọi đoạn code đều có comment**
- [ ] Viết **Project Review and Monitoring Report**

## Giai đoạn 6 · Tài liệu & nộp bài (ngày 28–30 · 25/11 → 27/11)

### Project Report (ghép theo đúng thứ tự)
- [ ] Certificate of Completion
- [ ] Table of Contents
- [ ] Problem Definition
- [ ] Customer Requirement Specification
- [ ] Project Plan
- [ ] E-R Diagrams
- [ ] Algorithms
- [ ] GUI Standards Document
- [ ] Interface Design Document
- [ ] Task Sheet
- [ ] Project Review and Monitoring Report
- [ ] Unit Testing Check List
- [ ] Final Check List

### Chuẩn bị file ZIP
- [ ] Ghi thông tin đăng nhập (admin, tài khoản CSDL) và hướng dẫn cài đặt, chạy project
- [ ] Backup CSDL ra file `.bak`
- [ ] Xuất lại script `.sql`
- [ ] Chụp ảnh màn hình các trang
- [ ] Điền **Feedback Form** của eProjects
- [ ] Nén ZIP, đặt tên `Batch_Group_KarnelTravelGuide.zip`
- [ ] **Giải nén thử trên máy khác và chạy lại** để chắc chắn đủ file
- [ ] 📤 **Gửi bài** với tiêu đề `PROJECT SUBMISSION:` (chỉ nộp **1 lần**)

```
Batch_Group_KarnelTravelGuide.zip
├─ SourceCode/
├─ Database/
│  ├─ KarnelTravel.bak
│  └─ script.sql
├─ Documentation/
│  ├─ ProjectReport.pdf
│  └─ LoginCredentials.txt
├─ Screenshots/
└─ FeedbackForm.doc
```

---

## Thói quen hằng ngày
- Backup dữ liệu mỗi ngày (để phòng virus, hỏng máy, hỏng file)
- Commit và push code lên Git sau mỗi buổi làm
- Có thắc mắc thì gửi email với tiêu đề `DOUBT:`
