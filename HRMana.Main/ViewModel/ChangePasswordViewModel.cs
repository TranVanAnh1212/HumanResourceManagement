using HRMana.Common.Commons;
using HRMana.Main.View.SystemManagement;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private string _matKhauCu;
        private string _matKhauMoi;
        private string _xacNhanMatKhauMoi;

        public ICommand ChangePasswordCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public string MatKhauCu { get => _matKhauCu; set { _matKhauCu = value; OnPropertyChanged(); } }
        public string MatKhauMoi { get => _matKhauMoi; set { _matKhauMoi = value; OnPropertyChanged(); } }
        public string XacNhanMatKhauMoi { get => _xacNhanMatKhauMoi; set { _xacNhanMatKhauMoi = value; OnPropertyChanged(); } }

        public ChangePasswordViewModel()
        {
            ChangePasswordCommand = new RelayCommand<object>(
                (param) => true,
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
                            if (string.IsNullOrEmpty(XacNhanMatKhauMoi))
                            {
                                MessageBox.Show("Chưa xác nhận mật khẩu!", "Cảnh báo.", MessageBoxButton.OK, MessageBoxImage.Warning);

                            }
                            else
                            {
                                if (MatKhauCu.Equals(MatKhauMoi))
                                {
                                    MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ!", "Cảnh báo.", MessageBoxButton.OK, MessageBoxImage.Warning);

                                }
                                else
                                {
                                    if (!XacNhanMatKhauMoi.Equals(MatKhauMoi))
                                    {
                                        MessageBox.Show("Xác nhận mật khẩu thất bại!", "Cảnh báo.", MessageBoxButton.OK, MessageBoxImage.Warning);
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
