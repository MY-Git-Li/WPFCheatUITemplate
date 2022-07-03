﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFCheatUITemplate.MVVM;

namespace WPFCheatUITemplate
{
    public class MainWindowsViewModel: ViewModelBase
    {

        Visibility checkVersionedvisibility = Visibility.Hidden;
        public Visibility CheckVersionedvisibility { get => checkVersionedvisibility; set { checkVersionedvisibility = value;RaisePropertyChanged(); } }

        public CommandBase CloseWindowsCommand { get; set; }

        public CommandBase MinimumButtonCommand { get; set; }

        public CommandBase SimplifiedChineseLanguage { get; set; }

        public CommandBase TraditionalChineseLanguage { get; set; }

        public CommandBase EnglishDescriptionLanguage { get; set; }
       

        public MainWindowsViewModel()
        {
            CloseWindowsCommand = new CommandBase();
            CloseWindowsCommand.DoExecute = new Action<object>((o) =>
            {
                (o as Window).Close();
            });
            CloseWindowsCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            MinimumButtonCommand = new CommandBase();
            MinimumButtonCommand.DoExecute = new Action<object>((o) => 
            {
                (o as Window).WindowState = WindowState.Minimized;
            });
            MinimumButtonCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            SimplifiedChineseLanguage = new CommandBase();
            SimplifiedChineseLanguage.DoExecute = new Action<object>((o) =>
            {
                AppGameFunManager.Instance.SetSimplifiedChinese();
            });
            SimplifiedChineseLanguage.DoCanExecute = new Func<object, bool>((o) => { return true; });


            TraditionalChineseLanguage = new CommandBase();
            TraditionalChineseLanguage.DoExecute = new Action<object>((o) =>
            {
                AppGameFunManager.Instance.SetTraditionalChinese();
            });
            TraditionalChineseLanguage.DoCanExecute = new Func<object, bool>((o) => { return true; });

            EnglishDescriptionLanguage = new CommandBase();
            EnglishDescriptionLanguage.DoExecute = new Action<object>((o) =>
            {
                AppGameFunManager.Instance.SetEnglish();
            });
            EnglishDescriptionLanguage.DoCanExecute = new Func<object, bool>((o) => { return true; });

        }
    }
}
