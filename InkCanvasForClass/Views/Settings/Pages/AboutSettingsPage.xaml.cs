using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// AboutSettingsPage.xaml 的交互逻辑
    /// 关于页面
    /// </summary>
    public partial class AboutSettingsPage : UserControl
    {
        public AboutSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
