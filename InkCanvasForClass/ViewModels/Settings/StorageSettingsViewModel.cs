using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ink_Canvas.Models.Settings;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace Ink_Canvas.ViewModels.Settings
{
    public class StorageLocationOption
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Tag { get; set; }
        public string Icon { get; set; }
    }

    public partial class StorageSettingsViewModel : ObservableObject
    {
        private readonly StorageSettings _settings;
        private readonly Action _saveAction;

        public ObservableCollection<StorageLocationOption> StorageLocationOptions { get; } = new ObservableCollection<StorageLocationOption>();

        public StorageSettingsViewModel(StorageSettings settings, Action saveAction)
        {
            _settings = settings;
            _saveAction = saveAction;

            InitializeStorageOptions();

            // Initialize with dummy data for now
            _totalSpace = 512L * 1024 * 1024 * 1024; // 512 GB
            _usedSpace = 143.99 * 1024 * 1024 * 1024;
            _freeSpace = 332.64 * 1024 * 1024 * 1024;
            _iccDataSpace = 1.36 * 1024 * 1024; // 1.36 MB
        }

        private void InitializeStorageOptions()
        {
            StorageLocationOptions.Add(new StorageLocationOption
            {
                Name = "icc安装目录",
                Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"),
                Tag = "a-",
                Icon = "\uE8B7"
            });

            StorageLocationOptions.Add(new StorageLocationOption
            {
                Name = "“文档”文件夹",
                Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Tag = "documents",
                Icon = "\uF000"
            });

            StorageLocationOptions.Add(new StorageLocationOption
            {
                Name = "当前用户目录",
                Path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Tag = "user",
                Icon = "\uE77B"
            });

            StorageLocationOptions.Add(new StorageLocationOption
            {
                Name = "“桌面”文件夹",
                Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Tag = "desktop",
                Icon = "\uE8D8"
            });

            StorageLocationOptions.Add(new StorageLocationOption
            {
                Name = "自定义...",
                Path = "",
                Tag = "custom",
                Icon = "\uE8B7"
            });
        }

        [ObservableProperty]
        private string _storageLocation;

        [ObservableProperty]
        private string _userStorageLocation;

        partial void OnStorageLocationChanged(string value)
        {
            _settings.StorageLocation = value;
            _saveAction?.Invoke();
            OnPropertyChanged(nameof(CurrentStoragePath));
        }

        partial void OnUserStorageLocationChanged(string value)
        {
            _settings.UserStorageLocation = value;
            _saveAction?.Invoke();
            OnPropertyChanged(nameof(CurrentStoragePath));
        }

        // Disk Usage Properties (Mock Data for UI)
        [ObservableProperty] private double _totalSpace;
        [ObservableProperty] private double _usedSpace;
        [ObservableProperty] private double _freeSpace;
        [ObservableProperty] private double _iccDataSpace;

        public double OtherUsedSpace => Math.Max(0, UsedSpace - IccDataSpace);

        partial void OnUsedSpaceChanged(double value) => OnPropertyChanged(nameof(OtherUsedSpace));
        partial void OnIccDataSpaceChanged(double value) => OnPropertyChanged(nameof(OtherUsedSpace));

        [ObservableProperty] private double _autoSavedInkSize;
        [ObservableProperty] private double _boardImageRefSize;
        [ObservableProperty] private double _exportedBoardSize;
        [ObservableProperty] private double _cacheSize;
        [ObservableProperty] private double _autoSavedScreenshotSize;

        [ObservableProperty] private int _autoSavedInkCount;
        [ObservableProperty] private int _boardImageRefCount;
        [ObservableProperty] private int _exportedBoardCount;
        [ObservableProperty] private int _cacheCount;
        [ObservableProperty] private int _autoSavedScreenshotCount;

        public string CurrentStoragePath => _settings.StorageLocation == "a-"
            ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data")
            : _settings.UserStorageLocation;

        [RelayCommand]
        private void BrowseFolder()
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "选择存储位置";
            dialog.UseDescriptionForTitle = true;

            if (!string.IsNullOrEmpty(_settings.UserStorageLocation))
            {
                dialog.SelectedPath = _settings.UserStorageLocation;
            }

            if (dialog.ShowDialog() == true)
            {
                _settings.UserStorageLocation = dialog.SelectedPath;
                _saveAction?.Invoke();
                OnPropertyChanged(nameof(UserStorageLocation));
                OnPropertyChanged(nameof(CurrentStoragePath));
            }
        }

        [RelayCommand]
        private void OpenFolder(string folderType)
        {
            // TODO: Implement folder opening logic
            Debug.WriteLine($"Open folder: {folderType}");
        }

        [RelayCommand]
        private void ClearCache()
        {
            // TODO: Implement clear cache logic
            Debug.WriteLine("Clear cache");
        }

        [RelayCommand]
        private void ClearAutoSavedScreenshots()
        {
            // TODO: Implement clear screenshots logic
            Debug.WriteLine("Clear auto saved screenshots");
        }
    }
}
