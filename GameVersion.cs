using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate
{
    class GameVersion
    {
        public enum Version
        {
            Null,
        }

        public static Version GetCurrentVersion(IntPtr handle)
        {
            //var ret = CheatUITemplt.CheatTools.ReadMemoryValue(0x555000,handle);
            //switch (ret)
            //{
            //    default:
            //        return Version.Null;
            //}
            return Version.Null;
        }

    }
}
