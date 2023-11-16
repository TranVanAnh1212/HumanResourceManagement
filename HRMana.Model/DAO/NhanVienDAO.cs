using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRMana.Model.DAO
{
    public class NhanVienDAO
    {
        public List<NhanVien> GetList_NhanVien()
        {
            List<NhanVien> nv = null;

            try
            {
                var result = DataProvider.Instance.DBContext.NhanVien.ToList();

                if (result.Count() > 0)
                {
                    nv = result.ToList();
                }
            }
            catch (Exception ex)
            {
                return nv;
            }
            return nv;
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

        public NhanVien Get_NhanVien_By_MaNhanVien(int maNhanVien)
        {
            NhanVien nv = null;

            try
            {
                nv = DataProvider.Instance.DBContext.NhanVien.SingleOrDefault(x => x.maNhanVien == maNhanVien);
            }
            catch (Exception ex)
            {
                return nv = null;
            }

            return nv;
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

        public int CreateNew_NhanVien(NhanVien nhanVien)
        {
            try
            {
                if (nhanVien == null)
                {
                    return -1;
                }
                else
                {
                    DataProvider.Instance.DBContext.NhanVien.Add(nhanVien);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return nhanVien.maNhanVien;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool Delete_NhanVien (int id)
        {
            try
            {
                var nv = DataProvider.Instance.DBContext.NhanVien.Where(x => x.maNhanVien == id).FirstOrDefault();

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

            }catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update_NhanVien(NhanVien nv)
        {
            try
            {
                var nhanvien = DataProvider.Instance.DBContext.NhanVien.Where(x => x.maNhanVien == nv.maNhanVien).FirstOrDefault();

                if (nhanvien != null)
                {
                    nhanvien.tenNhanVien = nv.tenNhanVien;
                    nhanvien.gioiTinh = nv.gioiTinh;
                    nhanvien.ngaySinh = nv.ngaySinh;
                    nhanvien.noiSinh = nv.noiSinh;
                    nhanvien.CCCD = nv.CCCD;
                    nhanvien.dienThoai = nv.dienThoai;
                    nhanvien.noiOHienTai = nv.noiOHienTai;
                    nhanvien.queQuan = nv.queQuan;
                    nhanvien.giaDinh = nv.giaDinh;
                    nhanvien.emailCaNhan = nv.emailCaNhan;
                    nhanvien.emailNoiBo = nv.emailNoiBo;
                    nhanvien.coSoLamViec = nv.coSoLamViec;
                    nhanvien.loaiHinhLamViec = nv.loaiHinhLamViec;
                    nhanvien.luongOffer = nv.luongOffer;
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
                return false;
            }
        }
    }
}
