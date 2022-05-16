using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Core.Tools
{
    //本注入方法，与本程序运行的平台有关
    //当本平台为x86时，注入的dll与目标程序都为x86时，成功
    //当本平台为x64时，注入的dll与目标程序都为x64时，成功
    class BaseInjector
    {

        // OpenProcess signture https://www.pinvoke.net/default.aspx/kernel32.openprocess
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        // VirtualAllocEx signture https://www.pinvoke.net/default.aspx/kernel32.virtualallocex
        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }


        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hwnd);

        public static void DLLInjector(int ProcId, string DllPath)
        {
            IntPtr Size = (IntPtr)DllPath.Length;

            IntPtr ProcHandle = OpenProcess(ProcessAccessFlags.All, false, ProcId);

            IntPtr DllSpace = VirtualAllocEx(ProcHandle, IntPtr.Zero, Size, AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ExecuteReadWrite);

            byte[] bytes = Encoding.ASCII.GetBytes(DllPath);
            bool DllWrite = WriteProcessMemory(ProcHandle, DllSpace, bytes, (int)bytes.Length, out var bytesread);

            IntPtr Kernel32Handle = GetModuleHandle("Kernel32.dll");
            IntPtr LoadLibraryAAddress = GetProcAddress(Kernel32Handle, "LoadLibraryA");

            IntPtr RemoteThreadHandle = CreateRemoteThread(ProcHandle, IntPtr.Zero, 0, LoadLibraryAAddress, DllSpace, 0, IntPtr.Zero);

            //bool FreeDllSpace = VirtualFreeEx(ProcHandle, DllSpace, 0, AllocationType.Release);

            CloseHandle(RemoteThreadHandle);

            CloseHandle(ProcHandle);
        }

        public static void DLLUnInjector(int ProcId, string DllPath)
        {

            var Dllname = DllPath.Split('\\').Last();


            IntPtr Size = (IntPtr)DllPath.Length;

            IntPtr ProcHandle = OpenProcess(ProcessAccessFlags.All, false, ProcId);


            IntPtr Kernel32Handle = GetModuleHandle("Kernel32.dll");
            IntPtr LoadLibraryAAddress = GetProcAddress(Kernel32Handle, "FreeLibraryAndExitThread");


            var hDll = GetDLLHandle(Dllname, ProcId);


            IntPtr RemoteThreadHandle = CreateRemoteThread(ProcHandle, IntPtr.Zero, 0, LoadLibraryAAddress, hDll, 0, IntPtr.Zero);

            //bool FreeDllSpace = VirtualFreeEx(ProcHandle, DllSpace, 0, AllocationType.Release);

            CloseHandle(RemoteThreadHandle);

            CloseHandle(ProcHandle);
        }

        private static IntPtr GetDLLHandle(string szDName, int dwPID)
        {
            IntPtr address;
            address = CheatTools.MyGetProcessModuleHandle((uint)dwPID, szDName);

            return address;
        }

    }
}
