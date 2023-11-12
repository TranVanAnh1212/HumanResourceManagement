﻿using HRMana.Main.View.Personnel;
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
    internal class WorkingRotationViewModel : BaseViewModel
    {
        private string _soQuyetDinh;
        private DateTime _ngayQuyetDinh;
        private DateTime _thoiGianThiHanh;
        private int _maNhanVien;
        private string _hoTen;
        private int _maChucVuCu;
        private string _tenChucVuCu;
        private int _maPhongCu;
        private string _tenPhongCu;
        private int _maChucVuMoi;
        private string _tenChucVuMoi;
        private int _maPhongMoi;
        private string _tenPhongMoi;
        private ObservableCollection<NhanVien> _dsNhanVien;
        private ObservableCollection<WorkingRotationViewModel> _dsDieuDongCongTac;
        private WorkingRotationViewModel _selectedDieuDongCongTac;
        private NhanVien _selectedNhanVien;
        private ObservableCollection<ChucVu> _dsChucVu;
        private ObservableCollection<PhongBan> _dsPhongBan;
        private ChucVu _selectedChucVuCu;
        private PhongBan _selectedPhongBanCu;
        private ChucVu _selectedChucVuMoi;
        private PhongBan _selectedPhongBanMoi;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;

        public ICommand LoadWindowCommand { get; set; }
        public ICommand Create_DieuDongCongtacCommand { get; set; }
        public ICommand Update_DieuDongCongtacCommand { get; set; }
        public ICommand Delete_DieuDongCongtacCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public int MaNhanVien { get => _maNhanVien; set { _maNhanVien = value; OnPropertyChanged(); } }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public int MaChucVuCu { get => _maChucVuCu; set { _maChucVuCu = value; OnPropertyChanged(); } }
        public string TenChucVuCu { get => _tenChucVuCu; set { _tenChucVuCu = value; OnPropertyChanged(); } }
        public int MaPhongCu { get => _maPhongCu; set { _maPhongCu = value; OnPropertyChanged(); } }
        public string TenPhongCu { get => _tenPhongCu; set { _tenPhongCu = value; OnPropertyChanged(); } }

        public int MaChucVuMoi { get => _maChucVuMoi; set { _maChucVuMoi = value; OnPropertyChanged(); } }
        public string TenChucVuMoi { get => _tenChucVuMoi; set { _tenChucVuMoi = value; OnPropertyChanged(); } }
        public int MaPhongMoi { get => _maPhongMoi; set { _maPhongMoi = value; OnPropertyChanged(); } }
        public string TenPhongMoi { get => _tenPhongMoi; set { _tenPhongMoi = value; OnPropertyChanged(); } }

        public ObservableCollection<NhanVien> DsNhanVien { get => _dsNhanVien; set { _dsNhanVien = value; OnPropertyChanged(); } }
        public NhanVien SelectedNhanVien
        {
            get => _selectedNhanVien;
            set
            {
                _selectedNhanVien = value;
                OnPropertyChanged();

                if (SelectedNhanVien != null)
                {
                    MaNhanVien = SelectedNhanVien.maNhanVien;
                    HoTen = SelectedNhanVien.tenNhanVien;
                    SelectedChucVuCu = DsChucVu.SingleOrDefault(x => x.maChucVu == SelectedNhanVien.maChucVu);
                    SelectedPhongBanCu = DsPhongBan.SingleOrDefault(x => x.maPhong == SelectedNhanVien.maPhong);
                }
            }
        }
        public ObservableCollection<ChucVu> DsChucVu { get => _dsChucVu; set { _dsChucVu = value; OnPropertyChanged(); } }
        public ObservableCollection<PhongBan> DsPhongBan { get => _dsPhongBan; set { _dsPhongBan = value; OnPropertyChanged(); } }
        public ChucVu SelectedChucVuCu
        {
            get => _selectedChucVuCu;
            set
            {
                _selectedChucVuCu = value;
                OnPropertyChanged();

                if (SelectedChucVuCu != null)
                {
                    MaChucVuCu = SelectedChucVuCu.maChucVu;
                }
            }
        }
        public PhongBan SelectedPhongBanCu
        {
            get => _selectedPhongBanCu;
            set
            {
                _selectedPhongBanCu = value;
                OnPropertyChanged();

                if (SelectedPhongBanCu != null)
                {
                    MaPhongCu = SelectedPhongBanCu.maPhong;
                }
            }
        }
        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }
        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }
        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }

        public ChucVu SelectedChucVuMoi
        {
            get => _selectedChucVuMoi; set
            {
                _selectedChucVuMoi = value;
                OnPropertyChanged();

                if (SelectedChucVuMoi != null)
                {
                    MaChucVuMoi = SelectedChucVuMoi.maChucVu;
                }
            }
        }
        public PhongBan SelectedPhongBanMoi
        {
            get => _selectedPhongBanMoi;
            set
            {
                _selectedPhongBanMoi = value;
                OnPropertyChanged();

                if (SelectedPhongBanMoi != null)
                {
                    MaPhongMoi = SelectedPhongBanMoi.maPhong;
                }
            }
        }

        public ObservableCollection<WorkingRotationViewModel> DsDieuDongCongTac { get => _dsDieuDongCongTac; set { _dsDieuDongCongTac = value; OnPropertyChanged(); } }

        public string SoQuyetDinh { get => _soQuyetDinh; set { _soQuyetDinh = value; OnPropertyChanged(); } }
        public DateTime ThoiGianThiHanh { get => _thoiGianThiHanh; set { _thoiGianThiHanh = value; OnPropertyChanged(); } }

        public DateTime NgayQuyetDinh { get => _ngayQuyetDinh; set { _ngayQuyetDinh = value; OnPropertyChanged(); } }

        public WorkingRotationViewModel SelectedDieuDongCongTac
        {
            get => _selectedDieuDongCongTac;
            set
            {
                _selectedDieuDongCongTac = value;
                OnPropertyChanged();

                if (SelectedDieuDongCongTac != null)
                {
                    SoQuyetDinh = SelectedDieuDongCongTac.SoQuyetDinh;
                    NgayQuyetDinh = SelectedDieuDongCongTac.NgayQuyetDinh;
                    ThoiGianThiHanh = SelectedDieuDongCongTac.ThoiGianThiHanh;
                    MaNhanVien = SelectedDieuDongCongTac.MaNhanVien;
                    HoTen = SelectedDieuDongCongTac.HoTen;
                    SelectedChucVuCu = DsChucVu.SingleOrDefault(x => x.maChucVu == SelectedDieuDongCongTac.MaChucVuCu);
                    SelectedChucVuMoi = DsChucVu.SingleOrDefault(x => x.maChucVu == SelectedDieuDongCongTac.MaChucVuMoi);
                    SelectedPhongBanCu = DsPhongBan.SingleOrDefault(x => x.maPhong == SelectedDieuDongCongTac.MaPhongCu);
                    SelectedPhongBanMoi = DsPhongBan.SingleOrDefault(x => x.maPhong == SelectedDieuDongCongTac.MaPhongMoi);
                }
            }
        }

        public WorkingRotationViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            Page = 1;
            TotalPage = 1;
            PageSize = 20;
            NgayQuyetDinh = DateTime.Now;
            ThoiGianThiHanh = DateTime.Now;

            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    Thread GetData_Thread = new Thread(() =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            GetList_NhanVien();
                            GetList_ChucVu();
                            GetList_PhongBan();
                            GetList_ChuyenCongTac_NhanVien();
                        }));
                    });
                    GetData_Thread.IsBackground = true;
                    GetData_Thread.Start();
                }
                );

            Create_DieuDongCongtacCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedChucVuMoi == null || SelectedPhongBanMoi == null || SelectedNhanVien == null)
                        return false;

                    if (string.IsNullOrEmpty(SoQuyetDinh))
                        return false;

                    return true;
                },
                (param) =>
                {
                    Create_DieuDongCongTac();
                }
                );

            Update_DieuDongCongtacCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedDieuDongCongTac == null)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    Update_DieuDongCongtac();
                }
                );

            Delete_DieuDongCongtacCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedDieuDongCongTac == null)
                    {
                        return false;
                    }

                    return true;
                },
                (param) =>
                {
                    Delete_DieuDongCongtac();
                }
                );

            CancelCommand = new RelayCommand<object>(
                (param) => { return true; },
                (param) =>
                {
                    EmptyField();
                }
                );
        }

        private void Delete_DieuDongCongtac()
        {
            try
            {
                var result_cct_nv = new ChuyenCongTac_NhanVienDAO().Delete_ChuyenCongTacNhanVien(SelectedDieuDongCongTac.SoQuyetDinh, SelectedDieuDongCongTac.MaNhanVien);
                
                if (result_cct_nv)
                {
                    var result_cct = new ChuyenCongTacDAO().Delete_ChuyenCongTac(SelectedDieuDongCongTac.SoQuyetDinh);

                    if (result_cct)
                    {

                        var nv = new NhanVienDAO().Get_NhanVien_By_MaNhanVien(MaNhanVien);
                        nv.maChucVu = SelectedDieuDongCongTac.MaChucVuCu;
                        nv.maPhong = SelectedDieuDongCongTac.MaPhongCu;

                        var result_nv = new NhanVienDAO().Update_NhanVien(nv);

                        if (result_nv)
                        {
                            MessageBox.Show($"Xóa chuyển công tác nhân viên \"{HoTen}\" thành công");
                            GetList_ChuyenCongTac_NhanVien();
                            GetList_NhanVien();
                            EmptyField();
                        }
                        else
                        {
                            MessageBox.Show($"Xóa chuyển công tác nhân viên \"{HoTen}\" không thành công");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Xóa chuyển công tác nhân viên \"{HoTen}\" không thành công");

                    }
                }
                else
                {
                    MessageBox.Show($"Xóa chuyển công tác nhân viên \"{HoTen}\" không thành công");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update_DieuDongCongtac()
        {
            try
            {
                var cct = new ChuyenCongTac()
                {
                    soQuyetDinh = SoQuyetDinh,
                    ngayQuyetDinh = NgayQuyetDinh,
                    thoiGianThiHanh = ThoiGianThiHanh,
                };

                var cct_nv = new ChuyenCongTac_NhanVien()
                {
                    soQuyetDinh = SoQuyetDinh,
                    maNhanVien = MaNhanVien,
                    chucVuCu = MaChucVuCu,
                    chucVuMoi = MaChucVuMoi,
                    phongBanCu = MaPhongCu,
                    phongBanMoi = MaPhongMoi,
                };

                var result_cct = new ChuyenCongTacDAO().Update_ChuyenCongtac(cct);
                var result_cct_nv = new ChuyenCongTac_NhanVienDAO().Update_ChuyenCongTacNhanVien(cct_nv);

                if (result_cct && result_cct_nv)
                {

                    var nv = new NhanVienDAO().Get_NhanVien_By_MaNhanVien(MaNhanVien);
                    nv.maChucVu = MaChucVuMoi;
                    nv.maPhong = MaPhongMoi;

                    var result_nv = new NhanVienDAO().Update_NhanVien(nv);

                    if (result_nv)
                    {
                        MessageBox.Show($"Sửa đổi chuyển công tác nhân viên \"{HoTen}\" thành công");
                        GetList_ChuyenCongTac_NhanVien();
                        GetList_NhanVien();
                        EmptyField();
                    }
                    else
                    {
                        MessageBox.Show($"Sửa đổi chuyển công tác nhân viên \"{HoTen}\" không thành công");
                    }
                }
                else
                {
                    MessageBox.Show($"Sửa đổi chuyển công tác nhân viên \"{HoTen}\" không thành công");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Create_DieuDongCongTac()
        {
            try
            {
                var cct = new ChuyenCongTac()
                {
                    soQuyetDinh = SoQuyetDinh,
                    ngayQuyetDinh = NgayQuyetDinh,
                    thoiGianThiHanh = ThoiGianThiHanh,
                };

                var cct_nv = new ChuyenCongTac_NhanVien()
                {
                    soQuyetDinh = SoQuyetDinh,
                    maNhanVien = MaNhanVien,
                    chucVuCu = MaChucVuCu,
                    chucVuMoi = MaChucVuMoi,
                    phongBanCu = MaPhongCu,
                    phongBanMoi = MaPhongMoi,
                };

                var result_cct = new ChuyenCongTacDAO().CreateNew_ChuyenCongTac(cct);
                var result_cct_nv = new ChuyenCongTac_NhanVienDAO().CreateNew_ChuyenCongTacNhanVien(cct_nv);

                if (result_cct > 0 && result_cct_nv > 0)
                {

                    var nv = new NhanVienDAO().Get_NhanVien_By_MaNhanVien(MaNhanVien);
                    nv.maChucVu = MaChucVuMoi;
                    nv.maPhong = MaPhongMoi;

                    var result_nv = new NhanVienDAO().Update_NhanVien(nv);

                    if (result_nv)
                    {
                        MessageBox.Show($"Chuyển công tác nhân viên \"{HoTen}\" thành công");
                        GetList_ChuyenCongTac_NhanVien();
                        GetList_NhanVien();
                        EmptyField();
                    }
                    else
                    {
                        MessageBox.Show($"Chuyển công tác nhân viên \"{HoTen}\" không thành công");
                    }
                }
                else
                {
                    MessageBox.Show($"Chuyển công tác nhân viên \"{HoTen}\" không thành công");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EmptyField()
        {
            SelectedNhanVien = null;
            SelectedChucVuCu = null;
            SelectedPhongBanCu = null;
            SelectedPhongBanMoi = null;
            SelectedChucVuMoi = null;
            SelectedDieuDongCongTac = null;

            MaNhanVien = 0;
            SoQuyetDinh = string.Empty;
            HoTen = string.Empty;
            NgayQuyetDinh = DateTime.Now;
            ThoiGianThiHanh = DateTime.Now;
            MaChucVuCu = 0;
            MaPhongCu = 0;
            TenChucVuCu = string.Empty;
            TenPhongCu = string.Empty;
            MaChucVuMoi = 0;
            MaPhongMoi = 0;
            TenChucVuMoi = string.Empty;
            TenPhongMoi = string.Empty;

        }

        private void GetList_NhanVien()
        {
            try
            {
                var result = new NhanVienDAO().GetList_NhanVien();

                DsNhanVien = new ObservableCollection<NhanVien>(result.OrderBy(x => x.tenNhanVien));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra, {ex.Message}");
            }
        }

        private void GetList_ChuyenCongTac_NhanVien()
        {
            try
            {
                var db = DataProvider.Instance.DBContext;
                IQueryable<WorkingRotationViewModel> result = from cct in db.ChuyenCongTac
                                                              join cct_nv in db.ChuyenCongTac_NhanVien on cct.soQuyetDinh equals cct_nv.soQuyetDinh
                                                              join nv in db.NhanVien on cct_nv.maNhanVien equals nv.maNhanVien
                                                              join pb in db.PhongBan on cct_nv.phongBanCu equals pb.maPhong
                                                              join cv in db.ChucVu on cct_nv.chucVuCu equals cv.maChucVu
                                                              select new WorkingRotationViewModel()
                                                              {
                                                                  SoQuyetDinh = cct.soQuyetDinh,
                                                                  NgayQuyetDinh = (DateTime)cct.ngayQuyetDinh,
                                                                  ThoiGianThiHanh = (DateTime)cct.thoiGianThiHanh,
                                                                  MaNhanVien = cct_nv.maNhanVien,
                                                                  HoTen = nv.tenNhanVien,
                                                                  MaChucVuCu = (int)cct_nv.chucVuCu,
                                                                  TenChucVuCu = cv.tenChucVu,
                                                                  MaPhongCu = (int)cct_nv.phongBanCu,
                                                                  TenPhongCu = pb.tenPhong,
                                                                  MaChucVuMoi = (int)cct_nv.chucVuMoi,
                                                                  TenChucVuMoi = cct_nv.ChucVu.tenChucVu,
                                                                  MaPhongMoi = (int)cct_nv.phongBanMoi,
                                                                  TenPhongMoi = cct_nv.PhongBan.tenPhong,
                                                              };


                TotalRecord = result.Count();
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                DsDieuDongCongTac = new ObservableCollection<WorkingRotationViewModel>(result.OrderBy(x => x.NgayQuyetDinh).Skip((Page - 1) * PageSize).Take(PageSize).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra, {ex.Message}");
            }
        }

        private void GetList_PhongBan()
        {
            try
            {
                var result = new PhongBanDAO().GetList_PhongBan();

                DsPhongBan = new ObservableCollection<PhongBan>(result);
            }
            catch (Exception ex) { }
        }

        private void GetList_ChucVu()
        {
            try
            {
                var result = new ChucVuDAO().GetListChucVu();

                DsChucVu = new ObservableCollection<ChucVu>(result);
            }
            catch (Exception ex) { }
        }
    }
}