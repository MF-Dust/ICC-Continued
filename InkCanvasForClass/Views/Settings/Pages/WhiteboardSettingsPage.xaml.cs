using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// WhiteboardSettingsPage.xaml 的交互逻辑
    /// 白板设置页面
    /// </summary>
    public partial class WhiteboardSettingsPage : UserControl
    {
        public WhiteboardSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
