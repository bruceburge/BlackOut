using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace BlackOut
{
    public class taskBar
    {
        [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
        static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        #region Struct RECT
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        #endregion

        #region Struct APPBARDATA
        [StructLayout(LayoutKind.Sequential)]
        struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }
        #endregion

        #region Struct ABMsg
        enum ABMsg : int
        {
            ABM_NEW = 0,
            ABM_REMOVE = 1,
            ABM_QUERYPOS = 2,
            ABM_SETPOS = 3,
            ABM_GETSTATE = 4,
            ABM_GETTASKBARPOS = 5,
            ABM_ACTIVATE = 6,
            ABM_GETAUTOHIDEBAR = 7,
            ABM_SETAUTOHIDEBAR = 8,
            ABM_WINDOWPOSCHANGED = 9,
            ABM_SETSTATE = 10
        }
        #endregion

        #region Struct ABEdge
        enum ABEdge : int
        {
            ABE_LEFT = 0,
            ABE_TOP,
            ABE_RIGHT,
            ABE_BOTTOM
        }
        #endregion

        #region Enum ABState
        enum ABState : int
        {
            ABS_MANUAL = 0,
            ABS_AUTOHIDE = 1,
            ABS_ALWAYSONTOP = 2,
            ABS_AUTOHIDEANDONTOP = 3,
        }
        #endregion

        #region Enum TaskBarEdge
        public enum TaskBarEdge : int
        {
            Bottom,
            Top,
            Left,
            Right
        }
        #endregion

        public Point touchSysTray(Form Form)
        {
            int x=0, y = 0;
         
            APPBARDATA abd = new APPBARDATA();

            //use if you want to return a different point if the taskbar is autohiding
            #region TaskBar AutoHide Property
            bool autoHide = false;
            abd = new APPBARDATA();
            uint uState = SHAppBarMessage((int)ABMsg.ABM_GETSTATE, ref abd);
            switch (uState)
            {
                case (int)ABState.ABS_ALWAYSONTOP:
                    autoHide = false;
                    break;
                case (int)ABState.ABS_AUTOHIDE:
                    autoHide = true;
                    break;
                case (int)ABState.ABS_AUTOHIDEANDONTOP:
                    autoHide = true;
                    break;
                case (int)ABState.ABS_MANUAL:
                    autoHide = false;
                    break;
            }
            #endregion

            uint ret = SHAppBarMessage((int)ABMsg.ABM_GETTASKBARPOS, ref abd);
            switch (abd.uEdge)
            {
                case (int)ABEdge.ABE_BOTTOM:                    
                    y = abd.rc.top - Form.Height;
                    x = abd.rc.right - Form.Width;
                    break;
                case (int)ABEdge.ABE_TOP:
                    y = abd.rc.bottom;
                    x = abd.rc.right - Form.Width;
                    break;
                case (int)ABEdge.ABE_LEFT:                   
                    y = abd.rc.bottom - Form.Height;
                    x = abd.rc.right;
                    break;
                case (int)ABEdge.ABE_RIGHT:
                    y = abd.rc.bottom - Form.Height;
                    x = abd.rc.left - Form.Width; 
                    break;
            }
            return new Point(x, y);

        }
    }
}

