﻿using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class ChamCongDAO
    {
        public ChamCong Get_ChamCong_By_MaNhanVien(int mnv)
        {
            var cc = new ChamCong();

            try
            {
                if (mnv > 0)
                {
                    var result = DataProvider.Instance.DBContext.ChamCong.Where(x => x.maNhanVien == mnv).FirstOrDefault();

                    if (result != null)
                    {
                        cc = result;
                        return cc;
                    }
                    else
                    {
                        return cc;
                    }
                }
                else
                {
                    return cc;
                }
            }
            catch
            {
                return cc;
            }

        }


        public int Create_ChamCong(ChamCong cc)
        {
            try
            {
                if (cc == null) return 0;
                else
                {
                    DataProvider.Instance.DBContext.ChamCong.Add(cc);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return cc.maChamCong;
                }
            }
            catch
            {
                return -1;
            }
        }

        public bool Update_ChamCong(ChamCong cc)
        {
            try
            {
                if (cc.maChamCong <= 0) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.ChamCong.FirstOrDefault(x => x.maChamCong == cc.maChamCong);
                    result.maNhanVien = cc.maNhanVien;
                    result.heSoLuong = cc.heSoLuong;
                    result.SoNgayCong = cc.SoNgayCong;
                    result.ungTruocLuong = cc.ungTruocLuong;
                    result.conLai = cc.conLai;
                    result.nghiPhep = cc.nghiPhep;
                    result.soNgayTangCa = cc.soNgayTangCa;
                    result.luongTangCa = cc.luongTangCa;
                    result.phuCapCongViec = cc.phuCapCongViec;

                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Delete_ChamCong(int mcc)
        {
            try
            {
                if (mcc <= 0) return false;
                else
                {
                    var result = DataProvider.Instance.DBContext.ChamCong.FirstOrDefault(x => x.maChamCong == mcc);

                    DataProvider.Instance.DBContext.ChamCong.Remove(result);
                    DataProvider.Instance.DBContext.SaveChanges();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}