using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFCheatUITemplate.ViewMode
{
    public class UpdateWindowViewModel:MainWindowsViewModel, IDisposable
    {
        string mainTitle;
        string subtitle;

        public CommandBase HideWindowsCommand { get; set; }

        public UpdateWindowViewModel()
        {
            mainTitle = "mainTitle";
            subtitle = "Early Access 更新程序";

            HideWindowsCommand = new CommandBase();
            HideWindowsCommand.DoExecute = new Action<object>((o) =>
            {
                (o as Window).Hide();
            });
            HideWindowsCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


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

        public void Dispose()
        {
            var uILangerManger = AppGameFunManager.Instance.UILangerManger;
            uILangerManger.OnLanguageChange -= UILangerManger_OnLanguageChange;

            uILangerManger.DecString("updata_mainTitle");
            uILangerManger.DecString("updata_subtitle");
        }

        public string MainTitle { get => mainTitle; set { mainTitle = value; RaisePropertyChanged("MainTitle"); } }
        public string Subtitle { get => subtitle; set {  subtitle = value; RaisePropertyChanged("Subtitle"); } }
    }
}
