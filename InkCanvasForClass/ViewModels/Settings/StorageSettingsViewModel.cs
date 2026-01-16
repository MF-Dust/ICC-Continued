using CommunityToolkit.Mvvm.ComponentModel;
using Ink_Canvas.Models.Settings;
using System;

namespace Ink_Canvas.ViewModels.Settings
{
    public partial class StorageSettingsViewModel : ObservableObject
    {
        private readonly StorageSettings _settings;
        private readonly Action _saveAction;

        public StorageSettingsViewModel(StorageSettings settings, Action saveAction)
        {
            _settings = settings;
            _saveAction = saveAction;
        }

        [ObservableProperty]
        private string _storageLocation;

        [ObservableProperty]
        private string _userStorageLocation;

        partial void OnStorageLocationChanged(string value)
        {
            _settings.StorageLocation = value;
            _saveAction?.Invoke();
        }

        partial void OnUserStorageLocationChanged(string value)
        {
            _settings.UserStorageLocation = value;
            _saveAction?.Invoke();
        }
    }
}
