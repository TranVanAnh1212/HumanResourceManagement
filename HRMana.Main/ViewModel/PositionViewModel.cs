using HRMana.Common;
using HRMana.Common.Events;
using HRMana.Main.View.Dialog;
using HRMana.Main.View.Position;
using HRMana.Model.DAO;
using HRMana.Model.EF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class PositionViewModel : BaseViewModel
    {
        private int _maChucVu;
        private string _tenChucVu;
        private ObservableCollection<PositionViewModel> _positions;
        private PositionViewModel _SelectedPosition;
        private int _totalPage;
        private int _page;
        private int _pageSize;
        private int _totalRecord;
        private string _message;
        private string _fill;

        public ICommand IncreasePageCommand { get; set; }
        public ICommand ReducePageCommand { get; set; }
        public ICommand BackToTopCommand { get; set; }
        public ICommand GoToEndCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand CreateNew_ChucVuCommand { get; set; }
        public ICommand Update_ChucVuCommand { get; set; }
        public ICommand Delete_ChucVuCommand { get; set; }
        public ICommand CancelCommandCommand { get; set; }

        public int MaChucVu { get => _maChucVu; set { _maChucVu = value; OnPropertyChanged(); } }
        public string TenChucVu { get => _tenChucVu; set { _tenChucVu = value; OnPropertyChanged(); } }
        public ObservableCollection<PositionViewModel> Positions { get => _positions; set { _positions = value; OnPropertyChanged(); } }
        public PositionViewModel SelectedPosition
        {
            get => _SelectedPosition;
            set
            {
                _SelectedPosition = value;
                OnPropertyChanged();

                if (_SelectedPosition != null)
                {
                    MaChucVu = SelectedPosition.MaChucVu;
                    TenChucVu = SelectedPosition.TenChucVu;
                }
            }
        }

        public int TotalPage { get => _totalPage; set { _totalPage = value; OnPropertyChanged(); } }
        public int Page { get => _page; set { _page = value; OnPropertyChanged(); } }

        public int TotalRecord { get => _totalRecord; set { _totalRecord = value; OnPropertyChanged(); } }

        public int PageSize { get => _pageSize; set { _pageSize = value; OnPropertyChanged(); } }

        public string Message { get => _message; set { _message = value; OnPropertyChanged(); } }
        public string Fill { get => _fill; set { _fill = value; OnPropertyChanged(); } }

        public PositionViewModel()
        {
            Initialized();
        }

        private void Initialized()
        {
            Page = 1;
            PageSize = 5;
            TotalPage = 1;

            LoadWindowCommand = new RelayCommand<Page>(
                (param) => { return true; },
                (param) =>
                {
                    GetList_ChucVu();
                }
                );

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
                        GetList_ChucVu();
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
                        GetList_ChucVu();
                    }
                }
                );

            BackToTopCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    Page = 1;
                    GetList_ChucVu();
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
                    GetList_ChucVu();
                }
                );

            CreateNew_ChucVuCommand = new RelayCommand<object>(
                (param) =>
                {
                    return true;
                },
                (param) =>
                {
                    if (TenChucVu == string.Empty)
                    {
                        var result = new ChucVuDAO().CreateNew_ChucVu(TenChucVu);

                        if (result != null)
                        {
                            ShowNotification("Thêm mới chức vụ thành công.", "#FF58FF7B");
                            EmptyTextbox();
                            GetList_ChucVu();
                        }
                        else
                        {
                            ShowNotification("Thêm mới chức vụ thành công.", "#FFFF5858");
                        }
                    }
                }
                );

            CancelCommandCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedPosition == null || TenChucVu == string.Empty)
                        return false;

                    return true;
                },
                (param) =>
                {
                    EmptyTextbox();
                }
                );

            Update_ChucVuCommand = new RelayCommand<object>(
                (param) =>
                {
                    if (SelectedPosition == null) return false;

                    return true;
                },
                (param) =>
                {
                    DialogWindow d = new DialogWindow();
                    d.DialogMessage = "Bạn có chắc muốn sửa chức vụ.";
                    d.Owner = Window.GetWindow(new PositionPage());

                    if (true == d.ShowDialog())
                    {
                        var result = new ChucVuDAO().Update_ChucVu(MaChucVu, TenChucVu);

                        if (result)
                        {
                            ShowNotification("Cập nhật chức vụ thành công. ", "#FF58FF7B");
                            EmptyTextbox();
                            GetList_ChucVu();
                        }
                        else
                        {
                            ShowNotification("Cập nhật chức vụ không thành công. ", "#FFFF5858");
                        }
                    }
                }
                );

        }

        private void GetList_ChucVu()
        {
            try
            {
                //var result = (IEnumerable<ChucVu>)new ChucVuDAO().GetListChucVu();
                var db = DataProvider.Instance.DBContext;
                var result = from cv in db.ChucVu
                             select new PositionViewModel()
                             {
                                 MaChucVu = cv.maChucVu,
                                 TenChucVu = cv.tenChucVu,
                             };

                TotalRecord = result.Count();
                TotalPage = (int)Math.Ceiling((double)TotalRecord / PageSize);

                Positions = new ObservableCollection<PositionViewModel>(result.OrderBy(x => x.TenChucVu).Skip((Page - 1) * PageSize).Take(PageSize).ToList());
            }
            catch (Exception ex) { }
        }

        private void EmptyTextbox()
        {
            SelectedPosition = null;
            MaChucVu = 0;
            TenChucVu = string.Empty;
        }

        public void ShowNotification(string message, string fill)
        {
            Message = message;
            Fill = fill;
            NotificationEvent.Instance.ReqquestShowNotification();
        }


    }
}
