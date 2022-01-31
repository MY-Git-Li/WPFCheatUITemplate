using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace WPFCheatUITemplate.Other.Draw
{
    class Memory
    {
        private Process m_Process;
        private IntPtr m_pWindowHandle;
        private IntPtr m_pProcessHandle;

        public struct WindowData
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
        }

        public void Initialize(string ProcessName)
        {
            m_Process = Process.GetProcessesByName(ProcessName)[0];
            m_pWindowHandle = m_Process.MainWindowHandle;
            m_pProcessHandle = WinAPI.OpenProcess(WinAPI.PROCESS_VM_READ | WinAPI.PROCESS_VM_WRITE | WinAPI.PROCESS_VM_OPERATION, false, m_Process.Id);
        }

        public void SetProcessHandle(IntPtr handle)
        {
            m_pProcessHandle = handle;
        }


        public void CloseHandle()
        {
            WinAPI.CloseHandle(m_pProcessHandle);
        }

        public int GetModule(string moduleName)
        {
            foreach (ProcessModule module in m_Process.Modules)
            {
                if (module.ModuleName == (moduleName))
                {
                    return (int)module.BaseAddress;
                }
            }

            return 0;
        }

        public void SetForegroundWindow()
        {
            WinAPI.SetForegroundWindow(m_pWindowHandle);
        }

        public WindowData GetGameWindowData()
        {
            // 获取指定窗口句柄的窗口矩形数据和客户区矩形数据
            WinAPI.GetWindowRect(m_pWindowHandle, out RECT windowRect);
            WinAPI.GetClientRect(m_pWindowHandle, out RECT clientRect);

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

        public T ReadMemory<T>(int address) where T : struct
        {
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            WinAPI.ReadProcessMemory(m_pProcessHandle, address, buffer, buffer.Length, out _);
            return ByteArrayToStructure<T>(buffer);
        }

        public void WriteMemory<T>(int address, object Value) where T : struct
        {
            byte[] buffer = StructureToByteArray(Value);
            WinAPI.WriteProcessMemory(m_pProcessHandle, address, buffer, buffer.Length, out _);
        }
        public void WriteMemory<T>(int address, byte[] Value) where T : struct
        {
            //byte[] buffer = StructureToByteArray(Value);
            WinAPI.WriteProcessMemory(m_pProcessHandle, address, Value, Value.Length, out _);
        }


        public void WriteMemoryByID(string id,bool isOrc = false)
        {
            if (isOrc)
            {
                WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress(id), Other.GameFuns.AddressDataManager.GetOrcData(id));
            }else
            {
                WriteMemory<byte>(Other.GameFuns.AddressDataManager.GetAddress(id), Other.GameFuns.AddressDataManager.GetModifyData(id));
            }

        }

        public void WriteMemoryByID<T>(string id, object value) where T : struct
        {
            WriteMemory<T>(Other.GameFuns.AddressDataManager.GetAddress(id), value);
        }


        public float[] ReadMatrix<T>(int address, int MatrixSize) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[ByteSize * MatrixSize];
            WinAPI.ReadProcessMemory(m_pProcessHandle, address, buffer, buffer.Length, out _);
            return ConvertToFloatArray(buffer);
        }

        public string ReadString(int address, int size)
        {
            byte[] buffer = new byte[size];
            WinAPI.ReadProcessMemory(m_pProcessHandle, address, buffer, size, out _);

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

        #region Conversion
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

        private static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
            {
                throw new ArgumentException();
            }

            float[] floats = new float[bytes.Length / 4];
            for (int i = 0; i < floats.Length; i++)
            {
                floats[i] = BitConverter.ToSingle(bytes, i * 4);
            }
            return floats;
        }
        #endregion
    }
}
