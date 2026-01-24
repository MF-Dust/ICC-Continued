using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// AdvancedSettingsPage.xaml 的交互逻辑
    /// 高级设置页面
    /// </summary>
    public partial class AdvancedSettingsPage : UserControl
    {
        public AdvancedSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
