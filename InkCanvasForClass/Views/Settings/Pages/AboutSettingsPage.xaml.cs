using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;
using Ink_Canvas.Core;
using Ink_Canvas.ViewModels.Settings;

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
            DataContext = ServiceLocator.GetRequiredService<AboutSettingsViewModel>();
        }

        private void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (e.Uri is null)
            {
                return;
            }

            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}
