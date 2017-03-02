using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Launchy
{
    public class RegisterKeyBinder
    {
        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);

        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS

        private HwndSource _source;
        private readonly MainWindow window;

        public RegisterKeyBinder(MainWindow context)
        {
            this.window = context;
            var helper = new WindowInteropHelper(this.window);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
            RegisterHotKey();
        }
        
        private void RegisterHotKey()
        {            
            var helper = new WindowInteropHelper(this.window);
            const uint VK_SPACE = 0x20;
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_ALT, VK_SPACE))
            {
                // handle error
            }
        }

        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(this.window);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            OnHotKeyPressed();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            this.window.OnHotKeyKeyPressed();
                    
        }

        ~RegisterKeyBinder()
        {
            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterHotKey();
        }
    }
}
