using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Core.Tools.Extensions
{
    public static class ASMExtend
    {

        const int HOOKALLOCSIZE = 1024;

        static long BaseAddre;

        static int index;

        static long currentAddre;
        static int lastSize;


        static Dictionary<string, int> symbols;

        static ASMExtend()
        {

            symbols = new Dictionary<string, int>();
            IntPtr hwnd = GameMode.GameInformation.Handle;
            BaseAddre = WinAPI.VirtualAllocEx(hwnd, 0, HOOKALLOCSIZE, WinAPI.MEM_COMMIT, WinAPI.PAGE_EXECUTE_READWRITE);
            currentAddre = BaseAddre;
            lastSize = 0;
            index = 0;
        }

        static void Reloading()
        {
            IntPtr hwnd = GameMode.GameInformation.Handle;
            BaseAddre = WinAPI.VirtualAllocEx(hwnd, 0, HOOKALLOCSIZE, WinAPI.MEM_COMMIT, WinAPI.PAGE_EXECUTE_READWRITE);
            currentAddre = BaseAddre;
            index = 0;
        }


        public static IntPtr RegisterSymbol(string id,int size)
        {

            index++;

            currentAddre += (index - 1) * lastSize;

            if (currentAddre > BaseAddre + HOOKALLOCSIZE - 8)
            {
                Reloading();


                index++;

                currentAddre += (index - 1) * lastSize;

            }

            symbols[id] = size;

            lastSize = size;

            AppGameFunManager.Instance.AddressDataMg.AddData(id, (IntPtr)currentAddre);

            return (IntPtr)currentAddre;
        }

        public static IntPtr GetSymbolAddress(string id)
        {
            return AppGameFunManager.Instance.AddressDataMg.GetAddress(id);
        }

        public static void SetSymbolData<T>(string id, T data) where T : struct
        {
            IntPtr hwnd = GameMode.GameInformation.Handle;
            int size = 0;
            try
            {
                size = symbols[id];
            }catch
            {
                throw new Exception($"未找到该ID:{id},请检查");
            }

            IntPtr address = AppGameFunManager.Instance.AddressDataMg.GetAddress(id);

            if (Marshal.SizeOf(typeof(T)) > size)
            {
                throw new Exception($"写入的字节大于与该ID:{id}字节，请修改");
            }

            CheatTools.WriteMemory<T>(hwnd, address, data);

        }

        public static T GetSymbolData<T>(string id) where T : struct
        {
            IntPtr hwnd = GameMode.GameInformation.Handle;
            int size = 0;
            try
            {
                size = symbols[id];
            }
            catch
            {
                throw new Exception($"未找到该ID:{id},请检查");
            }

            if (Marshal.SizeOf(typeof(T)) > size)
            {
                throw new Exception($"读取的字节大于与该ID:{id}字节，请修改");
            }

            IntPtr address = AppGameFunManager.Instance.AddressDataMg.GetAddress(id);

           return CheatTools.ReadMemory<T>(hwnd, address);

        }

    }
}
