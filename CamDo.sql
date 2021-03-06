USE [master]
GO
/****** Object:  Database [CamDo]    Script Date: 7/23/2020 1:24:45 AM ******/
CREATE DATABASE [CamDo]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CamDo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CamDo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CamDo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CamDo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CamDo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CamDo] SET ARITHABORT OFF 
GO
ALTER DATABASE [CamDo] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CamDo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CamDo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CamDo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CamDo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CamDo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CamDo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CamDo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CamDo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CamDo] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CamDo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CamDo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CamDo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CamDo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CamDo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CamDo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CamDo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CamDo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CamDo] SET  MULTI_USER 
GO
ALTER DATABASE [CamDo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CamDo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CamDo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CamDo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
USE [CamDo]
GO
/****** Object:  UserDefinedFunction [dbo].[fnTinhLaiSuat]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnTinhLaiSuat](@NgayCam date,@NgayChuoc date)
	RETURNS INT
BEGIN
	DECLARE @SoThangCam INT;
	SET @SoThangCam = ROUND(DATEDIFF(Month , @NgayCam ,@NgayChuoc),0);
	IF(@SoThangCam > 5) RETURN 5;
	RETURN @SoThangCam;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fnTinhSoDuNo]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnTinhSoDuNo](@SoTienKhachTra int, @NgayCam date, @LaiSuat int, @SoTienCam int, @MaPhieuCam varchar(50))
	RETURNS INT
BEGIN
	DECLARE @TongLai INT, @SoTienDaTra INT, @SoThangDaCam int ;
	SET @SoThangDaCam = ROUND(DATEDIFF(Month , @NgayCam ,GETDATE()),0);
	SET @TongLai= @SoThangDaCam*@LaiSuat*@SoTienCam*0.01;
	IF(SELECT SUM (SoTienKhachTra)FROM CamDo.dbo.PhieuTraGop WHERE MaPhieuCam=@MaPhieuCam) IS NULL RETURN @SoTienCam + @TongLai-@SoTienKhachTra;
	RETURN @SoTienCam + @TongLai- (SELECT SUM (SoTienKhachTra)FROM CamDo.dbo.PhieuTraGop WHERE MaPhieuCam=@MaPhieuCam) - @SoTienKhachTra;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fnTinhTienChuoc]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnTinhTienChuoc](@NgayCam date, @LaiSuat int, @SoTienCam int, @MaPhieuCam varchar(50), @TinhTrang nvarchar(50))
	RETURNS INT
BEGIN
	DECLARE @TongLai INT, @SoTienDaTra INT, @SoThangDaCam int , @TienChuoc int;
	SET @SoThangDaCam = ROUND(DATEDIFF(Month , @NgayCam ,GETDATE()),0);
	SET @TongLai= @SoThangDaCam*@LaiSuat*@SoTienCam*0.01;
	IF( @TinhTrang = N'Đã chuộc') set @TienChuoc = 0;
	ELSE IF(SELECT SUM (SoTienKhachTra)FROM CamDo.dbo.PhieuTraGop WHERE MaPhieuCam=@MaPhieuCam) IS NULL set @TienChuoc= @SoTienCam + @TongLai;
	ELSE set @TienChuoc = @SoTienCam + @TongLai- (SELECT SUM (SoTienKhachTra)FROM CamDo.dbo.PhieuTraGop WHERE MaPhieuCam=@MaPhieuCam);
	RETURN @TienChuoc;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[fnTinhTrang]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fnTinhTrang](@MaHang nvarchar(50))
	RETURNS NVARCHAR(50)
BEGIN
	DECLARE @TinhTrang NVARCHAR(50), @Count int;
	SET @Count= convert(int,GETDATE() - CONVERT(datetime,(SELECT NgayChuoc FROM PhieuCamDo WHERE MaHang=@MaHang)));
	IF( (SELECT NgayKhachChuoc FROM PhieuChuocDo,PhieuCamDo WHERE MaHang=@MaHang AND PhieuChuocDo.MaPhieuCam=PhieuCamDo.MaPhieuCam) IS NOT NULL ) SET @TinhTrang = N'Đã chuộc';
	ELSE IF( @Count > 0) SET @TinhTrang = N'Qúa hạn';
	ELSE SET @TinhTrang = N'Còn hàng';
	RETURN @TinhTrang;
END;
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[CMND] [nvarchar](50) NOT NULL,
	[Ten] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SoDT] [nchar](10) NULL,
	[NgaySinh] [date] NULL,
	[NoiCap] [nvarchar](50) NULL,
	[GioiTinh] [nvarchar](50) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[CMND] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MatHang]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MatHang](
	[MaHang] [nvarchar](50) NOT NULL,
	[LoaiHang] [nvarchar](50) NULL,
	[TenHang] [nvarchar](50) NULL,
	[GiaTri] [int] NULL,
	[TinhTrang] [nvarchar](50) NULL,
	[CMND] [nvarchar](50) NULL,
 CONSTRAINT [PK_MatHang] PRIMARY KEY CLUSTERED 
(
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
	[Ten] [nvarchar](50) NULL,
	[GioiTinh] [nvarchar](50) NULL,
	[SoDT] [nchar](10) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[Quyen] [nvarchar](50) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuCamDo]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuCamDo](
	[MaPhieuCam] [nvarchar](50) NOT NULL,
	[MaHang] [nvarchar](50) NULL,
	[NgayCam] [date] NULL,
	[NgayChuoc] [date] NULL,
	[SoTienCam] [int] NULL,
	[LaiSuat] [int] NULL,
	[MaNV] [nvarchar](50) NULL,
 CONSTRAINT [PK_PhieuCamDo] PRIMARY KEY CLUSTERED 
(
	[MaPhieuCam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuChuocDo]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuChuocDo](
	[MaPhieuChuoc] [nvarchar](50) NOT NULL,
	[MaPhieuCam] [nvarchar](50) NULL,
	[NgayKhachChuoc] [date] NULL,
	[SoTienChuoc] [int] NULL,
	[MaNV] [nvarchar](50) NULL,
 CONSTRAINT [PK_PhieuChuocDo] PRIMARY KEY CLUSTERED 
(
	[MaPhieuChuoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuTraGop]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuTraGop](
	[MaTraGop] [nvarchar](50) NOT NULL,
	[NgayTraGop] [date] NULL,
	[MaPhieuCam] [nvarchar](50) NULL,
	[SoTienKhachTra] [int] NULL,
	[SoTienDuNo] [int] NULL,
	[MaNV] [nvarchar](50) NULL,
 CONSTRAINT [PK_PhieuTraGop] PRIMARY KEY CLUSTERED 
(
	[MaTraGop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SearchKhachHang_View]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[spLoadCMNDTen]    Script Date: 8/11/2019 4:20:42 AM *****
CÁC PROCEDURES TÌM KIẾM
VIEW*/
CREATE VIEW [dbo].[SearchKhachHang_View]
AS
SELECT        Ten AS [Họ và tên], DiaChi AS [Địa chỉ], SoDT AS [Số điện thoại], NoiCap AS [Nơi cấp], GioiTinh AS [Giới tính], NgaySinh AS [Ngày sinh], CMND
FROM            dbo.KhachHang
GO
/****** Object:  View [dbo].[SearchNhanVien_View]    Script Date: 7/23/2020 1:24:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*View tìm kiếm nhân viên*/
CREATE VIEW [dbo].[SearchNhanVien_View]
AS
SELECT MaNV as [Mã NV], Email,MatKhau as [Mật khẩu],Ten as [Họ và tên], GioiTinh as [Giới tính], SoDT as [Số điện thoại],DiaChi as [Địa chỉ], Quyen as [Quyền] FROM NhanVien
GO
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'1', N'Đoàn Văn Long', N'Quận Thủ Đức', N'0123456789', CAST(N'1999-08-28' AS Date), N'TPHCM', N'Nam')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'10', N'Trần Nguyên Khai', N'Quận Thủ Đức', N'0765002122', CAST(N'1999-02-01' AS Date), N'TPHCM', N'Nam')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'12', N'Nguyễn Văn Linh', N'Quận 2', N'0763345886', CAST(N'2000-12-10' AS Date), N'TPHCM', N'Nam')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'13', N'Ngô tâm', N'quận 8', N'0765002121', CAST(N'1999-07-23' AS Date), N'TPHCM', N'Nam')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'2', N'Ngô Hoàng Minh Tâm', N'Quận 10', N'0987654321', CAST(N'1999-09-28' AS Date), N'TPHCM', N'Nam')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'3', N'Lê Kim Đỉnh', N'Quận 10', N'0124789563', CAST(N'1999-08-28' AS Date), N'TPHCM', N'Nam')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'4', N'Nguyễn Anh Kim', N'Quận 6', N'0985335433', CAST(N'1999-09-28' AS Date), N'TPHCM', N'Nữ')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'5', N'Lê Như Trúc', N'Quận 8', N'0861353215', CAST(N'1999-07-01' AS Date), N'TPHCM', N'Nữ')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'6', N'Hoàng Vĩnh Phúc', N'Quận 1', N'0541636546', CAST(N'2000-12-13' AS Date), N'TPHCM', N'Nam')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'7', N'Trần Nguyên Tài', N'Vũng Tàu', N'0908465658', CAST(N'2001-07-17' AS Date), N'Vũng Tàu', N'Nữ')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'8', N'Phạm Thế Hiển', N'Long An', N'0846362612', CAST(N'2001-02-01' AS Date), N'Long An', N'Nữ')
INSERT [dbo].[KhachHang] ([CMND], [Ten], [DiaChi], [SoDT], [NgaySinh], [NoiCap], [GioiTinh]) VALUES (N'9', N'Ngô Sĩ Liên', N'Bình Dương', N'0285695665', CAST(N'2000-05-13' AS Date), N'Bình Phước', N'Nam')
GO
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'001', N'Điện Tử', N'Laptop Asus', 10000000, N'Còn hạn', N'1')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'002', N'Điện Tử', N'Macbook Pro 2018', 10000000, N'Còn hạn', N'2')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'004', N'Điện Tử', N'Samsung S7', 16000000, N'', N'1')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'005', N'Điện Tử', N'Máy Ảnh SONY', 15000000, NULL, N'4')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'006', N'Xe', N'Future', 10000000, NULL, N'5')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'007', N'Tư Trang', N'Dây chuyền bạc', 20000000, NULL, N'3')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'012', N'Điện Tử', N'IPHONE 11', 20000000, N'Còn hạn', N'9')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'013', N'Điện Tử', N'IPAD AIR 2', 500000, N'Còn hạn', N'9')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'014', N'Điện Tử', N'Asus Zenphone', 9000000, N'Đã Chuộc', N'1')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'015', N'Điện Tử', N'Đồng Hồ Mạ Vàng', 10000000, N'Còn Hạn', N'10')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'021', N'Khác', N'DÉP', 100000, N'Còn Hạn', N'2')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'031', N'Khác', N'7 miếng đất', 100000, N'Còn Hạn', N'3')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'090', N'Điện Tử', N'Apple watch', 7000000, N'Còn Hạn', N'6')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'091', N'Điện Tử', N'Apple watch 5', 10000000, N'Còn Hạn', N'8')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'121', N'Xe', N'MAI THÚY', 10000000, N'Đã Chuộc', N'12')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'122', N'Khác', N'nhà', 20000000, N'Đã Chuộc', N'12')
INSERT [dbo].[MatHang] ([MaHang], [LoaiHang], [TenHang], [GiaTri], [TinhTrang], [CMND]) VALUES (N'130', N'Xe', N'xe máy', 1000000, N'Còn Hạn', N'13')
GO
INSERT [dbo].[NhanVien] ([MaNV], [Email], [MatKhau], [Ten], [GioiTinh], [SoDT], [DiaChi], [Quyen]) VALUES (N'001', N'TAMINH99', N'admin', N'TÂM', N'Nam', N'0765002122', N'TPHCM', N'Admin')
INSERT [dbo].[NhanVien] ([MaNV], [Email], [MatKhau], [Ten], [GioiTinh], [SoDT], [DiaChi], [Quyen]) VALUES (N'002', N'NgoTam', N'17110218', N'Ngô Tâm', N'Nam', N'916516516 ', N'TPHCM', N'NhanVien')
INSERT [dbo].[NhanVien] ([MaNV], [Email], [MatKhau], [Ten], [GioiTinh], [SoDT], [DiaChi], [Quyen]) VALUES (N'003', N'DoanLong', N'17110174', N'Đoàn Long', N'Nam', N'046515315 ', N'TPHCM', N'NhanVien')
INSERT [dbo].[NhanVien] ([MaNV], [Email], [MatKhau], [Ten], [GioiTinh], [SoDT], [DiaChi], [Quyen]) VALUES (N'004', N'loanngo', N'loan', N'Loan', N'Nữ', N'019314141 ', N'TPHCM', N'NhanVien')
GO
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'001', N'001', CAST(N'2019-07-07' AS Date), CAST(N'2019-07-07' AS Date), 2000000, 0, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'002', N'007', CAST(N'2019-08-08' AS Date), CAST(N'2019-12-02' AS Date), 300000, 1, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'003', N'006', CAST(N'2019-09-09' AS Date), CAST(N'2020-06-06' AS Date), 4000000, 2, N'002')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'005', N'004', CAST(N'2019-11-11' AS Date), CAST(N'2019-12-01' AS Date), 600000, 2, N'003')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'008', N'002', CAST(N'2019-12-03' AS Date), CAST(N'2019-12-31' AS Date), 10000000, 0, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'012', N'012', CAST(N'2019-12-11' AS Date), CAST(N'2019-12-11' AS Date), 3000000, 2, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'013', N'013', CAST(N'2019-12-11' AS Date), CAST(N'2019-12-11' AS Date), 300000, 1, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'014', N'014', CAST(N'2019-12-12' AS Date), CAST(N'2019-12-19' AS Date), 50000000, 2, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'021', N'021', CAST(N'2020-07-23' AS Date), CAST(N'2021-01-08' AS Date), 50000, 0, N'002')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'031', N'031', CAST(N'2020-07-23' AS Date), CAST(N'2020-11-27' AS Date), 0, 70000, N'002')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'090', N'090', CAST(N'2020-07-20' AS Date), CAST(N'2020-11-10' AS Date), 50000000, 0, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'121', N'121', CAST(N'2020-07-18' AS Date), CAST(N'2020-12-30' AS Date), 70000000, 5, N'001')
INSERT [dbo].[PhieuCamDo] ([MaPhieuCam], [MaHang], [NgayCam], [NgayChuoc], [SoTienCam], [LaiSuat], [MaNV]) VALUES (N'122', N'122', CAST(N'2020-07-23' AS Date), CAST(N'2020-09-25' AS Date), 10000000, 2, N'002')
GO
INSERT [dbo].[PhieuChuocDo] ([MaPhieuChuoc], [MaPhieuCam], [NgayKhachChuoc], [SoTienChuoc], [MaNV]) VALUES (N'003', N'001', CAST(N'2019-12-08' AS Date), 200000000, N'001')
INSERT [dbo].[PhieuChuocDo] ([MaPhieuChuoc], [MaPhieuCam], [NgayKhachChuoc], [SoTienChuoc], [MaNV]) VALUES (N'004', N'014', CAST(N'2019-12-17' AS Date), 8000000, N'002')
INSERT [dbo].[PhieuChuocDo] ([MaPhieuChuoc], [MaPhieuCam], [NgayKhachChuoc], [SoTienChuoc], [MaNV]) VALUES (N'121', N'121', CAST(N'2020-07-22' AS Date), 70000000, N'001')
INSERT [dbo].[PhieuChuocDo] ([MaPhieuChuoc], [MaPhieuCam], [NgayKhachChuoc], [SoTienChuoc], [MaNV]) VALUES (N'122', N'122', CAST(N'2020-07-23' AS Date), 10000000, N'002')
GO
INSERT [dbo].[PhieuTraGop] ([MaTraGop], [NgayTraGop], [MaPhieuCam], [SoTienKhachTra], [SoTienDuNo], [MaNV]) VALUES (N'001', CAST(N'2019-08-08' AS Date), N'001', 200000, 0, N'001')
INSERT [dbo].[PhieuTraGop] ([MaTraGop], [NgayTraGop], [MaPhieuCam], [SoTienKhachTra], [SoTienDuNo], [MaNV]) VALUES (N'002', CAST(N'2019-09-20' AS Date), N'002', 30000, NULL, N'002')
INSERT [dbo].[PhieuTraGop] ([MaTraGop], [NgayTraGop], [MaPhieuCam], [SoTienKhachTra], [SoTienDuNo], [MaNV]) VALUES (N'003', CAST(N'2019-12-01' AS Date), N'005', 20000, NULL, N'002')
INSERT [dbo].[PhieuTraGop] ([MaTraGop], [NgayTraGop], [MaPhieuCam], [SoTienKhachTra], [SoTienDuNo], [MaNV]) VALUES (N'004', CAST(N'2019-11-28' AS Date), N'001', 40000, NULL, N'003')
INSERT [dbo].[PhieuTraGop] ([MaTraGop], [NgayTraGop], [MaPhieuCam], [SoTienKhachTra], [SoTienDuNo], [MaNV]) VALUES (N'008', CAST(N'2020-07-23' AS Date), N'008', 5000000, 10000000, N'001')
INSERT [dbo].[PhieuTraGop] ([MaTraGop], [NgayTraGop], [MaPhieuCam], [SoTienKhachTra], [SoTienDuNo], [MaNV]) VALUES (N'009', CAST(N'2020-07-23' AS Date), N'008', 1000000, 5000000, N'001')
INSERT [dbo].[PhieuTraGop] ([MaTraGop], [NgayTraGop], [MaPhieuCam], [SoTienKhachTra], [SoTienDuNo], [MaNV]) VALUES (N'021', CAST(N'2020-07-23' AS Date), N'021', 20000, 50000, N'001')
GO
ALTER TABLE [dbo].[MatHang]  WITH CHECK ADD  CONSTRAINT [FK_MatHang_KhachHang] FOREIGN KEY([CMND])
REFERENCES [dbo].[KhachHang] ([CMND])
GO
ALTER TABLE [dbo].[MatHang] CHECK CONSTRAINT [FK_MatHang_KhachHang]
GO
ALTER TABLE [dbo].[PhieuCamDo]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCamDo_MatHang] FOREIGN KEY([MaHang])
REFERENCES [dbo].[MatHang] ([MaHang])
GO
ALTER TABLE [dbo].[PhieuCamDo] CHECK CONSTRAINT [FK_PhieuCamDo_MatHang]
GO
ALTER TABLE [dbo].[PhieuCamDo]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCamDo_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[PhieuCamDo] CHECK CONSTRAINT [FK_PhieuCamDo_NhanVien]
GO
ALTER TABLE [dbo].[PhieuChuocDo]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChuocDo_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[PhieuChuocDo] CHECK CONSTRAINT [FK_PhieuChuocDo_NhanVien]
GO
ALTER TABLE [dbo].[PhieuChuocDo]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChuocDo_PhieuCamDo] FOREIGN KEY([MaPhieuCam])
REFERENCES [dbo].[PhieuCamDo] ([MaPhieuCam])
GO
ALTER TABLE [dbo].[PhieuChuocDo] CHECK CONSTRAINT [FK_PhieuChuocDo_PhieuCamDo]
GO
ALTER TABLE [dbo].[PhieuTraGop]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTraGop_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[PhieuTraGop] CHECK CONSTRAINT [FK_PhieuTraGop_NhanVien]
GO
ALTER TABLE [dbo].[PhieuTraGop]  WITH CHECK ADD  CONSTRAINT [FK_PhieuTraGop_PhieuCamDo] FOREIGN KEY([MaPhieuCam])
REFERENCES [dbo].[PhieuCamDo] ([MaPhieuCam])
GO
ALTER TABLE [dbo].[PhieuTraGop] CHECK CONSTRAINT [FK_PhieuTraGop_PhieuCamDo]
GO
/****** Object:  StoredProcedure [dbo].[spCheckedUser]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Kiểm tra đăng nhập*/
CREATE proc [dbo].[spCheckedUser]
@MaNV nvarchar(50),
@MatKhau nvarchar(50),
@Quyen nvarchar(50)
AS 
SELECT * FROM NhanVien
WHERE MaNV=@MaNV and MatKhau=@MatKhau and Quyen=@Quyen
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCamDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteCamDo]
@MaPhieu nvarchar(50)
AS
DELETE FROM PhieuCamDo
WHERE MaPhieuCam = @MaPhieu
GO
/****** Object:  StoredProcedure [dbo].[spDeleteCamDoFromMaHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteCamDoFromMaHang]
@MaHang nvarchar(50)
AS
DELETE FROM PhieuCamDo
WHERE MaHang = @MaHang
GO
/****** Object:  StoredProcedure [dbo].[spDeleteChuocDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteChuocDo]
@MaPhieuChuoc nvarchar(50)
AS
DELETE FROM PhieuChuocDo
WHERE MaPhieuChuoc = @MaPhieuChuoc
GO
/****** Object:  StoredProcedure [dbo].[spDeleteChuocDoFromMaHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteChuocDoFromMaHang]
@MaHang nvarchar(50)
AS
DECLARE @MaPhieuCam nvarchar(50);
SET @MaPhieuCam= (SELECT PhieuCamDo.MaPhieuCam FROM PhieuChuocDo, PhieuCamDo WHERE MaHang=@MaHang AND PhieuCamDo.MaPhieuCam=PhieuChuocDo.MaPhieuCam)
DELETE FROM PhieuChuocDo
WHERE MaPhieuCam = @MaPhieuCam
GO
/****** Object:  StoredProcedure [dbo].[spDeleteChuocDoFromMaPhieuCam]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[spDeleteChuocDoFromMaPhieuCam]
@MaPhieuCam nvarchar(50)
AS
DELETE FROM PhieuChuocDo
WHERE MaPhieuCam = @MaPhieuCam
GO
/****** Object:  StoredProcedure [dbo].[spDeleteKhachHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteKhachHang]
@CMND nvarchar(50)
AS
DELETE FROM KhachHang
WHERE CMND = @CMND
GO
/****** Object:  StoredProcedure [dbo].[spDeleteMatHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteMatHang]
@MaHang nvarchar(50)
AS
DELETE FROM MatHang
WHERE MaHang = @MaHang
GO
/****** Object:  StoredProcedure [dbo].[spDeleteNhanVien]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteNhanVien]
@MaNV nvarchar(50)
AS
DELETE FROM NhanVien
WHERE MaNV = @MaNV
GO
/****** Object:  StoredProcedure [dbo].[spDeleteTraGop]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spDeleteTraGop]
@MaTraGop nvarchar(50)
AS
DELETE FROM PhieuTraGop
WHERE MaTraGop = @MaTraGop
GO
/****** Object:  StoredProcedure [dbo].[spDeleteTraGopFromMaHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[spDeleteTraGopFromMaHang]
@MaHang nvarchar(50)
AS
DECLARE @MaPhieuCam nvarchar(50);
SET @MaPhieuCam= (SELECT PhieuTraGop.MaPhieuCam FROM PhieuTraGop, PhieuCamDo WHERE MaHang=@MaHang AND PhieuCamDo.MaPhieuCam=PhieuTraGop.MaPhieuCam)
DELETE FROM PhieuTraGop
WHERE MaPhieuCam = @MaPhieuCam
GO
/****** Object:  StoredProcedure [dbo].[spDeleteTraGopFromMaPhieuCam]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[spDeleteTraGopFromMaPhieuCam]
@MaPhieuCam nvarchar(50)
AS
DELETE FROM PhieuTraGop
WHERE MaPhieuCam = @MaPhieuCam
GO
/****** Object:  StoredProcedure [dbo].[spInsertCamDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spInsertCamDo]
@MaPhieuCam nvarchar(50),
@MaHang nvarchar(50),
@NgayCam date,
@NgayChuoc date,
@SoTienCam int,
@LaiSuat int,
@MaNV nvarchar(50)
AS
INSERT INTO PhieuCamDo(MaPhieuCam, MaHang,NgayCam,NgayChuoc, SoTienCam,LaiSuat,MaNV)
VALUES(@MaPhieuCam,@MaHang,@NgayCam,@NgayChuoc,@SoTienCam,@LaiSuat, @MaNV)
GO
/****** Object:  StoredProcedure [dbo].[spInsertChuocDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertChuocDo]
@MaPhieuChuoc nvarchar(50),
@NgayChuoc date,
@SoTienChuoc int,
@MaPhieu nvarchar(50),
@MaNV nvarchar(50)
AS
INSERT INTO PhieuChuocDo(MaPhieuChuoc, NgayKhachChuoc, SoTienChuoc, MaPhieuCam, MaNV)
VALUES (@MaPhieuChuoc, @NgayChuoc, @SoTienChuoc, @MaPhieu, @MaNV);
GO
/****** Object:  StoredProcedure [dbo].[spInsertKhachHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertKhachHang]
@CMND nvarchar(50),
@Ten nvarchar(50),
@DiaChi nvarchar(50),
@SoDT nvarchar(50),
@NgaySinh date,
@NoiCap nvarchar(50),
@GioiTinh nvarchar(50)

AS
INSERT INTO KhachHang(CMND, Ten, DiaChi, SoDT,NgaySinh, NoiCap, GioiTinh)
VALUES (@CMND, @Ten,@DiaChi,@SoDT,@NgaySinh,@NoiCap,@GioiTinh);
GO
/****** Object:  StoredProcedure [dbo].[spInsertMatHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertMatHang]
@MaHang nvarchar(50),
@LoaiHang nvarchar(50),
@TenHang nvarchar(50), 
@GiaTri float,
@CMND nvarchar(50)
AS
INSERT INTO MatHang (MaHang, LoaiHang, TenHang, GiaTri, CMND)
VALUES(@MaHang, @LoaiHang, @TenHang, @GiaTri, @CMND)
GO
/****** Object:  StoredProcedure [dbo].[spInsertNhanVien]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spInsertNhanVien]
@MaNV nvarchar(50),
@Email nvarchar(50),
@MatKhau nvarchar(50),
@Ten nvarchar(50),
@GioiTinh nvarchar(50),
@SoDT nvarchar(50),
@DiaChi nvarchar(50),
@Quyen nvarchar(50)
AS
INSERT INTO NhanVien(MaNV, Email, MatKhau,Ten, GioiTinh, SoDT, DiaChi,Quyen)
VALUES (@MaNV,@Email, @MatKhau, @Ten, @GioiTinh, @SoDT, @DiaChi, @Quyen);
GO
/****** Object:  StoredProcedure [dbo].[spInsertTraGop]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Procedure Thêm Phiếu Trả Góp*/
CREATE PROC [dbo].[spInsertTraGop]
@MaPhieuTraGop nvarchar(50),
@NgayTraGop date,
@SoTienTraGop int,
@SoTieDuNo int,
@MaPhieu nvarchar(50),
@MaNV nvarchar(50)
AS
INSERT INTO PhieuTraGop(MaTraGop, NgayTraGop, SoTienKhachTra,SoTienDuNo,MaPhieuCam,MaNV)
VALUES (@MaPhieuTraGop, @NgayTraGop, @SoTienTraGop,@SoTieDuNo, @MaPhieu,@MaNV);
GO
/****** Object:  StoredProcedure [dbo].[spLoadCamDoByMaHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spLoadCamDoByMaHang]
@MaHang nvarchar(50)
AS
SELECT MaPhieuCam as [Mã phiếu cầm], PhieuCamDo.MaHang as [Mã hàng], NgayCam as [Ngày cầm đồ], NgayChuoc as [Ngày quá hạn], SoTienCam as [Số tiền cầm], dbo.fnTinhLaiSuat(NgayCam, NgayChuoc) as [Lãi suất],TenHang as [Tên món hàng],dbo.fnTinhTienChuoc(NgayCam,LaiSuat,SoTienCam,MaPhieuCam,dbo.fnTinhTrang(PhieuCamDo.MaHang)) as [Số tiền dư nợ], MaNV as [Mã NV]
FROM PhieuCamDo, MatHang
WHERE PhieuCamDo.MaHang = @MaHang and PhieuCamDo.MaHang = MatHang.MaHang;
GO
/****** Object:  StoredProcedure [dbo].[spLoadCamDoByMaKH]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spLoadCamDoByMaKH]
@MaKH nvarchar(50)
AS
SELECT MaPhieuCam as [Mã phiếu cầm],PhieuCamDo.MaHang as [Mã hàng], NgayCam as [Ngày cầm đồ], NgayChuoc as [Ngày quá hạn], SoTienCam as [Số tiền cầm], dbo.fnTinhLaiSuat(NgayCam, NgayChuoc) as [Lãi suất], TenHang as [Tên món hàng],dbo.fnTinhTienChuoc(NgayCam,LaiSuat,SoTienCam,MaPhieuCam,dbo.fnTinhTrang(PhieuCamDo.MaHang)) as [Số tiền dư nợ], MaNV as [Mã NV]
FROM PhieuCamDo, MatHang, KhachHang
WHERE KhachHang.CMND = @MaKH and MatHang.MaHang = PhieuCamDo.MaHang and KhachHang.CMND=MatHang.CMND
GO
/****** Object:  StoredProcedure [dbo].[spLoadChuocDoByCamDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Lấy thông tin Chuộc đồ từ phiếu cầm đồ*/
CREATE PROC [dbo].[spLoadChuocDoByCamDo]
@MaPhieu nvarchar(50)
AS
SELECT MaPhieuChuoc as [Mã phiếu chuộc], NgayKhachChuoc as [Ngày chuộc], SoTienChuoc as [Số tiền chuộc],PhieuChuocDo.MaPhieuCam as [Mã phiếu cầm], PhieuChuocDo.MaNV as [Mã nhân viên] FROM PhieuChuocDo,PhieuCamDo
WHERE PhieuChuocDo.MaPhieuCam like @MaPhieu and PhieuCamDo.MaPhieuCam=PhieuChuocDo.MaPhieuCam
GO
/****** Object:  StoredProcedure [dbo].[spLoadKhachHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spLoadKhachHang]
AS
SELECT * FROM SearchKhachHang_View
GO
/****** Object:  StoredProcedure [dbo].[spLoadMatHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spLoadMatHang]
AS
SELECT MaHang as [Mã hàng], LoaiHang as [Loại Hàng], TenHang as [Tên món hàng], GiaTri as [Gía trị thực], dbo.fnTinhTrang(MaHang) as [Tình trạng], CMND as [CMND] FROM MatHang
GO
/****** Object:  StoredProcedure [dbo].[spLoadMatHangByCMND]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Tìm kiếm Mặt hàng theo CMND của khách hàng*/
CREATE PROC [dbo].[spLoadMatHangByCMND]
@CMND nvarchar(50)
AS
SELECT MaHang as [Mã hàng], LoaiHang as [Loại Hàng], TenHang as [Tên món hàng], GiaTri as [Gía trị thực], dbo.fnTinhTrang(MaHang) as [Tình trạng], Khachhang.CMND as [CMND] FROM MatHang, KhachHang
WHERE KhachHang.CMND = @CMND and KhachHang.CMND=MatHang.CMND
GO
/****** Object:  StoredProcedure [dbo].[spLoadMatHangByMaPhieuCam]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spLoadMatHangByMaPhieuCam]
@MaPhieu nvarchar(50)
AS
SELECT MatHang.MaHang as [Mã hàng] , LoaiHang as [Loại hàng], TenHang as [Tên hàng], GiaTri as [Giá trị thực], PhieuChuocDo.NgayKhachChuoc as [Ngày chuộc], SoTienChuoc as [Số tiền chuộc], MaPhieuChuoc as [Mã phiếu chuộc] FROM MatHang, PhieuChuocDo, PhieuCamDo
WHERE PhieuCamDo.MaPhieuCam = @MaPhieu and MatHang.MaHang = PhieuCamDo.MaHang and PhieuChuocDo.MaPhieuCam=PhieuCamDo.MaPhieuCam
GO
/****** Object:  StoredProcedure [dbo].[spLoadNhanVien]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spLoadNhanVien]
AS
SELECT MaNV as [Mã NV], Email as [Email]  , MatKhau as [Mật khẩu], Ten as [Họ và tên], GioiTinh as [Giới tính], SoDT as [Số điện thoại], DiaChi as [Địa chỉ] , Quyen as [Quyền] From NhanVien
GO
/****** Object:  StoredProcedure [dbo].[spLoadTraGopByCamDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Lấy thông tin trả góp từ phiếu cầm đồ*/
CREATE PROC [dbo].[spLoadTraGopByCamDo]
@MaPhieu nvarchar(50)
AS
SELECT MaTraGop as [Mã trả góp], NgayTraGop as [Ngày trả góp], SoTienKhachTra as [Số tiền khách trả], ((SELECT SoTienDuNo FROM PhieuCamDo WHERE MaPhieuCam=@MaPhieu)-SoTienKhachTra) as [Số tiền dư nợ],PhieuTraGop.MaPhieuCam as [Mã phiếu cầm], PhieuTraGop.MaNV as [Mã nhân viên] FROM PhieuTraGop,PhieuCamDo
WHERE PhieuTraGop.MaPhieuCam like @MaPhieu and PhieuCamDo.MaPhieuCam=PhieuTraGop.MaPhieuCam
GO
/****** Object:  StoredProcedure [dbo].[spSearchCamDoByTenKhach]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spSearchCamDoByTenKhach]
@TenKhach nvarchar(50)
AS
SELECT MaPhieuCam as [Mã phiếu cầm], NgayCam as [Ngày cầm đồ], NgayChuoc as [Ngày chuộc], SoTienCam as [Số tiền cầm], LaiSuat as [Lãi suất],KhachHang.CMND as [CMND], KhachHang.Ten as [Họ và tên], MatHang.MaHang as [Mặt hàng], LoaiHang as [Loại hàng], TenHang as [Tên hàng], GiaTri as [Giá trị thực]
FROM PhieuCamDo, KhachHang, MatHang
WHERE Ten = @TenKhach and PhieuCamDo.MaHang=MatHang.MaHang;
GO
/****** Object:  StoredProcedure [dbo].[spSearchKhachHangBySDT]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Tìm kiếm khách hàng theo Số điện thoại*/
CREATE PROC [dbo].[spSearchKhachHangBySDT]
@SDT nvarchar(50)
AS
SELECT * FROM SearchKhachHang_View
WHERE SearchKhachHang_View.[Số điện thoại] like @SDT
GO
/****** Object:  StoredProcedure [dbo].[spSearchKhachHangByTen]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[spLoadKhachHangByTen]    Script Date: 8/11/2019 4:20:42 AM ******/

/*Tìm kiếm khách hàng theo Tên*/
CREATE PROC [dbo].[spSearchKhachHangByTen]
@Ten nvarchar(50)
AS
SELECT * FROM SearchKhachHang_View
WHERE SearchKhachHang_View.[Họ và tên] like @Ten
GO
/****** Object:  StoredProcedure [dbo].[spSearchMatHangByTenMatHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spSearchMatHangByTenMatHang]
@TenHang nvarchar(50)
AS
SELECT MaHang as [Mã hàng], LoaiHang as [Loại Hàng], TenHang as [Tên món hàng], GiaTri as [Gía trị thực], TinhTrang as [Tình trạng], KhachHang.CMND as [CMND]
FROM KhachHang, MatHang
WHERE KhachHang.CMND = MatHang.CMND and TenHang like @TenHang 
GO
/****** Object:  StoredProcedure [dbo].[spSearchMHByCMND]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spSearchMHByCMND]
@CMND nvarchar(50)
AS
SELECT MaHang as [Mã hàng], LoaiHang as [Loại Hàng], TenHang as [Tên món hàng], GiaTri as [Gía trị thực], TinhTrang as [Tình trạng], KhachHang.CMND as [CMND]
FROM KhachHang, MatHang
WHERE KhachHang.CMND = MatHang.CMND and MatHang.CMND like @CMND
GO
/****** Object:  StoredProcedure [dbo].[spSearchNhanVienBySDT]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*View tìm kiếm nhân viên theo Số điện thoại*/
CREATE PROC [dbo].[spSearchNhanVienBySDT]
@SDT nvarchar(50)
AS
SELECT * FROM SearchNhanVien_View
WHERE SearchNhanVien_View.[Số điện thoại] like @SDT
GO
/****** Object:  StoredProcedure [dbo].[spSearchNhanVienByTen]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*View tìm kiếm nhân viên theo Tên*/
CREATE PROC [dbo].[spSearchNhanVienByTen]
@Ten nvarchar(50)
AS
SELECT * FROM SearchNhanVien_View
WHERE SearchNhanVien_View.[Họ và tên] like @Ten
GO
/****** Object:  StoredProcedure [dbo].[spThongKePhieuCamDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spThongKePhieuCamDo]
@NgayBatDau date,
@NgayHetHan date
AS
SELECT MaPhieuCam as [Mã phiếu cầm], NgayCam [Ngày cầm], SoTienCam as [Tiền cầm], NhanVien.MaNV as [Mã NV],  NhanVien.Ten as [Tên NV]
FROM PhieuCamDo, NhanVien
WHERE PhieuCamDo.MaNV = NhanVien.MaNV and NgayCam >= @NgayBatDau and NgayCam <= @NgayHetHan
GO
/****** Object:  StoredProcedure [dbo].[spThongKePhieuChuocDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spThongKePhieuChuocDo]
@NgayBatDau date,
@NgayHetHan date
AS
SELECT MaPhieuChuoc as [Mã phiếu chuộc], NgayKhachChuoc as [Ngày chuộc], SoTienChuoc as [Tiền chuộc], NhanVien.MaNV as [Mã NV], NhanVien.Ten as [Tên NV]
FROM PhieuChuocDo, NhanVien
WHERE PhieuChuocDo.MaNV = NhanVien.MaNV and NgayKhachChuoc >= @NgayBatDau and NgayKhachChuoc <= @NgayHetHan
GO
/****** Object:  StoredProcedure [dbo].[spThongKePhieuTraGop]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spThongKePhieuTraGop]
@NgayBatDau date,
@NgayHetHan date
AS
SELECT MaTraGop as [Mã trả góp], NgayTraGop as [Ngày trả góp], SoTienKhachTra as [Tiền trả góp] ,NhanVien.MaNV as [Mã NV], NhanVien.Ten as [Tên NV]
FROM PhieuTraGop, NhanVien
WHERE PhieuTraGop.MaNV = NhanVien.MaNV and NgayTraGop >= @NgayBatDau and NgayTraGop <= @NgayHetHan
GO
/****** Object:  StoredProcedure [dbo].[spUpdateCamDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateCamDo]
@MaPhieuCam nvarchar(50),
@MaHang nvarchar(50),
@NgayCam date,
@NgayChuoc date,
@SoTienCam int,
@LaiSuat int,
@MaNV nvarchar(50)
AS
UPDATE PhieuCamDo SET  
MaHang = @MaHang, NgayCam = @NgayCam, NgayChuoc=@NgayChuoc,SoTienCam = @SoTienCam,  LaiSuat = @LaiSuat , MaNV=@MaNV
WHERE MaPhieuCam = @MaPhieuCam  
GO
/****** Object:  StoredProcedure [dbo].[spUpdateChuocDo]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateChuocDo]
@MaPhieuChuoc nvarchar(50),
@NgayChuoc date,
@SoTienChuoc int,
@MaPhieuCam nvarchar(50),
@MaNV nvarchar(50)
AS
UPDATE PhieuChuocDo SET  
MaPhieuCam = @MaPhieuCam, NgayKhachChuoc = @NgayChuoc, SoTienChuoc = @SoTienChuoc, MaNV = @MaNV
WHERE MaPhieuChuoc = @MaPhieuChuoc
GO
/****** Object:  StoredProcedure [dbo].[spUpdateKhachHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateKhachHang]
@CMND nvarchar(50),
@Ten nvarchar(50),
@DiaChi nvarchar(50),
@SoDT nvarchar(50),
@NgaySinh date,
@NoiCap nvarchar(50),
@GioiTinh nvarchar(50)
AS
UPDATE KhachHang SET  
Ten = @Ten, DiaChi = @DiaChi, SoDT = @SoDT, NgaySinh = @NgaySinh, NoiCap = @NoiCap, GioiTinh = @GioiTinh
WHERE CMND = @CMND  
GO
/****** Object:  StoredProcedure [dbo].[spUpdateMatHang]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateMatHang]
@MaHang nvarchar(50),
@LoaiHang nvarchar(50),
@ChiTiet nvarchar(50), 
@GiaTri int,
@CMND nvarchar(50)
AS
UPDATE MatHang SET  
LoaiHang = @LoaiHang, TenHang = @ChiTiet, GiaTri = @GiaTri, CMND = @CMND
WHERE MaHang = @MaHang  
GO
/****** Object:  StoredProcedure [dbo].[spUpdateNhanVien]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateNhanVien]
@MaNV nvarchar(50),
@Email nvarchar(50),
@MatKhau nvarchar(50),
@Ten nvarchar(50),
@GioiTinh nvarchar(50),
@SoDT nvarchar(50),
@DiaChi nvarchar(50),
@Quyen nvarchar(50)
AS
UPDATE NhanVien SET  Email = @Email,
MatKhau = @MatKhau, Ten = @Ten, GioiTinh = @GioiTinh, SoDT = @SoDT, DiaChi = @DiaChi, Quyen = @Quyen
WHERE MaNV = @MaNV
GO
/****** Object:  StoredProcedure [dbo].[spUpdateTraGop]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[spUpdateTraGop]
@MaPhieuTraGop nvarchar(50),
@NgayTraGop date,
@SoTienTraGop int,
@SoTienDuNo int,
@MaPhieuCam nvarchar(50),
@MaNV nvarchar(50)
AS
UPDATE PhieuTraGop SET  
NgayTraGop = @NgayTraGop, SoTienKhachTra = @SoTienTraGop,SoTienDuNo=@SoTienDuNo,MaPhieuCam = @MaPhieuCam, MaNV = @MaNV
WHERE MaTraGop = @MaPhieuTraGop
GO
/****** Object:  Trigger [dbo].[PhieuCamDo_INSERT]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[PhieuCamDo_INSERT]
ON [dbo].[PhieuCamDo]
AFTER INSERT
AS
BEGIN
	UPDATE MatHang
	SET TinhTrang = N'Còn Hạn'
	From MatHang Join inserted on MatHang.MaHang = inserted.MaHang
END	
GO
ALTER TABLE [dbo].[PhieuCamDo] ENABLE TRIGGER [PhieuCamDo_INSERT]
GO
/****** Object:  Trigger [dbo].[PhieuChuocDo_INSERT]    Script Date: 7/23/2020 1:24:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[PhieuChuocDo_INSERT]
ON [dbo].[PhieuChuocDo]
AFTER INSERT
AS
BEGIN
	UPDATE MatHang
	SET TinhTrang = N'Đã Chuộc'
	From MatHang, PhieuCamDo, inserted 
	Where MatHang.MaHang = PhieuCamDo.MaHang and PhieuCamDo.MaPhieuCam = inserted.MaPhieuCam
END	
GO
ALTER TABLE [dbo].[PhieuChuocDo] ENABLE TRIGGER [PhieuChuocDo_INSERT]
GO
USE [master]
GO
ALTER DATABASE [CamDo] SET  READ_WRITE 
GO
