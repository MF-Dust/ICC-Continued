using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// WritingSettingsPage.xaml 的交互逻辑
    /// 书写设置页面
    /// </summary>
    public partial class WritingSettingsPage : UserControl
    {
        public WritingSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
