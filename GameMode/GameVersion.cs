﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCheatUITemplate.Core.GameFuns;
using WPFCheatUITemplate;
namespace WPFCheatUITemplate
{
    class GameVersion
    {
        public enum Version
        {
            Default,
            V1_0_0_1051,
            V1_1_0_1056,
            Steam,
        }

        public static Version GetCurrentVersion(IntPtr handle)
        {
            Version ret = Version.Default;
            var ver = CheatTools.ReadMemory<uint>(handle, (IntPtr)0x552013);

            switch (ver)
            {
                case 80439528:
                    ret = Version.Steam;
                    break;
                case 0x878B0000:
                    ret = Version.V1_1_0_1056;
                    break;
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
