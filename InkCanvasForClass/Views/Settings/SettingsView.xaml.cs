using Ink_Canvas.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ink_Canvas.Views.Settings
{
    /// <summary>
    /// SettingsView.xaml 的交互逻辑
    /// 设置面板 UserControl，提供设置分类导航和内容显示
    /// </summary>
    public partial class SettingsView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ViewModel 属性
        /// </summary>
        public SettingsViewModel ViewModel
        {
            get => DataContext as SettingsViewModel;
            set => DataContext = value;
        }

        /// <summary>
        /// 获取滚动查看器
        /// </summary>
        public ScrollViewer GetScrollViewer() => SettingsPanelScrollViewer;

        /// <summary>
        /// 获取内容面板
        /// </summary>
        public StackPanel GetContentPanel() => SettingsContentPanel;

        /// <summary>
        /// 导航事件
        /// </summary>
        public event EventHandler<SettingsNavigationEventArgs> NavigateToCategory;

        /// <summary>
        /// 滚动变化事件
        /// </summary>
        public event EventHandler<ScrollChangedEventArgs> ScrollChanged;

        /// <summary>
        /// 触发滚动变化事件（供外部调用）
        /// </summary>
        internal void OnScrollChanged(ScrollChangedEventArgs e)
        {
            ScrollChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 打开新版设置窗口
        /// </summary>
        private void BtnOpenNewSettingsWindow_Click(object sender, RoutedEventArgs e)
        {
            // 检查是否已经打开了设置窗口
            foreach (Window window in Application.Current.Windows)
            {
                if (window is SettingsWindow)
                {
                    window.Activate();
                    if (window.WindowState == WindowState.Minimized)
                    {
                        window.WindowState = WindowState.Normal;
                    }
                    return;
                }
            }

            var settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }

    /// <summary>
    /// 设置导航事件参数
    /// </summary>
    public class SettingsNavigationEventArgs : EventArgs
    {
        /// <summary>
        /// 目标分类名称
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="categoryName">分类名称</param>
        public SettingsNavigationEventArgs(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
