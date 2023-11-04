

create database QLNhanSu
go

use QLNhanSu
go


CREATE TABLE TonGiao
(
	maTonGiao	int		identity(1,1)	primary key not null,
	tenTonGiao	nvarchar(255)		not null
)
go

select * from TonGiao
go

-- bảng chuyên môn
create table ChuyenMon
(
	maChuyenMon		int		identity(1,1)	primary key		not null,
	tenChuyenMon	nvarchar(255)	not null
)
go

select * from ChuyenMon
go

-- bảng dân tộc
create table DanToc
(
	maDanToc	int		identity(1,1)	 primary key not null,
	tenDanToc	nvarchar(255)	not null
)
go

select * from DanToc
go

-- bảng báo cáo đăng nhập
create table BaoCaoDangNhap
(
	idDangNhap		int		identity(1,1)	primary key not null,
	maTaiKhoan		int		not null,
	tgDangNhap		DateTime	not null,
	tgDangXuat		DateTime	not null
)
go

ALTER TABLE BaoCaoDangNhap
ALTER COLUMN tgDangNhap DateTime NULL
go

select * from BaoCaoDangNhap
go

-- bảng chức vụ
create table ChucVu
(
	maChucVu	int		identity(1,1)	 primary key not null,
	tenChucVu	nvarchar(255) not null
)
go

select * from ChucVu
go

-- bảng lương
create table Luong
(
	bacLuong	int		identity(1,1)	primary key not null,
	luongCoBan	decimal		not null
)

select * from Luong
go

-- bảng quyền
create table Quyen
(
	maQuyen		int		identity(1,1)	 primary key not null,
	tenQuyen	nvarchar(100)	not null
)
go

select * from Quyen
go

delete from Quyen where maQuyen = 3

-- bảng phòng ban
create table PhongBan
(
	maPhong		int		identity(1,1)	 primary key not null,
	tenPhong	nvarchar(255)	not null,
	dienThoai	char(11)
)
go

select * from PhongBan
go

-- bảng trình độ
create table TrinhDo
(
	maTrinhDo	int		identity(1,1)	 primary key not null,
	tenTrinhDo	nvarchar(15)	not null
)
go

select * from TrinhDo
go

-- bảng hồ sơ
create table HoSo
(
	maHoSo			int	identity(1,1)	primary key	 not null,
	soYeuLyLich		nvarchar(100)  not null,
	giayKhaiSinh	nvarchar(100)  not null,
	soHoKhau		nvarchar(100)  not null,
	bangTotNghiep	nvarchar(100)  not null,
	giayKhamSK		nvarchar(100)  not null,
	anhThe			nvarchar(100)  not null,
	tinhTrangHoSo	nvarchar(100)  not null,
	hinhThucThanhToanLuong nvarchar(100)  not null,
	soTkNganHang	varchar(50)  not null,
	nganHang		nvarchar(100)  not null,
	tinhTrangHonNhan nvarchar(100)  not null,
	maSoThue		char(100)  not null,
	maSoBHXH		char(100)  not null,
	ngayBatDauLamViec Date,
	kinhNghiemLamViec ntext,
	thoiGian		Date,
)
go

select * from HoSo
go

-- bảng chuyển công tác
create table ChuyenCongTac
(
	soQuyetDinh		int primary key not null,
	ngayQuyetDinh	date,
	chucVuCu		nvarchar(255),
	phongBanCu		nvarchar(255),
	chucVuMoi		nvarchar(255),
	phongBanMoi		nvarchar(255),
)
go

select * from ChuyenCongTac
go

-- bảng chuyển công tác nhân viên
create table ChuyenCongTac_NhanVien
(
	soQuyetDinh		int		not null,
	maNhanVien		int		not null,
	primary key (soQuyetDinh, maNhanVien),
	foreign key (soQuyetDinh) references ChuyenCongTac(soQuyetDinh),
	foreign key (maNhanVien) references NhanVien(maNhanVien)
)
go

select * from ChuyenCongTac_NhanVien
go

-- bảng tài khoản
create table TaiKhoan
(
	maTaiKhoan	int		identity(1,1) primary key not null,
	tenTaiKhoan	varchar(255) not null,
	matKhau		varchar(255) not null,
	maQuyen		int		foreign key (maQuyen) references Quyen(maQuyen) not null,
	trangThai	bit not null,
)
go

select * from TaiKhoan
go


-- bảng chấm công
create table ChamCong
(
	maChamCong		int identity(1, 1) primary key not null,
	maNhanVien		int	foreign key (maNhanVien) references NhanVien(maNhanVien) not null,
	bacLuong		int foreign key(bacLuong) references Luong(bacLuong) not null,
	SoNgayCong		int,
	ungTruocLuong	decimal,
	conLai			decimal,
	nghiPhep		int,
	ngungViec		int,
	soNgayTangCa	int,
	phuCapKhac		ntext,
	luongTangCa		decimal,
	phuCapCongViec	decimal,
)
go

select* from ChamCong
go

-- bảng hợp đồng
create table HopDong
(
	maHopDong	int	identity(1,1) primary key not null,
	soHopDong	varchar(50)	not null,
	ngayKyHD	datetime,
	ngayKetThucHD datetime,
	loaiHopDong nvarchar(10),
	thoiHanHD	nvarchar(100),
	tinhTrangChuKi nvarchar(50),
)
go

select * from HopDong
go

-- bảng nhân viên
create table NhanVien
(
	maNhanVien	int identity(1,1) primary key not null,
	tenNhanVien nvarchar(255) not null,
	gioiTinh	nvarchar(10)	not null,
	ngaySinh	Date	not null,
	noiSinh		nvarchar(255)	not null,
	CCCD		char(100) not null,
	dienThoai	char(11)	not null,
	noiOHienTai nvarchar(255)	not null,
	queQuan		nvarchar(255)	not null,
	giaDinh		nvarchar(255)	,
	emailCaNhan varchar(255)	not null,
	emailNoiBo	varchar(255)	not null,
	coSoLamViec nvarchar(255)	not null,
	loaiHinhLamViec nvarchar(255),
	luongOffer	decimal,
	truongHoc	nvarchar(255),
	chuyenNganh nvarchar(255),
	maHoSo		int	foreign key (maHoSo) references HoSo(maHoSo) not null,
	maTrinhDo	int	foreign key (maTrinhDo) references TrinhDo(maTrinhDo) not null,
	maTonGiao	int	foreign key (maTonGiao) references TonGiao(maTonGiao) not null,
	maChuyenMon int	foreign key (maChuyenMon) references ChuyenMon(maChuyenMon) not null,
	maDanToc	int	foreign key (maDanToc) references DanToc(maDanToc) not null,
	maChucVu	int	foreign key (maChucVu) references ChucVu(maChucVu) not null,
	maHopDong	int	foreign key (maHopDong) references HopDong(maHopDong) not null,
	maPhong		int	foreign key (maPhong) references PhongBan(maPhong) not null,
)
go

--ALTER TABLE NhanVien
--DROP CONSTRAINT FK__NhanVien__maTaiK__6B24EA82;

--ALTER TABLE NhanVien
--DROP COLUMN maTaiKhoan

select * from NhanVien
go


-- insert data
insert into DanToc values 
(N'Kinh'),
(N'Kinh'),
(N'Tày'),
(N'Thái'),
(N'E-De'),
(N'Mông'),
(N'Mường')
go
select * from DanToc
go

insert into PhongBan values 
(N'Kinh doanh', ''),
(N'Quản lý hành chính', ''),
(N'Kĩ thuật', ''),
(N'Kế toán', ''),
(N'Chăm sóc khách hàng', ''),
(N'Công nghệ thông tin', '')
go
select * from PhongBan
go

insert into TonGiao values 
(N'Không'),
(N'Thiên chúa giáo'),
(N'Phật giáo'),
(N'Tin lành'),
(N'Hồi giáo'),
(N'Hin-du')
go
select * from TonGiao
go

insert into TrinhDo values 
(N'Cao đẳng'),
(N'Cao học'),
(N'Đại học'),
(N'Trung cấp nghề'),
(N'12/12'),
(N'9/12')
go
select * from TrinhDo
go

insert into ChucVu values 
(N'Giám đốc'),
(N'Phó giám đốc'),
(N'Nhân viên'),
(N'Trưởng phòng'),
(N'Phó phòng'),
(N'Chủ tịch hội đồng quản trị'),
(N'Thư ký giám đốc')
go
select * from ChucVu
go

insert into TaiKhoan values
('administrator','admin',1,1),
('TranVanAnh','admin',2,1),
('NguyenVietAnh','staff',2,1)
go
select * from TaiKhoan
go

insert into Quyen values
('administrator'),
( N'Nhân viên'),
( N'Trưởng phòng')
go
select * from Quyen
go

-- store procherduce
create proc DangNhap_Proc
@tenTaiKhoan varchar(255), @matKhau varchar(255)
as
begin
	select * from TaiKhoan where tenTaiKhoan = @tenTaiKhoan and matKhau = @matKhau
end

exec DangNhap_Proc 'TranVanAnh', 'staff'
go

-- Tạo một tài khoản 
create PROCEDURE [dbo].[TaoMoiTaiKhoan]
(
  @tenTaiKhoan varchar(255),
  @matKhau varchar(255),
  @maQuyen int,
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
go


create proc [dbo].[XoaTaiKhoan]
( @maTaiKhoan int)
as
Begin
	Delete from TaiKhoan where maTaiKhoan = @maTaiKhoan
end
go

create proc [dbo].[LayDanhSach_ChucVu]
as
begin
	select * from ChucVu
end
go

exec [dbo].[LayDanhSach_ChucVu]
go

create proc [dbo].[TaoMoi_ChucVu]
(@tenChucVu nvarchar(255))
as
begin
	insert into ChucVu (tenChucVu) values (@tenChucVu)

	select * from ChucVu where maChucVu = SCOPE_IDENTITY();
end
go


select * from TaiKhoan
select * from BaoCaoDangNhap

