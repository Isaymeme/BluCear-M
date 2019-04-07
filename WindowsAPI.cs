using System;
using System.Runtime.InteropServices;

namespace Private.BlurClear
{
    public class WindowsAPI
    {
        [DllImport("User32.dll", EntryPoint = "SetWindowCompositionAttribute")]
        public static extern bool SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositonAttributeData pAttrData);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
    }
}
