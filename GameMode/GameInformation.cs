using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCheatUITemplate.GameMode
{
    static class GameInformation
    {
        static IntPtr handle;

        static int pid;

        static string processName = "PlantsVsZombies";


        public static IntPtr Handle
        {
            get
            {
                return handle;
            }

            set
            {
                handle = value;
            }
        }
        public static int Pid
        {
            get
            {
                return pid;
            }

            set
            {
                pid = value;
            }
        }

        public static string ProcessName
        {
            get
            {
                return processName;
            }

            set
            {
                processName = value;
            }
        }

        public static GameVersion.Version CurentVersion
        {
            get
            {
                return GameVersion.GetCurrentVersion(handle);
            }

        }
    }
}
