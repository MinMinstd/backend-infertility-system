DROP DATABASE InfertilitySystem;
CREATE DATABASE InfertilitySystem
USE InfertilitySystem

CREATE TABLE [dbo].[Users] (
    [UserId]            INT             IDENTITY (1, 1) NOT NULL,
    [Email]             NVARCHAR (MAX)  NULL,
    [Phone]             NVARCHAR (MAX)  NULL,
    [PasswordHash]      VARBINARY (MAX) NULL,
    [PasswordSalt]      VARBINARY (MAX) NULL,
    [Role]              NVARCHAR (MAX)  NULL,
    [CreatedAt]         DATE            NOT NULL,
    [LastActiveAt]      DATE            NULL,
    [TotalActiveDays]   INT             NULL,
    [IsActive]          BIT             NULL,
    [TokenConfirmation] NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

INSERT INTO [dbo].[Users] ([Email], [Phone], [CreatedAt], [LastActiveAt], [TotalActiveDays], [PasswordHash], [PasswordSalt], [Role], [IsActive])
VALUES 
(N'duy@gmail.com', N'0903456789', '2025-01-01', '2025-06-20', 3, 0xE1F51BE5B7A05D4CF0CB1C1E3C13621D19A774B9586DEC17E21DD02C2D95580964E07BF87964441D5AA06E37CC342A113B29FA1925D978DD6474C250BBC61D80,
 0xDDE2BD20EE630525633836761EA057A2375921FBFD865714CF38FB0A79983AE23543C224C0550FB5207518DD7F6BFC97F761307DCFE8EA1821709CFB64B8E399, N'Admin', '1'),
(N'doctor1@gmail.com', N'0903456789', '2025-01-02', '2025-06-20', 4, 0xE1F51BE5B7A05D4CF0CB1C1E3C13621D19A774B9586DEC17E21DD02C2D95580964E07BF87964441D5AA06E37CC342A113B29FA1925D978DD6474C250BBC61D80, 
	0xDDE2BD20EE630525633836761EA057A2375921FBFD865714CF38FB0A79983AE23543C224C0550FB5207518DD7F6BFC97F761307DCFE8EA1821709CFB64B8E399,N'Doctor', '1'),
(N'doctor2@gmail.com', N'0903544878', '2025-01-03', '2025-06-20', 5, 0xE1F51BE5B7A05D4CF0CB1C1E3C13621D19A774B9586DEC17E21DD02C2D95580964E07BF87964441D5AA06E37CC342A113B29FA1925D978DD6474C250BBC61D80, 
	0xDDE2BD20EE630525633836761EA057A2375921FBFD865714CF38FB0A79983AE23543C224C0550FB5207518DD7F6BFC97F761307DCFE8EA1821709CFB64B8E399,N'Doctor', '1'),
(N'doctor3@gmail.com', N'0903544878', '2025-01-04', '2025-06-20', 6, 0xE1F51BE5B7A05D4CF0CB1C1E3C13621D19A774B9586DEC17E21DD02C2D95580964E07BF87964441D5AA06E37CC342A113B29FA1925D978DD6474C250BBC61D80, 
	0xDDE2BD20EE630525633836761EA057A2375921FBFD865714CF38FB0A79983AE23543C224C0550FB5207518DD7F6BFC97F761307DCFE8EA1821709CFB64B8E399,N'Doctor', '1'),
(N'customer1@gmail.com', N'0903544878', '2025-01-05', '2025-06-20', 7, 0xE1F51BE5B7A05D4CF0CB1C1E3C13621D19A774B9586DEC17E21DD02C2D95580964E07BF87964441D5AA06E37CC342A113B29FA1925D978DD6474C250BBC61D80, 
	0xDDE2BD20EE630525633836761EA057A2375921FBFD865714CF38FB0A79983AE23543C224C0550FB5207518DD7F6BFC97F761307DCFE8EA1821709CFB64B8E399,N'Customer', '1'),
(N'customer2@gmail.com', N'0903544878', '2025-01-06', '2025-06-20', 8, 0xE1F51BE5B7A05D4CF0CB1C1E3C13621D19A774B9586DEC17E21DD02C2D95580964E07BF87964441D5AA06E37CC342A113B29FA1925D978DD6474C250BBC61D80, 
	0xDDE2BD20EE630525633836761EA057A2375921FBFD865714CF38FB0A79983AE23543C224C0550FB5207518DD7F6BFC97F761307DCFE8EA1821709CFB64B8E399,N'Customer', '1'),
(N'huy@gmail.com', N'0903456789', '2025-01-07', '2025-06-20', 9, 0xE1F51BE5B7A05D4CF0CB1C1E3C13621D19A774B9586DEC17E21DD02C2D95580964E07BF87964441D5AA06E37CC342A113B29FA1925D978DD6474C250BBC61D80, 
	0xDDE2BD20EE630525633836761EA057A2375921FBFD865714CF38FB0A79983AE23543C224C0550FB5207518DD7F6BFC97F761307DCFE8EA1821709CFB64B8E399,N'Manager', '1');


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
select * from Orders
select * from OrderDetails
select * from Customers
select * from Bookings
select * from MedicalRecords
select * from TreatmentRoadmaps
select * from MedicalRecordDetails
select * from TreatmentResults
select * from TypeTests
select * from Doctors
select * from Users
select * from DoctorSchedules
select * from Services
select * from DoctorDegrees
select * from ConsulationResults
select * from TypeTests
select * from BlogPosts
select * from Payments
select * from Embryos

update OrderDetails set ServiceId = 2 where OrderDetailId = 3
select b.CustomerId, b.BookingId from MedicalRecords mr left join Customers c on mr.CustomerId = c.CustomerId
left join Bookings b on c.CustomerId = b.CustomerId where b.CustomerId = 1

SELECT d.MedicalRecordDetailId, d.MedicalRecordId, d.TreatmentRoadmapId, t.Stage
FROM MedicalRecordDetails d
LEFT JOIN TreatmentRoadmaps t ON d.TreatmentRoadmapId = t.TreatmentRoadmapId
WHERE d.MedicalRecordId = 1;

INSERT INTO [dbo].[Customers] ([UserId], [FullName], [Email], [Phone], [Gender], [Birthday], [Address]) VALUES
(5, N'Vũ Trần Bình Minh', 'customer1@gmail.com', '0903544878', N'Nam', '1990-01-01', N'Hanoi'),
(6, N'Tran Thi B', 'customer2@gmail.com', '0903544878', N'Nữ', '1992-02-02', N'HCMCity');


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
(7, N'Manager 1', 'huy@gmail.com', '0903456789', N'Hồ Chí Minh City');

-- Service table
CREATE TABLE [dbo].[Services] (
    [ServiceDBId] INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX)  NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [ManagerId]   INT             NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED ([ServiceDBId] ASC),
    CONSTRAINT [FK_Services_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId]) ON DELETE CASCADE
);

INSERT INTO [Services] ([Name], [Description], [ManagerId])
VALUES 
(N'IVF', N'IVF là viết tắt của cụm từ In vitro fertilization, tức là thụ tinh trong ống nghiệm. Đây là một trong những phương pháp hỗ trợ sinh sản bằng cách kết hợp tinh trùng và trứng với nhau trong ống nghiệm tại phòng thí nghiệm để tạo phôi. Sau khi phôi thai này được nuôi cấy khoảng 3 – 5 ngày thì sẽ được đưa trở lại buồng tử cung của người phụ nữ.', 1),
(N'IUI', N'IUI là phương pháp bơm tinh trùng đã lọc rửa vào tử cung, giúp chọn tinh trùng tốt, rút ngắn quãng đường đến trứng, tăng khả năng đậu thai. Đây là phương pháp điều trị hiếm muộn ngắn và gần giống thụ thai tự nhiên.
', 1);

-- Doctor table
CREATE TABLE [dbo].[Doctors] (
    [DoctorId]    INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      INT            NOT NULL,
    [FullName]    NVARCHAR (MAX) NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [Phone]       NVARCHAR (MAX) NULL,
    [Experience]  INT            NOT NULL,
    [ServiceDBId] INT            NOT NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED ([DoctorId] ASC),
    CONSTRAINT [FK_Doctors_Services_ServiceDBId] FOREIGN KEY ([ServiceDBId]) REFERENCES [dbo].[Services] ([ServiceDBId]) ON DELETE CASCADE
);

INSERT INTO [dbo].[Doctors] ([UserId], [FullName], [Email], [Phone], [Experience], [ServiceDBId]) VALUES
(2, N'Doctor 1', 'doctor1@gmail.com', '0903456789', 6, 1),
(3, N'Doctor 2', 'doctor2@gmail.com', '0903544878', 7, 1),
(4, N'Doctor 3', 'doctor3@gmail.com', '0903544878', 8, 2);
select * from Doctors

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
(N'IVF', 2001, 1),
(N'IVF', 2002, 2),
(N'IUI', 2003, 3);



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
( '2025-09-01', '08:00:00', '10:30:00', N'Unavailable', 1, 1), --2 1
( '2025-09-01', '11:00:00', '13:30:00', N'Available', 1, 1), --2 2
( '2025-09-01', '14:00:00', '16:30:00', N'Available', 1, 1), --2 3
( '2025-09-04', '08:00:00', '10:30:00', N'Available', 1, 1), --5 4
( '2025-09-04', '11:00:00', '13:30:00', N'Available', 1, 1), --5 5
( '2025-09-04', '14:00:00', '16:30:00', N'Available', 1, 1), --5 6
( '2025-09-07', '08:00:00', '10:30:00', N'Available', 1, 1), --2 1
( '2025-09-07', '11:00:00', '13:30:00', N'Available', 1, 1), --2 2
( '2025-09-07', '14:00:00', '16:30:00', N'Available', 1, 1), --2 3
( '2025-09-09', '08:00:00', '10:30:00', N'Available', 1, 1), --5 4
( '2025-09-09', '11:00:00', '13:30:00', N'Available', 1, 1), --5 5
( '2025-09-09', '14:00:00', '16:30:00', N'Available', 1, 1),
( '2025-09-12', '08:00:00', '10:30:00', N'Available', 1, 1), --5 4
( '2025-09-12', '11:00:00', '13:30:00', N'Available', 1, 1), --5 5
( '2025-09-12', '14:00:00', '16:30:00', N'Available', 1, 1),
( '2025-09-02', '08:00:00', '10:30:00', N'Available', 2, 1), --3 7
( '2025-09-02', '11:00:00', '13:30:00', N'Available', 2, 1), --3 8
( '2025-09-02', '14:00:00', '16:30:00', N'Available', 2, 1), --3 9
( '2025-09-05', '08:00:00', '10:30:00', N'Available', 2, 1), --6
( '2025-09-05', '11:00:00', '13:30:00', N'Available', 2, 1), --6
( '2025-09-05', '14:00:00', '16:30:00', N'Available', 2, 1), --6
( '2025-09-08', '08:00:00', '10:30:00', N'Available', 2, 1), --3 7
( '2025-09-08', '11:00:00', '13:30:00', N'Available', 2, 1), --3 8
( '2025-09-08', '14:00:00', '16:30:00', N'Available', 2, 1), --3 9
( '2025-09-10', '08:00:00', '10:30:00', N'Available', 2, 1), --6
( '2025-09-10', '11:00:00', '13:30:00', N'Available', 2, 1), --6
( '2025-09-10', '14:00:00', '16:30:00', N'Available', 2, 1),
( '2025-09-14', '08:00:00', '10:30:00', N'Available', 2, 1), --6
( '2025-09-14', '11:00:00', '13:30:00', N'Available', 2, 1), --6
( '2025-09-14', '14:00:00', '16:30:00', N'Available', 2, 1),
( '2025-09-03', '08:00:00', '10:30:00', N'Unavailable', 3, 1), --4
( '2025-09-03', '11:00:00', '13:30:00', N'Available', 3, 1), --4
( '2025-09-03', '14:00:00', '16:30:00', N'Available', 3, 1), --4
( '2025-09-06', '08:00:00', '10:30:00', N'Available', 3, 1), --7
( '2025-09-06', '11:00:00', '13:30:00', N'Available', 3, 1), --7
( '2025-09-06', '14:00:00', '16:30:00', N'Available', 3, 1),
( '2025-09-10', '08:00:00', '10:30:00', N'Available', 3, 1), --4
( '2025-09-10', '11:00:00', '13:30:00', N'Available', 3, 1), --4
( '2025-09-10', '14:00:00', '16:30:00', N'Available', 3, 1), --4
( '2025-09-15', '08:00:00', '10:30:00', N'Available', 3, 1), --7
( '2025-09-15', '11:00:00', '13:30:00', N'Available', 3, 1), --7
( '2025-09-15', '14:00:00', '16:30:00', N'Available', 3, 1),
( '2025-09-17', '08:00:00', '10:30:00', N'Available', 3, 1), --4
( '2025-09-17', '11:00:00', '13:30:00', N'Available', 3, 1), --4
( '2025-09-17', '14:00:00', '16:30:00', N'Available', 3, 1); --7


-- Consultation_registration table
--CREATE TABLE [dbo].[ConsulationRegistrations] (
--    [ConsulationRegistrationId] INT            IDENTITY (1, 1) NOT NULL,
--    [Type]                      NVARCHAR (MAX) NULL,
--    [Note]                      NVARCHAR (MAX) NULL,
--    CONSTRAINT [PK_ConsulationRegistrations] PRIMARY KEY CLUSTERED ([ConsulationRegistrationId] ASC)
--);

--INSERT INTO [dbo].[ConsulationRegistrations] ([Type], [Note])
--VALUES 
--(N'Thường', N'Không vấn đề'),
--(N'VIP', N'Được ưu tiên xét nghiệm sớm');


-- Booking table
CREATE TABLE [dbo].[Bookings] (
    [BookingId]        INT            IDENTITY (1, 1) NOT NULL,
    [Date]             DATE           NOT NULL,
    [Time]             NVARCHAR (MAX) NULL,
    [Type]             NVARCHAR (MAX) NULL,
    [Status]           NVARCHAR (MAX) NULL,
    [Note]             NVARCHAR (MAX) NULL,
    [CustomerId]       INT            NULL,
    [DoctorScheduleId] INT            NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED ([BookingId] ASC),
    CONSTRAINT [FK_Bookings_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]),
    CONSTRAINT [FK_Bookings_DoctorSchedules_DoctorScheduleId] FOREIGN KEY ([DoctorScheduleId]) REFERENCES [dbo].[DoctorSchedules] ([DoctorScheduleId])
);
select * from Customers
select * from Bookings
INSERT INTO [Bookings] (CustomerId, DoctorScheduleId, Status, Date, Time, Note, Type)
VALUES
(1, 1,  N'Pending', '2025-09-01', '08:00:00 - 10:30:00', N'Dịch vụ IVF', N'Service'),
(2, 31,  N'Pending', '2025-09-03', '08:00:00 - 10:30:00', N'Dịch vụ IUI', N'Service');


-- Consultation_result table
CREATE TABLE [dbo].[ConsulationResults] (
    [ConsulationResultId] INT            IDENTITY (1, 1) NOT NULL,
    [Date]                DATE           NOT NULL,
    [ResultValue]         NVARCHAR (MAX) NULL,
    [Note]                NVARCHAR (MAX) NULL,
    [BookingId]           INT            NULL,
    CONSTRAINT [PK_ConsulationResults] PRIMARY KEY CLUSTERED ([ConsulationResultId] ASC),
    CONSTRAINT [FK_ConsulationResults_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [dbo].[Bookings] ([BookingId])
);

INSERT INTO [dbo].[ConsulationResults] ([Date], [ResultValue], [Note], [BookingId]) VALUES
('2025-09-01', N'Ổn định', N'Không cần can thiệp', 1),
('2025-09-03', N'Theo dõi thêm', N'Xem lại sau 1 tuần', 2);



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
('2025-09-01', '2025-09-30', N'Đã thử thai', N'Hiếm muộn do buồng trứng', N'Thành công', 1, 1, 1),
-- Khách hàng 2 - IVF lần 2, đang điều trị
('2025-09-03', '2025-09-30', N'Chuyển phôi', N'Hiếm muộn không rõ nguyên nhân', N'Đang điều trị', 1, 2, 3);



-- Treatment_roadmap table
INSERT INTO [TreatmentRoadmaps] ([ServiceId], [Date], [Stage], [Description], [DurationDay], [Price])
VALUES
-- Quy trình IVF (ServiceId = 1)
(1, '2025-06-03', N'Kích thích buồng trứng', N'Bác sĩ tiêm thuốc kích buồng trứng trong 10–12 ngày, người vợ được xét nghiệm máu và siêu âm theo dõi. Khi nang đạt chuẩn, tiêm thuốc kích rụng trứng đúng giờ để trứng trưởng thành.', 1, 10000000),
(1, '2025-06-06', N'Chọc hút trứng', N'Sau 36 giờ tiêm kích rụng trứng, người vợ được gây mê để chọc hút trứng qua âm đạo trong 10–15 phút, sau đó nằm viện theo dõi 2–3 giờ.', 3, 20000000),
(1, '2025-06-09', N'Chọn lọc tinh trùng', N'Trong lúc vợ chọc hút trứng, chồng sẽ chuẩn bị tinh trùng: mẫu đông lạnh được rã đông, còn không thì lấy mẫu tươi, lọc rửa và chọn tinh trùng khỏe.', 5, 30000000),
(1, '2025-06-12', N'Tạo phôi', N'Tinh trùng được tiêm trực tiếp vào trứng để tạo phôi, sau đó nuôi cấy 2–5 ngày trước khi chuyển vào tử cung. Bác sĩ sẽ cập nhật tình trạng phôi cho vợ chồng.', 2, 20000000),
(1, '2025-06-15', N'Chuyển phôi', N'Chuyển phôi gồm 2 loại: phôi đông lạnh (trữ và chuyển ở chu kỳ sau) và phôi tươi (chuyển ngay sau tạo phôi). Quá trình chuyển phôi diễn ra nhanh (5–10 phút), không cần nằm viện, và vợ dùng thuốc nội tiết 2 tuần theo hướng dẫn bác sĩ.', 4, 40000000),
(1, '2025-06-16', N'Thử thai', N'Bước cuối cùng là thử thai bằng xét nghiệm máu. Beta HCG > 25 IU/L nghĩa là đậu thai, < 25 IU/L là thất bại. Nếu còn phôi trữ đông, có thể chuyển phôi ở chu kỳ sau mà không cần kích trứng hay chọc hút lại.
', 1, 30000000),

-- Quy trình IUI (ServiceId = 2)
(2, '2025-06-01', N'Kiểm tra sức khỏe sinh sản', N'Vợ chồng được khám và xét nghiệm chuyên sâu để đánh giá đủ điều kiện làm IUI.
', 3, 10000000),
(2, '2025-06-05', N'Kích thích buồng trứng', N'Người bệnh dùng thuốc từ ngày 2–3 chu kỳ, siêu âm 2–3 lần theo dõi nang noãn. Khi nang trưởng thành, tiêm hCG kích rụng trứng và bơm tinh trùng sau 36–38 giờ.
', 3, 10000000),
(2, '2025-06-07', N'Chọn lọc tinh trùng', N'Người chồng lấy tinh trùng trước bơm 2 giờ, tinh trùng được lọc rửa bằng môi trường IVF chuẩn để chọn tinh trùng khỏe.', 3, 20000000),
(2, '2025-06-10', N'Bơm tinh trùng vào buồng tử cung', N'Tinh trùng sau lọc rửa được bơm vào tử cung tại 2 thời điểm: 24h trước và 48h sau rụng trứng, bằng ống thông mềm, an toàn và không đau.
', 1, 20000000),
(2, '2025-06-14', N'Thử thai', N'Sau quy trình IUI khoảng 14 ngày, người vợ sẽ được thử thai và siêu âm thai để xác định kết quả thụ thai có thành công hay không. Nếu kết quả có thai, người vợ cần dưỡng thai theo lời khuyên của bác sĩ, cũng như tuân thủ chỉ định khám thai định kỳ để quá trình mang thai an toàn, khỏe mạnh.', 5, 10000000);


-- Treatment_result table
CREATE TABLE [dbo].[TreatmentResults] (
    [TreatmentResultId]   INT            IDENTITY (1, 1) NOT NULL,
    [DateTreatmentResult] DATE           NOT NULL,
    [Stage]               NVARCHAR (MAX) NULL,
    [Description]         NVARCHAR (MAX) NULL,
    [DurationDay]         INT            NOT NULL,
    [Result]              NVARCHAR (MAX) NULL,
    [TreatmentRoadmapId]  INT            NOT NULL,
    CONSTRAINT [PK_TreatmentResults] PRIMARY KEY CLUSTERED ([TreatmentResultId] ASC),
    CONSTRAINT [FK_TreatmentResults_TreatmentRoadmaps_TreatmentRoadmapId] FOREIGN KEY ([TreatmentRoadmapId]) REFERENCES [dbo].[TreatmentRoadmaps] ([TreatmentRoadmapId]) ON DELETE CASCADE
);

select * from TreatmentResults
INSERT INTO [TreatmentResults] 
    ([TreatmentRoadmapId], [DurationDay], [Stage], [DateTreatmentResult], [Description], [Result])
VALUES 
    (1, 1, N'IVF', '2025-06-03', N'Bắt đầu kích thích buồng trứng, đáp ứng tốt', N'Đang theo dõi'),
    (2, 3, N'IVF', '2025-06-06', N'Chọc hút trứng thành công, thu được nhiều trứng đạt chuẩn', N'Đạt yêu cầu'),
    (3, 5, N'IVF', '2025-06-09', N'Đã chọn lọc tinh trùng chất lượng cao', N'Hiệu quả tốt'),
    (4, 2, N'IVF', '2025-06-12', N'Tạo phôi thành công, số lượng phôi đạt chuẩn', N'Đạt yêu cầu'),
    (5, 4, N'IVF', '2025-06-15', N'Chuyển phôi thuận lợi, không biến chứng', N'Đang theo dõi'),
    (6, 1, N'IVF', '2025-06-16', N'Xét nghiệm beta HCG dương tính', N'Thành công'),
	(7, 1, 'IUI', '2025-09-06', N'Bắt đầu tiêm thuốc', N'Đang theo dõi'),
    (8, 2, 'IUI', '2025-09-07', N'Đáp ứng tốt với thuốc', N'Hiệu quả tốt'),
    (9, 3, 'IUI', '2025-09-08', N'Canh trứng chính xác', N'Cần theo dõi tiếp'),
    (10, 4, 'IUI', '2025-09-09', N'Tiêm tinh trùng thành công', N'Đạt yêu cầu'),
    (11, 5, 'IUI', '2025-09-10', N'Xét nghiệm beta HCG dương tính', N'Thành công');


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

select * from ConsulationResults
INSERT INTO [TypeTests] ([ConsulationResultId], [TreatmentResultId], [Name], [Description])
VALUES
-- IVF
(1, 1, N'Xét nghiệm máu', N'Kiểm tra nội tiết trước khi kích thích buồng trứng'),
(1, 2, N'Siêu âm đầu dò', N'Theo dõi nang trứng trước khi chọc hút'),
(1, 3, N'Phân tích tinh dịch', N'Đánh giá chất lượng tinh trùng'),
(1, 4, N'Kiểm tra phôi', N'Đánh giá số lượng và chất lượng phôi tạo thành'),
(1, 5, N'Siêu âm tử cung', N'Đánh giá niêm mạc tử cung trước khi chuyển phôi'),
(1, 6, N'Xét nghiệm beta HCG', N'Xác nhận kết quả mang thai'),

-- IUI
(2, 7, N'Xét nghiệm máu', N'Kiểm tra nội tiết trước khi tiêm thuốc'),
(2, 8, N'Siêu âm', N'Theo dõi nang trứng trong quá trình tiêm thuốc'),
(2, 9, N'Xét nghiệm LH', N'Xác định thời điểm rụng trứng'),
(2, 10, N'Xét nghiệm tinh dịch', N'Đánh giá mẫu tinh trùng trước IUI'),
(2, 11, N'Xét nghiệm beta HCG', N'Xác nhận kết quả mang thai sau IUI');


-- Medical_record table




-- Medical_record_detail table
CREATE TABLE [dbo].[MedicalRecordDetails] (
    [MedicalRecordDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [Date]                  DATE           NOT NULL,
    [TestResult]            NVARCHAR (MAX) NULL,
    [Note]                  NVARCHAR (MAX) NULL,
    [TypeName]                  NVARCHAR (MAX) NULL,
    [Status]                NVARCHAR (MAX) NULL,
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
select * from TreatmentResults
select * from Bookings
-- INSERT hợp lệ cho MedicalRecordDetails
-- Hồ sơ bệnh nhân 1 - IVF
INSERT INTO MedicalRecordDetails ([Date], [TestResult], [Note], [TypeName], [Status], [MedicalRecordId], [ConsulationResultId], [TreatmentResultId], [TreatmentRoadmapId]) VALUES
('2025-06-03', N'Bình thường', N'Tư vấn khởi đầu IVF', N'Consultation', N'Complete', 1, 1, 1, 1),
('2025-06-06', N'15 trứng được lấy', N'Không biến chứng', N'Treatment', N'Complete', 1, NULL, 2, 2),
('2025-06-09', N'Tinh trùng đạt chuẩn', N'Sẵn sàng tạo phôi', N'Treatment', N'Complete', 1, NULL, 3, 3),
('2025-06-12', N'Tạo 5 phôi tốt', N'Đánh giá phôi ok', N'Treatment', N'Complete', 1, NULL, 4, 4),
('2025-06-15', N'Chuyển 2 phôi', N'Tiến hành thành công', N'Treatment', N'Complete', 1, NULL, 5, 5),
('2025-06-16', N'HCG dương tính', N'Thành công', N'Result', N'Complete', 1, NULL, 6, 6);

-- Hồ sơ bệnh nhân 2 - IVF lần 2
INSERT INTO MedicalRecordDetails ([Date], [TestResult], [Note], [TypeName], [Status], [MedicalRecordId], [ConsulationResultId], [TreatmentResultId], [TreatmentRoadmapId]) VALUES
('2025-06-01', N'Khám tổng quát', N'IUI lần 1 - kiểm tra sức khỏe sinh sản', N'Consultation', N'Complete', 2, 2, 7, 7),
('2025-06-05', N'Kích thích buồng trứng', N'Đáp ứng tốt, sẵn sàng bơm tinh trùng', N'Treatment', N'Complete', 2, NULL, 8, 8),
('2025-06-07', N'Chọn lọc tinh trùng', N'Tinh trùng đạt chuẩn', N'Treatment', N'Complete', 2, NULL, 9, 9),
('2025-06-10', N'Bơm tinh trùng', N'Thực hiện thuận lợi', N'Treatment', N'Complete', 2, NULL, 10, 10),
('2025-06-14', N'HCG dương tính', N'Thành công', N'Result', N'Complete', 2, NULL, 11, 11);





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

select * from Orders
INSERT INTO [Orders] ([BookingId], [CustomerId], [ManagerId], [Date], [Time], [Status], [Wife], [Husband])
VALUES 
(1, 1, 1, '2025-09-01', '08:00:00', N'Pending', N'Đoàn Ngọc Khánh', N'Nguyen Van B'),
(2, 2, 1, '2025-09-03', '08:00:00', N'Pending', N'Vũ Trần Bình Minh', N'Tran Van D');



-- Order_detail table
CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [DoctorName]    NVARCHAR (MAX) NULL,
    [ServiceName]   NVARCHAR (MAX) NULL,
    [StageName]     NVARCHAR (MAX) NULL,
    [DateTreatment] DATE           NULL,
    [TimeTreatment] NVARCHAR (MAX) NULL,
    [OrderId]       INT            NULL,
    [ServiceId]     INT            NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([OrderDetailId] ASC),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]),
    CONSTRAINT [FK_OrderDetails_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([ServiceDBId])
);

INSERT INTO [dbo].[OrderDetails] 
(
    [OrderId], 
    [ServiceId], 
    [DoctorName], 
    [ServiceName],
	[StageName],
	[DateTreatment],
	[TimeTreatment]
)
VALUES
(1, 1, N'Doctor 1', N'IVF', null, null, null),
(2, 2, N'Doctor 3', N'IUI', null, null, null);





-- Feedback table
CREATE TABLE [dbo].[Feedbacks] (
    [FeedbackId]  INT            IDENTITY (1, 1) NOT NULL,
    [Date]        DATE           NOT NULL,
    [FullName]    NVARCHAR (MAX) NOT NULL,
    [Comments]    NVARCHAR (MAX) NULL,
    [Rating]      INT            NOT NULL,
	[Status]	  NVARCHAR (MAX) NOT NULL,
    [CustomerId]  INT            NOT NULL,
    [ManagerId]   INT            NOT NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED ([FeedbackId] ASC),
    CONSTRAINT [FK_Feedbacks_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Feedbacks_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Feedbacks_Services_ServiceDBId] FOREIGN KEY ([ServiceDBId]) REFERENCES [dbo].[Services] ([ServiceDBId])
);


INSERT INTO [Feedbacks] ([CustomerId], [ManagerId], [Date], [FullName], [Comments], [Rating], [Status])
VALUES 
(1, 1, '2025-09-30', N'Vũ Trần Bình Minh', N'Dịch vụ tốt', 5, N'Confirm'),
(2, 1, '2025-09-29', 'Tran Thi B', N'Tư vấn nhiệt tình', 4, N'Confirm');



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
-- IVF
(1, 1, 10000000, N'Chuyển khoản', '2025-06-03', N'Đã thanh toán'),
(2, 1, 20000000, N'Tiền mặt', '2025-06-06', N'Đã thanh toán'),
(3, 1, 30000000, N'Chuyển khoản', '2025-06-09', N'Đã thanh toán'),
(4, 1, 20000000, N'Tiền mặt', '2025-06-12', N'Đã thanh toán'),
(5, 1, 40000000, N'Chuyển khoản', '2025-06-15', N'Đã thanh toán'),
(6, 1, 30000000, N'Tiền mặt', '2025-06-16', N'Đã thanh toán'),

-- IUI
(7, 2, 10000000, N'Chuyển khoản', '2025-06-01', N'Đã thanh toán'),
(8, 2, 10000000, N'Tiền mặt', '2025-06-05', N'Đã thanh toán'),
(9, 2, 20000000, N'Chuyển khoản', '2025-06-07', N'Đã thanh toán'),
(10, 2, 20000000, N'Tiền mặt', '2025-06-10', N'Đã thanh toán'),
(11, 2, 10000000, N'Chuyển khoản', '2025-06-14', N'Đã thanh toán');




-- Blog_post table
CREATE TABLE [dbo].[BlogPosts] (
    [BlogPostId]    INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (MAX) NULL,
    [Story]         NVARCHAR (MAX) NULL,
    [TreatmentType] NVARCHAR (MAX) NULL,
    [Date]          DATETIME2 (7)  NOT NULL,
    [Image]         NVARCHAR (MAX) NULL,
    [Status]        NVARCHAR (MAX) NULL,
    [CustomerId]    INT            NOT NULL,
    [ManagerId]     INT            NOT NULL,
    CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED ([BlogPostId] ASC),
    CONSTRAINT [FK_BlogPosts_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_BlogPosts_Managers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [dbo].[Managers] ([ManagerId]) ON DELETE CASCADE
);

INSERT INTO [BlogPosts] ([CustomerId], [ManagerId], [Title], [Story], [TreatmentType], [Date], [Image], [Status])
VALUES 
(1, 1, N'Hành trình IVF', N'Kể lại quá trình IVF', N'IVF', '2025-06-01', N'ivf1.jpg', 'Pending'),
(2, 1, N'Kinh nghiệm IUI', N'Chia sẻ sau điều trị', N'IUI', '2025-06-02', N'iui1.jpg', 'Pending');


-- Embryo table
CREATE TABLE [dbo].[Embryos] (
    [EmbryoId]      INT            IDENTITY (1, 1) NOT NULL,
    [CreateAt]      DATE           NOT NULL,
    [TransferredAt] DATE           NULL,
    [Quality]       NVARCHAR (MAX) NULL,
    [Type]          NVARCHAR (MAX) NULL,
    [Status]        NVARCHAR (MAX) NULL,
    [Note]          NVARCHAR (MAX) NULL,
    [OrderId]       INT            NOT NULL,
    CONSTRAINT [PK_Embryos] PRIMARY KEY CLUSTERED ([EmbryoId] ASC),
    CONSTRAINT [FK_Embryos_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]) ON DELETE CASCADE
);

INSERT INTO Embryos (CreateAt, TransferredAt, Quality, Type, Status, Note, OrderId)
VALUES 
('2025-07-01', NULL, N'Tốt',         N'NA', N'Đạt chuẩn',  N'Trứng chọc hút lần 1, số lượng tốt', 1),
('2025-07-02', NULL, N'Trung bình',  N'NA', N'Đạt chuẩn',  N'Trứng chờ thụ tinh ICSI',            1),
('2025-07-03', NULL, N'Kém',         N'NA', N'Không đạt',  N'Trứng không đạt tiêu chuẩn',         1),
('2025-07-04', NULL, N'Tốt',         N'NA', N'Đạt chuẩn',  N'Trứng đông lạnh sau chọc hút',       1),
('2025-07-05', NULL, N'Trung bình',  N'NA', N'Đạt chuẩn',  N'Trứng thu được, chờ thụ tinh IVF',   1),
('2025-07-06', NULL, N'Tốt',         N'NA', N'Đạt chuẩn',  N'Trứng chọc hút lần 2, số lượng tốt', 2),
('2025-07-07', NULL, N'Trung bình',  N'NA', N'Đạt chuẩn',  N'Trứng chờ thụ tinh ICSI',            2),
('2025-07-08', NULL, N'Kém',         N'NA', N'Không đạt',  N'Trứng không đạt tiêu chuẩn',         2),
('2025-07-09', NULL, N'Tốt',         N'NA', N'Đạt chuẩn',  N'Trứng đông lạnh sau chọc hút',       2),
('2025-07-10', NULL, N'Trung bình',  N'NA', N'Đạt chuẩn',  N'Trứng thu được, chờ thụ tinh IVF',   2);




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