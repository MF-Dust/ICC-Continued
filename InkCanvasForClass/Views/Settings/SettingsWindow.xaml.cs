using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Ink_Canvas.ViewModels;
using Wpf.Ui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Ink_Canvas.Views.Settings
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// Fluent Design 风格的设置窗口
    /// </summary>
    public partial class SettingsWindow : FluentWindow
    {
        public SettingsViewModel ViewModel { get; private set; }

        public SettingsWindow()
        {
            InitializeComponent();

            // 获取 ViewModel
            if (App.Current is App app && app.Services != null)
            {
                ViewModel = app.Services.GetService<SettingsViewModel>();
                if (ViewModel != null)
                {
                    DataContext = ViewModel;
                    ViewModel.RestartRequested += ViewModel_RestartRequested;
                    ViewModel.ExitRequested += ViewModel_ExitRequested;
                    ViewModel.ResetRequested += ViewModel_ResetRequested;
                }
            }

            Loaded += SettingsWindow_Loaded;
            Unloaded += SettingsWindow_Unloaded;
        }

        private void SettingsWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.RestartRequested -= ViewModel_RestartRequested;
                ViewModel.ExitRequested -= ViewModel_ExitRequested;
                ViewModel.ResetRequested -= ViewModel_ResetRequested;
            }
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (NavView.SelectedItem == null)
            {
                var firstItem = ViewModel?.NavigationItems
                    .OfType<NavigationViewItem>()
                    .FirstOrDefault(item => item.TargetPageType != null);

                if (firstItem != null)
                {
                    NavView.Navigate(firstItem.TargetPageType);
                    firstItem.IsActive = true;
                }
            }

            if (NavView.SelectedItem is NavigationViewItem selectedItem && selectedItem.TargetPageType != null)
            {
                NavView.Navigate(selectedItem.TargetPageType);
            }
        }

        private void ViewModel_RestartRequested(object sender, EventArgs e)
        {
            HandleRestart();
        }

        private void ViewModel_ExitRequested(object sender, EventArgs e)
        {
            HandleExit();
        }

        private async void ViewModel_ResetRequested(object sender, EventArgs e)
        {
            await HandleResetAsync();
        }

        private async System.Threading.Tasks.Task<bool> ShowResetConfirmationDialog()
        {
            // 创建确认对话框
            var dialog = new ContentDialog(RootContentDialogPresenter)
            {
                Title = "重置设置确认",
                Content = "您确定要重置所有设置到默认值吗？\n\n此操作将清除所有自定义配置，包括：\n• 外观设置\n• 手势设置\n• 书写设置\n• PowerPoint设置\n• 存储设置\n• 其他所有配置\n\n此操作不可撤销！",
                PrimaryButtonText = "确认重置",
                SecondaryButtonText = "取消",
                DefaultButton = ContentDialogButton.Secondary
            };

            // 设置对话框样式 - Wpf.Ui 会自动处理大部分样式，但如果需要可以保留
            // dialog.Background = ... // 通常不需要手动设置背景，除非有特殊需求

            // 显示对话框并等待结果
            var result = await dialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }

        private void HandleRestart()
        {
            try
            {
                // 重启应用
                Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"重启失败: {ex.Message}", "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private async System.Threading.Tasks.Task HandleResetAsync()
        {
            // 显示确认对话框
            var result = await ShowResetConfirmationDialog();
            if (result == true)
            {
                try
                {
                    // 重置设置
                    if (ViewModel != null)
                    {
                        ViewModel.ResetSettings();
                    }
                    
                    System.Windows.MessageBox.Show("设置已重置为默认值", "重置完成", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"重置失败: {ex.Message}", "错误", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }

        private void HandleExit()
        {
            // 退出应用
            Application.Current.Shutdown();
        }

    }
}
