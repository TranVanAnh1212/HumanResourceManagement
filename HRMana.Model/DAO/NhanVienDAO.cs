using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HRMana.Model.DAO
{
    public class NhanVienDAO
    {
        public List<NhanVien> GetList_NhanVien()
        {
            List<NhanVien> nv = new List<NhanVien>();

            try
            {
                return nv = DataProvider.Instance.DBContext.NhanVien.ToList();

            }
            catch (Exception ex)
            {
                return nv;
            }
        }

        public List<NhanVien> GetList_NhanVien(string tnv)
        {
            List<NhanVien> nv = new List<NhanVien>();

            try
            {
                var result = DataProvider.Instance.DBContext.NhanVien.Where(x => x.tenNhanVien.Contains(tnv)).ToList();

                if (result.Count() > 0)
                {
                    nv = result;
                }
            }
            catch (Exception ex)
            {
                return nv;
            }
            return nv;
        }

        public NhanVien Get_NhanVien_By_MaNhanVien(string maNhanVien)
        {
            NhanVien nv = null;

            try
            {
                nv = DataProvider.Instance.DBContext
                    .NhanVien
                    .SingleOrDefault(x => x.maNhanVien.Equals(maNhanVien));
            }
            catch (Exception ex)
            {
                return nv = null;
            }

            return nv;
        }

        public bool Update_MaHoSo(NhanVien nv)
        {
            try
            {
                var nhanvien = DataProvider.Instance.DBContext.NhanVien.Where(x => x.maNhanVien == nv.maNhanVien).FirstOrDefault();

                if (nhanvien != null)
                {
                    nhanvien.maHoSo = nv.maHoSo;

                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public NhanVien Get_NhanVien_By_MaHopDong(int maHopDong)
        {
            NhanVien nv = null;

            try
            {
                nv = DataProvider.Instance.DBContext.NhanVien.SingleOrDefault(x => x.maHopDong == maHopDong);
            }
            catch (Exception ex)
            {
                return nv = null;
            }

            return nv;
        }

        public string CreateNew_NhanVien(NhanVien nhanVien)
        {
            // Sử dụng context của Entity Framework để tạo mã nhân viên mới
            string newEmployeeCode = DataProvider.Instance.DBContext.Database.SqlQuery<string>("SELECT dbo.GenerateEmployeeCode()").FirstOrDefault();

            // Gán mã nhân viên mới cho đối tượng nhân viên
            nhanVien.maNhanVien = newEmployeeCode;

            DataProvider.Instance.DBContext.NhanVien.Add(nhanVien);

            //DataProvider.Instance.DBContext.Database
            //    .SqlQuery<NhanVien>($"INSERT INTO NhanVien VALUES (dbo.GenerateEmployeeCode(), N'{nhanVien.tenNhanVien}', N'{nhanVien.gioiTinh}', '{nhanVien.ngaySinh}', '{nhanVien.CCCD}', '{nhanVien.dienThoai}', N'{nhanVien.noiOHienTai}', N'{nhanVien.queQuan}', {nhanVien.maHoSo}, {nhanVien.maTrinhDo}, {nhanVien.maTonGiao}, {nhanVien.maChuyenMon}, {nhanVien.DanToc}, {nhanVien.maChucVu}, {nhanVien.maHopDong}, {nhanVien.maPhong}, '{nhanVien.anhThe}')");

            DataProvider.Instance.DBContext.SaveChanges();

            return nhanVien.maNhanVien;
        }

        public bool Delete_NhanVien(string id)
        {
            try
            {
                var nv = DataProvider.Instance.DBContext.NhanVien
                    .Where(x => x.maNhanVien.Equals(id))
                    .FirstOrDefault();

                if (nv != null)
                {
                    DataProvider.Instance.DBContext.NhanVien.Remove(nv);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
        }

        public bool Update_NhanVien(NhanVien nv)
        {
            try
            {
                var nhanvien = DataProvider.Instance.DBContext
                    .NhanVien
                    .Where(x => x.maNhanVien.Equals(nv.maNhanVien))
                    .FirstOrDefault();

                if (nhanvien != null)
                {
                    nhanvien.tenNhanVien = nv.tenNhanVien;
                    nhanvien.gioiTinh = nv.gioiTinh;
                    nhanvien.ngaySinh = nv.ngaySinh;
                    nhanvien.CCCD = nv.CCCD;
                    nhanvien.dienThoai = nv.dienThoai;
                    nhanvien.noiOHienTai = nv.noiOHienTai;
                    nhanvien.queQuan = nv.queQuan;
                    nhanvien.maHopDong = nv.maHopDong;
                    nhanvien.maHoSo = nv.maHoSo;
                    nhanvien.maChucVu = nv.maChucVu;
                    nhanvien.maPhong = nv.maPhong;
                    nhanvien.maDanToc = nv.maDanToc;
                    nhanvien.maTonGiao = nv.maTonGiao;
                    nhanvien.maTrinhDo = nv.maTrinhDo;
                    nhanvien.maChuyenMon = nv.maChuyenMon;
                    nhanvien.anhThe = nv.anhThe;

                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
        }
    }
}
