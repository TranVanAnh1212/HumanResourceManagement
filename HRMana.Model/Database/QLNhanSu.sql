

use master
go

create database QLNhanSu
go

use QLNhanSu
go

-- bảng quyền
create table Quyen
(
	maQuyen		varchar(50)		primary key not null,
	tenQuyen	nvarchar(100)	not null
)
go

insert into Quyen values
('ADMIN', N'administrator'),
('NV', N'Nhân viên'),
('QL', N'QUản lý')
go

select * from TaiKhoan
select * from Quyen
select * from Chitiet_Quyen
select * from ChitietQuyen_Quyen
go

create table Chitiet_Quyen
(
	maChitietQuyen	varchar(50)	 primary key not null,
	tenhanhDong		nvarchar(100),
	mahanhDong		varchar(100),
)
go

create table ChiTietQuyen_Quyen 
(
	maQuyen varchar(50) not null foreign key(maQuyen) references Quyen(maQuyen),
	maChitietQuyen varchar(50) not null foreign key(maChitietQuyen) references Chitiet_Quyen(maChitietQuyen),
	moTa	nvarchar(255),
	primary key (maQuyen, maChitietQuyen)
)
go

insert into ChiTietQuyen_Quyen values 
( 'ADMIN', 'ADD', N'Quyền được thêm mới'),
( 'ADMIN', 'DEL', N'Quyền được xoa'),
( 'ADMIN', 'EDIT', N'Quyền được chỉnh sửa'),
( 'ADMIN', 'VIEW', N'Quyền được xem'),
( 'ADMIN', 'MUSER', N'Quyền quản lý người dùng'),
( 'NV', 'ADD', N'Quyền được thêm mới'),
( 'QL', 'VIEW', N'Quyền được xem')
--go

select * from ChiTietQuyen_Quyen
select * from Quyen
select * from TaiKhoan
go



--SELECT CONSTRAINT_NAME
--FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
--WHERE TABLE_NAME = 'Chitiet_Quyen' AND COLUMN_NAME = 'maQuyen';

--alter table Chitiet_Quyen
--drop constraint FK__Chitiet_Q__maQuy__54CB950F

--alter table ChiTiet_Quyen
--drop column maQuyen

insert into ChiTiet_Quyen values
('ADD', N'Thêm', 'ADD'),
('DEL', N'Xóa', 'DEL'),
('EDIT', N'Sửa', 'EDIT'),
('VIEW', N'Xem', 'VIEW'),
('MUSER', N'Quản lý người dùng', 'MUSER')
go

select * from Chitiet_Quyen
go

-- bảng tài khoản
create table TaiKhoan
(
	maTaiKhoan	int		identity(1,1) primary key not null,
	tenTaiKhoan	varchar(255) not null,
	matKhau		varchar(255) not null,
	maQuyen		varchar(50)		foreign key (maQuyen) references Quyen(maQuyen) not null,
	trangThai	bit not null,
)
go

insert into TaiKhoan values
('administrator','db69fc039dcbd2962cb4d28f5891aae1', 'ADMIN',1),
('TranVanAnh','db69fc039dcbd2962cb4d28f5891aae1', 'NV',1),
('NguyenVietAnh','978aae9bb6bee8fb75de3e4830a1be46', 'NV' ,1)
go
select * from TaiKhoan
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
	tgDangXuat		DateTime	null
)
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
	maSoThue		char(100)  not null,
	maSoBHXH		char(100)  not null,
)
go

select * from NhanVien
go

-- bảng chuyển công tác
create table ChuyenCongTac
(
	soQuyetDinh		varchar(100) primary key not null,
	ngayQuyetDinh	date,
	thoiGianThiHanh dateTime,
)
go

insert into ChuyenCongTac values
('NQ345NAH678', '2023-10-20', '2023-11-20'),
('NQ3438TF970', '2023-11-21', '2023-11-26')

--drop table ChuyenCongTac
select * from ChuyenCongTac
go

-- bảng chuyển công tác nhân viên
create table ChuyenCongTac_NhanVien
(
	soQuyetDinh		varchar(100)	foreign key (soQuyetDinh) references ChuyenCongTac(soQuyetDinh)		not null,
	maNhanVien		varchar(10)		foreign key (maNhanVien) references NhanVien(maNhanVien)	not null,
	chucVuCu		int,
	phongBanCu		int,
	chucVuMoi		int foreign key(chucVuMoi) references ChucVu(maChucVu),
	phongBanMoi		int foreign key(phongBanMoi) references PhongBan(maPhong),
	primary key (soQuyetDinh, maNhanVien),
)
go

insert into ChuyenCongTac_NhanVien values
('NQ345NAH678', 'NV00001', 6, 6, 1, 6),
('NQ3438TF970', 'NV00002', 6, 6, 10, 6)
go

--select * from ChuyenCongTac_NhanVien
--inner join PhongBan on maPhong = phongBanCu
--inner join NhanVien on NhanVien.maNhanVien = ChuyenCongTac_NhanVien.maNhanVien


--drop table ChuyenCongTac_NhanVien
select * from ChuyenCongTac_NhanVien
select * from NhanVien
select * from ChucVu
select * from PhongBan

-- bảng chấm công
create table ChamCong
(
	maChamCong		int identity(1, 1) primary key not null,
	maNhanVien		varchar(10)	foreign key (maNhanVien) references NhanVien(maNhanVien) not null,
	thang int not null, 
	nam int not null,
	heSoLuong		DECIMAL(5, 2) foreign key(heSoLuong) references BacLuong(heSoLuong) not null,
	SoNgayCong		int,
	ungTruocLuong	decimal,
	conLai			decimal,
	nghiPhep		int,
	soGioTangCa	int,
	luongTangCa		decimal,
	phuCapCongViec	decimal,
	tongNhan decimal(18, 1) 
)
go

--SP_RENAME 'ChamCong.soNgayTangCa', 'soGioTangCa', 'COLUMN';

--alter table ChamCong
--add tongNhan decimal(18, 1) 

--alter table ChamCong
--add nam int 

--delete from ChamCong
--where maChamCong = 5

select* from ChamCong
go

-- bảng lương
create table BacLuong
(
	heSoLuong	DECIMAL(5, 2)		primary key	not null,
	luongCoBan	decimal		not null
)

INSERT INTO BacLuong VALUES 
(2.2, 4500000),
(2.4, 5500000),
(2.6, 7500000),
(2.8, 10500000),
(2.9, 12000000),
(3.0, 15000000),
(3.4, 20000000)

select * from BacLuong
go


--Drop table ChamCong
--drop table BacLuong


-- bảng hợp đồng
create table HopDong
(
	maHopDong	int	identity(1,1) primary key not null,
	soHopDong	varchar(50)	not null,
	ngayKyHD	datetime,
	ngayKetThucHD datetime,
	loaiHopDong nvarchar(255),
	thoiHanHD	nvarchar(100),
	tinhTrangChuKi nvarchar(50),
)
go

--insert into HopDong values 
--(N'32131sd', '12-06-2022', '12-06-2027', N'Có thời hạn', N'5 năm', N'Đã ký'),
--(N'32ed1sd', '12-06-2023', '12-06-2027', N'không thời hạn', N'', N'Đã ký')
--go
select * from HopDong 
go

-- bảng nhân viên
create table NhanVien
(
	maNhanVien	varchar(10) primary key not null,
	tenNhanVien nvarchar(255)	not null,
	gioiTinh	nvarchar(10)	not null,
	ngaySinh	Date	not null,
	CCCD		char(100)		not null,
	dienThoai	char(11)	 null,
	noiOHienTai nvarchar(255)	not null,
	queQuan		nvarchar(255)	not null,
	maHoSo		int,
	maTrinhDo	int	foreign key (maTrinhDo) references TrinhDo(maTrinhDo) not null,
	maTonGiao	int	foreign key (maTonGiao) references TonGiao(maTonGiao) not null,
	maChuyenMon int	foreign key (maChuyenMon) references ChuyenMon(maChuyenMon) not null,
	maDanToc	int	foreign key (maDanToc) references DanToc(maDanToc) not null,
	maChucVu	int	foreign key (maChucVu) references ChucVu(maChucVu) not null,
	maHopDong	int,
	maPhong		int	foreign key (maPhong) references PhongBan(maPhong) not null,
	anhThe		varchar(255),
)
go

select * from NhanVien 

--SELECT CONSTRAINT_NAME
--FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
--WHERE TABLE_NAME = 'NhanVien' AND COLUMN_NAME = 'maHoSo';

--alter table NhanVien
--drop column chuyenNganh

--alter table NhanVien
--drop constraint FK__NhanVien__maHoSo__6477ECF3

--insert into NhanVien values
--(N'Trần Văn Anh', N'Nam', '12-06-2003', N'Định Tân - Yên Định - Thanh Hóa', '3133542342', '0334237519', N'250 Tây Tự - Phường Tây Tựu - Quận bắc Từ Liêm - Hà Nội',  N'Định Tân - Yên Định - Thanh Hóa', N'Chưa lập gia đình', 'anh@gmail.com', 'work@gmail.com', N'Hà Nội', 'fulltime', 12000000, '', '', 1, 3, 8, 2, 1, 6, 3, 6)
--go

--insert into NhanVien values
--(N'Nguyễn Việt Anh', N'Nam', '12-06-2003', N'Hà Nội', '3133542342', '0334237519', N'Nhổn City',  N'Hà Nội', N'Chưa lập gia đình', 'anh@gmail.com', 'work@gmail.com', N'Hà Nội', 'fulltime', 15000000, '', '', 1, 3, 8, 2, 1, 6, 2, 6)
--go

--insert into NhanVien values
--(N'Đặng Thọ Chiến', N'Nam', '12-06-2023', N'Hà Nội', '3133542342', '0334237519', N'Nhổn City',  N'Hà Nội', N'Chưa lập gia đình', 'anh@gmail.com', 'work@gmail.com', N'Hà Nội', 'fulltime', 10000000, '', '', 1, 3, 8, 2, 1, 6, 2, 6)
--go

--insert into NhanVien values
--(N'Dư Ngọc Ánh', N'Nam', '12-06-2123', N'Hà Nội', '3133542342', '0334237519', N'Nhổn City',  N'Hà Nội', N'Chưa lập gia đình', 'anh@gmail.com', 'work@gmail.com', N'Hà Nội', 'fulltime', 9000000, '', '', 1, 3, 8, 2, 1, 6, 2, 6)
--go

--ALTER TABLE NhanVien
--DROP CONSTRAINT FK__NhanVien__maTaiK__6B24EA82;

--ALTER TABLE NhanVien
--DROP COLUMN maTaiKhoan

select * from NhanVien
go


-- insert data
insert into ChuyenMon values 
(N'Quản lý nhân sự'),
(N'Kĩ thuật viên'),
(N'Sale'),
(N'CSKH')
go
select * from ChuyenMon
go

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

-- store procherduce
create proc DangNhap_Proc
@tenTaiKhoan varchar(255), @matKhau varchar(255)
as
begin
	select * from TaiKhoan where tenTaiKhoan = @tenTaiKhoan and matKhau = @matKhau
end

exec DangNhap_Proc 'TranVanAnh', 'staff'
go

---
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
go

create proc [dbo].[XoaTaiKhoan]
( @maTaiKhoan int)
as
Begin
	Delete from TaiKhoan where maTaiKhoan = @maTaiKhoan
end
go






select * from TaiKhoan
select * from BaoCaoDangNhap
select * from NhanVien
select * from HopDong
select * from DanToc
select * from HoSo
select * from ChucVu
select * from PhongBan
go

--update NhanVien
--set anhThe = '4b4db64d-57d6-41d8-b61d-ac23752d3857.jpg'
--where maNhanVien = 15

--delete from BaoCaoDangNhap
--where maTaiKhoan = 20

----------- ------ Func Nhân Viên Tự Tăng -------- --------

CREATE FUNCTION dbo.GenerateEmployeeCode()
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @MaxCode INT
    DECLARE @NewCode VARCHAR(10)

    SELECT @MaxCode = ISNULL(MAX(SUBSTRING(maNhanVien, 3, 5)), 0) FROM NhanVien

    SET @NewCode = 'NV' + RIGHT('00000' + CAST(@MaxCode + 1 AS VARCHAR(5)), 5)

    RETURN @NewCode
END
go

-------- ---------- ------------ ------------- -----------

--INSERT INTO NhanVien VALUES (dbo.GenerateEmployeeCode(), N'Trần Văn Anh', N'Nam', '12/06/2003', '13212412412', '313231321', N'Hà Nội', N'Thanh Hóa', 0, 3, 1, 2, 1, 6, 0, 6, '')

select * from NhanVien
