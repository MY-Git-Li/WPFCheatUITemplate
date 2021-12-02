using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace CheatUITemplt
{
    class CheatTools
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


        //打开进程handle  0x1F0FFF 最高权限
        public static IntPtr GetProcessHandle(int Pid)
        {
            return WinAPI.OpenProcess(0x1F0FFF, false, Pid);
        }

        #endregion

        #region 内存相关方法

        #region 泛型读写

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, [In, Out] byte[] lpBuffer, int nsize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, [In, Out] byte[] lpBuffer, int nsize, out IntPtr lpNumberOfBytesWritten);

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

        public static T ReadMemory<T>(IntPtr m_pProcessHandle, int[] address) where T : struct
        {

            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];

            for (int i = 0; i < address.Length; i++)
            {
                if (i != address.Length - 1)
                {
                    ReadProcessMemory(m_pProcessHandle, (ByteArrayToStructure<int>(buffer) + address[i]), buffer, buffer.Length, out _);

                    int ret = ByteArrayToStructure<int>(buffer);


                    if (ret == 0)
                        return ByteArrayToStructure<T>(new byte[] { 0 });
                }
                else
                {
                    ReadProcessMemory(m_pProcessHandle, (ByteArrayToStructure<int>(buffer) + address[i]), buffer, buffer.Length, out _);

                    return ByteArrayToStructure<T>(buffer);
                }

            }
            return ByteArrayToStructure<T>(buffer);
        }

        public static void WriteMemory<T>(IntPtr m_pProcessHandle, int[] address, T vaule) where T : struct
        {

            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];

            for (int i = 0; i < address.Length; i++)
            {
                if (i != address.Length - 1)
                {
                    ReadProcessMemory(m_pProcessHandle, (ByteArrayToStructure<int>(buffer) + address[i]), buffer, buffer.Length, out _);

                    int ret = ByteArrayToStructure<int>(buffer);


                    if (ret == 0)
                        break;
                }
                else
                {
                    var dd = StructureToByteArray(vaule);

                    WriteProcessMemory(m_pProcessHandle, (ByteArrayToStructure<int>(buffer) + address[i]), dd, dd.Length, out _);

                }

            }

        }

        public static T ReadMemory<T>(IntPtr m_pProcessHandle, int address) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            ReadProcessMemory(m_pProcessHandle, address, buffer, buffer.Length, out _);
            return ByteArrayToStructure<T>(buffer);
        }

        public static void WriteMemory<T>(IntPtr m_pProcessHandle, int address, object Value) where T : struct
        {
            byte[] buffer = StructureToByteArray(Value);
            WriteProcessMemory(m_pProcessHandle, address, buffer, buffer.Length, out _);
        }

        public static void WriteMemory<T>(IntPtr m_pProcessHandle, int address, byte[] Value) where T : struct
        {
            //byte[] buffer = StructureToByteArray(Value);
           WriteProcessMemory(m_pProcessHandle, address, Value, Value.Length, out _);
        }


        #endregion

        #region 字符串读写

        public string ReadStringASCII(IntPtr m_pProcessHandle, int address, int size)
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

        public string ReadStringUnicode(IntPtr m_pProcessHandle, int address, int size)
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

        #endregion

        #region 指定读写
        //读内存模块  
        public static IntPtr ReadModule(string ModuleName)
        {
            return WinAPI.GetModuleHandle(ModuleName);
        }

        //读取内存中的值
        public static int ReadMemoryValue(int baseAddress, IntPtr hProcess)
        {
            try
            {
                byte[] buffer = new byte[4];
                //获取缓冲区地址
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                //将制定内存中的值读入缓冲区
                WinAPI.ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
                ////关闭操作
                //WinAPI.CloseHandle(hProcess);
                //从非托管内存中读取一个 32 位带符号整数。
                return Marshal.ReadInt32(byteAddress);
            }
            catch
            {
                return 0;
            }
        }

        //读浮点数
        public static float ReadMemoryFloat(int baseAddress, IntPtr hProcess)
        {
            try
            {
                byte[] buffer = new byte[4];
                //获取缓冲区地址
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                //将制定内存中的值读入缓冲区
                WinAPI.ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
                ////关闭操作
                //WinAPI.CloseHandle(hProcess);

                byte[] res = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    res[i] = Marshal.ReadByte(byteAddress + i);
                }

                return ByteToFloat(res);
            }
            catch
            {
                return 0;
            }
        }

        //读长整数型
        public static long ReadMemoryValue(long baseAddress, IntPtr hProcess)
        {
            try
            {
                string temp = ((IntPtr)baseAddress).ToString("x");
                byte[] buffer = new byte[4];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                WinAPI.ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
                //WinAPI.CloseHandle(hProcess);
                string ss = ((IntPtr)baseAddress).ToString("x");
                return Marshal.ReadInt32(byteAddress);
            }
            catch
            {
                return 0;
            }
        }

        //写内存整数型
        public static void WriteMemoryInt(long baseAddress, IntPtr hProcess, int value)
        {
            bool flag;
            int[] Data = new int[] { value };
            flag = WinAPI.WriteProcessMemory(hProcess, (IntPtr)baseAddress, Data, 4, IntPtr.Zero);
            //WinAPI.CloseHandle(hProcess);
        }
        //写内存整数型
        public static void WriteMemoryInt(int baseAddress, IntPtr hProcess, int value)
        {
            bool flag;
            int[] Data = new int[] { value };
            flag = WinAPI.WriteProcessMemory(hProcess, (IntPtr)baseAddress, Data, 4, IntPtr.Zero);
            //WinAPI.CloseHandle(hProcess);
        }

        //写内存字节型  
        public static void WriteMemoryByte(int baseAddress, IntPtr hProcess, byte[] value)
        {
            bool flag;
            flag = WinAPI.WriteProcessMemory(hProcess, (IntPtr)baseAddress, value, value.Length, IntPtr.Zero);
            //WinAPI.CloseHandle(hProcess);
        }

        //写内存浮点型 
        public static void WriteMemoryFloat(int baseAddress, IntPtr hProcess, float value_f)
        {
            byte[] value = FloatToByte(value_f);
            bool flag;
            flag = WinAPI.WriteProcessMemory(hProcess, (IntPtr)baseAddress, value, value.Length, IntPtr.Zero);
            //WinAPI.CloseHandle(hProcess);
        }
        //关闭句柄
        public static void CloseHandle(IntPtr hProcess)
        {
            WinAPI.CloseHandle(hProcess);
        }

        //寻找地址
        public static List<uint> FindData(IntPtr hProcess, uint beginAddr, uint endAddr, String data)
        {
            List<uint> result = new List<uint>();
            data = data.ToUpper();
            data = data.Replace(" ", "");
            data = data.Replace("??", @"\S{2}");
            Console.WriteLine(data);
            uint len = (endAddr - beginAddr) / 2;
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
                    uint b = (uint)(beginAddr + i * pageSize);
                    WinAPI.ReadProcessMemory(hProcess, (IntPtr)b, byteAddress, slen, IntPtr.Zero);
                    String hex = BitConverter.ToString(buffer, 0).Replace("-", "").ToUpper();
                    Regex regex = new Regex(data, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    MatchCollection matchs = regex.Matches(hex);
                    foreach (Match m in matchs)
                    {
                        uint r = (uint)(m.Index / 2 + m.Index % 2 + b);
                        //Console.WriteLine(r.ToString("X8"));
                        result.Add(r);
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
        public static uint GetProcessModuleHandle(uint pid, string moduleName)
        {
            uint address;
            address = MyGetProcessModuleHandle(pid, moduleName);
            //如果自己的方法读取不到，则使用MyAPI.dll的方法去读
            if (address == 0)
            {
                address = WinAPI.GetProcessModuleHandle(pid, moduleName);
                int count = 0;
                while (address < 5 && count < 1000)
                {
                    address = WinAPI.GetProcessModuleHandle(pid, moduleName);
                    count++;
                }
            }

            return address;
        }
        public static uint GetProcessModuleSize(uint pid, string moduleName)
        {
            //获取该系统下所有进程
            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                if (process.Id == pid)
                {
                    foreach (ProcessModule item in process.Modules)
                    {
                        if (item.ModuleName == moduleName)
                        {
                            return (uint)item.ModuleMemorySize;
                        }
                    }
                }
            }
            return 0;
        }
        public static uint MyGetProcessModuleHandle(uint pid, string moduleName)
        {
            //获取该系统下所有进程
            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                if (process.Id == pid)
                {
                    foreach (ProcessModule item in process.Modules)
                    {
                        if (item.ModuleName == moduleName)
                        {
                            return (uint)item.BaseAddress;
                        }
                    }
                }
            }
            return 0;
        }

        #endregion

        #endregion

        #region Windows窗口相关方法

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

       
    }
    
   

}
