using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// AutomationSettingsPage.xaml 的交互逻辑
    /// 自动化设置页面
    /// </summary>
    public partial class AutomationSettingsPage : UserControl
    {
        public AutomationSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
