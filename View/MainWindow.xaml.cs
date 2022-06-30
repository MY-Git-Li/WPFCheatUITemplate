using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using WPFCheatUITemplate.Core.UI;

namespace WPFCheatUITemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
      
        Storyboard myStoryboard;
       

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowsViewModel();

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

        private void updata_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //TODO:联网更新
            MessageBox.Show("更新代码正在构建中");
        }
    }
}
