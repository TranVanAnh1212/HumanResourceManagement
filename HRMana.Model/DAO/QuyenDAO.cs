using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.DAO
{
    public class QuyenDAO
    {
        public List<Quyen> GetListQuyen()
        {
            var listQuyen = new List<Quyen>();

            try
            {
                listQuyen = DataProvider.Instance.DBContext.Quyen.ToList();
            }
            catch (Exception ex) 
            {
                return listQuyen;
            }

            return listQuyen;
        }
    }
}
