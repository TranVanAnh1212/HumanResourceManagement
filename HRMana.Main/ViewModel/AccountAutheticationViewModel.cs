using HRMana.Common.Commons;
using HRMana.Main.View.Dialog;
using HRMana.Main.View.SystemManagement;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace HRMana.Main.ViewModel
{
    public class AccountAutheticationViewModel : BaseViewModel
    {
        private string _maQuyen;
        private string _tenQuyen;
        private ObservableCollection<ChiTietQuyen_Quyen> _CTQ_Q;
        private ObservableCollection<Quyen> _dsQuyen;
        private Quyen _selectedQuyen;
        private bool _isADD_Cheked;
        private bool _isEDIT_Cheked;
        private bool _isDEL_Cheked;
        private bool _isVIEW_Cheked;
        private bool _isMUSER_Cheked;
        private int _maTaiKhoan;
        private string _tenTaiKhoan;
        private string _quyenTaiKhoan;
        private string _maQuyen_tk;
        private ObservableCollection<TaiKhoan> _listTaiKhoan;
        private TaiKhoan _selectedTaiKhoan;
        private ObservableCollection<Quyen> _listQuyen;
        private Quyen _selectedQuyenTK;

        public ICommand LoadWindowCommand { get; set; }
        public ICommand CanCelCommand { get; set; }
        public ICommand Cancel_Quyen_TaiKhoan { get; set; }
        public ICommand CreateNew_Quyen { get; set; }
        public ICommand Delete_Quyen { get; set; }
        public ICommand Update_Quyen { get; set; }
        public ICommand Update_Quyen_TaiKhoan { get; set; }

        public string MaQuyen { get => _maQuyen; set { _maQuyen = value; OnPropertyChanged(); } }
        public string TenQuyen { get => _tenQuyen; set { _tenQuyen = value; OnPropertyChanged(); } }
        public ObservableCollection<ChiTietQuyen_Quyen> CTQ_Q { get => _CTQ_Q; set { _CTQ_Q = value; OnPropertyChanged(); } }
        public ObservableCollection<Quyen> DsQuyen { get => _dsQuyen; set { _dsQuyen = value; OnPropertyChanged(); } }
        public Quyen SelectedQuyen
        {
            get => _selectedQuyen;
            set
            {
                _selectedQuyen = value;
                OnPropertyChanged();

                if (SelectedQuyen != null)
                {
                    MaQuyen = SelectedQuyen.maQuyen;
                    TenQuyen = SelectedQuyen.tenQuyen;

                    CTQ_Q = new ObservableCollection<ChiTietQuyen_Quyen>(new QuyenDAO().GetList_CTQQ_By_MaQuyen(SelectedQuyen.maQuyen));

                    var permissions = new Dictionary<string, bool>
                            {
                                { "VIEW", true },
                                { "ADD", false },
                                { "EDIT", false },
                                { "DEL", false },
                                { "MUSER", false },
                            };

                    foreach (var i in CTQ_Q)
                    {
                        if (permissions.ContainsKey(i.Chitiet_Quyen.mahanhDong))
                        {
                            permissions[i.Chitiet_Quyen.mahanhDong] = true;
                        }
                    }

                    IsADD_Cheked = permissions["ADD"];
                    IsEDIT_Cheked = permissions["EDIT"];
                    IsDEL_Cheked = permissions["DEL"];
                    IsVIEW_Cheked = permissions["VIEW"];
                    IsMUSER_Cheked = permissions["MUSER"];

                }
            }
        }

        public bool IsADD_Cheked { get => _isADD_Cheked; set { _isADD_Cheked = value; OnPropertyChanged(); } }
        public bool IsEDIT_Cheked { get => _isEDIT_Cheked; set { _isEDIT_Cheked = value; OnPropertyChanged(); } }
        public bool IsDEL_Cheked { get => _isDEL_Cheked; set { _isDEL_Cheked = value; OnPropertyChanged(); } }
        public bool IsVIEW_Cheked { get => _isVIEW_Cheked; set { _isVIEW_Cheked = value; OnPropertyChanged(); } }
        public bool IsMUSER_Cheked { get => _isMUSER_Cheked; set { _isMUSER_Cheked = value; OnPropertyChanged(); } }

        public string TenTaiKhoan { get => _tenTaiKhoan; set { _tenTaiKhoan = value; OnPropertyChanged(); } }
        public string QuyenTaiKhoan { get => _quyenTaiKhoan; set { _quyenTaiKhoan = value; OnPropertyChanged(); } }
        public string MaQuyen_TK { get => _maQuyen_tk; set { _maQuyen_tk = value; } }
        public ObservableCollection<TaiKhoan> ListTaiKhoan { get => _listTaiKhoan; set { _listTaiKhoan = value; OnPropertyChanged(); } }
        public ObservableCollection<Quyen> ListQuyen { get => _listQuyen; set { _listQuyen = value; OnPropertyChanged(); } }

        public Quyen SelectedQuyenTK
        {
            get
            {
                return _selectedQuyenTK;
            }
            set
            {
                _selectedQuyenTK = value;
                OnPropertyChanged();

                if (SelectedQuyenTK != null)
                {
                    MaQuyen_TK = SelectedQuyenTK.maQuyen;
                }
            }
        }

        public TaiKhoan SelectedTaiKhoan
        {
            get => _selectedTaiKhoan;
            set
            {
                _selectedTaiKhoan = value;
                OnPropertyChanged();

                if (SelectedTaiKhoan != null)
                {
                    MaTaiKhoan = SelectedTaiKhoan.maTaiKhoan;
                    TenTaiKhoan = SelectedTaiKhoan.tenTaiKhoan;
                    MaQuyen_TK = SelectedTaiKhoan.maQuyen;
                    SelectedQuyenTK = ListQuyen.FirstOrDefault(x => x.maQuyen == SelectedTaiKhoan.maQuyen);
                }
            }
        }
        public int MaTaiKhoan { get => _maTaiKhoan; set { _maTaiKhoan = value; OnPropertyChanged(); } }

        public AccountAutheticationViewModel()
        {
            LoadWindowCommand = new RelayCommand<Object>(
                (param) => { return true; },
                async (param) =>
                {
                    Task loading_Task = new Task(() =>
                    {
                        GetList_Quyen();
                        ListTaiKhoan = new ObservableCollection<TaiKhoan>(new TaiKhoanDAO().GetListTaiKhoan());
                        ListQuyen = new ObservableCollection<Quyen>(new QuyenDAO().GetListQuyen());
                    });

                    loading_Task.Start();
                    await loading_Task;
                }
                );

            CanCelCommand = new RelayCommand<Object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    EmptyField();
                }
                );

            Cancel_Quyen_TaiKhoan = new RelayCommand<Object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    EmptyField_TKChanged();
                }
                );

            CreateNew_Quyen = new RelayCommand<Object>(
                (param) =>
                {
                    if (SelectedQuyen != null) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        IsVIEW_Cheked = true;

                        var q = new Quyen()
                        {
                            maQuyen = MaQuyen,
                            tenQuyen = TenQuyen,
                        };

                        var result = new QuyenDAO().CreateNew_Quyen(q);

                        if (result)
                        {
                            string ADD_Action = IsADD_Cheked ? "ADD" : "";
                            string EDIT_Action = IsEDIT_Cheked ? "EDIT" : "";
                            string DEL_Action = IsDEL_Cheked ? "DEL" : "";
                            string VIEW_Action = IsVIEW_Cheked ? "VIEW" : "VIEW";
                            string MUSER_Action = IsMUSER_Cheked ? "MUSER" : "";

                            if (IsADD_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = ADD_Action,
                                    moTa = "Quyền được thêm mới.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsEDIT_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = EDIT_Action,
                                    moTa = "Quyền được sửa đổi.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsDEL_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = DEL_Action,
                                    moTa = "Quyền được xóa.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsVIEW_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = VIEW_Action,
                                    moTa = "Quyền được xem.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsMUSER_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = MUSER_Action,
                                    moTa = "Quyền được quản tri người dùng.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            GetList_Quyen();
                            ShowMessageBoxCustom("Thêm mới quyền thành công.", CommonConstant.Success_ICon);
                        }
                        else
                        {
                            ShowMessageBoxCustom("Thêm mới quyền thất bại.", CommonConstant.Error_ICon);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            Delete_Quyen = new RelayCommand<Object>(
                (param) =>
                {
                    if (SelectedQuyen == null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow dialog = new DialogWindow();
                        dialog.DialogMessage = "Bạn có chắc muốn xóa?";
                        dialog.Owner = Window.GetWindow(new AccountUserPage());

                        if (true == dialog.ShowDialog())
                        {
                            // Thay đổi quyền của tk thành quyền nv
                            var result_tk = new TaiKhoanDAO().ChangePermission_ListTaiKhoan(MaQuyen);

                            // tiến hành xóa CTQ_Q
                            var list_ctqq = new QuyenDAO().GetList_CTQQ_By_MaQuyen(MaQuyen);

                            foreach (var item in list_ctqq)
                            {
                                var result = new ChiTietQuyen_QuyenDAO().Delete_CTQQ(item.maQuyen, item.maChitietQuyen);
                            }

                            // Xóa quyền
                            var result_q = new QuyenDAO().Delete_Quyen(MaQuyen);

                            if (result_q)
                            {
                                EmptyField();
                                GetList_Quyen();
                                ShowMessageBoxCustom("Xóa quyền thành công.", CommonConstant.Success_ICon);
                            }
                            else
                            {
                                ShowMessageBoxCustom("Xóa quyền thất bại.", CommonConstant.Error_ICon);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            Update_Quyen = new RelayCommand<Object>(
                (param) =>
                {
                    if (SelectedQuyen == null) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        DialogWindow d = new DialogWindow();
                        d.DialogMessage = "Bạn có chắc muốn cập nhật quyền?.";
                        d.Owner = Window.GetWindow(new AccountUserPage());

                        if (true == d.ShowDialog())
                        {
                            var q = new Quyen()
                            {
                                maQuyen = MaQuyen,
                                tenQuyen = TenQuyen
                            };

                            var result_q = new QuyenDAO().Update_Quyen(q);

                            // Xóa chi tiết quyền quyền hiện tại
                            var list_ctqq = new QuyenDAO().GetList_CTQQ_By_MaQuyen(MaQuyen);

                            foreach ( var ctqq in list_ctqq )
                            {
                                var result_Ctqq = new ChiTietQuyen_QuyenDAO().Delete_CTQQ(ctqq.maQuyen, ctqq.maChitietQuyen);
                            }

                            // Thêm lại chi tiết quyền quyền
                            string ADD_Action = IsADD_Cheked ? "ADD" : "";
                            string EDIT_Action = IsEDIT_Cheked ? "EDIT" : "";
                            string DEL_Action = IsDEL_Cheked ? "DEL" : "";
                            string VIEW_Action = IsVIEW_Cheked ? "VIEW" : "";
                            string MUSER_Action = IsMUSER_Cheked ? "MUSER" : "";

                            if (IsADD_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = ADD_Action,
                                    moTa = "Quyền được thêm mới.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsEDIT_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = EDIT_Action,
                                    moTa = "Quyền được sửa đổi.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsDEL_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = DEL_Action,
                                    moTa = "Quyền được xóa.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsVIEW_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = VIEW_Action,
                                    moTa = "Quyền được xem.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            if (IsMUSER_Cheked)
                            {
                                var ctqq = new ChiTietQuyen_Quyen()
                                {
                                    maQuyen = MaQuyen,
                                    maChitietQuyen = MUSER_Action,
                                    moTa = "Quyền được quản tri người dùng.",
                                };

                                var result_ADD = new ChiTietQuyen_QuyenDAO().CreateNew_ChiTietQuyen_Quyen(ctqq);
                            }

                            GetList_Quyen();
                            EmptyField();
                            ShowMessageBoxCustom("Cập nhật quyền thành công.", CommonConstant.Success_ICon);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );

            Update_Quyen_TaiKhoan = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedTaiKhoan == null) return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        var tk = new TaiKhoan()
                        {
                            maTaiKhoan = MaTaiKhoan,
                            maQuyen = MaQuyen_TK

                        };

                        var result = new TaiKhoanDAO().ChangePermission_TaiKhoan(tk);

                        if (result)
                        {
                            EmptyField_TKChanged();
                            ListTaiKhoan = new ObservableCollection<TaiKhoan>(new TaiKhoanDAO().GetListTaiKhoan());

                            ShowMessageBoxCustom("Cập nhật quyền tài khoản thành công.", CommonConstant.Success_ICon);
                        }
                        else
                        {
                            ShowMessageBoxCustom("Cập nhật quyền tài khoản thất bại.", CommonConstant.Error_ICon);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );
        }

        private void EmptyField_TKChanged()
        {
            MaQuyen_TK = TenTaiKhoan = string.Empty;
            MaTaiKhoan = 0;
            SelectedQuyenTK = null;
            SelectedTaiKhoan = null;
        }

        private void ShowMessageBoxCustom(string msg, string imagePath)
        {
            MessageBox_Custom messageBox_Custom = new MessageBox_Custom();
            messageBox_Custom.MsgBox_Content = msg;

            // Chuyển đổi đường dẫn hình ảnh từ kiểu string sang ImageSource
            ImageSource msgIcon = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

            messageBox_Custom.Img_MsgIcon = msgIcon;

            messageBox_Custom.ShowDialog();
        }


        private void EmptyField()
        {
            MaQuyen = TenQuyen = string.Empty;
            CTQ_Q = null;
            IsADD_Cheked = IsDEL_Cheked = IsEDIT_Cheked = IsMUSER_Cheked = IsVIEW_Cheked = false;
            SelectedQuyen = null;
        }

        private void GetList_Quyen()
        {
            try
            {
                DsQuyen = new ObservableCollection<Quyen>(new QuyenDAO().GetListQuyen());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo lỗi.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
