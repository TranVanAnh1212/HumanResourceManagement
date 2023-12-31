USE MASTER
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'QLNhanSu')
DROP DATABASE QLNhanSu
GO

CREATE DATABASE QLNhanSu
GO

USE [QLNhanSu]
GO
/****** Object:  UserDefinedFunction [dbo].[GenerateEmployeeCode]    Script Date: 26/12/2023 8:31:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GenerateEmployeeCode]()
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @MaxCode INT
    DECLARE @NewCode VARCHAR(10)

    SELECT @MaxCode = ISNULL(MAX(SUBSTRING(maNhanVien, 3, 5)), 0) FROM NhanVien

    SET @NewCode = 'NV' + RIGHT('00000' + CAST(@MaxCode + 1 AS VARCHAR(5)), 5)

    RETURN @NewCode
END
GO
/****** Object:  Table [dbo].[BacLuong]    Script Date: 26/12/2023 8:31:07 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BacLuong](
	[heSoLuong] [decimal](5, 2) NOT NULL,
	[luongCoBan] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[heSoLuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCaoDangNhap]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCaoDangNhap](
	[idDangNhap] [int] IDENTITY(1,1) NOT NULL,
	[maTaiKhoan] [int] NOT NULL,
	[tgDangNhap] [datetime] NOT NULL,
	[tgDangXuat] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChamCong]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChamCong](
	[maChamCong] [int] IDENTITY(1,1) NOT NULL,
	[maNhanVien] [varchar](10) NOT NULL,
	[thang] [int] NOT NULL,
	[nam] [int] NOT NULL,
	[heSoLuong] [decimal](5, 2) NOT NULL,
	[SoNgayCong] [int] NULL,
	[ungTruocLuong] [decimal](18, 0) NULL,
	[conLai] [decimal](18, 0) NULL,
	[nghiPhep] [int] NULL,
	[soGioTangCa] [int] NULL,
	[luongTangCa] [decimal](18, 0) NULL,
	[phuCapCongViec] [decimal](18, 0) NULL,
	[tongNhan] [decimal](18, 1) NULL,
PRIMARY KEY CLUSTERED 
(
	[maChamCong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chitiet_Quyen]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chitiet_Quyen](
	[maChitietQuyen] [varchar](50) NOT NULL,
	[tenhanhDong] [nvarchar](100) NULL,
	[mahanhDong] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[maChitietQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietQuyen_Quyen]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietQuyen_Quyen](
	[maQuyen] [varchar](50) NOT NULL,
	[maChitietQuyen] [varchar](50) NOT NULL,
	[moTa] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[maQuyen] ASC,
	[maChitietQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[maChucVu] [int] IDENTITY(1,1) NOT NULL,
	[tenChucVu] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maChucVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuyenCongTac]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuyenCongTac](
	[soQuyetDinh] [varchar](100) NOT NULL,
	[ngayQuyetDinh] [date] NULL,
	[thoiGianThiHanh] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[soQuyetDinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuyenCongTac_NhanVien]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuyenCongTac_NhanVien](
	[soQuyetDinh] [varchar](100) NOT NULL,
	[maNhanVien] [varchar](10) NOT NULL,
	[chucVuCu] [int] NULL,
	[phongBanCu] [int] NULL,
	[chucVuMoi] [int] NULL,
	[phongBanMoi] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[soQuyetDinh] ASC,
	[maNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChuyenMon]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChuyenMon](
	[maChuyenMon] [int] IDENTITY(1,1) NOT NULL,
	[tenChuyenMon] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maChuyenMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanToc]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanToc](
	[maDanToc] [int] IDENTITY(1,1) NOT NULL,
	[tenDanToc] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maDanToc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HopDong]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopDong](
	[maHopDong] [int] IDENTITY(1,1) NOT NULL,
	[soHopDong] [varchar](50) NOT NULL,
	[ngayKyHD] [datetime] NULL,
	[ngayKetThucHD] [datetime] NULL,
	[loaiHopDong] [nvarchar](255) NULL,
	[thoiHanHD] [nvarchar](100) NULL,
	[tinhTrangChuKi] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[maHopDong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoSo]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoSo](
	[maHoSo] [int] IDENTITY(1,1) NOT NULL,
	[soYeuLyLich] [nvarchar](100) NOT NULL,
	[giayKhaiSinh] [nvarchar](100) NOT NULL,
	[soHoKhau] [nvarchar](100) NOT NULL,
	[bangTotNghiep] [nvarchar](100) NOT NULL,
	[giayKhamSK] [nvarchar](100) NOT NULL,
	[anhThe] [nvarchar](100) NOT NULL,
	[tinhTrangHoSo] [nvarchar](100) NOT NULL,
	[hinhThucThanhToanLuong] [nvarchar](100) NOT NULL,
	[soTkNganHang] [varchar](50) NOT NULL,
	[nganHang] [nvarchar](100) NOT NULL,
	[maSoThue] [char](100) NOT NULL,
	[maSoBHXH] [char](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maHoSo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[maNhanVien] [varchar](10) NOT NULL,
	[tenNhanVien] [nvarchar](255) NOT NULL,
	[gioiTinh] [nvarchar](10) NOT NULL,
	[ngaySinh] [date] NOT NULL,
	[CCCD] [char](100) NOT NULL,
	[dienThoai] [char](11) NULL,
	[noiOHienTai] [nvarchar](255) NOT NULL,
	[queQuan] [nvarchar](255) NOT NULL,
	[maHoSo] [int] NULL,
	[maTrinhDo] [int] NOT NULL,
	[maTonGiao] [int] NOT NULL,
	[maChuyenMon] [int] NOT NULL,
	[maDanToc] [int] NOT NULL,
	[maChucVu] [int] NOT NULL,
	[maHopDong] [int] NULL,
	[maPhong] [int] NOT NULL,
	[anhThe] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[maNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongBan]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongBan](
	[maPhong] [int] IDENTITY(1,1) NOT NULL,
	[tenPhong] [nvarchar](255) NOT NULL,
	[dienThoai] [char](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[maPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quyen]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quyen](
	[maQuyen] [varchar](50) NOT NULL,
	[tenQuyen] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maQuyen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[maTaiKhoan] [int] IDENTITY(1,1) NOT NULL,
	[tenTaiKhoan] [varchar](255) NOT NULL,
	[matKhau] [varchar](255) NOT NULL,
	[maQuyen] [varchar](50) NOT NULL,
	[trangThai] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TonGiao]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TonGiao](
	[maTonGiao] [int] IDENTITY(1,1) NOT NULL,
	[tenTonGiao] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maTonGiao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrinhDo]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrinhDo](
	[maTrinhDo] [int] IDENTITY(1,1) NOT NULL,
	[tenTrinhDo] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maTrinhDo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BacLuong] ([heSoLuong], [luongCoBan]) VALUES (CAST(2.20 AS Decimal(5, 2)), CAST(4500000 AS Decimal(18, 0)))
INSERT [dbo].[BacLuong] ([heSoLuong], [luongCoBan]) VALUES (CAST(2.40 AS Decimal(5, 2)), CAST(5500000 AS Decimal(18, 0)))
INSERT [dbo].[BacLuong] ([heSoLuong], [luongCoBan]) VALUES (CAST(2.60 AS Decimal(5, 2)), CAST(7500000 AS Decimal(18, 0)))
INSERT [dbo].[BacLuong] ([heSoLuong], [luongCoBan]) VALUES (CAST(2.80 AS Decimal(5, 2)), CAST(10500000 AS Decimal(18, 0)))
INSERT [dbo].[BacLuong] ([heSoLuong], [luongCoBan]) VALUES (CAST(2.90 AS Decimal(5, 2)), CAST(12000000 AS Decimal(18, 0)))
INSERT [dbo].[BacLuong] ([heSoLuong], [luongCoBan]) VALUES (CAST(3.00 AS Decimal(5, 2)), CAST(15000000 AS Decimal(18, 0)))
INSERT [dbo].[BacLuong] ([heSoLuong], [luongCoBan]) VALUES (CAST(3.40 AS Decimal(5, 2)), CAST(20000000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[BaoCaoDangNhap] ON 

INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (1, 1, CAST(N'2023-12-15T14:07:51.897' AS DateTime), CAST(N'2023-12-15T14:10:08.493' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (2, 1, CAST(N'2023-12-15T15:35:31.960' AS DateTime), CAST(N'2023-12-15T15:39:07.720' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (3, 1, CAST(N'2023-12-15T18:17:19.637' AS DateTime), CAST(N'2023-12-15T18:25:29.553' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (4, 1, CAST(N'2023-12-15T21:11:43.070' AS DateTime), CAST(N'2023-12-15T21:12:01.277' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (5, 1, CAST(N'2023-12-15T21:15:57.470' AS DateTime), CAST(N'2023-12-15T21:18:50.187' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (6, 1, CAST(N'2023-12-15T21:47:13.490' AS DateTime), CAST(N'2023-12-15T21:48:44.950' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (7, 1, CAST(N'2023-12-15T22:17:22.037' AS DateTime), CAST(N'2023-12-15T22:18:17.223' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (8, 1, CAST(N'2023-12-15T22:26:23.000' AS DateTime), CAST(N'2023-12-15T22:27:03.417' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (9, 1, CAST(N'2023-12-15T22:38:42.437' AS DateTime), CAST(N'2023-12-15T22:40:28.563' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (10, 1, CAST(N'2023-12-15T22:43:06.937' AS DateTime), CAST(N'2023-12-15T22:45:16.450' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (11, 1, CAST(N'2023-12-16T08:31:50.667' AS DateTime), CAST(N'2023-12-16T08:33:24.260' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (12, 1, CAST(N'2023-12-16T08:45:01.500' AS DateTime), CAST(N'2023-12-16T08:46:24.450' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (13, 1, CAST(N'2023-12-16T08:46:47.580' AS DateTime), CAST(N'2023-12-16T08:47:00.747' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (14, 1, CAST(N'2023-12-16T09:39:57.227' AS DateTime), CAST(N'2023-12-16T09:42:00.777' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (15, 1, CAST(N'2023-12-19T09:21:58.870' AS DateTime), CAST(N'2023-12-19T09:24:24.510' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (16, 1, CAST(N'2023-12-19T10:03:44.407' AS DateTime), CAST(N'2023-12-19T10:04:04.893' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (17, 1, CAST(N'2023-12-19T10:05:34.243' AS DateTime), CAST(N'2023-12-19T10:17:31.857' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (18, 1, CAST(N'2023-12-25T10:14:37.243' AS DateTime), CAST(N'2023-12-25T11:14:01.473' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (19, 1, CAST(N'2023-12-25T11:22:07.523' AS DateTime), CAST(N'2023-12-25T11:22:48.153' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (20, 1, CAST(N'2023-12-25T11:31:04.637' AS DateTime), CAST(N'2023-12-25T11:34:26.057' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (21, 1, CAST(N'2023-12-25T11:43:54.250' AS DateTime), CAST(N'2023-12-25T11:44:09.193' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (22, 1, CAST(N'2023-12-25T11:58:28.730' AS DateTime), CAST(N'2023-12-25T12:00:06.317' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (23, 1, CAST(N'2023-12-25T15:40:31.650' AS DateTime), CAST(N'2023-12-25T15:41:08.170' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (24, 1, CAST(N'2023-12-25T15:42:55.870' AS DateTime), CAST(N'2023-12-25T15:43:40.770' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (25, 1, CAST(N'2023-12-25T16:10:29.600' AS DateTime), CAST(N'2023-12-25T16:11:07.943' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (26, 1, CAST(N'2023-12-25T16:16:21.213' AS DateTime), CAST(N'2023-12-25T16:17:21.030' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (27, 1, CAST(N'2023-12-25T16:26:34.853' AS DateTime), CAST(N'2023-12-25T16:28:41.510' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (28, 1, CAST(N'2023-12-25T16:29:59.533' AS DateTime), CAST(N'2023-12-25T16:31:18.477' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (29, 1, CAST(N'2023-12-25T16:58:34.873' AS DateTime), CAST(N'2023-12-25T16:58:56.223' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (30, 1, CAST(N'2023-12-25T18:03:51.707' AS DateTime), CAST(N'2023-12-25T18:04:05.013' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (31, 1, CAST(N'2023-12-25T18:07:47.893' AS DateTime), CAST(N'2023-12-25T18:08:33.253' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (32, 1, CAST(N'2023-12-25T18:19:02.420' AS DateTime), CAST(N'2023-12-25T18:19:31.350' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (33, 1, CAST(N'2023-12-25T18:20:02.507' AS DateTime), CAST(N'2023-12-25T18:20:14.080' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (34, 1, CAST(N'2023-12-25T18:22:52.440' AS DateTime), CAST(N'2023-12-25T18:23:05.127' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (35, 1, CAST(N'2023-12-26T09:42:36.460' AS DateTime), CAST(N'2023-12-26T09:43:15.260' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (36, 1, CAST(N'2023-12-26T10:34:47.750' AS DateTime), CAST(N'2023-12-26T10:41:11.713' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (37, 1, CAST(N'2023-12-26T11:56:57.187' AS DateTime), CAST(N'2023-12-26T11:58:41.107' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (38, 1, CAST(N'2023-12-26T12:19:24.013' AS DateTime), CAST(N'2023-12-26T12:20:04.863' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (39, 1, CAST(N'2023-12-26T12:24:06.700' AS DateTime), CAST(N'2023-12-26T12:24:33.767' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (40, 1, CAST(N'2023-12-26T12:59:40.987' AS DateTime), CAST(N'2023-12-26T13:01:37.123' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (41, 1, CAST(N'2023-12-26T13:46:54.120' AS DateTime), CAST(N'2023-12-26T13:47:31.333' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (42, 1, CAST(N'2023-12-26T14:04:24.123' AS DateTime), CAST(N'2023-12-26T14:04:46.247' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (43, 1, CAST(N'2023-12-26T14:12:53.123' AS DateTime), CAST(N'2023-12-26T14:13:05.760' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (44, 1, CAST(N'2023-12-26T14:18:42.410' AS DateTime), CAST(N'2023-12-26T14:19:01.650' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (45, 1, CAST(N'2023-12-26T14:25:58.300' AS DateTime), CAST(N'2023-12-26T14:26:30.283' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (46, 1, CAST(N'2023-12-26T14:30:30.037' AS DateTime), CAST(N'2023-12-26T14:46:51.023' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (47, 1, CAST(N'2023-12-26T14:59:42.623' AS DateTime), CAST(N'2023-12-26T15:00:10.457' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (48, 1, CAST(N'2023-12-26T15:34:47.577' AS DateTime), CAST(N'2023-12-26T15:35:02.477' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (49, 1, CAST(N'2023-12-26T15:41:30.967' AS DateTime), CAST(N'2023-12-26T15:41:51.853' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (50, 1, CAST(N'2023-12-26T15:42:11.327' AS DateTime), CAST(N'2023-12-26T15:47:03.417' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (51, 1, CAST(N'2023-12-26T17:51:37.450' AS DateTime), CAST(N'2023-12-26T17:57:17.480' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (52, 1, CAST(N'2023-12-26T18:16:16.133' AS DateTime), CAST(N'2023-12-26T18:19:55.480' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (53, 1, CAST(N'2023-12-26T18:23:01.430' AS DateTime), CAST(N'2023-12-26T18:28:09.867' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (54, 1, CAST(N'2023-12-26T18:28:57.217' AS DateTime), CAST(N'2023-12-26T18:33:58.043' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (55, 1, CAST(N'2023-12-26T18:50:22.977' AS DateTime), CAST(N'2023-12-26T18:51:19.130' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (56, 1, CAST(N'2023-12-26T19:12:40.433' AS DateTime), CAST(N'2023-12-26T19:13:08.863' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (57, 1, CAST(N'2023-12-26T19:15:23.430' AS DateTime), CAST(N'2023-12-26T19:20:53.417' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (58, 1, CAST(N'2023-12-26T20:14:09.143' AS DateTime), CAST(N'2023-12-26T20:14:15.557' AS DateTime))
INSERT [dbo].[BaoCaoDangNhap] ([idDangNhap], [maTaiKhoan], [tgDangNhap], [tgDangXuat]) VALUES (59, 1, CAST(N'2023-12-26T20:27:10.977' AS DateTime), CAST(N'2023-12-26T20:27:16.613' AS DateTime))
SET IDENTITY_INSERT [dbo].[BaoCaoDangNhap] OFF
GO
SET IDENTITY_INSERT [dbo].[ChamCong] ON 

INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (1, N'NV00001', 12, 2023, CAST(2.80 AS Decimal(5, 2)), 22, CAST(2000000 AS Decimal(18, 0)), CAST(8185096 AS Decimal(18, 0)), 3, 9, CAST(454327 AS Decimal(18, 0)), CAST(1000000 AS Decimal(18, 0)), CAST(10185096.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (2, N'NV00001', 11, 2023, CAST(2.60 AS Decimal(5, 2)), 22, CAST(200000 AS Decimal(18, 0)), CAST(6748077 AS Decimal(18, 0)), 3, 12, CAST(432692 AS Decimal(18, 0)), CAST(200000 AS Decimal(18, 0)), CAST(6948077.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (3, N'NV00002', 12, 2023, CAST(2.40 AS Decimal(5, 2)), 22, CAST(3000000 AS Decimal(18, 0)), CAST(2225000 AS Decimal(18, 0)), 6, 12, CAST(317308 AS Decimal(18, 0)), CAST(300000 AS Decimal(18, 0)), CAST(5225000.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (4, N'NV00004', 12, 2023, CAST(2.90 AS Decimal(5, 2)), 23, CAST(4132234 AS Decimal(18, 0)), CAST(7298535 AS Decimal(18, 0)), 4, 8, CAST(461538 AS Decimal(18, 0)), CAST(400000 AS Decimal(18, 0)), CAST(11430769.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (5, N'NV00003', 12, 2023, CAST(3.00 AS Decimal(5, 2)), 26, CAST(123123 AS Decimal(18, 0)), CAST(16050723 AS Decimal(18, 0)), 6, 16, CAST(1153846 AS Decimal(18, 0)), CAST(20000 AS Decimal(18, 0)), CAST(16173846.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (6, N'NV00002', 11, 2023, CAST(2.60 AS Decimal(5, 2)), 26, CAST(3423423 AS Decimal(18, 0)), CAST(4961962 AS Decimal(18, 0)), 12, 24, CAST(865385 AS Decimal(18, 0)), CAST(20000 AS Decimal(18, 0)), CAST(8385385.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (7, N'NV00004', 11, 2023, CAST(2.60 AS Decimal(5, 2)), 25, CAST(2300000 AS Decimal(18, 0)), CAST(7267308 AS Decimal(18, 0)), 3, 12, CAST(432692 AS Decimal(18, 0)), CAST(2000000 AS Decimal(18, 0)), CAST(9567308.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (8, N'NV00006', 12, 2023, CAST(2.60 AS Decimal(5, 2)), 26, CAST(232245 AS Decimal(18, 0)), CAST(9015987 AS Decimal(18, 0)), 4, 14, CAST(504808 AS Decimal(18, 0)), CAST(1243424 AS Decimal(18, 0)), CAST(9248232.0 AS Decimal(18, 1)))
INSERT [dbo].[ChamCong] ([maChamCong], [maNhanVien], [thang], [nam], [heSoLuong], [SoNgayCong], [ungTruocLuong], [conLai], [nghiPhep], [soGioTangCa], [luongTangCa], [phuCapCongViec], [tongNhan]) VALUES (9, N'NV00007', 12, 2023, CAST(2.90 AS Decimal(5, 2)), 26, CAST(4343435 AS Decimal(18, 0)), CAST(8590215 AS Decimal(18, 0)), 7, 12, CAST(692308 AS Decimal(18, 0)), CAST(241342 AS Decimal(18, 0)), CAST(12933650.0 AS Decimal(18, 1)))
SET IDENTITY_INSERT [dbo].[ChamCong] OFF
GO
INSERT [dbo].[Chitiet_Quyen] ([maChitietQuyen], [tenhanhDong], [mahanhDong]) VALUES (N'ADD', N'Thêm', N'ADD')
INSERT [dbo].[Chitiet_Quyen] ([maChitietQuyen], [tenhanhDong], [mahanhDong]) VALUES (N'DEL', N'Xóa', N'DEL')
INSERT [dbo].[Chitiet_Quyen] ([maChitietQuyen], [tenhanhDong], [mahanhDong]) VALUES (N'EDIT', N'Sửa', N'EDIT')
INSERT [dbo].[Chitiet_Quyen] ([maChitietQuyen], [tenhanhDong], [mahanhDong]) VALUES (N'MUSER', N'Quản lý người dùng', N'MUSER')
INSERT [dbo].[Chitiet_Quyen] ([maChitietQuyen], [tenhanhDong], [mahanhDong]) VALUES (N'VIEW', N'Xem', N'VIEW')
GO
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'ADMIN', N'ADD', N'Quyền được thêm mới')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'ADMIN', N'DEL', N'Quyền được xoa')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'ADMIN', N'EDIT', N'Quyền được chỉnh sửa')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'ADMIN', N'MUSER', N'Quyền quản lý người dùng')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'ADMIN', N'VIEW', N'Quyền được xem')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'NV', N'VIEW', N'Quyền được xem.')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'QL', N'ADD', N'Quyền được thêm mới.')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'QL', N'EDIT', N'Quyền được sửa đổi.')
INSERT [dbo].[ChiTietQuyen_Quyen] ([maQuyen], [maChitietQuyen], [moTa]) VALUES (N'QL', N'VIEW', N'Quyền được xem.')
GO
SET IDENTITY_INSERT [dbo].[ChucVu] ON 

INSERT [dbo].[ChucVu] ([maChucVu], [tenChucVu]) VALUES (1, N'Giám đốc')
INSERT [dbo].[ChucVu] ([maChucVu], [tenChucVu]) VALUES (2, N'Phó giám đốc')
INSERT [dbo].[ChucVu] ([maChucVu], [tenChucVu]) VALUES (3, N'Nhân viên')
INSERT [dbo].[ChucVu] ([maChucVu], [tenChucVu]) VALUES (4, N'Trưởng phòng')
INSERT [dbo].[ChucVu] ([maChucVu], [tenChucVu]) VALUES (5, N'Phó phòng')
INSERT [dbo].[ChucVu] ([maChucVu], [tenChucVu]) VALUES (6, N'Chủ tịch hội đồng quản trị')
INSERT [dbo].[ChucVu] ([maChucVu], [tenChucVu]) VALUES (7, N'Thư ký giám đốc')
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
GO
INSERT [dbo].[ChuyenCongTac] ([soQuyetDinh], [ngayQuyetDinh], [thoiGianThiHanh]) VALUES (N'NQ3438TF970', CAST(N'2023-11-21' AS Date), CAST(N'2023-11-26T00:00:00.000' AS DateTime))
INSERT [dbo].[ChuyenCongTac] ([soQuyetDinh], [ngayQuyetDinh], [thoiGianThiHanh]) VALUES (N'NQ345NAH678', CAST(N'2023-10-20' AS Date), CAST(N'2023-11-20T00:00:00.000' AS DateTime))
INSERT [dbo].[ChuyenCongTac] ([soQuyetDinh], [ngayQuyetDinh], [thoiGianThiHanh]) VALUES (N'QÐ32142', CAST(N'2023-12-26' AS Date), CAST(N'2023-12-26T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ChuyenCongTac_NhanVien] ([soQuyetDinh], [maNhanVien], [chucVuCu], [phongBanCu], [chucVuMoi], [phongBanMoi]) VALUES (N'QÐ32142', N'NV00008', 2, 2, 3, 2)
GO
SET IDENTITY_INSERT [dbo].[ChuyenMon] ON 

INSERT [dbo].[ChuyenMon] ([maChuyenMon], [tenChuyenMon]) VALUES (1, N'Quản lý nhân sự')
INSERT [dbo].[ChuyenMon] ([maChuyenMon], [tenChuyenMon]) VALUES (2, N'Kĩ thuật viên')
INSERT [dbo].[ChuyenMon] ([maChuyenMon], [tenChuyenMon]) VALUES (3, N'Sale')
INSERT [dbo].[ChuyenMon] ([maChuyenMon], [tenChuyenMon]) VALUES (4, N'CSKH')
SET IDENTITY_INSERT [dbo].[ChuyenMon] OFF
GO
SET IDENTITY_INSERT [dbo].[DanToc] ON 

INSERT [dbo].[DanToc] ([maDanToc], [tenDanToc]) VALUES (1, N'Kinh')
INSERT [dbo].[DanToc] ([maDanToc], [tenDanToc]) VALUES (3, N'Tày')
INSERT [dbo].[DanToc] ([maDanToc], [tenDanToc]) VALUES (4, N'Thái')
INSERT [dbo].[DanToc] ([maDanToc], [tenDanToc]) VALUES (5, N'E-De')
INSERT [dbo].[DanToc] ([maDanToc], [tenDanToc]) VALUES (6, N'Mông')
INSERT [dbo].[DanToc] ([maDanToc], [tenDanToc]) VALUES (7, N'Mường')
SET IDENTITY_INSERT [dbo].[DanToc] OFF
GO
SET IDENTITY_INSERT [dbo].[HopDong] ON 

INSERT [dbo].[HopDong] ([maHopDong], [soHopDong], [ngayKyHD], [ngayKetThucHD], [loaiHopDong], [thoiHanHD], [tinhTrangChuKi]) VALUES (1, N'hd2123', CAST(N'2023-12-07T00:00:00.000' AS DateTime), CAST(N'2025-12-23T00:00:00.000' AS DateTime), N'Có thời hạn', N'5 năm', N'Đã ký')
INSERT [dbo].[HopDong] ([maHopDong], [soHopDong], [ngayKyHD], [ngayKetThucHD], [loaiHopDong], [thoiHanHD], [tinhTrangChuKi]) VALUES (2, N'hd3133', CAST(N'2023-12-13T00:00:00.000' AS DateTime), CAST(N'2025-12-12T00:00:00.000' AS DateTime), N'Có thời hạn', N'2 năm', N'Đã ký')
INSERT [dbo].[HopDong] ([maHopDong], [soHopDong], [ngayKyHD], [ngayKetThucHD], [loaiHopDong], [thoiHanHD], [tinhTrangChuKi]) VALUES (3, N'dfsdfew', CAST(N'2023-12-08T00:00:00.000' AS DateTime), NULL, N'Không thời hạn', NULL, N'Đã ký')
INSERT [dbo].[HopDong] ([maHopDong], [soHopDong], [ngayKyHD], [ngayKetThucHD], [loaiHopDong], [thoiHanHD], [tinhTrangChuKi]) VALUES (4, N'sdasda3', CAST(N'2023-12-06T00:00:00.000' AS DateTime), CAST(N'2028-12-21T00:00:00.000' AS DateTime), N'Có thời hạn', N'5 năm', N'Đã ký')
INSERT [dbo].[HopDong] ([maHopDong], [soHopDong], [ngayKyHD], [ngayKetThucHD], [loaiHopDong], [thoiHanHD], [tinhTrangChuKi]) VALUES (5, N'HD412sd', CAST(N'2023-12-07T00:00:00.000' AS DateTime), CAST(N'2028-12-10T00:00:00.000' AS DateTime), N'Có thời hạn', N'5 năm', N'Đa ký')
SET IDENTITY_INSERT [dbo].[HopDong] OFF
GO
SET IDENTITY_INSERT [dbo].[HoSo] ON 

INSERT [dbo].[HoSo] ([maHoSo], [soYeuLyLich], [giayKhaiSinh], [soHoKhau], [bangTotNghiep], [giayKhamSK], [anhThe], [tinhTrangHoSo], [hinhThucThanhToanLuong], [soTkNganHang], [nganHang], [maSoThue], [maSoBHXH]) VALUES (1, N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Tài khoản ngân hàng', N'3434134124', N'VietComBank', N'342324134                                                                                           ', N'412412                                                                                              ')
INSERT [dbo].[HoSo] ([maHoSo], [soYeuLyLich], [giayKhaiSinh], [soHoKhau], [bangTotNghiep], [giayKhamSK], [anhThe], [tinhTrangHoSo], [hinhThucThanhToanLuong], [soTkNganHang], [nganHang], [maSoThue], [maSoBHXH]) VALUES (2, N'Đủ', N'Đủ', N'Đủ', N'Thiếu', N'Thiếu', N'Đủ', N'Chưa hoàn thành', N'Tài khoản ngân hàng', N'233535254', N'VietComBank', N'3423423                                                                                             ', N'342342                                                                                              ')
INSERT [dbo].[HoSo] ([maHoSo], [soYeuLyLich], [giayKhaiSinh], [soHoKhau], [bangTotNghiep], [giayKhamSK], [anhThe], [tinhTrangHoSo], [hinhThucThanhToanLuong], [soTkNganHang], [nganHang], [maSoThue], [maSoBHXH]) VALUES (3, N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Đủ', N'Qua thẻ ngân hàng', N'342343242', N'MB', N'4234234                                                                                             ', N'342342                                                                                              ')
INSERT [dbo].[HoSo] ([maHoSo], [soYeuLyLich], [giayKhaiSinh], [soHoKhau], [bangTotNghiep], [giayKhamSK], [anhThe], [tinhTrangHoSo], [hinhThucThanhToanLuong], [soTkNganHang], [nganHang], [maSoThue], [maSoBHXH]) VALUES (4, N'đủ', N'đủ', N'đủ', N'đủ', N'đủ', N'đủ', N'đủ', N'qua tài khoản ngân hàng', N'2341241', N'VCB', N'24121                                                                                               ', N'41242                                                                                               ')
SET IDENTITY_INSERT [dbo].[HoSo] OFF
GO
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00001', N'Trần Văn Anh', N'Nam', CAST(N'2003-12-06' AS Date), N'13212412412                                                                                         ', N'313231321  ', N'Hà Nội', N'Thanh Hóa', 0, 3, 1, 2, 1, 6, 1, 6, N'')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00002', N'Abc', N'Nam', CAST(N'2000-12-12' AS Date), N'542624245                                                                                           ', N'352452     ', N'Ádsa', N'Fgrrr', 0, 1, 3, 2, 3, 2, 4, 2, N'DefaultAvatar.jpg')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00003', N'Nguyễn Việt Anh', N'Nam', CAST(N'2003-12-02' AS Date), N'3525352                                                                                             ', N'0334521568 ', N'Hà nội', N'Việt Nam', 0, 3, 1, 1, 1, 2, 2, 2, N'DefaultAvatar.jpg')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00004', N'Dư Ngọc Ánh', N'Nam', CAST(N'2000-12-23' AS Date), N'3435366345                                                                                          ', N'054334356  ', N'HÀ Nội', N'Việt Nam', 0, 3, 3, 3, 1, 1, 0, 3, N'Screenshot 2023-12-11 205300.png')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00005', N'Trần Ván Vanh', N'Nam', CAST(N'2003-06-12' AS Date), N'56546464                                                                                            ', N'343252455  ', N'Hà nội', N'Thanh Hóa', 0, 3, 1, 3, 1, 2, 2, 3, N'DefaultAvatar.jpg')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00006', N'Ngô Mạnh Anh', N'Nam', CAST(N'2000-12-12' AS Date), N'342354853232                                                                                        ', N'0344212456 ', N'Hà Nội', N'Viêt Nam', 0, 3, 1, 2, 1, 4, 0, 3, N'DefaultAvatar.jpg')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00007', N'Nguyễn Đăng Đức', N'Nam', CAST(N'2004-03-22' AS Date), N'542642542                                                                                           ', N'0433256742 ', N'Hà Nội', N'Việt Nam', 3, 3, 3, 2, 1, 2, 2, 6, N'DefaultAvatar.jpg')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00008', N'Đặng Thọ Chiến', N'Nam', CAST(N'2003-12-12' AS Date), N'64764874545324                                                                                      ', N'0382934231 ', N'Hà nội', N'Việt Nam', 0, 3, 1, 3, 1, 3, 2, 2, N'DefaultAvatar.jpg')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00009', N'Người Bí Ẩn', N'Nam', CAST(N'2002-12-12' AS Date), N'412312414                                                                                           ', NULL, N'Hà nội', N'Thanh hóa', 4, 3, 1, 2, 1, 3, 3, 3, N'Screenshot 2023-12-22 061200.png')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00010', N'Trần Văn Anh Dz', N'Nam', CAST(N'2003-06-12' AS Date), N'3423566545                                                                                          ', N'0334237519 ', N'Hà nội', N'Thanh Hóa', 0, 3, 1, 2, 1, 6, 4, 2, N'DefaultAvatar.jpg')
INSERT [dbo].[NhanVien] ([maNhanVien], [tenNhanVien], [gioiTinh], [ngaySinh], [CCCD], [dienThoai], [noiOHienTai], [queQuan], [maHoSo], [maTrinhDo], [maTonGiao], [maChuyenMon], [maDanToc], [maChucVu], [maHopDong], [maPhong], [anhThe]) VALUES (N'NV00011', N'41312', N'Nam', CAST(N'2000-06-12' AS Date), N'432534                                                                                              ', N'2123123211 ', N'43423', N'43423', 0, 2, 2, 2, 3, 2, 0, 2, N'DefaultAvatar.jpg')
GO
SET IDENTITY_INSERT [dbo].[PhongBan] ON 

INSERT [dbo].[PhongBan] ([maPhong], [tenPhong], [dienThoai]) VALUES (1, N'Kinh doanh', N'           ')
INSERT [dbo].[PhongBan] ([maPhong], [tenPhong], [dienThoai]) VALUES (2, N'Quản lý hành chính', N'           ')
INSERT [dbo].[PhongBan] ([maPhong], [tenPhong], [dienThoai]) VALUES (3, N'Kĩ thuật', N'           ')
INSERT [dbo].[PhongBan] ([maPhong], [tenPhong], [dienThoai]) VALUES (4, N'Kế toán', N'           ')
INSERT [dbo].[PhongBan] ([maPhong], [tenPhong], [dienThoai]) VALUES (5, N'Chăm sóc khách hàng', N'           ')
INSERT [dbo].[PhongBan] ([maPhong], [tenPhong], [dienThoai]) VALUES (6, N'Công nghệ thông tin', N'           ')
SET IDENTITY_INSERT [dbo].[PhongBan] OFF
GO
INSERT [dbo].[Quyen] ([maQuyen], [tenQuyen]) VALUES (N'ADMIN', N'administrator')
INSERT [dbo].[Quyen] ([maQuyen], [tenQuyen]) VALUES (N'NV', N'Nhân viên')
INSERT [dbo].[Quyen] ([maQuyen], [tenQuyen]) VALUES (N'QL', N'Quản lý')
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenTaiKhoan], [matKhau], [maQuyen], [trangThai]) VALUES (1, N'administrator', N'db69fc039dcbd2962cb4d28f5891aae1', N'ADMIN', 1)
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenTaiKhoan], [matKhau], [maQuyen], [trangThai]) VALUES (2, N'TranVanAnh', N'db69fc039dcbd2962cb4d28f5891aae1', N'NV', 1)
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenTaiKhoan], [matKhau], [maQuyen], [trangThai]) VALUES (3, N'NguyenVietAnh', N'978aae9bb6bee8fb75de3e4830a1be46', N'NV', 1)
INSERT [dbo].[TaiKhoan] ([maTaiKhoan], [tenTaiKhoan], [matKhau], [maQuyen], [trangThai]) VALUES (4, N'TranVanAnhDz', N'195bbcd076f051abbd949681a1dfdad9', N'NV', 1)
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
SET IDENTITY_INSERT [dbo].[TonGiao] ON 

INSERT [dbo].[TonGiao] ([maTonGiao], [tenTonGiao]) VALUES (1, N'Không')
INSERT [dbo].[TonGiao] ([maTonGiao], [tenTonGiao]) VALUES (2, N'Thiên chúa giáo')
INSERT [dbo].[TonGiao] ([maTonGiao], [tenTonGiao]) VALUES (3, N'Phật giáo')
INSERT [dbo].[TonGiao] ([maTonGiao], [tenTonGiao]) VALUES (4, N'Tin lành')
INSERT [dbo].[TonGiao] ([maTonGiao], [tenTonGiao]) VALUES (5, N'Hồi giáo')
INSERT [dbo].[TonGiao] ([maTonGiao], [tenTonGiao]) VALUES (6, N'Hin-du')
SET IDENTITY_INSERT [dbo].[TonGiao] OFF
GO
SET IDENTITY_INSERT [dbo].[TrinhDo] ON 

INSERT [dbo].[TrinhDo] ([maTrinhDo], [tenTrinhDo]) VALUES (1, N'Cao đẳng')
INSERT [dbo].[TrinhDo] ([maTrinhDo], [tenTrinhDo]) VALUES (2, N'Cao học')
INSERT [dbo].[TrinhDo] ([maTrinhDo], [tenTrinhDo]) VALUES (3, N'Đại học')
INSERT [dbo].[TrinhDo] ([maTrinhDo], [tenTrinhDo]) VALUES (4, N'Trung cấp nghề')
INSERT [dbo].[TrinhDo] ([maTrinhDo], [tenTrinhDo]) VALUES (5, N'12/12')
INSERT [dbo].[TrinhDo] ([maTrinhDo], [tenTrinhDo]) VALUES (6, N'9/12')
SET IDENTITY_INSERT [dbo].[TrinhDo] OFF
GO
ALTER TABLE [dbo].[ChamCong]  WITH CHECK ADD FOREIGN KEY([heSoLuong])
REFERENCES [dbo].[BacLuong] ([heSoLuong])
GO
ALTER TABLE [dbo].[ChamCong]  WITH CHECK ADD FOREIGN KEY([maNhanVien])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[ChiTietQuyen_Quyen]  WITH CHECK ADD FOREIGN KEY([maChitietQuyen])
REFERENCES [dbo].[Chitiet_Quyen] ([maChitietQuyen])
GO
ALTER TABLE [dbo].[ChiTietQuyen_Quyen]  WITH CHECK ADD FOREIGN KEY([maQuyen])
REFERENCES [dbo].[Quyen] ([maQuyen])
GO
ALTER TABLE [dbo].[ChuyenCongTac_NhanVien]  WITH CHECK ADD FOREIGN KEY([chucVuMoi])
REFERENCES [dbo].[ChucVu] ([maChucVu])
GO
ALTER TABLE [dbo].[ChuyenCongTac_NhanVien]  WITH CHECK ADD FOREIGN KEY([maNhanVien])
REFERENCES [dbo].[NhanVien] ([maNhanVien])
GO
ALTER TABLE [dbo].[ChuyenCongTac_NhanVien]  WITH CHECK ADD FOREIGN KEY([phongBanMoi])
REFERENCES [dbo].[PhongBan] ([maPhong])
GO
ALTER TABLE [dbo].[ChuyenCongTac_NhanVien]  WITH CHECK ADD FOREIGN KEY([soQuyetDinh])
REFERENCES [dbo].[ChuyenCongTac] ([soQuyetDinh])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maChucVu])
REFERENCES [dbo].[ChucVu] ([maChucVu])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maChuyenMon])
REFERENCES [dbo].[ChuyenMon] ([maChuyenMon])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maDanToc])
REFERENCES [dbo].[DanToc] ([maDanToc])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maPhong])
REFERENCES [dbo].[PhongBan] ([maPhong])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maTonGiao])
REFERENCES [dbo].[TonGiao] ([maTonGiao])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([maTrinhDo])
REFERENCES [dbo].[TrinhDo] ([maTrinhDo])
GO
ALTER TABLE [dbo].[TaiKhoan]  WITH CHECK ADD FOREIGN KEY([maQuyen])
REFERENCES [dbo].[Quyen] ([maQuyen])
GO
/****** Object:  StoredProcedure [dbo].[DangNhap_Proc]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- store procherduce
create proc [dbo].[DangNhap_Proc]
@tenTaiKhoan varchar(255), @matKhau varchar(255)
as
begin
	select * from TaiKhoan where tenTaiKhoan = @tenTaiKhoan and matKhau = @matKhau
end

exec DangNhap_Proc 'TranVanAnh', 'staff'
GO
/****** Object:  StoredProcedure [dbo].[TaoMoiTaiKhoan]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Tạo một tài khoản 
create PROCEDURE [dbo].[TaoMoiTaiKhoan]
(
  @tenTaiKhoan varchar(255),
  @matKhau varchar(255),
  @maQuyen varchar(50),
  @trangThai bit
)
AS
BEGIN
  -- Chèn dữ liệu vào bảng tài khoản.
  INSERT INTO [dbo].[TaiKhoan]
  (
    tenTaiKhoan,
    matKhau,
    maQuyen,
    trangThai
  )
  VALUES
  (
    @tenTaiKhoan,
    @matKhau,
    @maQuyen,
    @trangThai
  );

  SELECT * FROM TaiKhoan WHERE maTaiKhoan = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[XoaTaiKhoan]    Script Date: 26/12/2023 8:31:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[XoaTaiKhoan]
( @maTaiKhoan int)
as
Begin
	Delete from TaiKhoan where maTaiKhoan = @maTaiKhoan
end
GO
