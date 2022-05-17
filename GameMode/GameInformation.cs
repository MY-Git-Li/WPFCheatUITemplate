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

        static IntPtr windowHandle;

        static string processName = Configuration.Configure.processName;

        static string windowsName = Configuration.Configure.windowsName;

        static string classWindowsName = Configuration.Configure.classWindowsName;

        static bool isByWindowsNamePrecedence = Configuration.Configure.isByWindowsNamePrecedence;

        #region 字段

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

        public static string WindowsName { get => windowsName; }

        public static string ClassWindowsName { get => classWindowsName; }

        public static GameVersion.Version CurentVersion { get => GameVersion.GetCurrentVersion(Handle);}
            
        public static bool IsByWindowsNamePrecedence { get => isByWindowsNamePrecedence;}

        public static IntPtr WindowHandle { get => windowHandle; set => windowHandle = value; }

        #endregion
    }
}
