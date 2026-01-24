using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// RandomPickSettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class RandomPickSettingsPage : UserControl
    {
        public RandomPickSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
        }
    }
}
