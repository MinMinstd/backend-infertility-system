DROP DATABASE InfertilitySystem
CREATE DATABASE InfertilitySystem
USE InfertilitySystem
-- Customer table
CREATE TABLE Customer (
  Customer_ID INT PRIMARY KEY,
  Fullname NVARCHAR(100),
  Email NVARCHAR(100),
  Phone NVARCHAR(20),
  Gender NVARCHAR(10),
  Birthday DATE,
  Address NVARCHAR(255)
);

INSERT INTO Customer VALUES (1, N'Nguyen Van A', 'a@gmail.com', '0900000001', N'Nam', '1990-01-01', N'Hanoi');
INSERT INTO Customer VALUES (2, N'Tran Thi B', 'b@gmail.com', '0900000002', N'Nữ', '1992-02-02', N'HCMC');
INSERT INTO Customer VALUES (3, N'Le Van C', 'c@gmail.com', '0900000003', N'Nam', '1988-03-03', N'Danang');
INSERT INTO Customer VALUES (4, N'Pham Thi D', 'd@gmail.com', '0900000004', N'Nữ', '1995-04-04', N'Hue');
INSERT INTO Customer VALUES (5, N'Do Van E', 'e@gmail.com', '0900000005', N'Nam', '1993-05-05', N'Can Tho');

-- Manager table
CREATE TABLE Manager (
  Manager_ID INT PRIMARY KEY,
  Fullname NVARCHAR(100),
  Email NVARCHAR(100),
  Phone NVARCHAR(20),
  Address NVARCHAR(255)
);



INSERT INTO Manager VALUES (1, N'Manager 1', 'manager1@gmail.com', '0911111111', N'Address 1');
INSERT INTO Manager VALUES (2, N'Manager 2', 'manager2@gmail.com', '0911111112', N'Address 2');
INSERT INTO Manager VALUES (3, N'Manager 3', 'manager3@gmail.com', '0911111113', N'Address 3');
INSERT INTO Manager VALUES (4, N'Manager 4', 'manager4@gmail.com', '0911111114', N'Address 4');
INSERT INTO Manager VALUES (5, N'Manager 5', 'manager5@gmail.com', '0911111115', N'Address 5');

-- Doctor table
CREATE TABLE Doctor (
  Doctor_ID INT PRIMARY KEY,
  Fullname NVARCHAR(100),
  Email NVARCHAR(100),
  Phone NVARCHAR(20),
  Experience INT
);


INSERT INTO Doctor VALUES (1, N'Doctor 1', 'doctor1@hospital.com', '0922222221', 6);
INSERT INTO Doctor VALUES (2, N'Doctor 2', 'doctor2@hospital.com', '0922222222', 7);
INSERT INTO Doctor VALUES (3, N'Doctor 3', 'doctor3@hospital.com', '0922222223', 8);
INSERT INTO Doctor VALUES (4, N'Doctor 4', 'doctor4@hospital.com', '0922222224', 9);
INSERT INTO Doctor VALUES (5, N'Doctor 5', 'doctor5@hospital.com', '0922222225', 10);

-- Doctor_Degree table
CREATE TABLE Doctor_Degree (
  Degree_ID INT PRIMARY KEY,
  Doctor_ID INT,
  Degree_Name NVARCHAR(100),
  Graduation_year INT,
  FOREIGN KEY (Doctor_ID) REFERENCES Doctor(Doctor_ID)
);



INSERT INTO Doctor_Degree VALUES (1, 1, N'Bác sĩ CK1', 2001);
INSERT INTO Doctor_Degree VALUES (2, 2, N'Bác sĩ CK2', 2002);
INSERT INTO Doctor_Degree VALUES (3, 3, N'Bác sĩ CK3', 2003);
INSERT INTO Doctor_Degree VALUES (4, 4, N'Bác sĩ CK4', 2004);
INSERT INTO Doctor_Degree VALUES (5, 5, N'Bác sĩ CK5', 2005);

-- Doctor_schedule table
CREATE TABLE Doctor_schedule (
  Schedule_ID INT PRIMARY KEY,
  Doctor_ID INT,
  Manager_ID INT,
  Work_date DATE,
  StartTime TIME,
  EndTime TIME,
  Status VARCHAR(50),
  FOREIGN KEY (Doctor_ID) REFERENCES Doctor(Doctor_ID),
  FOREIGN KEY (Manager_ID) REFERENCES Manager(Manager_ID)
);


INSERT INTO Doctor_schedule VALUES (1, 1, 1, '2025-06-01', '08:00', '17:00', 'Available');
INSERT INTO Doctor_schedule VALUES (2, 2, 2, '2025-06-02', '08:00', '17:00', 'Available');
INSERT INTO Doctor_schedule VALUES (3, 3, 3, '2025-06-03', '08:00', '17:00', 'Available');
INSERT INTO Doctor_schedule VALUES (4, 4, 4, '2025-06-04', '08:00', '17:00', 'Available');
INSERT INTO Doctor_schedule VALUES (5, 5, 5, '2025-06-05', '08:00', '17:00', 'Available');

-- Consultation_registration table
CREATE TABLE Consultation_registration (
  Consultation_ID INT PRIMARY KEY,
  Date DATE,
  Status NVARCHAR(50),
  Result_value NVARCHAR(100),
  Note NVARCHAR(255)
);


INSERT INTO Consultation_registration VALUES (1, '2025-06-01', N'Đã khám', N'Bình thường', N'Không vấn đề');
INSERT INTO Consultation_registration VALUES (2, '2025-06-02', N'Đã khám', N'Cần theo dõi', N'Khuyến cáo tái khám');
INSERT INTO Consultation_registration VALUES (3, '2025-06-03', N'Đã khám', N'Bình thường', N'Ổn định');
INSERT INTO Consultation_registration VALUES (4, '2025-06-04', N'Đang chờ', NULL, NULL);
INSERT INTO Consultation_registration VALUES (5, '2025-06-05', N'Đã khám', N'Cần điều trị', N'Bắt đầu lộ trình');

-- Consultation_result table
CREATE TABLE Consultation_result (
  Result_ID INT PRIMARY KEY,
  Consultation_ID INT,
  Date DATE,
  Result_value NVARCHAR(100),
  Note NVARCHAR(255),
  FOREIGN KEY (Consultation_ID) REFERENCES Consultation_registration(Consultation_ID)
);


INSERT INTO Consultation_result VALUES (1, 1, '2025-06-01', N'Ổn định', N'Không cần can thiệp');
INSERT INTO Consultation_result VALUES (2, 2, '2025-06-02', N'Theo dõi thêm', N'Xem lại sau 1 tuần');
INSERT INTO Consultation_result VALUES (3, 3, '2025-06-03', N'Tốt', N'Có thể điều trị');
INSERT INTO Consultation_result VALUES (4, 4, '2025-06-04', N'Đợi xét nghiệm', NULL);
INSERT INTO Consultation_result VALUES (5, 5, '2025-06-05', N'Bắt đầu lộ trình', N'Theo IVF');

-- Booking table
CREATE TABLE Booking (
  Book_ID INT PRIMARY KEY,
  Customer_ID INT,
  Schedule_ID INT,
  Consultation_ID INT,
  Type VARCHAR(50),
  Date DATE,
  Time TIME,
  Note NVARCHAR(255),
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID),
  FOREIGN KEY (Schedule_ID) REFERENCES Doctor_schedule(Schedule_ID),
  FOREIGN KEY (Consultation_ID) REFERENCES Consultation_registration(Consultation_ID)
);


INSERT INTO Booking VALUES (1, 1, 1, 1, 'IVF', '2025-06-01', '08:00', N'Lịch tư vấn IVF');
INSERT INTO Booking VALUES (2, 2, 2, 2, 'IUI', '2025-06-02', '09:00', N'Lịch tư vấn IUI');
INSERT INTO Booking VALUES (3, 3, 3, 3, 'IVF', '2025-06-03', '10:00', N'Kiểm tra kết quả');
INSERT INTO Booking VALUES (4, 4, 4, 4, 'IUI', '2025-06-04', '11:00', N'Lịch khám theo dõi');
INSERT INTO Booking VALUES (5, 5, 5, 5, 'IVF', '2025-06-05', '14:00', N'Bắt đầu điều trị');

-- Service table
CREATE TABLE Service (
  Service_ID INT PRIMARY KEY,
  Manager_ID INT,
  Type NVARCHAR(50),
  Name NVARCHAR(100),
  Description NVARCHAR(255),
  Price DECIMAL(10,2),
  FOREIGN KEY (Manager_ID) REFERENCES Manager(Manager_ID)
);


INSERT INTO Service VALUES (1, 1, 'IVF', N'Dịch vụ IVF cơ bản', N'Gói điều trị IVF cơ bản', 30000000);
INSERT INTO Service VALUES (2, 2, 'IUI', N'Dịch vụ IUI tiêu chuẩn', N'Gói điều trị IUI tiêu chuẩn', 10000000);

-- Treatment_roadmap table
CREATE TABLE Treatment_roadmap (
  Road_ID INT PRIMARY KEY,
  Service_ID INT,
  Date DATE,
  Stage NVARCHAR(50),
  Description NVARCHAR(255),
  Duration_day INT,
  Price DECIMAL(10,2),
  FOREIGN KEY (Service_ID) REFERENCES Service(Service_ID)
);

INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (1, 1, '2025-06-03', N'Kích thích buồng trứng', N'Mô tả bước Kích thích buồng trứng', 1, 808.38);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (2, 1, '2025-06-06', N'Chọc hút trứng', N'Mô tả bước Chọc hút trứng', 3, 4364.78);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (3, 1, '2025-06-09', N'Chọn lọc tinh trùng', N'Mô tả bước Chọn lọc tinh trùng', 5, 3831.91);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (4, 1, '2025-06-12', N'Tạo phôi', N'Mô tả bước Tạo phôi', 2, 646.21);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (5, 1, '2025-06-15', N'Chuyển phôi', N'Mô tả bước Chuyển phôi', 4, 2421.77);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (6, 1, '2025-06-16', N'Thử thai', N'Mô tả bước Thử thai', 1, 4373.81);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (7, 2, '2025-06-01', N'Kiểm tra sức khỏe sinh sản', N'Mô tả bước Kiểm tra sức khỏe sinh sản', 3, 2987.92);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (8, 2, '2025-06-05', N'Kích thích buồng trứng', N'Mô tả bước Kích thích buồng trứng', 3, 2850.3);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (9, 2, '2025-06-07', N'Chọn lọc tinh trùng', N'Mô tả bước Chọn lọc tinh trùng', 3, 4585.54);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (10, 2, '2025-06-10', N'Bơm tinh trùng vào buồng tử cung', N'Mô tả bước Bơm tinh trùng vào buồng tử cung', 1, 4486.73);
INSERT INTO Treatment_roadmap (Road_ID, Service_ID, Date, Stage, Description, Duration_day, Price) VALUES (11, 2, '2025-06-14', N'Thử thai', N'Mô tả bước Thử thai', 5, 3388.51);


-- Treatment_result table
CREATE TABLE Treatment_result (
  Treatment_result_ID INT PRIMARY KEY,
  Road_ID INT,
  Type VARCHAR(50),
  Date DATE,
  Description NVARCHAR(255),
  Result NVARCHAR(100),
  FOREIGN KEY (Road_ID) REFERENCES Treatment_roadmap(Road_ID)
);


INSERT INTO Treatment_result VALUES (1, 1, 'IVF', '2025-06-01', N'Bắt đầu tốt', N'Ổn định');
INSERT INTO Treatment_result VALUES (2, 2, 'IUI', '2025-06-02', N'Theo dõi sát', N'Tốt');
INSERT INTO Treatment_result VALUES (3, 3, 'IVF', '2025-06-03', N'Trứng phát triển', N'Khả quan');
INSERT INTO Treatment_result VALUES (4, 4, 'IUI', '2025-06-04', N'Chuẩn bị tốt', N'Đủ điều kiện');
INSERT INTO Treatment_result VALUES (5, 5, 'IVF', '2025-06-05', N'Thành công', N'Thành công bước 1');

-- Type_test table
CREATE TABLE Type_test (
  Test_ID INT PRIMARY KEY,
  Treatment_result_ID INT,
  Result_ID INT,
  Name NVARCHAR(100),
  Description NVARCHAR(255),
  FOREIGN KEY (Treatment_result_ID) REFERENCES Treatment_result(Treatment_result_ID),
  FOREIGN KEY (Result_ID) REFERENCES Consultation_result(Result_ID)
);


INSERT INTO Type_test VALUES (1, 1, 1, N'Xét nghiệm máu', N'Kiểm tra nội tiết');
INSERT INTO Type_test VALUES (2, 2, 2, N'Siêu âm', N'Theo dõi nang trứng');
INSERT INTO Type_test VALUES (3, 3, 3, N'Xét nghiệm nội tiết', N'AMH, FSH, LH');
INSERT INTO Type_test VALUES (4, 4, 4, N'Kiểm tra nội soi', N'Tình trạng tử cung');
INSERT INTO Type_test VALUES (5, 5, 5, N'Xét nghiệm tinh trùng', N'Đánh giá chất lượng');

-- Prescription table
CREATE TABLE Prescription (
  Prescription_ID INT PRIMARY KEY,
  Treatment_result_ID INT,
  Date DATE,
  Name NVARCHAR(100),
  Note NVARCHAR(255),
  FOREIGN KEY (Treatment_result_ID) REFERENCES Treatment_result(Treatment_result_ID)
);


INSERT INTO Prescription VALUES (1, 1, '2025-06-01', N'Thuốc kích trứng', N'Uống hàng ngày');
INSERT INTO Prescription VALUES (2, 2, '2025-06-02', N'Thuốc hỗ trợ IUI', N'Theo chỉ định');
INSERT INTO Prescription VALUES (3, 3, '2025-06-03', N'Thuốc nội tiết', N'Sáng – Tối');
INSERT INTO Prescription VALUES (4, 4, '2025-06-04', N'Hỗ trợ tử cung', N'Đúng giờ');
INSERT INTO Prescription VALUES (5, 5, '2025-06-05', N'Thuốc sau chọc hút', N'Kết hợp ăn uống');

-- Prescription_detail table
CREATE TABLE Prescription_detail (
  ID INT PRIMARY KEY,
  Prescription_ID INT,
  Medicine_name VARCHAR(100),
  Dosage VARCHAR(50),
  Duration_days INT,
  Instruction NVARCHAR(255),
  FOREIGN KEY (Prescription_ID) REFERENCES Prescription(Prescription_ID)
);

INSERT INTO Prescription_detail VALUES (1, 1, 'Clomiphene', '50mg', 5, N'Uống trước ăn sáng');
INSERT INTO Prescription_detail VALUES (2, 2, 'Letrozole', '2.5mg', 5, N'Uống 1 viên mỗi tối');
INSERT INTO Prescription_detail VALUES (3, 3, 'Progesterone', '100mg', 10, N'Uống sau bữa tối');
INSERT INTO Prescription_detail VALUES (4, 4, 'Estrogen', '1mg', 7, N'Buổi sáng và tối');
INSERT INTO Prescription_detail VALUES (5, 5, 'Duphaston', '10mg', 10, N'Sau ăn trưa');

-- Medical_record table
CREATE TABLE Medical_record (
  Record_ID INT PRIMARY KEY,
  Customer_ID INT,
  Doctor_ID INT,
  Road_ID INT,
  Stage VARCHAR(50),
  Diagnosis NVARCHAR(255),
  Status NVARCHAR(50),
  Attempt INT DEFAULT 1,
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID),
  FOREIGN KEY (Doctor_ID) REFERENCES Doctor(Doctor_ID),
  FOREIGN KEY (Road_ID) REFERENCES Treatment_roadmap(Road_ID)
);




-- IVF: Bệnh nhân 1 thử IVF lần đầu, bước "Tạo phôi"
INSERT INTO Medical_record (Record_ID, Customer_ID, Doctor_ID, Road_ID, Stage, Diagnosis, Status, Attempt)
VALUES (1, 1, 1, 4, 'Tạo phôi', N'Đạt 5 phôi tốt', N'Thành công', 1);

-- IVF: Bệnh nhân 1 chuyển phôi nhưng thất bại -> quay lại bước "Tạo phôi", lần điều trị thứ 2
INSERT INTO Medical_record (Record_ID, Customer_ID, Doctor_ID, Road_ID, Stage, Diagnosis, Status, Attempt)
VALUES (2, 1, 1, 4, 'Tạo phôi', N'Tạo lại phôi, chất lượng trung bình', N'Đang điều trị', 2);

-- IUI: Bệnh nhân 2 bơm tinh trùng lần đầu, chưa thành công
INSERT INTO Medical_record (Record_ID, Customer_ID, Doctor_ID, Road_ID, Stage, Diagnosis, Status, Attempt)
VALUES (3, 2, 2, 10, 'Bơm tinh trùng vào buồng tử cung', N'Không thành công sau 14 ngày', N'Thất bại', 1);

-- IUI: Bệnh nhân 2 bơm tinh trùng lần 2
INSERT INTO Medical_record (Record_ID, Customer_ID, Doctor_ID, Road_ID, Stage, Diagnosis, Status, Attempt)
VALUES (4, 2, 2, 10, 'Bơm tinh trùng vào buồng tử cung', N'Triệu chứng khả quan sau 10 ngày', N'Đang theo dõi', 2);

-- Medical_record_detail table
CREATE TABLE Medical_record_detail (
  Detail_ID INT PRIMARY KEY,
  Record_ID INT,
  Treatment_result_ID INT,
  Result_ID INT,
  Date DATE,
  Note NVARCHAR(255),
  Type VARCHAR(50),
  FOREIGN KEY (Record_ID) REFERENCES Medical_record(Record_ID),
  FOREIGN KEY (Treatment_result_ID) REFERENCES Treatment_result(Treatment_result_ID),
  FOREIGN KEY (Result_ID) REFERENCES Consultation_result(Result_ID)
);

-- Ghi nhận kết quả tạo phôi lần đầu cho bệnh nhân IVF (Record_ID = 1)
INSERT INTO Medical_record_detail
(Detail_ID, Record_ID, Treatment_result_ID, Result_ID, Date, Note, Type)
VALUES
(1, 1, 1, 1, '2025-06-01', N'Tạo được 5 phôi loại A', 'IVF');

-- Ghi nhận kết quả thử thai sau chuyển phôi (thất bại), lần 1
INSERT INTO Medical_record_detail
(Detail_ID, Record_ID, Treatment_result_ID, Result_ID, Date, Note, Type)
VALUES
(2, 2, 2, 2, '2025-06-10', N'Không đậu thai sau chuyển phôi', 'IVF');

-- Ghi nhận kết quả kiểm tra trước IUI lần 1
INSERT INTO Medical_record_detail
(Detail_ID, Record_ID, Treatment_result_ID, Result_ID, Date, Note, Type)
VALUES
(3, 3, 3, 3, '2025-06-01', N'Niêm mạc tử cung mỏng, cần theo dõi thêm', 'IUI');

-- Ghi nhận kết quả bơm tinh trùng lần 1 (IUI)
INSERT INTO Medical_record_detail
(Detail_ID, Record_ID, Treatment_result_ID, Result_ID, Date, Note, Type)
VALUES
(4, 3, 4, 3, '2025-06-02', N'Bơm tinh trùng thành công, chờ kết quả', 'IUI');

-- Ghi nhận kết quả bơm tinh trùng lần 2 (IUI - tái điều trị)
INSERT INTO Medical_record_detail
(Detail_ID, Record_ID, Treatment_result_ID, Result_ID, Date, Note, Type)
VALUES
(5, 4, 5, 3, '2025-06-15', N'Lần 2 niêm mạc ổn hơn, tiếp tục theo dõi', 'IUI');


-- Order table
CREATE TABLE [Order] (
  Order_ID INT PRIMARY KEY,
  Customer_ID INT,
  Book_ID INT,
  Manager_ID INT,
  Date DATE,
  Time TIME,
  Status NVARCHAR(50),
  Note NVARCHAR(255),
  Husband NVARCHAR(100),
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID),
  FOREIGN KEY (Book_ID) REFERENCES Booking(Book_ID),
  FOREIGN KEY (Manager_ID) REFERENCES Manager(Manager_ID)
);


INSERT INTO [Order] VALUES (1, 1, 1, 1, '2025-06-01', '09:00', N'Đã đặt', N'Không ghi chú', N'Nguyen Van B');
INSERT INTO [Order] VALUES (2, 2, 2, 2, '2025-06-02', '10:00', N'Đã đặt', N'Yêu cầu bác sĩ nữ', N'Tran Van D');
INSERT INTO [Order] VALUES (3, 3, 3, 3, '2025-06-03', '11:00', N'Chờ xác nhận', NULL, N'Pham Van E');
INSERT INTO [Order] VALUES (4, 4, 4, 4, '2025-06-04', '14:00', N'Đã xác nhận', NULL, N'Do Van F');
INSERT INTO [Order] VALUES (5, 5, 5, 5, '2025-06-05', '15:00', N'Đã hoàn tất', NULL, N'Le Van G');

-- Order_detail table
CREATE TABLE Order_detail (
  Detail_ID INT PRIMARY KEY,
  Order_ID INT,
  Service_ID INT,
  Consultation_ID INT,
  Quantity INT,
  Price DECIMAL(10,2),
  FOREIGN KEY (Order_ID) REFERENCES [Order](Order_ID),
  FOREIGN KEY (Service_ID) REFERENCES Service(Service_ID),
  FOREIGN KEY (Consultation_ID) REFERENCES Consultation_registration(Consultation_ID)
);


INSERT INTO Order_detail VALUES (1, 1, 1, 1, 1, 30000000);
INSERT INTO Order_detail VALUES (2, 2, 2, 2, 1, 10000000);
INSERT INTO Order_detail VALUES (3, 3, 2, 3, 1, 50000000);
INSERT INTO Order_detail VALUES (4, 4, 1, 4, 1, 12000000);
INSERT INTO Order_detail VALUES (5, 5, 1, 5, 1, 60000000);

-- Feedback table
CREATE TABLE Feedback (
  Feedback_ID INT PRIMARY KEY,
  Customer_ID INT,
  Manager_ID INT,
  Service_ID INT,
  Type VARCHAR(50),
  Date DATE,
  Comments NVARCHAR(255),
  Rating INT,
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID),
  FOREIGN KEY (Manager_ID) REFERENCES Manager(Manager_ID),
  FOREIGN KEY (Service_ID) REFERENCES Service(Service_ID)
);


INSERT INTO Feedback VALUES (1, 1, 1, 1, 'IVF', '2025-06-02', N'Dịch vụ tốt', 5);
INSERT INTO Feedback VALUES (2, 2, 2, 2, 'IUI', '2025-06-03', N'Tư vấn nhiệt tình', 4);
INSERT INTO Feedback VALUES (3, 3, 3, 2, 'IVF', '2025-06-04', N'Trải nghiệm ổn', 4);
INSERT INTO Feedback VALUES (4, 4, 4, 1, 'IUI', '2025-06-05', N'Cần cải thiện thời gian chờ', 3);
INSERT INTO Feedback VALUES (5, 5, 5, 1, 'IVF', '2025-06-06', N'Rất hài lòng', 5);

-- Payment table
CREATE TABLE Payment (
  Payment_ID INT PRIMARY KEY,
  Order_ID INT,
  Road_ID INT,
  Price_by_service DECIMAL(10,2),
  Method NVARCHAR(50),
  Date DATE,
  Status VARCHAR(50),
  FOREIGN KEY (Order_ID) REFERENCES [Order](Order_ID),
  FOREIGN KEY (Road_ID) REFERENCES Treatment_roadmap(Road_ID)
);



INSERT INTO Payment VALUES (1, 1, 1, 30000000, N'Chuyển khoản', '2025-06-02', 'Đã thanh toán');
INSERT INTO Payment VALUES (2, 2, 2, 10000000, N'Tiền mặt', '2025-06-03', 'Đã thanh toán');
INSERT INTO Payment VALUES (3, 3, 3, 50000000, N'VNPay', '2025-06-04', 'Chưa thanh toán');
INSERT INTO Payment VALUES (4, 4, 4, 12000000, N'Thẻ tín dụng', '2025-06-05', 'Đã thanh toán');
INSERT INTO Payment VALUES (5, 5, 5, 60000000, N'Chuyển khoản', '2025-06-06', 'Đã thanh toán');

-- Blog_post table
CREATE TABLE Blog_post (
  Blog_ID INT PRIMARY KEY,
  Manager_ID INT,
  Customer_ID INT,
  Title NVARCHAR(255),
  Story NVARCHAR(255),
  TreatmentType NVARCHAR(100),
  Date DATETIME,
  Image NVARCHAR(255),
  FOREIGN KEY (Manager_ID) REFERENCES Manager(Manager_ID),
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID)
);

INSERT INTO Blog_post VALUES (1, 1, 1, N'Hành trình IVF', N'Kể lại quá trình IVF', N'IVF', '2025-06-01 10:00:00', N'ivf1.jpg');
INSERT INTO Blog_post VALUES (2, 2, 2, N'Kinh nghiệm IUI', N'Chia sẻ sau điều trị', N'IUI', '2025-06-02 11:00:00', N'iui1.jpg');
INSERT INTO Blog_post VALUES (3, 3, 3, N'Tư vấn điều trị', N'Bài viết chuyên gia', N'IVF', '2025-06-03 12:00:00', N'ivf2.jpg');
INSERT INTO Blog_post VALUES (4, 4, 4, N'Giải đáp thắc mắc', N'Câu hỏi thường gặp', N'IUI', '2025-06-04 13:00:00', N'iui2.jpg');
INSERT INTO Blog_post VALUES (5, 5, 5, N'Thành công sau IVF', N'Niềm vui làm mẹ', N'IVF', '2025-06-05 14:00:00', N'ivf3.jpg');

-- Embryo table
CREATE TABLE Embryo (
  Embryo_ID INT PRIMARY KEY,
  Customer_ID INT,
  Created_at DATE,
  Quality NVARCHAR(50),
  Type VARCHAR(10),
  Amount INT,
  FOREIGN KEY (Customer_ID) REFERENCES Customer(Customer_ID)
);
SELECT * FROM Embryo


INSERT INTO Embryo VALUES (1, 1, '2025-06-01', N'Tốt', 'D5', 5);
INSERT INTO Embryo VALUES (2, 2, '2025-06-02', N'Trung bình', 'D3', 4);
INSERT INTO Embryo VALUES (3, 3, '2025-06-03', N'Tốt', 'D5', 3);
INSERT INTO Embryo VALUES (4, 4, '2025-06-04', N'Kém', 'D2', 2);
INSERT INTO Embryo VALUES (5, 5, '2025-06-05', N'Tốt', 'D5', 6);
