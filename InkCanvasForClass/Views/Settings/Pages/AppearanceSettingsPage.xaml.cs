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
        public AppearanceSettingsViewModel ViewModel { get; private set; }

        public AppearanceSettingsPage()
        {
            InitializeComponent();
            
            // 从服务定位器获取 ViewModel
            ViewModel = ServiceLocator.GetRequiredService<AppearanceSettingsViewModel>();
            DataContext = ViewModel;
        }
    }
}
