using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas.ViewModels;
using Ink_Canvas.Views.Settings.Pages;
using iNKORE.UI.WPF.Modern.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Ink_Canvas.Views.Settings
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// Fluent Design 风格的设置窗口
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsViewModel ViewModel { get; private set; }

        // 页面缓存
        private readonly Dictionary<string, UserControl> _pages = new Dictionary<string, UserControl>();

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
                }
            }

            Loaded += SettingsWindow_Loaded;
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 默认选中第一项并导航
            if (NavView.MenuItems.Count > 0)
            {
                NavView.SelectedItem = NavView.MenuItems[0];
                // 强制导航一次，以防 SelectionChanged 在 Loaded 之前没有触发
                if (NavView.SelectedItem is NavigationViewItem selectedItem)
                {
                    NavigateToPage(selectedItem.Tag?.ToString());
                }
            }
        }

        /// <summary>
        /// 导航选择变更事件处理
        /// </summary>
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                var tag = selectedItem.Tag?.ToString();
                NavigateToPage(tag);
            }
        }

        /// <summary>
        /// 根据标签导航到对应页面
        /// </summary>
        /// <param name="pageTag">页面标签</param>
        private void NavigateToPage(string pageTag)
        {
            if (string.IsNullOrEmpty(pageTag)) return;

            UserControl page = null;

            // 检查缓存中是否已有页面
            if (_pages.ContainsKey(pageTag))
            {
                page = _pages[pageTag];
            }
            else
            {
                // 创建新页面
                switch (pageTag)
                {
                    case "QuickSettings":
                        page = new QuickSettingsPage();
                        break;
                    case "Appearance":
                        page = new AppearanceSettingsPage();
                        break;
                    case "Gesture":
                        page = new GestureSettingsPage();
                        break;
                    case "Writing":
                        page = new WritingSettingsPage();
                        break;
                    case "Whiteboard":
                        page = new WhiteboardSettingsPage();
                        break;
                    case "PowerPoint":
                        page = new PowerPointSettingsPage();
                        break;
                    case "Storage":
                        page = new StorageSettingsPage();
                        break;
                    case "Advanced":
                        page = new AdvancedSettingsPage();
                        break;
                    case "Automation":
                        page = new AutomationSettingsPage();
                        break;
                    case "Snapshot":
                        page = new SnapshotSettingsPage();
                        break;
                    case "RandomPick":
                        page = new RandomPickSettingsPage();
                        break;
                    case "About":
                        page = new AboutSettingsPage();
                        break;
                    default:
                        page = new QuickSettingsPage();
                        break;
                }

                // 设置页面的 DataContext 为 SettingsViewModel
                if (page != null)
                {
                    page.DataContext = ViewModel;
                    _pages[pageTag] = page;
                }
            }

            // 导航到页面
            if (page != null)
            {
                ContentFrame.Navigate(page);
                ContentScrollViewer?.ScrollToTop();
            }
        }
    }
}
