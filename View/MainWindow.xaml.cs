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

        struct DoWorkResult
        {
            public bool Islatest { get; set; }
            public UpdateWindow Client { get; set; }
        }

        private void GetVersion(object sender, DoWorkEventArgs e)
        {
            var islatest = AppUpdateManager.Update();

            DoWorkResult doWorkResult = new DoWorkResult();
            doWorkResult.Client = e.Argument as UpdateWindow;
            doWorkResult.Islatest = islatest;

            e.Result = doWorkResult;
        }

        private void GetVersioned(object sender, RunWorkerCompletedEventArgs e)
        {
            var doWorkResult = (DoWorkResult)e.Result;
            if (doWorkResult.Islatest)
            {
                (DataContext as MainWindowsViewModel).CheckVersionedvisibility = Visibility.Visible;
            }
            else
            {
                (DataContext as MainWindowsViewModel).CheckVersionedvisibility = Visibility.Hidden;

                if (doWorkResult.Client == null)
                {
                    var ret =  MessageBox.Show("检查到新版本，是否前往更新界面？", "更新提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if(ret == MessageBoxResult.Yes)
                    {
                        var updateWindow = new UpdateWindow(null);
                        updateWindow.Owner = this;
                        updateWindow.Show();
                    }
                        
                }
                else
                {
                    doWorkResult.Client.Show();
                }
               


            }
        }


        public void CheakVersion(UpdateWindow ClientObj = null)
        {
            if (!startCheakVersion.IsBusy)
            {
                startCheakVersion.RunWorkerAsync(ClientObj);
            }
            
        }

        private void updata_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var updateWindow = new UpdateWindow(CheakVersion);
            updateWindow.Owner = this;
            updateWindow.CheakVersion();
        }
    }
}
