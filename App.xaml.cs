using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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

            System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);//重启软件

            Environment.Exit(0);//关闭程序

        }
    }
}
