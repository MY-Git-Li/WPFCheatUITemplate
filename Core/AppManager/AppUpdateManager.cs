using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFCheatUITemplate.Core.AppManager
{
    public class AppUpdateManager
    {
        public static void Update(MainWindowsViewModel mainWindowsViewModel)
        {
            //TODO:联网更新
            MessageBox.Show("更新代码正在构建中");
            mainWindowsViewModel.CheckVersionedvisibility = Visibility.Visible;
        }

    }
}
