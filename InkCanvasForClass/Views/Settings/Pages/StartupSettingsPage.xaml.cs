using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas.Core;
using Ink_Canvas.Helpers;
using Ink_Canvas.ViewModels;
using iNKORE.UI.WPF.Modern.Controls;

namespace Ink_Canvas.Views.Settings.Pages
{
    /// <summary>
    /// StartupSettingsPage.xaml 的交互逻辑
    /// 启动设置页面
    /// </summary>
    public partial class StartupSettingsPage : UserControl
    {
        private const string AppName = "Ink Canvas";

        public StartupSettingsPage()
        {
            InitializeComponent();
            DataContext = ServiceLocator.GetRequiredService<SettingsViewModel>();
            Loaded += StartupSettingsPage_Loaded;
        }

        /// <summary>
        /// 页面加载完成
        /// </summary>
        private void StartupSettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            // 检查开机自启动状态
            ToggleRunAtStartup.IsOn = CheckAutoStartEnabled();
        }

        /// <summary>
        /// 检查是否已启用开机自启动
        /// </summary>
        private bool CheckAutoStartEnabled()
        {
            try
            {
                string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                string shortcutPath = Path.Combine(startupPath, AppName + ".lnk");
                return File.Exists(shortcutPath);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"检查开机自启动状态失败：{ex.Message}", LogHelper.LogType.Warning);
                return false;
            }
        }

        /// <summary>
        /// 开机自启动开关切换
        /// </summary>
        private void ToggleRunAtStartup_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn)
                {
                    StartAutomaticallyCreate(AppName);
                }
                else
                {
                    StartAutomaticallyDel(AppName);
                }
            }
        }

        /// <summary>
        /// 创建开机自启动快捷方式
        /// </summary>
        private static bool StartAutomaticallyCreate(string exeName)
        {
            try
            {
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                dynamic shortcut = shell.CreateShortcut(
                    Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk");
                shortcut.TargetPath = System.Windows.Forms.Application.ExecutablePath;
                shortcut.WorkingDirectory = Environment.CurrentDirectory;
                shortcut.WindowStyle = 1;
                shortcut.Description = exeName + "_Ink";
                shortcut.Save();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"Failed to create auto-start shortcut: {ex.Message}", LogHelper.LogType.Error);
                return false;
            }
        }

        /// <summary>
        /// 删除开机自启动快捷方式
        /// </summary>
        private static bool StartAutomaticallyDel(string exeName)
        {
            try
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + exeName + ".lnk");
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"Failed to delete auto-start shortcut: {ex.Message}", LogHelper.LogType.Warning);
                return false;
            }
        }
    }
}
