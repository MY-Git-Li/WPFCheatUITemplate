using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using CheatUITemplt;
using WPFCheatUITemplate.Other;

namespace WPFCheatUITemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        CreateLayout createLayout;
        InvestigateGame investigateGame;
        SoundEffect soundEffect;
        UILangerManger uILangerManger;

        string processName = "PlantsVsZombies";

        public IntPtr Hwnd;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowsViewModel();

            GameFunManger.Instance.MainWindow = this;

            createLayout = new CreateLayout(keyAndDescribelayout,Resources);
            GameFunManger.Instance.CreateLayout = createLayout;

            soundEffect = new SoundEffect();
            GameFunManger.Instance.SoundEffect = soundEffect;

            lbl_gemeProcess.Text = processName + ".exe";
            lbl_processID.Text = "0";

            investigateGame = new InvestigateGame(processName);

            InitUi();
        }

        private void InitUi()
        {
            uILangerManger = new UILangerManger();
            GameFunManger.Instance.UILangerManger = uILangerManger;


            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = MainTitle,
                Description_SC = "《植物大战僵尸》",
                Description_TC = "《植物大戰僵屍》",
                Description_EN = "Plants vs Zombies"

            });

            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = subtitle,
                Description_SC = "Early Access 二十七项修改器",
                Description_TC = "Early Access 二十七項修改器",
                Description_EN = "Early Access Plus 27 Trainer"

            });

            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = keyDes,
                Description_SC = "快捷键",
                Description_TC = "快捷鍵",
                Description_EN = "Hotkeys"

            });

            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = funDes,
                Description_SC = "功能列表",
                Description_TC = "功能列表",
                Description_EN = "Options"

            });

            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = process,
                Description_SC = "功能列表",
                Description_TC = "遊戲進程名",
                Description_EN = "Game Process Name"

            });

            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = pid,
                Description_SC = "进程ID：",
                Description_TC = "進程ID：",
                Description_EN = "Process ID："

            });

            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = author,
                Description_SC = "作者：Mr.Li",
                Description_TC = "作者：Mr.Li",
                Description_EN = "Credit：Mr.Li"

            });

            uILangerManger.RegisterLanguageUI(new LanguageUI()
            {
                textBlock = otherDes,
                Description_SC = "其他说明：Ctrl+Shift+Home禁用/启用快捷键",
                Description_TC = "其他說明：Ctrl+Shift+Home禁用/啟用快捷鍵",
                Description_EN = "General Notes: Press Ctrl+Shift+home to disable/enable hotkeys"

            });
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Handle messages...

            if (GameFunManger.Instance.hotSystem.enable)
            {
                const int WM_HOTKEY = 0x0312;
                switch (msg)
                {
                    case WM_HOTKEY:
                        GameFunManger.Instance.hotSystem.RunHotKeyFun((int)wParam);
                        handled = true;
                        break;
                }
            }



            return IntPtr.Zero;
        }

        private void mainWindows_Loaded(object sender, RoutedEventArgs e)
        {

            WindowInteropHelper wndHelper = new WindowInteropHelper(this);
            Hwnd = wndHelper.Handle;

            Start.Init();
            investigateGame.FindingGame();
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void mainWindows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EndHotsystem();
        }
        public void EndHotsystem()
        {
            GameFunManger.Instance.hotSystem.UnRegisterHotKeyAll(Hwnd);
            GameFunManger.Instance.hotSystem.CloseHotKeyFunAll();

            if (GameFunManger.Instance.Pid != 0)
                GameFunManger.Instance.SetAllGameFunEnding();
            
        }

    }
}
