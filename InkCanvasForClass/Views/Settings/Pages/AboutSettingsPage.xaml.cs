using System.Windows.Controls;
using Ink_Canvas.ViewModels.Settings;
using Ink_Canvas.Core;
using Microsoft.Extensions.DependencyInjection;

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
            
            // 设置 DataContext
            DataContext = ServiceLocator.ServiceProvider?.GetService<AboutSettingsViewModel>();
        }
    }
}
