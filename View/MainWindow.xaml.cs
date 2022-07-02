using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using WPFCheatUITemplate.Core.AppManager;
using WPFCheatUITemplate.Core.UI;
using WPFCheatUITemplate.View;

namespace WPFCheatUITemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
      
        Storyboard myStoryboard;

        BackgroundWorker startCheakVersion;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowsViewModel();

            startCheakVersion = new BackgroundWorker();
            startCheakVersion.DoWork += new DoWorkEventHandler(GetVersion);
            startCheakVersion.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GetVersioned);

            AppGameFunManager.Instance.RegisterWindow(this, keyAndDescribelayouts);

            AppGameFunManager.Instance.StartUI(Start.Init);

            InitFlashAinimation();
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
            AppGameFunManager.Instance.WndProcWPF(hwnd, msg, wParam, lParam, ref handled);

            return IntPtr.Zero;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
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

        private void GetVersion(object sender, DoWorkEventArgs e)
        {
            var islatest = AppUpdateManager.Update();
            e.Result = islatest;
        }

        private void GetVersioned(object sender, RunWorkerCompletedEventArgs e)
        {

            if ((bool)e.Result)
            {
                (DataContext as MainWindowsViewModel).CheckVersionedvisibility = Visibility.Visible;
            }
            else
            {
                (DataContext as MainWindowsViewModel).CheckVersionedvisibility = Visibility.Hidden;
                //TODO 让用户选择是否弹出更新窗口
                var updateWindow = new UpdateWindow();
                updateWindow.Show();
            }
        }


        public void CheakVersion()
        {
            if (!startCheakVersion.IsBusy)
            {
                startCheakVersion.RunWorkerAsync();
            }
            
        }

        private void updata_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheakVersion();
        }
    }
}
