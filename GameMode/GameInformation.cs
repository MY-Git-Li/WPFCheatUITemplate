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

        static string windowsName = "Plants vs. Zombies";

        static string classWindowsName = "MainWindow";

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
        }

        public static string WindowsName { get => windowsName;}

        public static string ClassWindowsName { get => classWindowsName; }

        public static GameVersion.Version CurentVersion
        {
            get
            {
                return GameVersion.GetCurrentVersion(Handle);
            }

        }

       
    }
}
