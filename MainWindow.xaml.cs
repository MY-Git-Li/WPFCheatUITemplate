using CheatUITemplt;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
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

        Storyboard myStoryboard;
        string processName = GameMode.GameInformation.ProcessName;

        public IntPtr Hwnd;
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowsViewModel();

            AppGameFunManager.Instance.RegisterWindow(this);

            createLayout = new CreateLayout(Resources);
            AppGameFunManager.Instance.RegisterManger(createLayout, keyAndDescribelayouts);

            soundEffect = new SoundEffect();
            AppGameFunManager.Instance.RegisterManger(soundEffect);

            lbl_gemeProcess.Text = processName + ".exe";
            lbl_processID.Text = "";

            investigateGame = new InvestigateGame(processName);

            InitUi();

            AppGameFunManager.Instance.StartUI(Start.Init);

            InitFlashAinimation();
        }

        private void InitUi()
        {
            uILangerManger = new UILangerManger();
            AppGameFunManager.Instance.RegisterManger(uILangerManger);


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
                Description_SC = "Early Access 二十项修改器",
                Description_TC = "Early Access 二十項修改器",
                Description_EN = "Early Access Plus 20 Trainer"

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
                Description_SC = "游戏进程名：",
                Description_TC = "遊戲進程名：",
                Description_EN = "Game Process Name："

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
                Description_EN = "General Notes：Press Ctrl+Shift+home to disable/enable hotkeys"

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
            AppGameFunManager.Instance.hotSystem.WndProcWPF(hwnd, msg, wParam, lParam, ref handled);

            return IntPtr.Zero;
        }

        private void mainWindows_Loaded(object sender, RoutedEventArgs e)
        {

            WindowInteropHelper wndHelper = new WindowInteropHelper(this);
            Hwnd = wndHelper.Handle;

            investigateGame.FindingGame();

            PlayFlashAinimation();
            AppGameFunManager.Instance.SetSimplifiedChinese();
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

            AppGameFunManager.Instance.ClearRes();
        }
        public void EndHotsystem()
        {
            AppGameFunManager.Instance.hotSystem.UnRegisterHotKeyAll(Hwnd);
            AppGameFunManager.Instance.hotSystem.CloseHotKeyFunAll();

        }

        public void PlayFlashAinimation()
        {
            myStoryboard.Begin(this, true);
        }

        public void StopFlashAinimation()
        {
            myStoryboard.Stop(this);
        }

        void InitFlashAinimation()
        {

            ObjectAnimationUsingKeyFrames flashAnimation = new ObjectAnimationUsingKeyFrames();
            flashAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.6));


            flashAnimation.RepeatBehavior = RepeatBehavior.Forever;
            flashAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0))));
            flashAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Hidden, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.3))));
            flashAnimation.KeyFrames.Add(new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.6))));


            Storyboard.SetTargetName(flashAnimation, lbl_gemeProcess.Name);
            Storyboard.SetTargetProperty(
                flashAnimation, new PropertyPath(TextBlock.VisibilityProperty));

            // Create a storyboard to apply the animation.
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(flashAnimation);
        }
    }
}
