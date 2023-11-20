﻿using HRMana.Common.Commons;
using HRMana.Main.View.Dialog;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class ContractViewModel : BaseViewModel
    {
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;

        private int _maHopDong;
        private string _soHopDong;
        private DateTime? _ngayKyHopDong;
        private DateTime? _ngayKetThucHopDong;
        private string _loaiHopDong;
        private string _tinhTrangChuKy;
        private string _thoiHanHopDong;
        private ObservableCollection<HopDong> _dsHopDong;
        private HopDong _selectedHopDong;
        private string _permission_ADD;
        private string _permission_VIEW;
        private string _permission_EDIT;
        private string _permission_DEL;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToStartCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand Create_HopDongCommand { get; set; }
        public ICommand Update_HopDongCommand { get; set; }
        public ICommand Delete_HopDongCommand { get; set; }

        public string Permission_ADD { get => _permission_ADD; set { _permission_ADD = value; OnPropertyChanged(); } }
        public string Permission_VIEW { get => _permission_VIEW; set { _permission_VIEW = value; OnPropertyChanged(); } }
        public string Permission_EDIT { get => _permission_EDIT; set { _permission_EDIT = value; OnPropertyChanged(); } }
        public string Permission_DEL { get => _permission_DEL; set { _permission_DEL = value; OnPropertyChanged(); } }


        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }
        public int MaHopDong { get => _maHopDong; set { _maHopDong = value; OnPropertyChanged(); } }
        public string SoHopDong { get => _soHopDong; set { _soHopDong = value; OnPropertyChanged(); } }
        public DateTime? NgayKyHopDong { get => _ngayKyHopDong; set { _ngayKyHopDong = value; OnPropertyChanged(); } }
        public DateTime? NgayKetThucHopDong { get => _ngayKetThucHopDong; set { _ngayKetThucHopDong = value; OnPropertyChanged(); } }
        public string LoaiHopDong { get => _loaiHopDong; set { _loaiHopDong = value; OnPropertyChanged(); } }
        public string TinhTrangChuKy { get => _tinhTrangChuKy; set { _tinhTrangChuKy = value; OnPropertyChanged(); } }
        public string ThoiHanHopDong { get => _thoiHanHopDong; set { _thoiHanHopDong = value; OnPropertyChanged(); } }

        public ObservableCollection<HopDong> DsHopDong { get => _dsHopDong; set { _dsHopDong = value; OnPropertyChanged(); } }
        public HopDong SelectedHopDong
        {
            get => _selectedHopDong;
            set
            {
                _selectedHopDong = value;
                OnPropertyChanged();

                if (SelectedHopDong != null)
                {
                    MaHopDong = SelectedHopDong.maHopDong;
                    SoHopDong = SelectedHopDong.soHopDong;
                    NgayKyHopDong = SelectedHopDong.ngayKyHD;
                    NgayKetThucHopDong = SelectedHopDong.ngayKetThucHD;
                    LoaiHopDong = SelectedHopDong.loaiHopDong;
                    TinhTrangChuKy = SelectedHopDong.tinhTrangChuKi;
                    ThoiHanHopDong = SelectedHopDong.thoiHanHD;
                }
            }
        }

        public ContractViewModel()
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
                        GetList_HopDong();
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
                        GetList_HopDong();
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
                    GetList_HopDong();
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
                    GetList_HopDong();
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
                            GetList_HopDong();

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
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    EmptyField();
                }
                );

            Create_HopDongCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (string.IsNullOrEmpty(SoHopDong) || NgayKyHopDong == null)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    try
                    {
                        HopDong hd = new HopDong()
                        {
                            maHopDong = MaHopDong,
                            soHopDong = SoHopDong,
                            ngayKyHD = NgayKyHopDong,
                            ngayKetThucHD = NgayKetThucHopDong,
                            thoiHanHD = ThoiHanHopDong,
                            tinhTrangChuKi = TinhTrangChuKy,
                            loaiHopDong = LoaiHopDong,
                        };

                        var result = new HopDongDAO().CreateNew_HopDong(hd);

                        if (result > 0)
                        {
                            MessageBox.Show("Thêm mới hợp đồng thành công.", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                            EmptyField();
                            GetList_HopDong();

                        }
                        else if (result == 0)
                        {
                            MessageBox.Show("Dữ liệu truyền vào trống", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                        else
                        {
                            MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
                );

            Update_HopDongCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedHopDong == null)
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
                            HopDong hd = new HopDong()
                            {
                                maHopDong = MaHopDong,
                                soHopDong = SoHopDong,
                                ngayKyHD = NgayKyHopDong,
                                ngayKetThucHD = NgayKetThucHopDong,
                                thoiHanHD = ThoiHanHopDong,
                                tinhTrangChuKi = TinhTrangChuKy,
                                loaiHopDong = LoaiHopDong,
                            };

                            var result = new HopDongDAO().Update_HopDong(hd);

                            if (result)
                            {
                                MessageBox.Show("Cập nhật hợp đồng thành công.", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                                EmptyField();
                                GetList_HopDong();
                            }
                            else
                            {
                                MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }
                }
                );

            Delete_HopDongCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedHopDong == null)
                        return false;

                    return true;
                },
                (param) =>
                {
                    DialogWindow dm = new DialogWindow();
                    dm.DialogMessage = "Bạn có chắc muốn xóa?";

                    if (true == dm.ShowDialog())
                    {
                        try
                        {

                            var nv_hd = new NhanVienDAO().Get_NhanVien_By_MaHopDong(SelectedHopDong.maHopDong);

                            if (nv_hd != null)
                            {
                                DialogWindow d = new DialogWindow();
                                d.DialogMessage = $"Phát hiện thấy mã hợp đồng là của nhân viên {nv_hd.tenNhanVien} - {nv_hd.maNhanVien}, vẫn muốn xóa?";

                                if (true == d.ShowDialog())
                                {
                                    var result = new HopDongDAO().Delete_HopDong(SelectedHopDong.maHopDong);

                                    if (result)
                                    {
                                        var nv_by_hd = new NhanVienDAO().Get_NhanVien_By_MaHopDong(SelectedHopDong.maHopDong);
                                        nv_by_hd.maHopDong = 0;
                                        if (nv_by_hd != null)
                                        {
                                            var result_nv_by_hd = new NhanVienDAO().Update_NhanVien(nv_by_hd);
                                        }

                                        MessageBox.Show("Xóa hợp đồng thành công.", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                                        EmptyField();
                                        GetList_HopDong();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                                    }
                                }
                            }
                            else
                            {
                                var result = new HopDongDAO().Delete_HopDong(SelectedHopDong.maHopDong);

                                if (result)
                                {
                                    MessageBox.Show("Xóa hợp đồng thành công.", "Thông báo!", MessageBoxButton.OK, MessageBoxImage.Information);
                                    EmptyField();
                                    GetList_HopDong();
                                }
                                else
                                {
                                    MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }
                }
                );
        }

        private void EmptyField()
        {
            SelectedHopDong = null;
            MaHopDong = 0;
            SoHopDong = string.Empty;
            NgayKyHopDong = null;
            NgayKetThucHopDong = null;
            LoaiHopDong = string.Empty;
            ThoiHanHopDong = string.Empty;
            TinhTrangChuKy = string.Empty;
        }

        private void GetList_HopDong()
        {
            try
            {
                var result = new HopDongDAO().GetList_HopDong();

                TotalRecord = result.Count;
                TotalPage = (int)Math.Ceiling((double) TotalRecord/PageSize);

                DsHopDong = new ObservableCollection<HopDong>(result.OrderBy(x => x.soHopDong).Skip((Page - 1) * PageSize).Take(PageSize));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra.", "Thông báo lỗi!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
