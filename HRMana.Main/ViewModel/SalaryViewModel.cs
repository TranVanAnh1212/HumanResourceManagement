﻿using HRMana.Main.View.Dialog;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using HRMana.Common.Commons;

namespace HRMana.Main.ViewModel
{
    public class SalaryViewModel : BaseViewModel
    {
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;
        private decimal _heSoLuong;
        private string _luongCoBan;
        private ObservableCollection<BacLuong> _dsBacLuong;
        private BacLuong _selectedBacLuong;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_BacLuongCommand { get; set; }
        public ICommand Update_BacLuongCommand { get; set; }
        public ICommand Delete_BacLuongCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }
        public decimal HeSoLuong { get => _heSoLuong; set { _heSoLuong = value; OnPropertyChanged(); } }
        public string LuongCoBan { get => _luongCoBan; set { _luongCoBan = value; OnPropertyChanged(); } }

        public ObservableCollection<BacLuong> DsBacLuong { get => _dsBacLuong; set { _dsBacLuong = value; OnPropertyChanged(); } }
        public BacLuong SelectedBacLuong
        {
            get => _selectedBacLuong;
            set
            {
                _selectedBacLuong = value;
                OnPropertyChanged();

                if (SelectedBacLuong != null)
                {
                    HeSoLuong = SelectedBacLuong.heSoLuong;
                    LuongCoBan = SelectedBacLuong.luongCoBan.ToString();
                }
            }
        }

        public SalaryViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            Page = 1;
            TotalPage = 1;
            PageSize = 20;

            IncreasePageCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (Page == TotalPage) return false;

                    return true;
                },
                (param) =>
                {
                    if (Page < TotalPage)
                    {
                        Page += 1;
                        GetList_BacLuong();
                    }
                }
                );

            ReducePageCommand = new RelayCommand<Object>(
                (param) =>
                {
                    if (Page == 1) return false;

                    return true;
                },
                (param) =>
                {
                    if (Page > 1)
                    {
                        Page -= 1;
                        GetList_BacLuong();
                    }
                }
                );

            BackToStartCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    Page = 1;
                    GetList_BacLuong();
                }
                );

            GoToEndCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    Page = TotalPage;
                    GetList_BacLuong();
                }
                );

            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    Thread GetData_Thread = new Thread(() =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            GetList_BacLuong();

                            // Xét quyền của tài khoản
                            var permissions = new Dictionary<string, string>
                            {
                                { "VIEW", CommonConstant.Visibility_Visible },
                                { "ADD", CommonConstant.Visibility_Collapsed },
                                { "EDIT", CommonConstant.Visibility_Collapsed },
                                { "DEL", CommonConstant.Visibility_Collapsed },
                            };
                            var checkPermission = CommonConstant.DsQuyenCuaTKDN;
                            foreach (var i in checkPermission)
                            {
                                if (permissions.ContainsKey(i.Chitiet_Quyen.mahanhDong))
                                {
                                    permissions[i.Chitiet_Quyen.mahanhDong] = CommonConstant.Visibility_Visible;
                                }
                            }

                            // Gán giá trị từ dictionary vào các biến
                            Permission_ADD = permissions["ADD"];
                            Permission_EDIT = permissions["EDIT"];
                            Permission_DEL = permissions["DEL"];
                            Permission_VIEW = permissions["VIEW"];
                        }));
                    });
                    GetData_Thread.IsBackground = true;
                    GetData_Thread.Start();
                }
                );

            CancelCommand = new RelayCommand<object>(
                (param) => true,
                (param) =>
                {
                    EmptyField();
                }
                );

            Create_BacLuongCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedBacLuong != null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(LuongCoBan) && HeSoLuong > 0)
                        {
                            var checkExist_BacLuong = new BacLuongDAO().Get_BacLuong_By_HeSoLuong(HeSoLuong);

                            if (checkExist_BacLuong != null)
                            {
                                MessageBox.Show("Bậc lương đã tồn tại.", "Cảnh báo!", MessageBoxButton.OK, MessageBoxImage.Warning);

                            }
                            else
                            {
                                var cm = new BacLuong()
                                {
                                    heSoLuong = HeSoLuong,
                                    luongCoBan = StringHelper.ConvertSalary(LuongCoBan),
                                };

                                var result = new BacLuongDAO().Create_BacLuong(cm);

                                if (result < 0)
                                {
                                    MessageBox.Show("Có lỗi xảy ra ở máy chủ", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else if (result == 0)
                                {
                                    MessageBox.Show("Dữ liệu đang bị rỗng.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                else
                                {
                                    MessageBox.Show("Thêm mới bậc lương thành công", "Cảnh báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                                    GetList_BacLuong();
                                    EmptyField();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hệ số lương và luong cơ bản không được bỏ trống.", "Cảnh báo!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                );


            Update_BacLuongCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedBacLuong == null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    DialogWindow d = new DialogWindow();
                    d.DialogMessage = "Bạn có chắc muốn cập nhật?";

                    if (true == d.ShowDialog())
                    {
                        try
                        {
                            var cm = new BacLuong()
                            {
                                heSoLuong = HeSoLuong,
                                luongCoBan = StringHelper.ConvertSalary(LuongCoBan),
                            };

                            var result = new BacLuongDAO().Update_BacLuong(cm);

                            if (!result)
                            {
                                MessageBox.Show("Có lỗi xảy ra ở máy chủ", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                            else
                            {
                                MessageBox.Show("Cập nhật bậc lương thành công", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                                GetList_BacLuong();
                                EmptyField();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                );

            Delete_BacLuongCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedBacLuong == null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    DialogWindow d = new DialogWindow();
                    d.DialogMessage = "Bạn có chắc muốn xóa?";

                    if (true == d.ShowDialog())
                    {
                        try
                        {
                            var listNV_Constrain = new BacLuongDAO().GetCount_ChamCong_By_MaBacLuong(HeSoLuong);
                            if (listNV_Constrain.Count > 0)
                            {
                                MessageBox.Show($"Có {listNV_Constrain.Count} bảng chấm công đang sử dụng bậc lương {HeSoLuong}, \n Yêu cầu không có bảng chấm công nào đang sử dụng bậc lương {HeSoLuong}.", "Cảnh báo!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                            else
                            {
                                var result = new BacLuongDAO().Delete_BacLuong(HeSoLuong);

                                if (!result)
                                {
                                    MessageBox.Show("Có lỗi xảy ra ở máy chủ", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                                else
                                {
                                    MessageBox.Show("Xóa bậc lương thành công", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                                    GetList_BacLuong();
                                    EmptyField();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                );
        }

        private void EmptyField()
        {
            SelectedBacLuong = null;
            HeSoLuong = 0;
            LuongCoBan = string.Empty;
        }

        private void GetList_BacLuong()
        {
            try
            {
                var result = new BacLuongDAO().GetList_Luong();

                TotalRecord = result.Count;
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                DsBacLuong = new ObservableCollection<BacLuong>(result.OrderBy(x => x.heSoLuong).Skip((Page - 1) * PageSize).Take(PageSize));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}