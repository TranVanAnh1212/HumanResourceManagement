using Aspose.Words;
using Aspose.Words.Tables;
using HRMana.Model.DAO;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ThuVienWinform.Report.AsposeWordExtension;
using HRMana.Model.Models;
using System.Globalization;
using System.Threading.Tasks;
using HRMana.Main.View.Dialog;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using HRMana.Common.Commons;

namespace HRMana.Main.ViewModel
{
    public class ReportViewModel : BaseViewModel
    {
        private string _tenNV;
        private string _chucVu;
        private string _phongBan;
        private ObservableCollection<BaoCaoDangNhap_rptModel> _dsBCDN;
        private ObservableCollection<BaoCaoDsNhanVien_rptModel> _dsBCDSNV;
        private ObservableCollection<BaoCaoChamCong_prtModel> _dsBCCC;

        public int NgayBC { get; set; }
        public int ThangBC { get; set; }
        public int NamBC { get; set; }

        private DateTime? _fromDate;
        private DateTime? _toDate;

        public string TenNV { get => _tenNV; set { _tenNV = value; OnPropertyChanged(); } }
        public string ChucVu { get => _chucVu; set { _chucVu = value; OnPropertyChanged(); } }
        public string PhongBan { get => _phongBan; set { _phongBan = value; OnPropertyChanged(); } }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand Export_BaoCaoDangNhap_ReportWord { get; set; }
        public ICommand Export_BaoCaoDangNhap_ReportExcel { get; set; }
        public ICommand Export_DsNhanVien_ReportWord { get; set; }
        public ICommand Export_DsNhanVien_ReportExcel { get; set; }
        public ICommand Export_ChamCong_ReportWord { get; set; }
        public ICommand Export_ChamCong_ReportExcel { get; set; }

        public ObservableCollection<BaoCaoDangNhap_rptModel> DsBCDN { get => _dsBCDN; set { _dsBCDN = value; OnPropertyChanged(); } }
        public DateTime? FromDate { get => _fromDate; set { _fromDate = value; OnPropertyChanged(); } }
        public DateTime? ToDate { get => _toDate; set { _toDate = value; OnPropertyChanged(); } }
        public ObservableCollection<BaoCaoDsNhanVien_rptModel> DsBCDSNV { get => _dsBCDSNV; set { _dsBCDSNV = value; OnPropertyChanged(); } }
        public ObservableCollection<BaoCaoChamCong_prtModel> DsBCCC { get => _dsBCCC; set { _dsBCCC = value; OnPropertyChanged(); } }

        private void ShowMessageBoxCustom(string msg, string imagePath)
        {
            MessageBox_Custom messageBox_Custom = new MessageBox_Custom();
            messageBox_Custom.MsgBox_Content = msg;

            // Chuyển đổi đường dẫn hình ảnh từ kiểu string sang ImageSource
            ImageSource msgIcon = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            messageBox_Custom.Img_MsgIcon = msgIcon;

            messageBox_Custom.ShowDialog();
        }

        public ReportViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            ToDate = DateTime.Now;
            FromDate = DateTime.Now.AddDays(-7);
            NgayBC = DateTime.Now.Day;
            ThangBC = DateTime.Now.Month;
            NamBC = DateTime.Now.Year;

            LoadWindowCommand = new RelayCommand<object>(
                (param) => true,
                (param) =>
                {
                    GetList_BCDN(FromDate, ToDate);
                }
                );

            Export_BaoCaoDangNhap_ReportWord = new RelayCommand<object>(
                (param) => true,
                async (param) =>
                {
                    Task exportWord_ChamCong = new Task(
                        () =>
                        {
                            try
                            {
                                GetList_BCDN(FromDate, ToDate);

                                // Nạp file
                                Document baoCaoDangNhap_Template = new Document("../../Assets/Templates/BC_DangNhap.doc");

                                // Điền các thông tin cố định
                                baoCaoDangNhap_Template.MailMerge.Execute(new[] { "NgayBC" }, new[] { NgayBC.ToString() });
                                baoCaoDangNhap_Template.MailMerge.Execute(new[] { "ThangBC" }, new[] { ThangBC.ToString() });
                                baoCaoDangNhap_Template.MailMerge.Execute(new[] { "NamBC" }, new[] { NamBC.ToString() });
                                baoCaoDangNhap_Template.MailMerge.Execute(new[] { "HoTen" }, new[] { TenNV });
                                baoCaoDangNhap_Template.MailMerge.Execute(new[] { "ChucVu" }, new[] { ChucVu });
                                baoCaoDangNhap_Template.MailMerge.Execute(new[] { "PhongBan" }, new[] { PhongBan });

                                // Điền thông tin vào bảng
                                Table bangThongTinBCDN = baoCaoDangNhap_Template.GetChild(NodeType.Table, 1, true) as Table; // Láy ra bảng thứ 2

                                int hangHienTai = 1;
                                bangThongTinBCDN.InsertRows(hangHienTai, hangHienTai, DsBCDN.Count() - 1);
                                for (int i = 1; i <= DsBCDN.Count() - 1; i++)
                                {
                                    bangThongTinBCDN.PutValue(hangHienTai, 0, i.ToString()); // Cột STT
                                    bangThongTinBCDN.PutValue(hangHienTai, 2, DsBCDN[i].TenTaiKhoan.ToString()); // Cột Tên tài khoản
                                    bangThongTinBCDN.PutValue(hangHienTai, 3, DsBCDN[i].TgDangNhap.ToString()); // Cột thời gian đăng nhập
                                    bangThongTinBCDN.PutValue(hangHienTai, 4, DsBCDN[i].TgDangXuat.ToString()); // Cột thời gian đăng xuất
                                    hangHienTai++;
                                }

                                SaveFileDialog sfd = new SaveFileDialog();
                                sfd.Filter = "Word Document (*.docx)|*.docx";
                                sfd.FileName = "BaoCaoDangNhap.docx"; // Đặt tên file mặc định (có thể thay đổi)
                                sfd.Title = "Lưu file";

                                if (sfd.ShowDialog() == true)
                                {
                                    string duongDanFile = sfd.FileName;

                                    //if (File.Exists(duongDanFile))
                                    //{
                                    //    if (MessageBox.Show("File đã tồn tại. Bạn có muốn ghi đè không?", "Xác nhận ghi đè", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                    //    {
                                    //        return; // Không ghi đè, thoát khỏi phương thức
                                    //    }
                                    //}

                                    // Lưu và mở file
                                    baoCaoDangNhap_Template.Save(sfd.FileName);
                                    ShowMessageBoxCustom("Xuất báo cáo thành công. ", CommonConstant.Success_ICon);
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi xuất báo cáo: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                        }
                        );

                    exportWord_ChamCong.Start();
                    await exportWord_ChamCong;
                }
                );

            Export_BaoCaoDangNhap_ReportExcel = new RelayCommand<object>(
                (param) => true,
                async (param) =>
                {
                    Task exportExcel_BCDangNhap = new Task(() =>
                    {
                        GetList_BCDN(FromDate, ToDate);

                        string filePath = "";
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                        // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                        if (dialog.ShowDialog() == true)
                        {
                            filePath = dialog.FileName;
                        }

                        // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                        if (string.IsNullOrEmpty(filePath))
                        {
                            MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                            return;
                        }

                        try
                        {
                            // If you are a commercial business and have
                            // purchased commercial licenses use the static property
                            // LicenseContext of the ExcelPackage class:
                            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

                            // If you use EPPlus in a noncommercial context
                            // according to the Polyform Noncommercial license:
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            using (ExcelPackage p = new ExcelPackage())
                            {
                                // đặt tên người tạo file
                                p.Workbook.Properties.Author = TenNV;

                                // đặt tiêu đề cho file
                                p.Workbook.Properties.Title = "Báo cáo tài khoản đăng nhập";

                                //Tạo một sheet để làm việc trên đó
                                p.Workbook.Worksheets.Add("NewSheet");

                                // lấy sheet vừa add ra để thao tác
                                ExcelWorksheet ws = p.Workbook.Worksheets[0];

                                // đặt tên cho sheet
                                ws.Name = "Báo cáo tài khoản đăng nhập";
                                // fontsize mặc định cho cả sheet
                                ws.Cells.Style.Font.Size = 14;
                                // font family mặc định cho cả sheet
                                ws.Cells.Style.Font.Name = "Calibri";

                                // Tạo danh sách các column header
                                string[] arrColumnHeader = {
                                                "Tên tài khoản",
                                                "Thời gian đăng nhập",
                                                "Thời gian đăng xuất",
                            };

                                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                                var countColHeader = arrColumnHeader.Count();

                                // merge các column lại từ column 1 đến số column header
                                // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                                ws.Cells[1, 1].Value = "Thống kê thông tin tài khoản đăng nhập";
                                ws.Cells[1, 1, 1, countColHeader].Merge = true;
                                // in đậm
                                ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                                ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = 20;
                                ws.Cells[1, 1, 1, countColHeader].Style.WrapText = true;
                                // căn giữa
                                ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                ws.Cells[1, 1, 1, countColHeader].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                ws.Cells[1, 1, 1, countColHeader].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

                                ws.Cells[3, 1].Value = "Họ và tên người báo cáo";
                                ws.Cells[3, 1, 3, 2].Merge = true;
                                ws.Cells[3, 3].Value = TenNV;
                                ws.Cells[3, 3, 3, 3].Merge = true;

                                ws.Cells[3, 4].Value = "Chức vụ";
                                ws.Cells[3, 5].Value = ChucVu;

                                ws.Cells[3, 7].Value = "Phòng ban";
                                ws.Cells[3, 8].Value = PhongBan;

                                ws.Cells[4, 1].Value = "Ngày xuất báo cáo";
                                ws.Cells[4, 1, 4, 3].Merge = true;
                                ws.Cells[4, 4].Value = $"Ngày {NgayBC} - Tháng {ThangBC} - Năm {NamBC}";
                                ws.Cells[4, 4, 4, 6].Merge = true;

                                int colIndex = 1;
                                int rowIndex = 6;

                                //tạo các header từ column header đã tạo từ bên trên
                                foreach (var item in arrColumnHeader)
                                {
                                    var cell = ws.Cells[rowIndex, colIndex];

                                    //set màu thành gray
                                    var fill = cell.Style.Fill;
                                    fill.PatternType = ExcelFillStyle.Solid;
                                    fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                                    cell.Style.Font.Bold = true;
                                    cell.Style.WrapText = true;

                                    //căn chỉnh các border
                                    var border = cell.Style.Border;
                                    border.Bottom.Style =
                                        border.Top.Style =
                                        border.Left.Style =
                                        border.Right.Style = ExcelBorderStyle.Thin;

                                    //gán giá trị
                                    cell.Value = item;

                                    colIndex++;
                                }

                                // với mỗi item trong danh sách BCDN sẽ ghi trên 1 dòng
                                foreach (var item in DsBCDN)
                                {
                                    // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                                    colIndex = 1;

                                    // rowIndex tương ứng từng dòng dữ liệu
                                    rowIndex++;

                                    //gán giá trị cho từng cell                      
                                    // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                                    ws.Cells[rowIndex, colIndex++].Value = item.TenTaiKhoan;
                                    ws.Cells[rowIndex, colIndex++].Value = item.TgDangNhap.ToShortDateString();
                                    ws.Cells[rowIndex, colIndex++].Value = item.TgDangXuat.Value.ToShortDateString();

                                }

                                // AutoFit all columns after adding data
                                for (int i = 1; i <= ws.Dimension.End.Column; i++)
                                {
                                    ws.Column(i).AutoFit();
                                }

                                // Freeze panes so that columns 1 and 2 are always visible
                                ws.View.FreezePanes(7, 3);

                                // căn giữa
                                ws.Row(6).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                ws.Row(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                //Lưu file lại
                                Byte[] bin = p.GetAsByteArray();
                                File.WriteAllBytes(filePath, bin);
                            }
                            ShowMessageBoxCustom("Xuất excel thành công!",  CommonConstant.Success_ICon);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Có lỗi khi lưu file!\n{ex.Message}", "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });

                    exportExcel_BCDangNhap.Start();
                    await exportExcel_BCDangNhap;

                }
                );

            Export_DsNhanVien_ReportWord = new RelayCommand<object>(
                (param) => true,
                async (param) =>
                {
                    Task exportWord_DSNV = new Task(() =>
                    {
                        try
                        {
                            GetList_BCDSNV();

                            // Nạp file
                            Document baoCaoDsNhanVien_Template = new Document("../../Assets/Templates/BC_DsNhanVien.doc");

                            // Điền các thông tin cố định
                            baoCaoDsNhanVien_Template.MailMerge.Execute(new[] { "NgayBC" }, new[] { NgayBC.ToString() });
                            baoCaoDsNhanVien_Template.MailMerge.Execute(new[] { "ThangBC" }, new[] { ThangBC.ToString() });
                            baoCaoDsNhanVien_Template.MailMerge.Execute(new[] { "NamBC" }, new[] { NamBC.ToString() });
                            baoCaoDsNhanVien_Template.MailMerge.Execute(new[] { "HoTen" }, new[] { TenNV });
                            baoCaoDsNhanVien_Template.MailMerge.Execute(new[] { "ChucVu" }, new[] { ChucVu });
                            baoCaoDsNhanVien_Template.MailMerge.Execute(new[] { "PhongBan" }, new[] { PhongBan });

                            // Điền thông tin vào bảng
                            Table bangThongTinDSNV = baoCaoDsNhanVien_Template.GetChild(NodeType.Table, 1, true) as Table; // Láy ra bảng thứ 2

                            int hangHienTai = 1;
                            bangThongTinDSNV.InsertRows(hangHienTai, hangHienTai, DsBCDSNV.Count() - 1);
                            for (int i = 1; i <= DsBCDSNV.Count() - 1; i++)
                            {
                                bangThongTinDSNV.PutValue(hangHienTai, 0, DsBCDSNV[i].MaNhanVien.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 1, DsBCDSNV[i].TenNhanVien.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 2, DsBCDSNV[i].GioiTinh.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 3, DsBCDSNV[i].NgaySinh.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 4, DsBCDSNV[i].DienThoai.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 5, DsBCDSNV[i].QueQuan.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 6, DsBCDSNV[i].DanToc.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 7, DsBCDSNV[i].TonGiao.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 8, DsBCDSNV[i].ChucVu.ToString());
                                bangThongTinDSNV.PutValue(hangHienTai, 9, DsBCDSNV[i].PhongBan.ToString());
                                hangHienTai++;
                            }

                            try
                            {
                                SaveFileDialog sfd = new SaveFileDialog();
                                sfd.Filter = "Word Document (*.docx)|*.docx";
                                sfd.FileName = "BaoCaoDsNhanVien.docx"; // Đặt tên file mặc định (có thể thay đổi)
                                sfd.Title = "Lưu file";

                                if (sfd.ShowDialog() == true)
                                {
                                    string duongDanFile = sfd.FileName;

                                    //if (File.Exists(duongDanFile))
                                    //{
                                    //    if (MessageBox.Show("File đã tồn tại. Bạn có muốn ghi đè không?", "Xác nhận ghi đè", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                    //    {
                                    //        return; // Không ghi đè, thoát khỏi phương thức
                                    //    }
                                    //}

                                    // Lưu và mở file
                                    baoCaoDsNhanVien_Template.Save(sfd.FileName);
                                    ShowMessageBoxCustom("Xuất báo cáo thành công. ", CommonConstant.Success_ICon);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi lưu file: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Đã xảy ra lỗi khi xuất báo cáo: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    });

                    exportWord_DSNV.Start();
                    await exportWord_DSNV;
                }
                );

            Export_DsNhanVien_ReportExcel = new RelayCommand<object>(
                (param) => true,
                async (param) =>
                {
                    Task exportExcel_DSNV = new Task(() =>
                    {
                        try
                        {
                            GetList_BCDSNV();

                            string filePath = "";
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                            if (dialog.ShowDialog() == true)
                            {
                                filePath = dialog.FileName;
                            }

                            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                            if (string.IsNullOrEmpty(filePath))
                            {
                                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                                return;
                            }

                            try
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                using (ExcelPackage p = new ExcelPackage())
                                {
                                    // đặt tên người tạo file
                                    p.Workbook.Properties.Author = TenNV;

                                    // đặt tiêu đề cho file
                                    p.Workbook.Properties.Title = "Báo cáo Danh sách nhân viên";

                                    //Tạo một sheet để làm việc trên đó
                                    p.Workbook.Worksheets.Add("Ds Nhân viên Sheet");

                                    // lấy sheet vừa add ra để thao tác
                                    ExcelWorksheet ws = p.Workbook.Worksheets[0];

                                    // đặt tên cho sheet
                                    ws.Name = "Báo cáo Danh sách nhân viên";
                                    // fontsize mặc định cho cả sheet
                                    ws.Cells.Style.Font.Size = 14;
                                    // font family mặc định cho cả sheet
                                    ws.Cells.Style.Font.Name = "Calibri";

                                    // Tạo danh sách các column header
                                    string[] arrColumnHeader = {
                                                "Mã nhân viên",
                                                "Tên nhân viên",
                                                "Giới tính",
                                                "Ngày sinh",
                                                "Nơi sinh",
                                                "Quê quán",
                                                "Nơi ở hiện tại",
                                                "CCCD",
                                                "Điện thoại",
                                                "Gia đình",
                                                "Dân tộc",
                                                "Tôn giáo",
                                                "Email cá nhân",
                                                "Email nội bộ",
                                                "Cơ sở làm việc",
                                                "Loại hình làm việc",
                                                "Lương offer",
                                                "Trình độ",
                                                "Chuyên môn",
                                                "Chức vụ",
                                                "Phòng ban",
                                };

                                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                                    var countColHeader = arrColumnHeader.Count();

                                    // merge các column lại từ column 1 đến số column header
                                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                                    ws.Cells[1, 1].Value = "Thống kê thông tin nhân viên";
                                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                                    // in đậm
                                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = 20;
                                    ws.Cells[1, 1, 1, countColHeader].Style.WrapText = true;
                                    // căn giữa
                                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    ws.Cells[1, 1, 1, countColHeader].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    ws.Cells[1, 1, 1, countColHeader].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

                                    ws.Cells[3, 1].Value = "Họ và tên người báo cáo";
                                    ws.Cells[3, 1, 3, 2].Merge = true;
                                    ws.Cells[3, 3].Value = TenNV;
                                    ws.Cells[3, 3, 3, 3].Merge = true;

                                    ws.Cells[3, 4].Value = "Chức vụ";
                                    ws.Cells[3, 5].Value = ChucVu;

                                    ws.Cells[3, 7].Value = "Phòng ban";
                                    ws.Cells[3, 8].Value = PhongBan;

                                    ws.Cells[4, 1].Value = "Ngày xuất báo cáo";
                                    ws.Cells[4, 1, 4, 3].Merge = true;
                                    ws.Cells[4, 4].Value = $"Ngày {NgayBC} - Tháng {ThangBC} - Năm {NamBC}";
                                    ws.Cells[4, 4, 4, 6].Merge = true;

                                    int colIndex = 1;
                                    int rowIndex = 6;

                                    //tạo các header từ column header đã tạo từ bên trên
                                    foreach (var item in arrColumnHeader)
                                    {
                                        var cell = ws.Cells[rowIndex, colIndex];

                                        //set màu thành gray
                                        var fill = cell.Style.Fill;
                                        fill.PatternType = ExcelFillStyle.Solid;
                                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                                        cell.Style.Font.Bold = true;
                                        cell.Style.WrapText = true;


                                        //căn chỉnh các border
                                        var border = cell.Style.Border;
                                        border.Bottom.Style =
                                            border.Top.Style =
                                            border.Left.Style =
                                            border.Right.Style = ExcelBorderStyle.Thin;

                                        //gán giá trị
                                        cell.Value = item;

                                        colIndex++;
                                    }

                                    // với mỗi item trong danh sách BCDN sẽ ghi trên 1 dòng
                                    foreach (var item in DsBCDSNV)
                                    {
                                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                                        colIndex = 1;

                                        // rowIndex tương ứng từng dòng dữ liệu
                                        rowIndex++;

                                        //gán giá trị cho từng cell                      
                                        // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                                        ws.Cells[rowIndex, colIndex++].Value = item.MaNhanVien;
                                        ws.Cells[rowIndex, colIndex++].Value = item.TenNhanVien;
                                        ws.Cells[rowIndex, colIndex++].Value = item.GioiTinh;
                                        ws.Cells[rowIndex, colIndex++].Value = item.NgaySinh.ToShortDateString();
                                        ws.Cells[rowIndex, colIndex++].Value = item.NoiSinh;
                                        ws.Cells[rowIndex, colIndex++].Value = item.QueQuan;
                                        ws.Cells[rowIndex, colIndex++].Value = item.NoiOHienTai;
                                        ws.Cells[rowIndex, colIndex++].Value = item.CCCD;
                                        ws.Cells[rowIndex, colIndex++].Value = item.DienThoai;
                                        ws.Cells[rowIndex, colIndex++].Value = item.GiaDinh;
                                        ws.Cells[rowIndex, colIndex++].Value = item.DanToc;
                                        ws.Cells[rowIndex, colIndex++].Value = item.TonGiao;
                                        ws.Cells[rowIndex, colIndex++].Value = item.EmailCaNhan;
                                        ws.Cells[rowIndex, colIndex++].Value = item.EmailNoiBo;
                                        ws.Cells[rowIndex, colIndex++].Value = item.CoSoLamViec;
                                        ws.Cells[rowIndex, colIndex++].Value = item.LoaiHinhLamViec;
                                        ws.Cells[rowIndex, colIndex++].Value = item.LuongOffer.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN"));
                                        ws.Cells[rowIndex, colIndex++].Value = item.TrinhDo;
                                        ws.Cells[rowIndex, colIndex++].Value = item.ChuyenMon;
                                        ws.Cells[rowIndex, colIndex++].Value = item.ChucVu;
                                        ws.Cells[rowIndex, colIndex++].Value = item.PhongBan;

                                    }

                                    // AutoFit all columns after adding data
                                    for (int i = 1; i <= ws.Dimension.End.Column; i++)
                                    {
                                        ws.Column(i).AutoFit();
                                    }

                                    // Freeze panes so that columns 1 and 2 are always visible
                                    ws.View.FreezePanes(7, 3);

                                    // căn giữa
                                    ws.Row(6).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    ws.Row(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    //Lưu file lại
                                    Byte[] bin = p.GetAsByteArray();
                                    File.WriteAllBytes(filePath, bin);
                                }
                                ShowMessageBoxCustom("Xuất excel thành công!", CommonConstant.Success_ICon);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });

                    exportExcel_DSNV.Start();
                    await exportExcel_DSNV;
                }
                );

            Export_ChamCong_ReportWord = new RelayCommand<object>(
                (param) => true,
                async (param) =>
                {
                    Task exportWord = new Task(() =>
                    {
                        try
                        {
                            GetList_BCCC();

                            // Nạp file
                            Document baoCaoChamCong_Template = new Document("../../Assets/Templates/BC_ChamCong.doc");

                            // Điền các thông tin cố định
                            baoCaoChamCong_Template.MailMerge.Execute(new[] { "NgayBC" }, new[] { NgayBC.ToString() });
                            baoCaoChamCong_Template.MailMerge.Execute(new[] { "ThangBC" }, new[] { ThangBC.ToString() });
                            baoCaoChamCong_Template.MailMerge.Execute(new[] { "NamBC" }, new[] { NamBC.ToString() });
                            baoCaoChamCong_Template.MailMerge.Execute(new[] { "HoTen" }, new[] { TenNV });
                            baoCaoChamCong_Template.MailMerge.Execute(new[] { "ChucVu" }, new[] { ChucVu });
                            baoCaoChamCong_Template.MailMerge.Execute(new[] { "PhongBan" }, new[] { PhongBan });

                            // Điền thông tin vào bảng
                            Table bangThongTinChamCong = baoCaoChamCong_Template.GetChild(NodeType.Table, 1, true) as Table; // Láy ra bảng thứ 2

                            int hangHienTai = 1;
                            bangThongTinChamCong.InsertRows(hangHienTai, hangHienTai, DsBCCC.Count() - 1);
                            for (int i = 1; i <= DsBCCC.Count() - 1; i++)
                            {
                                bangThongTinChamCong.PutValue(hangHienTai, 0, DsBCCC[i].MaNhanVien.ToString());
                                bangThongTinChamCong.PutValue(hangHienTai, 1, DsBCCC[i].TenNhanVien.ToString());
                                bangThongTinChamCong.PutValue(hangHienTai, 2, DsBCCC[i].HeSoLuong.ToString());
                                bangThongTinChamCong.PutValue(hangHienTai, 3, DsBCCC[i].LuongCoBan.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN")));
                                bangThongTinChamCong.PutValue(hangHienTai, 4, DsBCCC[i].SoNgayCong.ToString());
                                bangThongTinChamCong.PutValue(hangHienTai, 5, DsBCCC[i].UngTruocLuong.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN")));
                                bangThongTinChamCong.PutValue(hangHienTai, 6, DsBCCC[i].ConLai.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN")));
                                bangThongTinChamCong.PutValue(hangHienTai, 7, DsBCCC[i].NghiPhep.ToString());
                                bangThongTinChamCong.PutValue(hangHienTai, 8, DsBCCC[i].SoNgayTangCa.ToString());
                                bangThongTinChamCong.PutValue(hangHienTai, 9, DsBCCC[i].LuongTangCa.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN")));
                                bangThongTinChamCong.PutValue(hangHienTai, 10, DsBCCC[i].PhuCapCongViec.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN")));
                                hangHienTai++;
                            }

                            try
                            {
                                SaveFileDialog sfd = new SaveFileDialog();
                                sfd.Filter = "Word Document (*.docx)|*.docx";
                                sfd.FileName = "BaoCaoChamCong.docx"; // Đặt tên file mặc định (có thể thay đổi)
                                sfd.Title = "Lưu file";

                                if (sfd.ShowDialog() == true)
                                {
                                    string duongDanFile = sfd.FileName;

                                    //if (File.Exists(duongDanFile))
                                    //{
                                    //    if (MessageBox.Show("File đã tồn tại. Bạn có muốn ghi đè không?", "Xác nhận ghi đè", MessageBoxButton.YesNo) == MessageBoxResult.No)
                                    //    {
                                    //        return; // Không ghi đè, thoát khỏi phương thức
                                    //    }
                                    //}

                                    // Lưu và mở file
                                    baoCaoChamCong_Template.Save(sfd.FileName);
                                    ShowMessageBoxCustom("Xuất báo cáo thành công. ", CommonConstant.Success_ICon);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Đã xảy ra lỗi khi lưu file: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Đã xảy ra lỗi khi xuất báo cáo: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    });

                    exportWord.Start();
                    await exportWord;
                });

            Export_ChamCong_ReportExcel = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                async (param) =>
                {
                    try
                    {
                        GetList_BCCC();

                        string filePath = "";
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

                        // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                        if (dialog.ShowDialog() == true)
                        {
                            filePath = dialog.FileName;
                        }

                        // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                        if (string.IsNullOrEmpty(filePath))
                        {
                            MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                            return;
                        }

                        Task exprtExcel_ChamCong = new Task(() =>
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            using (ExcelPackage p = new ExcelPackage())
                            {
                                // đặt tên người tạo file
                                p.Workbook.Properties.Author = TenNV;

                                // đặt tiêu đề cho file
                                p.Workbook.Properties.Title = "Báo cáo chấm công";

                                for (int i = 1; i <= 12; i++)
                                {
                                    //Tạo một sheet để làm việc trên đó
                                    p.Workbook.Worksheets.Add("Sheet-" + i);

                                    // lấy sheet vừa add ra để thao tác
                                    ExcelWorksheet ws = p.Workbook.Worksheets[i - 1];

                                    // đặt tên cho sheet
                                    ws.Name = "Sheet - tháng " + i;
                                    // fontsize mặc định cho cả sheet
                                    ws.Cells.Style.Font.Size = 14;
                                    // font family mặc định cho cả sheet
                                    ws.Cells.Style.Font.Name = "Calibri";

                                    #region export file excel chấm công
                                    // Tạo danh sách các column header
                                    string[] arrColumnHeader = {
                                                    "Thời gian",
                                                    "Mã nhân viên",
                                                    "Tên nhân viên",
                                                    "Hệ số lương",
                                                    "Lương cơ bản",
                                                    "Số ngày công",
                                                    "Ứng trước lương",
                                                    "Còn lại",
                                                    "Nghỉ phép",
                                                    "Số ngày tăng ca",
                                                    "Lương tăng ca",
                                                    "Phụ cấp công việc",
                                    };

                                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                                    var countColHeader = arrColumnHeader.Count();

                                    // merge các column lại từ column 1 đến số column header
                                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                                    ws.Cells[1, 1].Value = "Thống kê thông tin chấm công nhân viên";
                                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                                    // in đậm
                                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Size = 20;
                                    ws.Cells[1, 1, 1, countColHeader].Style.WrapText = true;
                                    // căn giữa
                                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    ws.Cells[1, 1, 1, countColHeader].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    ws.Cells[1, 1, 1, countColHeader].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

                                    ws.Cells[3, 1].Value = "Họ và tên người báo cáo";
                                    ws.Cells[3, 1, 3, 2].Merge = true;
                                    ws.Cells[3, 3].Value = TenNV;
                                    ws.Cells[3, 3, 3, 3].Merge = true;

                                    ws.Cells[3, 4].Value = "Chức vụ";
                                    ws.Cells[3, 5].Value = ChucVu;

                                    ws.Cells[3, 7].Value = "Phòng ban";
                                    ws.Cells[3, 8].Value = PhongBan;

                                    ws.Cells[4, 1].Value = "Ngày xuất báo cáo";
                                    ws.Cells[4, 1, 4, 3].Merge = true;
                                    ws.Cells[4, 4].Value = $"Ngày {NgayBC} - Tháng {ThangBC} - Năm {NamBC}";
                                    ws.Cells[4, 4, 4, 6].Merge = true;

                                    int colIndex = 1;
                                    int rowIndex = 6;

                                    //tạo các header từ column header đã tạo từ bên trên
                                    foreach (var item in arrColumnHeader)
                                    {
                                        var cell = ws.Cells[rowIndex, colIndex];

                                        //set màu thành gray
                                        var fill = cell.Style.Fill;
                                        fill.PatternType = ExcelFillStyle.Solid;
                                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                                        cell.Style.Font.Bold = true;
                                        cell.Style.WrapText = true;


                                        //căn chỉnh các border
                                        var border = cell.Style.Border;
                                        border.Bottom.Style =
                                            border.Top.Style =
                                            border.Left.Style =
                                            border.Right.Style = ExcelBorderStyle.Thin;

                                        //gán giá trị
                                        cell.Value = item;

                                        colIndex++;
                                    }

                                    var currentMonth = DsBCCC.Where(x => x.Thang == i && x.Nam == DateTime.Now.Year).ToList();

                                    // với mỗi item trong danh sách BCDN sẽ ghi trên 1 dòng
                                    foreach (var item in currentMonth)
                                    {
                                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                                        colIndex = 1;

                                        // rowIndex tương ứng từng dòng dữ liệu
                                        rowIndex++;

                                        //gán giá trị cho từng cell                      
                                        // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                                        //ws.Cells[rowIndex, colIndex++].Value = item.;
                                        ws.Cells[rowIndex, colIndex++].Value = $"{item.Thang}/{item.Nam}";
                                        ws.Cells[rowIndex, colIndex++].Value = item.MaNhanVien;
                                        ws.Cells[rowIndex, colIndex++].Value = item.TenNhanVien;
                                        ws.Cells[rowIndex, colIndex++].Value = item.HeSoLuong;
                                        ws.Cells[rowIndex, colIndex++].Value = item.LuongCoBan.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN"));
                                        ws.Cells[rowIndex, colIndex++].Value = item.SoNgayCong;
                                        ws.Cells[rowIndex, colIndex++].Value = item.UngTruocLuong.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN"));
                                        ws.Cells[rowIndex, colIndex++].Value = item.ConLai.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN"));
                                        ws.Cells[rowIndex, colIndex++].Value = item.NghiPhep;
                                        ws.Cells[rowIndex, colIndex++].Value = item.SoNgayTangCa;
                                        ws.Cells[rowIndex, colIndex++].Value = item.LuongTangCa.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN"));
                                        ws.Cells[rowIndex, colIndex++].Value = item.PhuCapCongViec.Value.ToString("N0", CultureInfo.CreateSpecificCulture("vi-VN"));

                                    }

                                    // AutoFit all columns after adding data
                                    for (int j = 1; j <= ws.Dimension.End.Column; j++)
                                    {
                                        ws.Column(j).AutoFit();
                                    }

                                    // Freeze panes so that columns 1 and 2 are always visible
                                    ws.View.FreezePanes(8, 3);

                                    // căn giữa
                                    ws.Row(6).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                    ws.Row(6).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                    #endregion

                                }

                                //Lưu file lại
                                Byte[] bin = p.GetAsByteArray();
                                File.WriteAllBytes(filePath, bin);
                            }

                        });

                        exprtExcel_ChamCong.Start();
                        await exprtExcel_ChamCong;

                        ShowMessageBoxCustom("Xuất excel thành công!", CommonConstant.Success_ICon);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
        }

        public void GetList_BCDN(DateTime? fromDate, DateTime? toDate)
        {
            var result = new BaoCaoDangNhapDAO().GetList_BaoCaoDangNhap();
            var dsBCTKDN = new List<BaoCaoDangNhap_rptModel>();
            foreach (var item in result)
            {
                var bc = new BaoCaoDangNhap_rptModel()
                {
                    IdDangNhap = item.idDangNhap,
                    TenTaiKhoan = new TaiKhoanDAO().Get_TaiKhoan_By_MaTaiKhoan(item.maTaiKhoan).tenTaiKhoan,
                    TgDangNhap = item.tgDangNhap,
                    TgDangXuat = item.tgDangXuat,
                };

                dsBCTKDN.Add(bc);
            }

            if (fromDate != null)
            {
                dsBCTKDN = dsBCTKDN.Where(x => x.TgDangNhap >= fromDate && x.TgDangNhap <= toDate).ToList();
            }

            DsBCDN = new ObservableCollection<BaoCaoDangNhap_rptModel>(dsBCTKDN);
        }

        public void GetList_BCDSNV()
        {
            try
            {
                var DsNv = new NhanVienDAO().GetList_NhanVien();
                var DsTam = new List<BaoCaoDsNhanVien_rptModel>();
                if (DsNv.Count > 0)
                {
                    foreach (var item in DsNv)
                    {
                        var nv = new BaoCaoDsNhanVien_rptModel()
                        {
                            MaNhanVien = item.maNhanVien,
                            TenNhanVien = item.tenNhanVien,
                            NgaySinh = item.ngaySinh,
                            GioiTinh = item.gioiTinh,
                            NoiSinh = item.noiSinh,
                            NoiOHienTai = item.noiOHienTai,
                            CCCD = item.CCCD,
                            EmailCaNhan = item.emailCaNhan,
                            EmailNoiBo = item.emailNoiBo,
                            DienThoai = item.dienThoai,
                            QueQuan = item.queQuan,
                            CoSoLamViec = item.coSoLamViec,
                            LoaiHinhLamViec = item.loaiHinhLamViec,
                            LuongOffer = item.luongOffer,
                            TrinhDo = item.TrinhDo.tenTrinhDo,
                            ChucVu = item.ChucVu.tenChucVu,
                            PhongBan = item.PhongBan.tenPhong,
                            TonGiao = item.TonGiao.tenTonGiao,
                            DanToc = item.DanToc.tenDanToc,
                        };

                        DsTam.Add(nv);
                    }
                }

                DsBCDSNV = new ObservableCollection<BaoCaoDsNhanVien_rptModel>(DsTam);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        public void GetList_BCCC()
        {
            try
            {
                var dsCC = new ChamCongDAO().GetList_ChamCong();
                var dsTam = new List<BaoCaoChamCong_prtModel>();

                if (dsCC != null)
                {
                    foreach (var item in dsCC)
                    {
                        var cc = new BaoCaoChamCong_prtModel()
                        {
                            Thang = item.Thang.Value,
                            Nam = item.Nam.Value,
                            MaNhanVien = item.maNhanVien,
                            TenNhanVien = item.NhanVien.tenNhanVien,
                            HeSoLuong = item.heSoLuong,
                            LuongCoBan = item.BacLuong.luongCoBan,
                            SoNgayCong = item.SoNgayCong,
                            UngTruocLuong = item.ungTruocLuong,
                            ConLai = item.conLai,
                            NghiPhep = item.nghiPhep,
                            SoNgayTangCa = item.soNgayTangCa,
                            LuongTangCa = item.luongTangCa,
                            PhuCapCongViec = item.phuCapCongViec,
                        };

                        dsTam.Add(cc);
                    }
                }

                DsBCCC = new ObservableCollection<BaoCaoChamCong_prtModel>(dsTam);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


