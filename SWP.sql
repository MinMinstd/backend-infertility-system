DROP DATABASE InfertilitySystem
CREATE DATABASE InfertilitySystem
USE InfertilitySystem

-- Customer table
CREATE TABLE [dbo].[Customers] (
    [CustomerId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [FullName]   NVARCHAR (MAX) NULL,
    [Email]      NVARCHAR (MAX) NULL,
    [Phone]      NVARCHAR (MAX) NULL,
    [Gender]     NVARCHAR (MAX) NOT NULL,
    [Birthday]   DATETIME2 (7)  NOT NULL,
    [Address]    NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

INSERT INTO [dbo].[Customers] ([UserId], [FullName], [Email], [Phone], [Gender], [Birthday], [Address]) VALUES
(1, N'Nguyen Van A', 'a@gmail.com', '0900000001', N'Nam', '1990-01-01', N'Hanoi'),
(2, N'Tran Thi B', 'b@gmail.com', '0900000002', N'Nữ', '1992-02-02', N'HCMC'),
(3, N'Le Van C', 'c@gmail.com', '0900000003', N'Nam', '1988-03-03', N'Danang'),
(4, N'Pham Thi D', 'd@gmail.com', '0900000004', N'Nữ', '1995-04-04', N'Hue'),
(5, N'Do Van E', 'e@gmail.com', '0900000005', N'Nam', '1993-05-05', N'Can Tho');

-- Manager table
CREATE TABLE [dbo].[Managers] (
    [ManagerId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]    INT            NOT NULL,
    [FullName]  NVARCHAR (MAX) NULL,
    [Email]     NVARCHAR (MAX) NULL,
    [Phone]     NVARCHAR (MAX) NULL,
    [Address]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Managers] PRIMARY KEY CLUSTERED ([ManagerId] ASC)
);

INSERT INTO [dbo].[Managers] ([UserId], [FullName], [Email], [Phone], [Address]) VALUES
(1, N'Manager 1', 'manager1@gmail.com', '0911111111', N'Address 1'),
(2, N'Manager 2', 'manager2@gmail.com', '0911111112', N'Address 2'),
(3, N'Manager 3', 'manager3@gmail.com', '0911111113', N'Address 3'),
(4, N'Manager 4', 'manager4@gmail.com', '0911111114', N'Address 4'),
(5, N'Manager 5', 'manager5@gmail.com', '0911111115', N'Address 5');

-- Doctor table
CREATE TABLE [dbo].[Doctors] (
    [DoctorId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [FullName]   NVARCHAR (MAX) NULL,
    [Email]      NVARCHAR (MAX) NULL,
    [Phone]      NVARCHAR (MAX) NULL,
    [Experience] INT            NOT NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED ([DoctorId] ASC)
);

INSERT INTO [dbo].[Doctors] ([UserId], [FullName], [Email], [Phone], [Experience]) VALUES
(1, N'Doctor 1', 'doctor1@hospital.com', '0922222221', 6),
(2, N'Doctor 2', 'doctor2@hospital.com', '0922222222', 7),
(3, N'Doctor 3', 'doctor3@hospital.com', '0922222223', 8),
(4, N'Doctor 4', 'doctor4@hospital.com', '0922222224', 9),
(5, N'Doctor 5', 'doctor5@hospital.com', '0922222225', 10);

-- Doctor_Degree table
CREATE TABLE [dbo].[DoctorDegrees] (
    [DoctorDegreeId] INT            IDENTITY (1, 1) NOT NULL,
    [DegreeName]     NVARCHAR (MAX) NULL,
    [GraduationYear] INT            NOT NULL,
    [DoctorId]       INT            NOT NULL,
    CONSTRAINT [PK_DoctorDegrees] PRIMARY KEY CLUSTERED ([DoctorDegreeId] ASC),
    CONSTRAINT [FK_DoctorDegrees_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([DoctorId]) ON DELETE CASCADE
);

INSERT INTO [DoctorDegrees] ([DegreeName], [GraduationYear], [DoctorId]) VALUES
(N'Bác sĩ CK1', 2001, 1),
(N'Bác sĩ CK2', 2002, 2),
(N'Bác sĩ CK3', 2003, 3),
(N'Bác sĩ CK4', 2004, 4),
(N'Bác sĩ CK5', 2005, 5);


-- Doctor_schedule table
CREATE TABLE [dbo].[DoctorSchedules] (
    [DoctorScheduleId] INT            IDENTITY (1, 1) NOT NULL,
    [WorkDate]         DATE           NOT NULL,
    [StartTime]        TIME (7)       NOT NULL,
    [EndTime]          TIME (7)       NOT NULL,
    [Status]           NVARCHAR (MAX) NULL,
    [DoctorId]         INT            NOT NULL,
    [ManagerId]        INT            NOT NULL,
    CONSTRAINT [PK_DoctorSchedules] PRIMARY KEY CLUSTERED ([DoctorScheduleId] ASC),
    CONSTRAINT [FK_DoctorSchedules_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([DoctorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_DoctorSchedules_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId]) ON DELETE CASCADE
);


INSERT INTO [dbo].[DoctorSchedules] ( [WorkDate], [StartTime], [EndTime], [Status], [DoctorId], [ManagerId]) VALUES
-- cũng có IDENTITY, có thể không ghi DoctorScheduleId
( '2025-06-01', '08:00:00', '17:00:00', N'Available', 1, 1),
( '2025-06-02', '08:00:00', '17:00:00', N'Available', 2, 2),
( '2025-06-03', '08:00:00', '17:00:00', N'Available', 3, 3),
( '2025-06-04', '08:00:00', '17:00:00', N'Available', 4, 4),
( '2025-06-05', '08:00:00', '17:00:00', N'Available', 5, 5);

-- Consultation_registration table
CREATE TABLE [dbo].[ConsulationRegistrations] (
    [ConsulationRegistrationId] INT            IDENTITY (1, 1) NOT NULL,
    [Date]                      DATE           NOT NULL,
    [Status]                    NVARCHAR (MAX) NULL,
    [Type]                      NVARCHAR (MAX) NULL,
    [Note]                      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ConsulationRegistrations] PRIMARY KEY CLUSTERED ([ConsulationRegistrationId] ASC)
);

INSERT INTO [dbo].[ConsulationRegistrations] ([Date], [Status], [Type], [Note]) VALUES
('2025-06-01', N'Đã khám', N'Bình thường', N'Không vấn đề'),
('2025-06-02', N'Đã khám', N'Cần theo dõi', N'Khuyến cáo tái khám'),
('2025-06-03', N'Đã khám', N'Bình thường', N'Ổn định'),
('2025-06-04', N'Đang chờ', NULL, NULL),
('2025-06-05', N'Đã khám', N'Cần điều trị', N'Bắt đầu lộ trình');



-- Consultation_result table
CREATE TABLE [dbo].[ConsulationResults] (
    [ConsulationResultId]       INT            IDENTITY (1, 1) NOT NULL,
    [Date]                      DATE           NOT NULL,
    [ResultValue]               NVARCHAR (MAX) NULL,
    [Note]                      NVARCHAR (MAX) NULL,
    [ConsulationRegistrationId] INT            NOT NULL,
    CONSTRAINT [PK_ConsulationResults] PRIMARY KEY CLUSTERED ([ConsulationResultId] ASC),
    CONSTRAINT [FK_ConsulationResults_ConsulationRegistrations_ConsulationRegistrationId] FOREIGN KEY ([ConsulationRegistrationId]) REFERENCES [dbo].[ConsulationRegistrations] ([ConsulationRegistrationId]) ON DELETE CASCADE
);

INSERT INTO [dbo].[ConsulationResults] ([Date], [ResultValue], [Note], [ConsulationRegistrationId]) VALUES
('2025-06-01', N'Ổn định', N'Không cần can thiệp', 1),
('2025-06-02', N'Theo dõi thêm', N'Xem lại sau 1 tuần', 2),
('2025-06-03', N'Tốt', N'Có thể điều trị', 3),
('2025-06-04', N'Đợi xét nghiệm', NULL, 4),
('2025-06-05', N'Bắt đầu lộ trình', N'Theo IVF', 5);


-- Booking table
CREATE TABLE [dbo].[Bookings] (
    [BookingId]                 INT            IDENTITY (1, 1) NOT NULL,
    [Date]                      DATE           NOT NULL,
    [Time]                      TIME (7)       NOT NULL,
    [Status]                    NVARCHAR (MAX) NULL,
    [Note]                      NVARCHAR (MAX) NULL,
    [CustomerId]                INT            NOT NULL,
    [DoctorScheduleId]          INT            NOT NULL,
    [ConsulationRegistrationId] INT            NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED ([BookingId] ASC),
    CONSTRAINT [FK_Bookings_ConsulationRegistrations_ConsulationRegistrationId] FOREIGN KEY ([ConsulationRegistrationId]) REFERENCES [dbo].[ConsulationRegistrations] ([ConsulationRegistrationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_DoctorSchedules_DoctorScheduleId] FOREIGN KEY ([DoctorScheduleId]) REFERENCES [dbo].[DoctorSchedules] ([DoctorScheduleId]) ON DELETE CASCADE
);

INSERT INTO [Bookings] (CustomerId, DoctorScheduleId, ConsulationRegistrationId, Status, Date, Time, Note)
VALUES 
(1, 1, 1, N'IVF', '2025-06-01', '08:00', N'Lịch tư vấn IVF'),
(2, 2, 2, N'IUI', '2025-06-02', '09:00', N'Lịch tư vấn IUI'),
(3, 3, 3, N'IVF', '2025-06-03', '10:00', N'Kiểm tra kết quả'),
(4, 4, 4, N'IUI', '2025-06-04', '11:00', N'Lịch khám theo dõi'),
(5, 5, 5, N'IVF', '2025-06-05', '14:00', N'Bắt đầu điều trị');


-- Service table
CREATE TABLE [dbo].[Services] (
    [ServiceDBId] INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX)  NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    [ManagerId]   INT             NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([ServiceDBId] ASC),
    CONSTRAINT [FK_Services_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId]) ON DELETE CASCADE
);

INSERT INTO [Services] ([Name], [Description], [Price], [ManagerId])
VALUES 
(N'IVF', N'Gói điều trị IVF cơ bản', 30000000, 1),
(N'IUI', N'Gói điều trị IUI tiêu chuẩn', 10000000, 2);

select * from Services

CREATE TABLE [dbo].[MedicalRecords] (
    [MedicalRecordId] INT            IDENTITY (1, 1) NOT NULL,
    [StartDate]       DATE           NOT NULL,
    [EndDate]         DATE           NOT NULL,
    [Stage]           NVARCHAR (MAX) NULL,
    [Diagnosis]       NVARCHAR (MAX) NULL,
    [Status]          NVARCHAR (MAX) NULL,
    [Attempt]         INT            NOT NULL,
    [CustomerId]      INT            NOT NULL,
    [DoctorId]        INT            NOT NULL,
    CONSTRAINT [PK_MedicalRecords] PRIMARY KEY CLUSTERED ([MedicalRecordId] ASC),
    CONSTRAINT [FK_MedicalRecords_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MedicalRecords_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([DoctorId]) ON DELETE CASCADE
);


-- Thêm 3 hồ sơ bệnh án vào bảng MedicalRecords
INSERT INTO [dbo].[MedicalRecords] 
([StartDate], [EndDate], [Stage], [Diagnosis], [Status], [Attempt], [CustomerId], [DoctorId])
VALUES
-- Khách hàng 1 - IVF, thành công
('2025-06-01', '2025-06-20', N'Đã thử thai', N'Hiếm muộn do buồng trứng', N'Thành công', 1, 1, 1),

-- Khách hàng 2 - IVF lần 2, đang điều trị
('2025-05-15', '2025-06-10', N'Chuyển phôi', N'Hiếm muộn không rõ nguyên nhân', N'Đang điều trị', 2, 2, 2),

-- Khách hàng 3 - IUI, thành công
('2025-06-01', '2025-06-18', N'Thử thai', N'Tinh trùng yếu', N'Thành công', 1, 3, 2);


-- Treatment_roadmap table
INSERT INTO [TreatmentRoadmaps] ([ServiceId], [Date], [Stage], [Description], [DurationDay], [Price])
VALUES
-- Quy trình IVF (ServiceId = 1)
(1, '2025-06-03', N'Kích thích buồng trứng', N'Mô tả bước Kích thích buồng trứng', 1, 808.38),
(1, '2025-06-06', N'Chọc hút trứng', N'Mô tả bước Chọc hút trứng', 3, 4364.78),
(1, '2025-06-09', N'Chọn lọc tinh trùng', N'Mô tả bước Chọn lọc tinh trùng', 5, 3831.91),
(1, '2025-06-12', N'Tạo phôi', N'Mô tả bước Tạo phôi', 2, 646.21),
(1, '2025-06-15', N'Chuyển phôi', N'Mô tả bước Chuyển phôi', 4, 2421.77),
(1, '2025-06-16', N'Thử thai', N'Mô tả bước Thử thai', 1, 4373.81),

-- Quy trình IUI (ServiceId = 2)
(2, '2025-06-01', N'Kiểm tra sức khỏe sinh sản', N'Mô tả bước Kiểm tra sức khỏe sinh sản', 3, 2987.92),
(2, '2025-06-05', N'Kích thích buồng trứng', N'Mô tả bước Kích thích buồng trứng', 3, 2850.30),
(2, '2025-06-07', N'Chọn lọc tinh trùng', N'Mô tả bước Chọn lọc tinh trùng', 3, 4585.54),
(2, '2025-06-10', N'Bơm tinh trùng vào buồng tử cung', N'Mô tả bước Bơm tinh trùng vào buồng tử cung', 1, 4486.73),
(2, '2025-06-14', N'Thử thai', N'Mô tả bước Thử thai', 5, 3388.51);


-- Treatment_result table
CREATE TABLE [dbo].[TreatmentResults] (
    [TreatmentResultId]  INT             IDENTITY (1, 1) NOT NULL,
    [Date]               DATE            NOT NULL,
    [Stage]              NVARCHAR (MAX)  NULL,
    [Description]        NVARCHAR (MAX)  NULL,
    [DurationDay]        INT             NOT NULL,
    [Price]              DECIMAL (18, 2) NOT NULL,
    [TreatmentRoadmapId] INT             NOT NULL,
    CONSTRAINT [PK_TreatmentResults] PRIMARY KEY CLUSTERED ([TreatmentResultId] ASC),
    CONSTRAINT [FK_TreatmentResults_TreatmentRoadmaps_TreatmentRoadmapId] FOREIGN KEY ([TreatmentRoadmapId]) REFERENCES [dbo].[TreatmentRoadmaps] ([TreatmentRoadmapId]) ON DELETE CASCADE
);


INSERT INTO [TreatmentResults] ([TreatmentRoadmapId], [DurationDay], [Stage], [Date], [Price], [Description])
VALUES 
(1, 1, 'IVF', '2025-06-01', 1000000, N'Bắt đầu tốt'),
(2, 2, 'IUI', '2025-06-02', 800000, N'Theo dõi sát'),
(3, 3, 'IVF', '2025-06-03', 1500000, N'Trứng phát triển'),
(4, 4, 'IUI', '2025-06-04', 900000, N'Chuẩn bị tốt'),
(5, 5, 'IVF', '2025-06-05', 2000000, N'Thành công');


-- Type_test table
CREATE TABLE [dbo].[TypeTests] (
    [TypeTestId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [ConsulationResultId] INT            NOT NULL,
    [TreatmentResultId]   INT            NOT NULL,
    CONSTRAINT [PK_TypeTests] PRIMARY KEY CLUSTERED ([TypeTestId] ASC),
    CONSTRAINT [FK_TypeTests_ConsulationResults_ConsulationResultId] FOREIGN KEY ([ConsulationResultId]) REFERENCES [dbo].[ConsulationResults] ([ConsulationResultId]) ON DELETE CASCADE,
    CONSTRAINT [FK_TypeTests_TreatmentResults_TreatmentResultId] FOREIGN KEY ([TreatmentResultId]) REFERENCES [dbo].[TreatmentResults] ([TreatmentResultId]) ON DELETE CASCADE
);


INSERT INTO [TypeTests] ([ConsulationResultId], [TreatmentResultId], [Name], [Description])
VALUES 
(1, 1, N'Xét nghiệm máu', N'Kiểm tra nội tiết'),
(2, 2, N'Siêu âm', N'Theo dõi nang trứng'),
(3, 3, N'Xét nghiệm nội tiết', N'AMH, FSH, LH'),
(4, 4, N'Kiểm tra nội soi', N'Tình trạng tử cung'),
(5, 5, N'Xét nghiệm tinh trùng', N'Đánh giá chất lượng');

-- Prescription table
CREATE TABLE [dbo].[Prescriptions] (
    [PrescriptionId]    INT            IDENTITY (1, 1) NOT NULL,
    [Date]              DATE           NOT NULL,
    [Name]              NVARCHAR (MAX) NULL,
    [Note]              NVARCHAR (MAX) NULL,
    [TreatmentResultId] INT            NOT NULL,
    CONSTRAINT [PK_Prescriptions] PRIMARY KEY CLUSTERED ([PrescriptionId] ASC),
    CONSTRAINT [FK_Prescriptions_TreatmentResults_TreatmentResultId] FOREIGN KEY ([TreatmentResultId]) REFERENCES [dbo].[TreatmentResults] ([TreatmentResultId]) ON DELETE CASCADE
);

INSERT INTO [Prescriptions] ([TreatmentResultId], [Date], [Name], [Note])
VALUES 
(1, '2025-06-01', N'Thuốc kích trứng', N'Uống hàng ngày'),
(2, '2025-06-02', N'Thuốc hỗ trợ IUI', N'Theo chỉ định'),
(3, '2025-06-03', N'Thuốc nội tiết', N'Sáng – Tối'),
(4, '2025-06-04', N'Hỗ trợ tử cung', N'Đúng giờ'),
(5, '2025-06-05', N'Thuốc sau chọc hút', N'Kết hợp ăn uống');

-- Prescription_detail table
CREATE TABLE [dbo].[PrescriptionDetails] (
    [PrescriptionDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [MedicineName]         NVARCHAR (MAX) NULL,
    [Dosage]               NVARCHAR (MAX) NULL,
    [DurationDay]          NVARCHAR (MAX) NULL,
    [Instruction]          NVARCHAR (MAX) NULL,
    [PrescriptionId]       INT            NOT NULL,
    CONSTRAINT [PK_PrescriptionDetails] PRIMARY KEY CLUSTERED ([PrescriptionDetailId] ASC),
    CONSTRAINT [FK_PrescriptionDetails_Prescriptions_PrescriptionId] FOREIGN KEY ([PrescriptionId]) REFERENCES [dbo].[Prescriptions] ([PrescriptionId]) ON DELETE CASCADE
);

INSERT INTO [PrescriptionDetails] ([PrescriptionId], [MedicineName], [Dosage], [DurationDay], [Instruction])
VALUES 
(1, N'Clomiphene', '50mg', '5', N'Uống trước ăn sáng'),
(2, N'Letrozole', '2.5mg', '5', N'Uống 1 viên mỗi tối'),
(3, N'Progesterone', '100mg', '10', N'Uống sau bữa tối'),
(4, N'Estrogen', '1mg', '7', N'Buổi sáng và tối'),
(5, N'Duphaston', '10mg', '10', N'Sau ăn trưa');

-- Medical_record table




-- Medical_record_detail table
CREATE TABLE [dbo].[MedicalRecordDetails] (
    [MedicalRecordDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [Date]                  DATE           NOT NULL,
    [TestResult]            NVARCHAR (MAX) NULL,
    [Note]                  NVARCHAR (MAX) NULL,
    [Type]                  NVARCHAR (MAX) NULL,
    [MedicalRecordId]       INT            NOT NULL,
    [ConsulationResultId]   INT            NULL,
    [TreatmentResultId]     INT            NULL,
    [TreatmentRoadmapId]    INT            NULL,
    CONSTRAINT [PK_MedicalRecordDetails] PRIMARY KEY CLUSTERED ([MedicalRecordDetailId] ASC),
    CONSTRAINT [FK_MedicalRecordDetails_ConsulationResults_ConsulationResultId] FOREIGN KEY ([ConsulationResultId]) REFERENCES [dbo].[ConsulationResults] ([ConsulationResultId]),
    CONSTRAINT [FK_MedicalRecordDetails_MedicalRecords_MedicalRecordId] FOREIGN KEY ([MedicalRecordId]) REFERENCES [dbo].[MedicalRecords] ([MedicalRecordId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MedicalRecordDetails_TreatmentResults_TreatmentResultId] FOREIGN KEY ([TreatmentResultId]) REFERENCES [dbo].[TreatmentResults] ([TreatmentResultId]),
    CONSTRAINT [FK_MedicalRecordDetails_TreatmentRoadmaps_TreatmentRoadmapId] FOREIGN KEY ([TreatmentRoadmapId]) REFERENCES [dbo].[TreatmentRoadmaps] ([TreatmentRoadmapId])
);
select * from TreatmentRoadmaps
select * from MedicalRecordDetails
-- INSERT hợp lệ cho MedicalRecordDetails
-- Hồ sơ bệnh nhân 1 - IVF
INSERT INTO MedicalRecordDetails ([Date], [TestResult], [Note], [Type], [MedicalRecordId], [ConsulationResultId], [TreatmentResultId], [TreatmentRoadmapId]) VALUES
('2025-06-03', N'Bình thường', N'Tư vấn khởi đầu IVF', N'Consultation', 1, 1, NULL, 1),
('2025-06-06', N'15 trứng được lấy', N'Không biến chứng', N'Treatment', 1, NULL, NULL, 2),
('2025-06-09', N'Tinh trùng đạt chuẩn', N'Sẵn sàng tạo phôi', N'Treatment', 1, NULL, NULL, 3),
('2025-06-12', N'Tạo 5 phôi tốt', N'Đánh giá phôi ok', N'Treatment', 1, NULL, NULL, 4),
('2025-06-15', N'Chuyển 2 phôi', N'Tiến hành thành công', N'Treatment', 1, NULL, NULL, 5),
('2025-06-16', N'HCG dương tính', N'Thành công', N'Result', 1, NULL, 1, 6);

-- Hồ sơ bệnh nhân 2 - IVF lần 2
INSERT INTO MedicalRecordDetails ([Date], [TestResult], [Note], [Type], [MedicalRecordId], [ConsulationResultId], [TreatmentResultId], [TreatmentRoadmapId]) VALUES
('2025-05-15', N'Bắt đầu lại chu kỳ', N'IVF lần 2', N'Consultation', 2, 2, NULL, 1),
('2025-05-18', N'16 trứng được lấy', N'Sẵn sàng tạo phôi', N'Treatment', 2, NULL, NULL, 2),
('2025-05-21', N'Tạo được 4 phôi', N'Phôi trung bình', N'Treatment', 2, NULL, NULL, 3),
('2025-05-24', N'Chuyển 1 phôi', N'Chờ kết quả', N'Treatment', 2, NULL, NULL, 4),
('2025-05-27', N'Ổn định', N'Không phản ứng phụ', N'Treatment', 2, NULL, NULL, 5),
('2025-06-10', N'HCG âm tính', N'Thất bại', N'Result', 2, NULL, 2, 6);

-- Hồ sơ bệnh nhân 3 - IUI
INSERT INTO MedicalRecordDetails ([Date], [TestResult], [Note], [Type], [MedicalRecordId], [ConsulationResultId], [TreatmentResultId], [TreatmentRoadmapId]) VALUES
('2025-06-01', N'Kiểm tra ok', N'Bắt đầu IUI', N'Consultation', 3, 3, NULL, 7),
('2025-06-05', N'Phản ứng tốt', N'Kích thích thành công', N'Treatment', 3, NULL, NULL, 8),
('2025-06-07', N'Tinh trùng đạt chuẩn', N'Sẵn sàng bơm', N'Treatment', 3, NULL, NULL, 9),
('2025-06-10', N'Bơm IUI xong', N'Chờ thử thai', N'Treatment', 3, NULL, NULL, 10),
('2025-06-14', N'HCG dương tính', N'Thành công', N'Result', 3, NULL, 3, 11);




-- Order table
CREATE TABLE [dbo].[Orders] (
    [OrderId]    INT            IDENTITY (1, 1) NOT NULL,
    [Date]       DATE           NOT NULL,
    [Time]       TIME (7)       NOT NULL,
    [Status]     NVARCHAR (MAX) NULL,
    [Wife]       NVARCHAR (MAX) NULL,
    [Husband]    NVARCHAR (MAX) NULL,
    [CustomerId] INT            NOT NULL,
    [BookingId]  INT            NOT NULL,
    [ManagerId]  INT            NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [dbo].[Bookings] ([BookingId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]),
    CONSTRAINT [FK_Orders_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId])
);


INSERT INTO [Orders] ([BookingId], [CustomerId], [ManagerId], [Date], [Time], [Status], [Wife], [Husband])
VALUES 
(1, 1, 1, '2025-06-01', '09:00', N'Đã đặt', N'Không ghi chú', N'Nguyen Van B'),
(2, 2, 2, '2025-06-02', '10:00', N'Đã đặt', N'Yêu cầu bác sĩ nữ', N'Tran Van D'),
(3, 3, 3, '2025-06-03', '11:00', N'Chờ xác nhận', NULL, N'Pham Van E'),
(4, 4, 4, '2025-06-04', '14:00', N'Đã xác nhận', NULL, N'Do Van F'),
(5, 5, 5, '2025-06-05', '15:00', N'Đã hoàn tất', NULL, N'Le Van G');


-- Order_detail table
CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailId]             INT             IDENTITY (1, 1) NOT NULL,
    [Quantity]                  INT             NOT NULL,
    [Price]                     DECIMAL (18, 2) NOT NULL,
    [OrderId]                   INT             NOT NULL,
    [ServiceId]                 INT             NOT NULL,
    [ConsulationRegistrationId] INT             NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([OrderDetailId] ASC),
    CONSTRAINT [FK_OrderDetails_ConsulationRegistrations_ConsulationRegistrationId] FOREIGN KEY ([ConsulationRegistrationId]) REFERENCES [dbo].[ConsulationRegistrations] ([ConsulationRegistrationId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]),
    CONSTRAINT [FK_OrderDetails_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([ServiceDBId]) ON DELETE CASCADE
);

select * from [ConsulationRegistrations]
select * from OrderDetails
INSERT INTO [OrderDetails] ([OrderId], [ServiceId], [ConsulationRegistrationId], [Quantity], [Price])
VALUES 
(1, 1, 1, 1, 30000000),
(2, 2, 2, 2, 10000000),
(3, 2, 3, 1, 10000000)




-- Feedback table
CREATE TABLE [dbo].[Feedbacks] (
    [FeedbackId]  INT            IDENTITY (1, 1) NOT NULL,
    [Date]        DATE           NOT NULL,
    [Comments]    NVARCHAR (MAX) NULL,
    [Rating]      INT            NOT NULL,
    [CustomerId]  INT            NOT NULL,
    [ServiceDBId] INT            NOT NULL,
    [ManagerId]   INT            NOT NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED ([FeedbackId] ASC),
    CONSTRAINT [FK_Feedbacks_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Feedbacks_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Feedbacks_Services_ServiceDBId] FOREIGN KEY ([ServiceDBId]) REFERENCES [dbo].[Services] ([ServiceDBId])
);


INSERT INTO [Feedbacks] ([CustomerId], [ServiceDBId], [ManagerId], [Date], [Comments], [Rating])
VALUES 
(1, 1, 1, '2025-06-02', N'Dịch vụ tốt', 5),
(2, 2, 2, '2025-06-03', N'Tư vấn nhiệt tình', 4),
(3, 1, 2, '2025-06-04', N'Trải nghiệm ổn', 4),
(4, 2, 1, '2025-06-05', N'Cần cải thiện thời gian chờ', 3),
(5, 1, 1, '2025-06-06', N'Rất hài lòng', 5);


-- Payment table
CREATE TABLE [dbo].[Payments] (
    [PaymentId]          INT             IDENTITY (1, 1) NOT NULL,
    [PriceByTreatement]  DECIMAL (18, 2) NOT NULL,
    [Method]             NVARCHAR (MAX)  NULL,
    [Date]               DATE            NOT NULL,
    [Status]             NVARCHAR (MAX)  NULL,
    [TreatmentRoadmapId] INT             NOT NULL,
    [OrderId]            INT             NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([PaymentId] ASC),
    CONSTRAINT [FK_Payments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Payments_TreatmentRoadmaps_TreatmentRoadmapId] FOREIGN KEY ([TreatmentRoadmapId]) REFERENCES [dbo].[TreatmentRoadmaps] ([TreatmentRoadmapId])
);


INSERT INTO [Payments] ([TreatmentRoadmapId], [OrderId], [PriceByTreatement], [Method], [Date], [Status])
VALUES 
(1, 1, 30000000, N'Chuyển khoản', '2025-06-02', N'Đã thanh toán'),
(2, 2, 10000000, N'Tiền mặt', '2025-06-03', N'Đã thanh toán'),
(3, 3, 50000000, N'VNPay', '2025-06-04', N'Chưa thanh toán')



-- Blog_post table
CREATE TABLE [dbo].[BlogPosts] (
    [BlogPostId]    INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (MAX) NULL,
    [Story]         NVARCHAR (MAX) NULL,
    [TreatmentType] NVARCHAR (MAX) NULL,
    [Date]          DATETIME2 (7)  NOT NULL,
    [Image]         NVARCHAR (MAX) NULL,
    [CustomerId]    INT            NOT NULL,
    [ManagerId]     INT            NOT NULL,
    CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED ([BlogPostId] ASC),
    CONSTRAINT [FK_BlogPosts_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlogPosts_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId]) ON DELETE CASCADE
);

INSERT INTO [BlogPosts] ([CustomerId], [ManagerId], [Title], [Story], [TreatmentType], [Date], [Image])
VALUES 
(1, 1, N'Hành trình IVF', N'Kể lại quá trình IVF', N'IVF', '2025-06-01 10:00:00', N'ivf1.jpg'),
(2, 2, N'Kinh nghiệm IUI', N'Chia sẻ sau điều trị', N'IUI', '2025-06-02 11:00:00', N'iui1.jpg'),
(3, 3, N'Tư vấn điều trị', N'Bài viết chuyên gia', N'IVF', '2025-06-03 12:00:00', N'ivf2.jpg'),
(4, 4, N'Giải đáp thắc mắc', N'Câu hỏi thường gặp', N'IUI', '2025-06-04 13:00:00', N'iui2.jpg'),
(5, 5, N'Thành công sau IVF', N'Niềm vui làm mẹ', N'IVF', '2025-06-05 14:00:00', N'ivf3.jpg');


-- Embryo table
CREATE TABLE [dbo].[Embryos] (
    [EmbryoId]   INT            IDENTITY (1, 1) NOT NULL,
    [CreateAt]   DATE           NOT NULL,
    [Quality]    NVARCHAR (MAX) NULL,
    [Type]       NVARCHAR (MAX) NULL,
    [Amount]     INT            NOT NULL,
    [CustomerId] INT            NOT NULL,
    CONSTRAINT [PK_Embryos] PRIMARY KEY CLUSTERED ([EmbryoId] ASC),
    CONSTRAINT [FK_Embryos_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE
);


INSERT INTO [Embryos] ([CustomerId], [CreateAt], [Quality], [Type], [Amount])
VALUES 
(1, '2025-06-01', N'Tốt', 'D5', 5),
(2, '2025-06-02', N'Trung bình', 'D3', 4),
(3, '2025-06-03', N'Tốt', 'D5', 3),
(4, '2025-06-04', N'Kém', 'D2', 2),
(5, '2025-06-05', N'Tốt', 'D5', 6);


select s.Name, tr.Stage ,mrd.Note, mrd.TestResult, mrd.Type, mrd.Date as N'Ngày thực hiện', d.FullName as 'DoctorName' from Services s 
left join TreatmentRoadmaps tr on s.ServiceDBId = tr.ServiceId
left join MedicalRecordDetails mrd on mrd.TreatmentRoadmapId = tr.TreatmentRoadmapId
left join MedicalRecords mr on mr.MedicalRecordId = mrd.MedicalRecordId
left join Doctors d on mr.DoctorId = d.DoctorId
where mr.CustomerId = 1


select * from Users
select * from Customers
--select * from MedicalRecords
--select * from MedicalRecordDetails