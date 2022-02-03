using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Other.GameFuns;

namespace WPFCheatUITemplate
{
    class GameVersion
    {
        public enum Version
        {
            Default,
            V1_0_0_1051,
        }

        public static Version GetCurrentVersion(IntPtr handle)
        {
            GameVersion.Version ret = Version.Default;
            var ver = CheatUITemplt.CheatTools.ReadMemory<int>(handle, (IntPtr)0x552013);
            switch ((uint)ver)
            {
                case 0xC35EDB74:
                    ret = Version.V1_0_0_1051;
                    break;
                default:
                    ret = Version.Default;
                    break;
            }
            return ret;
        }

    }
}
