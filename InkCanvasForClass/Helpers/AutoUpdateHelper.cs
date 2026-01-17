using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Ink_Canvas.Helpers
{
    internal class AutoUpdateHelper
    {
        public static async Task<string> CheckForUpdates(string proxy = null)
        {
            try
            {
                string localVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                string remoteAddress = proxy;
                remoteAddress += "https://gitea.bliemhax.com/kriastans/InkCanvasForClass/raw/branch/master/AutomaticUpdateVersionControl.txt";
                string remoteVersion = await GetRemoteVersion(remoteAddress);

                if (remoteVersion != null)
                {
                    Version local = new Version(localVersion);
                    Version remote = new Version(remoteVersion);
                    if (remote > local)
                    {
                        LogHelper.WriteLogToFile("自动更新 | 检测到新版本：" + remoteVersion, LogHelper.LogType.Info);
                        return remoteVersion;
                    }
                    else return null;
                }
                else
                {
                    LogHelper.WriteLogToFile("获取远程版本失败。", LogHelper.LogType.Error);
                    return null;
                }
            }
            catch (FormatException ex)
            {
                LogHelper.WriteLogToFile($"自动更新 | 版本格式无效：{ex.Message}", LogHelper.LogType.Error);
                LogHelper.NewLog(ex);
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"自动更新 | 检查更新时发生异常：{ex.Message}", LogHelper.LogType.Error);
                LogHelper.NewLog(ex);
                return null;
            }
        }

        public static async Task<string> GetRemoteVersion(string fileUrl)
        {
            try
            {
                // Ensure TLS 1.2/1.3 are supported
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
            } catch (Exception ex) {
                LogHelper.WriteLogToFile($"设置安全协议失败：{ex.Message}", LogHelper.LogType.Warning);
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(fileUrl);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    LogHelper.WriteLogToFile($"自动更新 | HTTP 请求错误（SSL/TLS？）：{ex.Message} | 内部异常：{ex.InnerException?.Message}", LogHelper.LogType.Error);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLogToFile($"自动更新 | 请求失败：{ex.Message}", LogHelper.LogType.Error);
                }

                return null;
            }
        }

        private static string updatesFolderPath = Path.Combine(App.RootPath, "AutoUpdate");
        private static string statusFilePath = null;

        public static async Task<bool> DownloadSetupFileAndSaveStatus(string version, string proxy = "")
        {
            try
            {
                statusFilePath = Path.Combine(updatesFolderPath, $"DownloadV{version}Status.txt");

                if (File.Exists(statusFilePath) && File.ReadAllText(statusFilePath).Trim().ToLower() == "true")
                {
                    LogHelper.WriteLogToFile("自动更新 | 安装包已下载，跳过重复下载。");
                    return true;
                }

                string downloadUrl = $"{proxy}https://github.com/ChangSakura/Ink-Canvas/releases/download/v{version}/Ink.Canvas.Annotation.V{version}.Setup.exe";

                SaveDownloadStatus(false);
                await DownloadFile(downloadUrl, $"{updatesFolderPath}\\Ink.Canvas.Annotation.V{version}.Setup.exe");
                SaveDownloadStatus(true);

                LogHelper.WriteLogToFile("自动更新 | 安装包下载完成。");
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"自动更新 | 下载或安装更新失败：{ex.Message}", LogHelper.LogType.Error);

                SaveDownloadStatus(false);
                return false;
            }
        }

        private static async Task DownloadFile(string fileUrl, string destinationPath)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(fileUrl);
                        response.EnsureSuccessStatusCode();

                        using (FileStream fileStream = File.Create(destinationPath))
                        {
                            await response.Content.CopyToAsync(fileStream);
                            fileStream.Close();
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        LogHelper.WriteLogToFile($"自动更新 | HTTP 请求错误：{ex.Message}", LogHelper.LogType.Error);
                        LogHelper.NewLog(ex);
                        throw;
                    }
                    catch (IOException ex)
                    {
                        LogHelper.WriteLogToFile($"自动更新 | 文件读写错误：{ex.Message}", LogHelper.LogType.Error);
                        LogHelper.NewLog(ex);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLogToFile($"自动更新 | 下载文件时发生异常：{ex.Message}", LogHelper.LogType.Error);
                        LogHelper.NewLog(ex);
                        throw;
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                LogHelper.WriteLogToFile($"自动更新 | HttpClient 被提前释放：{ex.Message}", LogHelper.LogType.Error);
                LogHelper.NewLog(ex);
                throw;
            }
        }

        private static void SaveDownloadStatus(bool isSuccess)
        {
            try
            {
                if (statusFilePath == null) return;

                string directory = Path.GetDirectoryName(statusFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(statusFilePath, isSuccess.ToString());
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"自动更新 | 保存下载状态失败：{ex.Message}", LogHelper.LogType.Error);
            }
        }

        public static void InstallNewVersionApp(string version, bool isInSilence)
        {
            try
            {
                string setupFilePath = Path.Combine(updatesFolderPath, $"Ink.Canvas.Annotation.V{version}.Setup.exe");

                if (!File.Exists(setupFilePath))
                {
                    LogHelper.WriteLogToFile($"AutoUpdate | Setup file not found: {setupFilePath}", LogHelper.LogType.Error);
                    return;
                }

                string InstallCommand = $"\"{setupFilePath}\" /SILENT";
                if (isInSilence) InstallCommand += " /VERYSILENT";
                ExecuteCommandLine(InstallCommand);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Application.Current.Shutdown();
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"AutoUpdate | Error installing update: {ex.Message}", LogHelper.LogType.Error);
            }
        }


        private static void ExecuteCommandLine(string command)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = processStartInfo })
                {
                    process.Start();
                    Application.Current.Shutdown();
                    /*process.WaitForExit();
                    int exitCode = process.ExitCode;*/
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"AutoUpdate | Failed to execute command: {ex.Message}", LogHelper.LogType.Error);
                LogHelper.NewLog(ex);
            }
        }

        public static void DeleteUpdatesFolder()
        {
            try
            {
                if (Directory.Exists(updatesFolderPath))
                {
                    Directory.Delete(updatesFolderPath, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"AutoUpdate clearing| Error deleting updates folder: {ex.Message}", LogHelper.LogType.Error);
            }
        }
    }

    internal class AutoUpdateWithSilenceTimeComboBox
    {
        public static ObservableCollection<string> Hours { get; set; } = new ObservableCollection<string>();
        public static ObservableCollection<string> Minutes { get; set; } = new ObservableCollection<string>();

        public static void InitializeAutoUpdateWithSilenceTimeComboBoxOptions(ComboBox startTimeComboBox, ComboBox endTimeComboBox)
        {
            for (int hour = 0; hour <= 23; ++hour)
            {
                Hours.Add(hour.ToString("00"));
            }
            for (int minute = 0; minute <= 59; minute += 20)
            {
                Minutes.Add(minute.ToString("00"));
            }
            startTimeComboBox.ItemsSource = Hours.SelectMany(h => Minutes.Select(m => $"{h}:{m}"));
            endTimeComboBox.ItemsSource = Hours.SelectMany(h => Minutes.Select(m => $"{h}:{m}"));
        }

        public static bool CheckIsInSilencePeriod(string startTime, string endTime)
        {
            if (startTime == endTime) return true;
            DateTime currentTime = DateTime.Now;

            DateTime StartTime = DateTime.ParseExact(startTime, "HH:mm", null);
            DateTime EndTime = DateTime.ParseExact(endTime, "HH:mm", null);
            if (StartTime <= EndTime)
            { // 单日时间段
                return currentTime >= StartTime && currentTime <= EndTime;
            }
            else
            { // 跨越两天的时间段
                return currentTime >= StartTime || currentTime <= EndTime;
            }
        }
    }
}
