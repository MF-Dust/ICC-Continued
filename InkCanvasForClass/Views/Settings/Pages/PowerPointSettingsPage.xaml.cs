using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// PowerPointSettingsPage.xaml 的交互逻辑
    /// PPT设置页面
    /// </summary>
    public partial class PowerPointSettingsPage : UserControl
    {
        public PowerPointSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
