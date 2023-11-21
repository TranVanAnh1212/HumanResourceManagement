using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Model.Models
{
    public class BaoCaoDangNhap_rptModel
    {
        public int IdDangNhap { get; set; }
        public string TenTaiKhoan { get; set; }
        public System.DateTime TgDangNhap { get; set; }
        public Nullable<System.DateTime> TgDangXuat { get; set; }
    }
}
