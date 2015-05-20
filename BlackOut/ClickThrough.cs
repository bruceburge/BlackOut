using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace BlackOut
{
    public class ClickThrough
    {
        public const int ExStyle = -20;
        public const int WS_EX_Transparent = 0x20;
        public const int WS_EX_Layered = 0x80000;
        public const int LWA_ColorKey = 0x01;
        public const int LWA_Alpha = 0x02;        

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);

    }
}
