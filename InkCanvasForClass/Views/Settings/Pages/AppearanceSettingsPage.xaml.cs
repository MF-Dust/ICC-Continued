using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// AppearanceSettingsPage.xaml 的交互逻辑
    /// 外观设置页面
    /// </summary>
    public partial class AppearanceSettingsPage : UserControl
    {
        public AppearanceSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
