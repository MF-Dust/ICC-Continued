using System.Windows.Controls;
using Ink_Canvas.ViewModels.Settings;
using Ink_Canvas.Core;
using Microsoft.Extensions.DependencyInjection;

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
            
            // 设置 DataContext
            DataContext = ServiceLocator.ServiceProvider?.GetService<AdvancedSettingsViewModel>();
        }
    }
}
