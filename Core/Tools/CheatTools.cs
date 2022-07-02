﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using WPFCheatUITemplate.Core.Tools;
using static WPFCheatUITemplate.Core.Tools.WinAPI;

namespace WPFCheatUITemplate
{
    static class CheatTools
    {

        #region 进程相关方法

        //根据进程名获取PID
        public static int GetPidByProcessName(string processName)
        {
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            foreach (Process p in arrayProcess)
            {
                return p.Id;
            }
            return 0;
        }

        public static int GetPidByHandle(IntPtr handle)
        {
            return WinAPI.GetProcessId(handle);
        }

        public static int GetPidByWindowsName(string lpClassName, string lpWindowName)
        {
            int pid;
            var handle =  WinAPI.FindWindow(lpClassName, lpWindowName);
            WinAPI.GetWindowThreadProcessId(handle, out pid);

            return pid;
        }

        public static void SuspendProcess(int processId)
        {
            ProcessMgr.SuspendProcess(processId);
        }

        public static void ResumeProcess(int processId)
        {
            ProcessMgr.ResumeProcess(processId);
        }

        //打开进程handle  0x1F0FFF 最高权限
        public static IntPtr GetProcessHandle(int Pid)
        {
            return WinAPI.OpenProcess(0x1F0FFF, false, Pid);
        }

        #endregion

        #region 内存相关方法

        #region 32-64位通用泛型读写

        //[DllImport("kernel32.dll")]
        //public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out IntPtr lpNumberOfBytesRead);
        //[DllImport("kernel32.dll")]
        //public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);

        public static T ReadMemory<T>(IntPtr _processHandle, IntPtr address) where T : struct
        {
            var ByteSize = Marshal.SizeOf(typeof(T));

            var buffer = new byte[ByteSize];

            ReadProcessMemory(_processHandle, address, buffer, buffer.Length, out var readBytes);

            return ByteArrayToStructure<T>(buffer);
        }

       
        public static byte[] ReadMemory(IntPtr _processHandle, IntPtr address, int size)
        {
            var buffer = new byte[size];

            ReadProcessMemory(_processHandle, address, buffer, size, out var readBytes);

            return buffer;
        }


        private static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0) throw new ArgumentException();

            var floats = new float[bytes.Length / 4];

            for (var i = 0; i < floats.Length; i++) floats[i] = BitConverter.ToSingle(bytes, i * 4);

            return floats;
        }

        public static float[] ReadMatrix<T>(IntPtr _processHandle, IntPtr address, int matrixSize) where T : struct
        {
            var ByteSize = Marshal.SizeOf(typeof(T));

            var buffer = new byte[ByteSize * matrixSize];

            ReadProcessMemory(_processHandle, address, buffer, buffer.Length, out var readBytes);

            return ConvertToFloatArray(buffer);
        }

        public static string ReadString(IntPtr _processHandle, IntPtr address, Encoding encoding, int maximumLength = 512)
        {
            var buffer = ReadMemory(_processHandle, address, maximumLength);
            var ret = encoding.GetString(buffer);

            if (ret.IndexOf('\0') != -1)
            {
                ret = ret.Remove(ret.IndexOf('\0'));
            }

            return ret;
        }

        
        public static bool WriteMemory<T>(IntPtr _processHandle, IntPtr address, object value) where T : struct
        {
            var buffer = StructureToByteArray(value);

            return WriteProcessMemory(_processHandle, address, buffer, buffer.Length, out var writtenBytes) && writtenBytes != 0;
        }

        public static void WriteMemory<T>(IntPtr m_pProcessHandle, IntPtr address, byte[] Value) where T : struct
        {
            //byte[] buffer = StructureToByteArray(Value);
            WriteProcessMemory(m_pProcessHandle, address, Value, Value.Length, out _);
        }

        public static T ReadMemory<T>(IntPtr _processHandle, IntPtr address, int[] offsets) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            ReadProcessMemory(_processHandle, GetPtrAddress(_processHandle, address, offsets), buffer, buffer.Length, out _);
            return ByteArrayToStructure<T>(buffer);
        }
        public static void WriteMemory<T>(IntPtr _processHandle, IntPtr address, int[] offsets, T value) where T : struct
        {
            byte[] buffer = StructureToByteArray(value);
            WriteProcessMemory(_processHandle, GetPtrAddress(_processHandle, address, offsets), buffer, buffer.Length, out _);
        }

        private static IntPtr GetPtrAddress(IntPtr processHandle, IntPtr pointer, int[] offset)
        {
            if (offset != null)
            {
                byte[] buffer = new byte[8];
                ReadProcessMemory(processHandle, pointer, buffer, buffer.Length, out _);

                for (int i = 0; i < (offset.Length - 1); i++)
                {
                    pointer = (IntPtr)BitConverter.ToInt64(buffer, 0) + offset[i];
                    ReadProcessMemory(processHandle, pointer, buffer, buffer.Length, out _);
                }

                pointer = (IntPtr)BitConverter.ToInt64(buffer, 0) + offset[offset.Length - 1];
            }

            return pointer;
        }


        #endregion

        #region 指针泛型读写

        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        private static byte[] StructureToByteArray(object obj)
        {
            int length = Marshal.SizeOf(obj);
            byte[] array = new byte[length];
            IntPtr pointer = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(obj, pointer, true);
            Marshal.Copy(pointer, array, 0, length);
            Marshal.FreeHGlobal(pointer);
            return array;
        }

        public static T ReadMemory<T>(IntPtr m_pProcessHandle, IntPtr[] address) where T : struct
        {

            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];

            for (int i = 0; i < address.Length; i++)
            {
                if (i != address.Length - 1)
                {
                    ReadProcessMemory(m_pProcessHandle, (IntPtr)(ByteArrayToStructure<IntPtr>(buffer).ToInt64() + address[i].ToInt64()), buffer, buffer.Length, out _);

                    int ret = ByteArrayToStructure<int>(buffer);


                    if (ret == 0)
                        return ByteArrayToStructure<T>(new byte[] { 0 });
                }
                else
                {
                    ReadProcessMemory(m_pProcessHandle, (IntPtr)(ByteArrayToStructure<IntPtr>(buffer).ToInt64() + address[i].ToInt64()), buffer, buffer.Length, out _);

                    return ByteArrayToStructure<T>(buffer);
                }

            }
            return ByteArrayToStructure<T>(buffer);
        }

        public static byte[] ReadMemory(IntPtr m_pProcessHandle, IntPtr[] address, int size)
        {
            var buffer = new byte[size];

            for (int i = 0; i < address.Length; i++)
            {
                if (i != address.Length - 1)
                {
                    ReadProcessMemory(m_pProcessHandle, (IntPtr)(ByteArrayToStructure<IntPtr>(buffer).ToInt64() + address[i].ToInt64()), buffer, buffer.Length, out _);

                    int ret = ByteArrayToStructure<int>(buffer);


                    if (ret == 0)
                        return new byte[] { 0 };
                }
                else
                {
                    ReadProcessMemory(m_pProcessHandle, (IntPtr)(ByteArrayToStructure<IntPtr>(buffer).ToInt64() + address[i].ToInt64()), buffer, buffer.Length, out _);

                    return buffer;
                }

            }
           
            return buffer;
        }

        public static void WriteMemory<T>(IntPtr m_pProcessHandle, IntPtr[] address, T vaule) where T : struct
        {

            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];

            for (int i = 0; i < address.Length; i++)
            {
                if (i != address.Length - 1)
                {
                    ReadProcessMemory(m_pProcessHandle, (IntPtr)(ByteArrayToStructure<IntPtr>(buffer).ToInt64() + address[i].ToInt64()), buffer, buffer.Length, out _);

                    int ret = ByteArrayToStructure<int>(buffer);


                    if (ret == 0)
                        break;
                }
                else
                {
                    var dd = StructureToByteArray(vaule);

                    WriteProcessMemory(m_pProcessHandle, (IntPtr)(ByteArrayToStructure<IntPtr>(buffer).ToInt64() + address[i].ToInt64()), dd, dd.Length, out _);

                }

            }

        }

        #endregion

        #region 字符串读写

        public static string ReadStringToASCII(IntPtr m_pProcessHandle, IntPtr address, int size)
        {
            byte[] buffer = new byte[size];
            ReadProcessMemory(m_pProcessHandle, address, buffer, size, out _);

            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                {
                    byte[] _buffer = new byte[i];
                    Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                    return Encoding.ASCII.GetString(_buffer);
                }
            }

            return Encoding.ASCII.GetString(buffer);
        }

        public static string ReadStringToUnicode(IntPtr m_pProcessHandle, IntPtr address, int size)
        {
            byte[] buffer = new byte[size];
            ReadProcessMemory(m_pProcessHandle, address, buffer, size, out _);

            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                {
                    byte[] _buffer = new byte[i];
                    Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                    return Encoding.Unicode.GetString(_buffer);
                }
            }

            return Encoding.Unicode.GetString(buffer);
        }

        public static string ReadStringToUTF8(IntPtr m_pProcessHandle, IntPtr address, int size)
        {
            byte[] buffer = new byte[size];
            ReadProcessMemory(m_pProcessHandle, address, buffer, size, out _);

            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                {
                    byte[] _buffer = new byte[i];
                    Buffer.BlockCopy(buffer, 0, _buffer, 0, i);
                    return Encoding.UTF8.GetString(_buffer);
                }
            }

            return Encoding.UTF8.GetString(buffer);
        }

        #endregion


        #region 特征码相关

       
        //寻找地址
        public static List<IntPtr> FindData(IntPtr hProcess, IntPtr beginAddr, IntPtr endAddr, string data)
        {
            List<IntPtr> result = new List<IntPtr>();
            data = data.ToUpper();
            data = data.Replace(" ", "");
            data = data.Replace("??", @"\S{2}");
            Console.WriteLine(data);
            long len = ((endAddr.ToInt64() - beginAddr.ToInt64()) / 2);
            int pageSize = 0x8000;
            int dlen = data.Length;
            int count = (int)(len / pageSize + 1);
            uint slen = (uint)(pageSize + dlen);
            try
            {
                for (int i = 0; i < count; i++)
                {
                    byte[] buffer = new byte[pageSize + dlen];
                    IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                    long b = (long)(beginAddr + i * pageSize);
                    WinAPI.ReadProcessMemory(hProcess, (IntPtr)b, byteAddress, slen, IntPtr.Zero);
                    String hex = BitConverter.ToString(buffer, 0).Replace("-", "").ToUpper();
                    Regex regex = new Regex(data, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    MatchCollection matchs = regex.Matches(hex);
                    foreach (Match m in matchs)
                    {
                        long r = (long)(m.Index / 2 + m.Index % 2 + b);
                        //Console.WriteLine(r.ToString("X8"));
                        result.Add((IntPtr)r);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return result;
            }
            finally
            {
                //WinAPI.CloseHandle(hProcess);
            }
        }

        public static long FindPattern(IntPtr hProcess, IntPtr beginAddr, IntPtr endAddr, string data)
        {
            return Aobscan.FindPattern(hProcess, beginAddr, endAddr, data);
        }



        //字节数组变为浮点数值
        private static float ByteToFloat(byte[] data)
        {
            return BitConverter.ToSingle(data, 0);
        }
        //浮点数值变为字节数组
        private static byte[] FloatToByte(float data)
        {
            return BitConverter.GetBytes(data);
        }

        //获得目标进程的模块地址
        public static IntPtr GetProcessModuleHandle(uint pid, string moduleName)
        {
            IntPtr address;
            address = MyGetProcessModuleHandle(pid, moduleName);
            //如果自己的方法读取不到，则使用MyAPI.dll的方法去读
            if (address.ToInt64() == 0)
            {
                address = (IntPtr)WinAPI.GetProcessModuleHandle(pid, moduleName);
                //尝试读取5次，有时候会取不到值，原因未知
                int count = 0;
                while (count < 5)
                {
                    address = (IntPtr)WinAPI.GetProcessModuleHandle(pid, moduleName);
                    count++;
                }
            }

            return address;
        }
        public static uint GetProcessModuleSize(uint pid, string moduleName)
        {
            if (pid.Equals(0))
            {
                return 0;
            }

            Process processes = Process.GetProcessById((int)pid);

            if (moduleName == "" || moduleName == null)
            {
                return (uint)processes.MainModule.ModuleMemorySize;
            }

            foreach (ProcessModule item in processes.Modules)
            {
                if (item.ModuleName == moduleName)
                {
                    return (uint)item.ModuleMemorySize;
                }
            }

            return 0;
        }
        public static IntPtr MyGetProcessModuleHandle(uint pid, string moduleName)
        {
            if (pid.Equals(0))
            {
                return IntPtr.Zero;
            }

            Process processes = Process.GetProcessById((int)pid);

            if (moduleName == "" || moduleName == null)
            {
                return processes.MainModule.BaseAddress;
            }

            foreach (ProcessModule item in processes.Modules)
            {
                if (item.ModuleName == moduleName)
                {
                    return item.BaseAddress;
                }
            }
            return IntPtr.Zero;
        }

        #endregion

        #endregion

        #region Windows窗口相关方法

        public struct WindowData
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
        }
        public static WindowData GetGameWindowData(IntPtr WindowHandle)
        {
            // 获取指定窗口句柄的窗口矩形数据和客户区矩形数据
            WinAPI.GetWindowRect(WindowHandle, out RECT windowRect);
            WinAPI.GetClientRect(WindowHandle, out RECT clientRect);

            // 计算窗口区的宽和高
            int windowWidth = windowRect.Right - windowRect.Left;
            int windowHeight = windowRect.Bottom - windowRect.Top;

            // 处理窗口最小化
            if (windowRect.Left < 0)
            {
                return new WindowData()
                {
                    Left = 0,
                    Top = 0,
                    Width = 1,
                    Height = 1
                };
            }

            // 计算客户区的宽和高
            int clientWidth = clientRect.Right - clientRect.Left;
            int clientHeight = clientRect.Bottom - clientRect.Top;

            // 计算边框
            int borderWidth = (windowWidth - clientWidth) / 2;
            int borderHeight = windowHeight - clientHeight - borderWidth;

            return new WindowData()
            {
                Left = windowRect.Left += borderWidth,
                Top = windowRect.Top += borderHeight,
                Width = clientWidth,
                Height = clientHeight
            };
        }

        public static int SetForegroundWindow(IntPtr hwnd)
        {
            return WinAPI.SetForegroundWindow(hwnd);
        }

        /// 设置窗体具有鼠标穿透效果
        public static void SetPenetrate(IntPtr Handle)
        {
            WinAPI.GetWindowLong(Handle, WinAPI.GWL_EXSTYLE);
            WinAPI.SetWindowLong(Handle, WinAPI.GWL_EXSTYLE, WinAPI.WS_EX_TRANSPARENT | WinAPI.WS_EX_LAYERED);
            WinAPI.SetLayeredWindowAttributes(Handle, 0, 100, WinAPI.LWA_COLORKEY);
        }

        #endregion

        #region 调用相关程序方法
        public static void StartOtherApp(string path)
        {
            Process process = new Process();
            process.StartInfo.FileName = path;
            process.Start();

        }
        #endregion

        #region DLL注入与卸载
        public static void DLLInjector(int ProcId, string DllPath)
        {
            WPFCheatUITemplate.Core.Tools.BaseInjector.DLLInjector(ProcId, DllPath);
        }
        public static void DLLUnInjector(int ProcId, string DllPath)
        {
            WPFCheatUITemplate.Core.Tools.BaseInjector.DLLUnInjector(ProcId, DllPath);
        }

        #endregion

        #region 按键相关
        public static bool GetKeyStateIsDown(System.Windows.Forms.Keys keys)
        {
            if ((WinAPI.GetAsyncKeyState((int)keys) & 0x8000) != 0)
            {
                return true;
            } else
            {
                return false;
            }
                
        }
        #endregion
    }



}
