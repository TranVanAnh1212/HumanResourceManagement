using HRMana.Common.Commons;
using HRMana.Main.View.SystemManagement;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel, IDataErrorInfo
    {
        private string _matKhauCu;
        private string _matKhauMoi;
        private string _xacNhanMatKhauMoi;

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string MatKhauCu
        {
            get => _matKhauCu;
            set
            {
                _matKhauCu = value; OnPropertyChanged();
            }
        }
        public string MatKhauMoi { get => _matKhauMoi; set { _matKhauMoi = value; OnPropertyChanged(); } }
        public string XacNhanMatKhauMoi { get => _xacNhanMatKhauMoi; set { _xacNhanMatKhauMoi = value; OnPropertyChanged(); } }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                var err = "";

                switch (columnName)
                {
                    case "MatKhauCu":
                        if (string.IsNullOrEmpty(MatKhauCu))
                        {
                            err = "Hãy nhập mật khẩu hiện tại";
                        }

                        break;
                    case "MatKhauMoi":
                        if (string.IsNullOrEmpty(MatKhauMoi))
                        {
                            err = "Hãy nhập mật khẩu mới";
                        }

                        if (!string.IsNullOrEmpty(MatKhauMoi) && MatKhauMoi.Length < 6)
                        {
                            err = "Mật khẩu tối thiểu 6 ký tự";
                        }

                        if (!string.IsNullOrEmpty(MatKhauCu) && !string.IsNullOrEmpty(MatKhauMoi) && MatKhauCu.Equals(MatKhauMoi))
                        {
                            err = "Mật khẩu mới phải khác mật khẩu cũ";
                        }
                        break;
                    case "XacNhanMatKhauMoi":
                        if (string.IsNullOrEmpty(XacNhanMatKhauMoi))
                        {
                            err = "Hãy nhập lại mật khẩu mới";
                        }
                        
                        if (!string.IsNullOrEmpty(MatKhauMoi) && !string.IsNullOrEmpty(XacNhanMatKhauMoi) && !XacNhanMatKhauMoi.Equals(MatKhauMoi, StringComparison.Ordinal))
                        {
                            err = "Xác nhận mật khẩu không đúng";
                        }
                        break;
                }


                return err;
            }
        }

        public ChangePasswordViewModel()
        {
            ChangePasswordCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (string.IsNullOrEmpty(MatKhauMoi))
                        return false;

                    if (string.IsNullOrEmpty(MatKhauCu))
                        return false;

                    if (MatKhauMoi.Length < 6)
                        return false;

                    if (MatKhauCu.Equals(MatKhauMoi))
                        return false;

                    if (string.IsNullOrEmpty(XacNhanMatKhauMoi))
                        return false;

                    if (!XacNhanMatKhauMoi.Equals(MatKhauMoi))
                        return false;



                    return true;
                },
                (param) =>
                {
                    try
                    {
                        if (!CommonConstant.taiKhoanDN.matKhau.Equals(StringHelper.MD5Hash(StringHelper.Base64Encode(MatKhauCu))))
                        {
                            MessageBox.Show("Mật khẩu hiện tại không đúng!", "Cảnh báo.", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                        else
                        {
                            var tk = new TaiKhoan()
                            {
                                maTaiKhoan = CommonConstant.taiKhoanDN.maTaiKhoan,
                                matKhau = StringHelper.MD5Hash(StringHelper.Base64Encode(MatKhauMoi))
                            };

                            var result = new TaiKhoanDAO().ChangePassword(tk);

                            if (result)
                            {
                                MessageBox.Show("Thay đổi mật khẩu thành công");
                                ChangedPasswordWindow changedPasswordWindow = new ChangedPasswordWindow();
                                changedPasswordWindow.Close();
                            }
                            else
                            {
                                MessageBox.Show("Thay đổi mật khẩu thất bại");

                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            CancelCommand = new RelayCommand<object>(
                (param) => true,
                (param) =>
                {
                    MatKhauCu = string.Empty;
                    MatKhauMoi = string.Empty;
                    XacNhanMatKhauMoi = string.Empty;
                }
                );
        }
    }
}
