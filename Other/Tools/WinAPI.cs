using System;
using System.Runtime.InteropServices;
namespace CheatUITemplt
{
    class WinAPI
    {
        #region MemoryAPI
        //从指定内存中读取字节集数据
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, uint nSize, IntPtr lpNumberOfBytesRead);

        //从指定内存中写入字节集数据
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        //写内存  
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory
        (
            IntPtr lpProcess,
            IntPtr lpBaseAddress,
            byte[] lpBuffer,
            int nSize,
            IntPtr BytesWrite
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx
        (
            //修改内存的句柄  
            IntPtr hProcess,
            //要修改的起始地址  
            IntPtr lpAddress,
            //页区域大小  
            int dwSize,
            //访问方式  
            int flNewProtect,
            //用于保护改变前的保护属性  
            ref IntPtr lpflOldProtect
        );

        #endregion

        #region ProcessAPI

        //打开一个已存在的进程对象，并返回进程的句柄
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        //关闭一个内核对象。其中包括文件、文件映射、进程、线程、安全和同步对象等。
        [DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hObject);

        //GetModuleHandle是获取应用程序或动态链接库的模块句柄。  
        [DllImport("kernel32")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        //函数通过获取进程信息为指定的进程、进程使用的堆[HEAP]、模块[MODULE]、线程建立一个快照.
        [DllImport("KERNEL32.DLL ")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);

        //unsafe  public static IntPtr GetProcessModuleHandle(uint pid, char* moduleName)
        //{
        //    ManageClass.ManageClass manage = new ManageClass.ManageClass();
        //    return (IntPtr)manage.GetProcessModuleHandle(pid, moduleName);
        //}

        [DllImport("KERNEL32.DLL ")]
        public static extern int GetProcessId(IntPtr Process);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        [DllImport("MyAPI.dll")]
        public static extern uint GetProcessModuleHandle(uint pid, [MarshalAs(UnmanagedType.LPWStr)] string moduleName);

        #endregion

        #region WindowsAPI

        #region Windows常量
        public const uint WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);
        public const int LWA_COLORKEY = 1;
        public const int LWA_ALPHA = 2;

        #endregion

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        public static extern uint SetWindowLong(
        IntPtr hwnd,
        int nIndex,
        uint dwNewLong
        );

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        public static extern uint GetWindowLong(
        IntPtr hwnd,
        int nIndex
        );

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern int SetLayeredWindowAttributes(
        IntPtr hwnd,
        int crKey,
        int bAlpha,
        int dwFlags
        );

        //取窗口句柄 FindWindow 根据窗体标题查找窗口句柄（支持模糊匹配）
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentProcess")]
        public static extern int GetCurrentProcess();

        [DllImport("User32.dll", EntryPoint = "UpdateWindow")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        #endregion

    }
}
