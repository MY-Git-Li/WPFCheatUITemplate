using System.Windows;
using System.Windows.Input;
using WPFCheatUITemplate.Core.UI;
using WPFCheatUITemplate.ViewMode;
using WPFCheatUITemplate.Core.Tools.Extensions;
namespace WPFCheatUITemplate.View
{
    /// <summary>
    /// UpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {

        public UpdateWindow()
        {
            InitializeComponent();

            this.DataContext = new UpdateWindowViewModel();

            this.Loaded += UpdateWindow_Loaded;
            this.Closed += UpdateWindow_Closed;

        }

        private void UpdateWindow_Closed(object sender, System.EventArgs e)
        {
            (DataContext as UpdateWindowViewModel).Dispose();
        }

        private void UpdateWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AppGameFunManager.Instance.SetDefaultLanguage();
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
