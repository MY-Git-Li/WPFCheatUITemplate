using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFCheatUITemplate.Core.Tools.Extensions;
namespace WPFCheatUITemplate
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Mutex AppMainMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            AppMainMutex = new Mutex(true, ResourceAssembly.GetName().Name, out var createdNew);

            if (createdNew)
            {
                base.OnStartup(e);
            }
            else
            {
                MessageBox.Show("请不要重复打开，程序已经运行\n如果一直提示，请到\"任务管理器-详细信息（win7为进程）\"里结束本程序",
                    "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                Current.Shutdown();
            }
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            string mode = WPFCheatUITemplate.Properties.Settings.Default.langer;
            string error = "";
            if (mode == "SC")
            {
                error = WPFCheatUITemplate.Properties.Resources.error_sc;
            }
            if (mode == "TC")
            {
                error = WPFCheatUITemplate.Properties.Resources.error_sc.ToTraditional();
            }
            if (mode == "EN")
            {
                error = WPFCheatUITemplate.Properties.Resources.error_en;
            }

            MessageBox.Show(error.Split('@')[0], error.Split('@')[1], MessageBoxButton.OK,MessageBoxImage.Error);
            e.Handled = true;

            RestartApp();
        }

        public void RestartApp()
        {
            App.AppMainMutex.Dispose();

            System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);//重启软件

            //Environment.Exit(0);//关闭程序
            Application.Current.Shutdown();
        }
    }
}
