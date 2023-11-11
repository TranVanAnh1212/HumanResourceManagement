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
    }
}
