using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class PhongBanDAO
    {
        public List<PhongBan> GetList_PhongBan()
        {
            List<PhongBan> phongBans = new List<PhongBan>();

            try
            {
                phongBans = DataProvider.Instance.DBContext.PhongBan.OrderBy(x => x.tenPhong).ToList();
            }
            catch (Exception ex)
            {
                return phongBans;
            }

            return phongBans;
        }

        public PhongBan Get_PhongBan_By_MaPhongBan(int id)
        {
            try
            {
                if (id > 0)
                {
                    return DataProvider.Instance.DBContext.PhongBan.FirstOrDefault(x => x.maPhong ==  id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
