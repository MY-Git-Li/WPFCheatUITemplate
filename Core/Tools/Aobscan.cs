using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text;

namespace WPFCheatUITemplate.Core.Tools
{
    class Aobscan
    {
        [DllImport("kernel32.dll")]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress,
        UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

#if X32

        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);
        
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

#endif

#if X64
        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION64 lpBuffer, uint dwLength);


        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION64
        {
            public ulong BaseAddress;
            public ulong AllocationBase;
            public int AllocationProtect;
            public int __alignment1;
            public ulong RegionSize;
            public int State;
            public int Protect;
            public int Type;
            public int __alignment2;
        }


#endif

        private const Int64 MEMORY_BASIC_INFORMATION64_SIZE = 48;

        //https://msdn.microsoft.com/zh-cn/library/windows/desktop/aa366786(v=vs.85).aspx
        private const UInt32 PAGE_EXECUTE_READ = 0x20;
        private const UInt32 PAGE_EXECUTE_READWRITE = 0x40;
        private const UInt32 PAGE_READWRITE = 0x04; 
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa366775(v=vs.85).aspx
        private const UInt32 MEM_COMMIT = 0x1000;

        public static long FindPattern(IntPtr handle, IntPtr beginAddr, IntPtr endAddr, string pattern)
        {
            List<byte> tempArray = new List<byte>();
            foreach (var each in pattern.Split(' '))
            {
                if (each == "??")
                {
                    tempArray.Add(Convert.ToByte("0", 16));
                }
                else
                {
                    tempArray.Add(Convert.ToByte(each, 16));
                }
            }

            byte[] data = tempArray.ToArray();

            var address = beginAddr;
            var stopAddress = endAddr;

#if X32
            while (address.ToInt64() < stopAddress.ToInt64())
            {
                //遍历内存页信息
                var infoLength = VirtualQueryEx(handle, address, out var memInfo, (uint)MEMORY_BASIC_INFORMATION64_SIZE);
                if (infoLength == 0)
                {
                    return 0;
                }
                //判断页面信息，如果State不是MEM_COMMIT，或Protect属性不是PAGE_EXECUTE_READ，则忽略
                //需要注意的是，这里我只比较了PAGE_EXECUTE_READ，实际上，如果写成通用的类，应该判断是否存在PAGE_GUARD位，这样才适用多种情况，具体见MSDN。
                if ((memInfo.State & MEM_COMMIT) != 0 && memInfo.Protect == PAGE_EXECUTE_READ)
                {
                    //读取整个内存页
                    var buffer = CheatTools.ReadMemory(handle, (IntPtr)memInfo.BaseAddress, (int)memInfo.RegionSize);
                    var index = QSIndexOf(buffer, data); //查找，
                    if (index != -1)
                    {
                        ////找到则修改Protect属性。这里只修改2字节即可。
                        var currentAddress = memInfo.BaseAddress + index;
                        //VirtualProtectEx(handle, currentAddress, (UIntPtr)2, PAGE_EXECUTE_READWRITE, out _);
                        return currentAddress.ToInt64();
                    }
                }
                address = (IntPtr)(memInfo.BaseAddress.ToInt64() + memInfo.RegionSize.ToInt64());
            }

#endif

#if X64
            while (address.ToInt64() < stopAddress.ToInt64())
            {
                //遍历内存页信息
                var infoLength = VirtualQueryEx(handle, address, out var memInfo, (uint)MEMORY_BASIC_INFORMATION64_SIZE);
                if (infoLength == 0)
                {
                    return 0;
                }
                //判断页面信息，如果State不是MEM_COMMIT，或Protect属性不是PAGE_EXECUTE_READ，则忽略
                //需要注意的是，这里我只比较了PAGE_EXECUTE_READ，实际上，如果写成通用的类，应该判断是否存在PAGE_GUARD位，这样才适用多种情况，具体见MSDN。
                if ((memInfo.State & MEM_COMMIT) != 0 && memInfo.Protect == PAGE_EXECUTE_READ)
                {
                    //读取整个内存页
                    var buffer = CheatTools.ReadMemory(handle, (IntPtr)memInfo.BaseAddress, (int)memInfo.RegionSize);
                    var index = QSIndexOf(buffer, data); //查找，
                    if (index != -1)
                    {
                        ////找到则修改Protect属性。这里只修改2字节即可。
                        var currentAddress = (long)memInfo.BaseAddress + index;
                        //VirtualProtectEx(handle, currentAddress, (UIntPtr)2, PAGE_EXECUTE_READWRITE, out _);
                        return currentAddress;
                    }
                }
                address = (IntPtr)(memInfo.BaseAddress + memInfo.RegionSize);
            }


#endif

            return 0;
        }


#region Sunday Quick-Search算法的C#实现
        private static Int32[] FlagBuffer = new Int32[256];

        private static Int32 QSIndexOf(Byte[] source, Byte[] pattern)
        {

            if (source.Length < pattern.Length)
            {
                return -1;
            }

            var sLength = source.Length;
            var pLength = pattern.Length;
            var pMaxIndex = pLength - 1;
            var startIndex = 0;
            var endPos = sLength - pLength;
            var badMov = pLength + 1;

            for (Int32 i = 0; i < 256; i++)
            {
                FlagBuffer[i] = badMov;

            }
            for (int i = 0; i <= pMaxIndex; i++)
            {
                FlagBuffer[pattern[i]] = pLength - i;
            }

            Int32 pIndex, step, result = -1;

            while (startIndex <= endPos)
            {
                for (pIndex = 0; pIndex <= pMaxIndex && source[startIndex + pIndex] == pattern[pIndex]; pIndex++)
                {
                    if (pIndex == pMaxIndex)
                    {
                        result = startIndex;
                    }
                }
                if (result > -1) break;
                step = startIndex + pLength;
                if (step >= sLength) break;
                startIndex += FlagBuffer[source[step]];
            }
            return result;
        }

#endregion


    }

}
