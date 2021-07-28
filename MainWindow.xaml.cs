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

            investigateGame.FindingGame();
            Start.Init();
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
        }

       
    }
}
