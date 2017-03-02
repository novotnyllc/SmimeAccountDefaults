using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmimeAccountDefaults
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
            
        }

        const int GWL_EXSTYLE = -20;
        const int WS_EX_DLGMODALFRAME = 0x0001;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_FRAMECHANGED = 0x0020;
        const int GWL_STYLE = -16;
        const int WS_MAXIMIZEBOX = 0x00010000;
        const int WS_MINIMIZEBOX = 0x00020000;
        const int WS_SYSMENU = 0x00080000;

        const int WM_SETICON = 0x0080;
        const int ICON_SMALL = 0;
        const int ICON_BIG = 1;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height, uint flags);


        protected override void OnSourceInitialized(EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int extendedStyle = GetWindowLongPtr(hwnd, GWL_EXSTYLE).ToInt32();
            SetWindowLongPtr(hwnd, GWL_EXSTYLE, new IntPtr(extendedStyle | WS_EX_DLGMODALFRAME));
           


            // reset the icon, both calls important
            SendMessage(hwnd, WM_SETICON, (IntPtr)ICON_SMALL, IntPtr.Zero);
            SendMessage(hwnd, WM_SETICON, (IntPtr)ICON_BIG, IntPtr.Zero);

            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);

            base.OnSourceInitialized(e);
        }

    
    }
}
