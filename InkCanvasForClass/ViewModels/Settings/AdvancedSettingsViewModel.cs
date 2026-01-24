using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Ink_Canvas.Models.Settings;

namespace Ink_Canvas.ViewModels.Settings
{
    /// <summary>
    /// Advanced 设置 ViewModel
    /// </summary>
    public partial class AdvancedSettingsViewModel : ObservableObject
    {
        private readonly AdvancedSettings _advanced;
        private readonly Action _saveAction;

        public AdvancedSettingsViewModel(AdvancedSettings advanced, Action saveAction)
        {
            _advanced = advanced ?? throw new ArgumentNullException(nameof(advanced));
            _saveAction = saveAction;
        }

        #region Touch and Eraser Settings

        public bool IsSpecialScreen
        {
            get => _advanced.IsSpecialScreen;
            set
            {
                if (SetProperty(_advanced.IsSpecialScreen, value, _advanced, (a, v) => a.IsSpecialScreen = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public double TouchMultiplier
        {
            get => _advanced.TouchMultiplier;
            set
            {
                if (SetProperty(_advanced.TouchMultiplier, value, _advanced, (a, v) => a.TouchMultiplier = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public double NibModeBoundsWidthThresholdValue
        {
            get => _advanced.NibModeBoundsWidthThresholdValue;
            set
            {
                if (SetProperty(_advanced.NibModeBoundsWidthThresholdValue, value, _advanced, (a, v) => a.NibModeBoundsWidthThresholdValue = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public double FingerModeBoundsWidthThresholdValue
        {
            get => _advanced.FingerModeBoundsWidthThresholdValue;
            set
            {
                if (SetProperty(_advanced.FingerModeBoundsWidthThresholdValue, value, _advanced, (a, v) => a.FingerModeBoundsWidthThresholdValue = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public double NibModeBoundsWidthEraserSize
        {
            get => _advanced.NibModeBoundsWidthEraserSize;
            set
            {
                if (SetProperty(_advanced.NibModeBoundsWidthEraserSize, value, _advanced, (a, v) => a.NibModeBoundsWidthEraserSize = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public double FingerModeBoundsWidthEraserSize
        {
            get => _advanced.FingerModeBoundsWidthEraserSize;
            set
            {
                if (SetProperty(_advanced.FingerModeBoundsWidthEraserSize, value, _advanced, (a, v) => a.FingerModeBoundsWidthEraserSize = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public bool IsQuadIR
        {
            get => _advanced.IsQuadIR;
            set
            {
                if (SetProperty(_advanced.IsQuadIR, value, _advanced, (a, v) => a.IsQuadIR = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        #endregion

        #region Advanced Features

        public bool IsLogEnabled
        {
            get => _advanced.IsLogEnabled;
            set
            {
                if (SetProperty(_advanced.IsLogEnabled, value, _advanced, (a, v) => a.IsLogEnabled = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public bool IsEnableFullScreenHelper
        {
            get => _advanced.IsEnableFullScreenHelper;
            set
            {
                if (SetProperty(_advanced.IsEnableFullScreenHelper, value, _advanced, (a, v) => a.IsEnableFullScreenHelper = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public bool IsEnableEdgeGestureUtil
        {
            get => _advanced.IsEnableEdgeGestureUtil;
            set
            {
                if (SetProperty(_advanced.IsEnableEdgeGestureUtil, value, _advanced, (a, v) => a.IsEnableEdgeGestureUtil = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public bool IsDisableCloseWindow
        {
            get => _advanced.IsDisableCloseWindow;
            set
            {
                if (SetProperty(_advanced.IsDisableCloseWindow, value, _advanced, (a, v) => a.IsDisableCloseWindow = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public bool EnableForceTopMost
        {
            get => _advanced.EnableForceTopMost;
            set
            {
                if (SetProperty(_advanced.EnableForceTopMost, value, _advanced, (a, v) => a.EnableForceTopMost = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public bool IsEnableForceFullScreen
        {
            get => _advanced.IsEnableForceFullScreen;
            set
            {
                if (SetProperty(_advanced.IsEnableForceFullScreen, value, _advanced, (a, v) => a.IsEnableForceFullScreen = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        public bool IsEnableResolutionChangeDetection
        {
            get => _advanced.IsEnableResolutionChangeDetection;
            set
            {
                if (SetProperty(_advanced.IsEnableResolutionChangeDetection, value, _advanced, (a, v) => a.IsEnableResolutionChangeDetection = v))
                {
                    _saveAction?.Invoke();
                }
            }
        }

        #endregion
    }
}
