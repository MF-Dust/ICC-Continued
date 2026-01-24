using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// SnapshotSettingsPage.xaml 的交互逻辑
    /// 截图设置页面
    /// </summary>
    public partial class SnapshotSettingsPage : UserControl
    {
        public SnapshotSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
