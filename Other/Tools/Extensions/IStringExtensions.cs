using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.Other.Tools.Extensions
{
    public static class IStringExtensions
    {
        #region 简繁转换

        [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
        static extern int LCMapString(int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr, int cchDest);
        const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        //转化方法
        static string ToTraditional(string source, int type)
        {
            byte[] srcByte2 = Encoding.Default.GetBytes(source);
            byte[] desByte2 = new byte[srcByte2.Length];
            LCMapString(2052, type, srcByte2, -1, desByte2, srcByte2.Length);
            string des2 = Encoding.Default.GetString(desByte2);
            return des2;
        }

        public static string ToSimplified(this string str)
        {
            return ToTraditional(str, LCMAP_SIMPLIFIED_CHINESE);   //繁体转简体
        }

        public static string ToTraditional(this string str)
        {
            return ToTraditional(str, LCMAP_TRADITIONAL_CHINESE);   //繁体转简体
        }

        #endregion
    }
}
