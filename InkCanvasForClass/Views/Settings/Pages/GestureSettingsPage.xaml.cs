using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// GestureSettingsPage.xaml 的交互逻辑
    /// 手势设置页面
    /// </summary>
    public partial class GestureSettingsPage : UserControl
    {
        public GestureSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
