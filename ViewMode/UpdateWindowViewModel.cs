using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFCheatUITemplate.ViewMode
{
    public class UpdateWindowViewModel:MainWindowsViewModel
    {
        string mainTitle;
        string subtitle;


        public UpdateWindowViewModel()
        {
            mainTitle = "mainTitle";
            subtitle = "Early Access 更新程序";

            var uILangerManger = AppGameFunManager.Instance.UILangerManger;

            uILangerManger.AddString("updata_mainTitle", "更新程序", "Update Program");

            uILangerManger.AddString("updata_subtitle", "Early Access 更新程序", "Early Access Update Program");

            uILangerManger.OnLanguageChange += UILangerManger_OnLanguageChange;

        }

        private void UILangerManger_OnLanguageChange(Core.UI.UILangerManger obj)
        {
            MainTitle = obj.GetString("updata_mainTitle");
            Subtitle = obj.GetString("updata_subtitle");
        }

        ~UpdateWindowViewModel()
        {
            var uILangerManger = AppGameFunManager.Instance.UILangerManger;
            uILangerManger.OnLanguageChange -= UILangerManger_OnLanguageChange;
        }

        public string MainTitle { get => mainTitle; set { mainTitle = value; RaisePropertyChanged("MainTitle"); } }
        public string Subtitle { get => subtitle; set {  subtitle = value; RaisePropertyChanged("Subtitle"); } }
    }
}
