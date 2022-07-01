using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFCheatUITemplate.Core.Tools.Extensions;
namespace WPFCheatUITemplate.Configuration
{
    static class Configure
    {
        #region 程序配置

        public static string WindowTitle = $"《植物大战僵尸》 Early Access";
        public static double WindowHeight = 800;
        public static double WindowWidth = 880;

        public static string MainTitle_SC = "《植物大战僵尸》";
        public static string MainTitle_TC = "《植物大戰僵屍》";
        public static string MainTitle_EN = "Plants vs Zombies";

        /// <summary>
        /// 界面的图片地址
        /// </summary>
        public static string PictureImagePath = "../Picture/2.png";

        #region 基本不用动

        public static string Subtitle_SC = $"Early Access";
        public static string Subtitle_TC = $"Early Access";
        public static string Subtitle_EN = $"Early Access Plus";

        public static string KeyDes_SC = "快捷键";
        public static string KeyDes_TC = "快捷鍵";
        public static string KeyDes_EN = "Hotkeys";

        public static string FunDes_SC = "功能列表";
        public static string FunDes_TC = "功能列表";
        public static string FunDes_EN = "Options";

        public static string ProcessDes_SC = "游戏进程名：";
        public static string ProcessDes_TC = "遊戲進程名：";
        public static string ProcessDes_EN = "Game Process Name:";

        public static string PidDes_SC = "进程ID：";
        public static string PidDes_TC = "進程ID：";
        public static string PidDes_EN = "Process ID:";

        public static string AuthorDes_SC = "作者：Mr.Li";
        public static string AuthorDes_TC = "作者：Mr.Li";
        public static string AuthorDes_EN = "Credit：Mr.Li";

        public static string OtherDes_SC = "其他说明：Ctrl+Shift+Home禁用/启用快捷键";
        public static string OtherDes_TC = "其他說明：Ctrl+Shift+Home禁用/啟用快捷鍵";
        public static string OtherDes_EN = "General Notes：Press Ctrl+Shift+home to disable/enable hotkeys";

        #endregion

        #endregion

        #region 目标程序配置

        public static string processName = "PlantsVsZombies";

        public static string windowsName = "Plants vs. Zombies";

        public static string classWindowsName = "MainWindow";

        public static bool isByWindowsNamePrecedence = true;


        #endregion

        #region 更新配置
        /// <summary>
        /// 检查更新URL地址
        /// </summary>
        public static string ConfigAddress = "";
        /// <summary>
        /// 要更新程序的URL地址
        /// </summary>
        public static string UpdateAddress = "";
        /// <summary>
        /// 当前版本
        /// </summary>
        public static Version version = Application.ResourceAssembly.GetName().Version;

        #endregion

    }
}
