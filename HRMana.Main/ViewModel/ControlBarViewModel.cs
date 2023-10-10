using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HRMana.Main.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand MoveWindowCommand { get; set; }


        public ControlBarViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            CloseCommand = new RelayCommand<UserControl>(
                (p) => { return true; },
                (p) =>
                {
                    FrameworkElement window = GetParentElement(p);
                    Window isWindow = window as Window;

                    if (isWindow != null)
                    {
                        Application.Current.Shutdown();
                    }
                }
                );

            MinimizeCommand = new RelayCommand<UserControl>(
                (p) => { return true; },
                (p) =>
                {
                    FrameworkElement window = GetParentElement(p);
                    Window isWindow = window as Window;

                    if (isWindow != null)
                    {
                        if (isWindow.WindowState == WindowState.Normal || isWindow.WindowState == WindowState.Maximized)
                        {
                            isWindow.WindowState = WindowState.Minimized;
                        }
                    }
                }
                );

            MaximizeCommand = new RelayCommand<UserControl>(
                (p) => { return true; },
                (p) =>
                {
                    FrameworkElement window = GetParentElement(p);
                    Window isWindow = window as Window;

                    if (isWindow != null)
                    {
                        if (isWindow.WindowState == WindowState.Normal)
                        {
                            isWindow.WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            isWindow.WindowState = WindowState.Normal;
                        }
                    }
                }
                );

            MoveWindowCommand = new RelayCommand<UserControl>(
                (p) => { return true; },
                (p) =>
                {
                    MoveWinDown(GetParentElement(p));
                });
        }

        private void MoveWinDown(FrameworkElement fele) 
        {
            FrameworkElement windown = fele;
            Window isWindow = windown as Window;

            if (isWindow != null)
            {
                isWindow.DragMove();
            }
        }

        private FrameworkElement GetParentElement(FrameworkElement fele)
        {
            FrameworkElement parent = fele;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
