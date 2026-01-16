using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Ink_Canvas.Models.Settings;

namespace Ink_Canvas.ViewModels
{
    /// <summary>
    /// Appearance 设置 ViewModel
    /// </summary>
    public partial class AppearanceSettingsViewModel : ObservableObject
    {
        private readonly AppearanceSettings _appearance;
        private readonly Action _saveAction;

        /// <summary>
        /// 外观设置变化事件
        /// </summary>
        public static event EventHandler<AppearanceSettingChangedEventArgs> AppearanceSettingChanged;

        public AppearanceSettingsViewModel(AppearanceSettings appearance, Action saveAction)
        {
            _appearance = appearance ?? throw new ArgumentNullException(nameof(appearance));
            _saveAction = saveAction;
        }

        /// <summary>
        /// 触发外观设置变化事件
        /// </summary>
        private void RaiseAppearanceSettingChanged(string propertyName)
        {
            AppearanceSettingChanged?.Invoke(this, new AppearanceSettingChangedEventArgs(propertyName));
        }

        public bool IsEnableDisPlayNibModeToggler
        {
            get => _appearance.IsEnableDisPlayNibModeToggler;
            set {
                if (SetProperty(_appearance.IsEnableDisPlayNibModeToggler, value, _appearance, (a, v) => a.IsEnableDisPlayNibModeToggler = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsEnableDisPlayNibModeToggler));
                }
            }
        }

        public bool IsColorfulViewboxFloatingBar
        {
            get => _appearance.IsColorfulViewboxFloatingBar;
            set {
                if (SetProperty(_appearance.IsColorfulViewboxFloatingBar, value, _appearance, (a, v) => a.IsColorfulViewboxFloatingBar = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsColorfulViewboxFloatingBar));
                }
            }
        }

        public double ViewboxFloatingBarScaleTransformValue
        {
            get => _appearance.ViewboxFloatingBarScaleTransformValue;
            set {
                if (SetProperty(_appearance.ViewboxFloatingBarScaleTransformValue, value, _appearance, (a, v) => a.ViewboxFloatingBarScaleTransformValue = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(ViewboxFloatingBarScaleTransformValue));
                }
            }
        }

        public int FloatingBarImg
        {
            get => _appearance.FloatingBarImg;
            set {
                if (SetProperty(_appearance.FloatingBarImg, value, _appearance, (a, v) => a.FloatingBarImg = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(FloatingBarImg));
                }
            }
        }

        public double ViewboxFloatingBarOpacityValue
        {
            get => _appearance.ViewboxFloatingBarOpacityValue;
            set {
                if (SetProperty(_appearance.ViewboxFloatingBarOpacityValue, value, _appearance, (a, v) => a.ViewboxFloatingBarOpacityValue = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(ViewboxFloatingBarOpacityValue));
                }
            }
        }

        public bool EnableTrayIcon
        {
            get => _appearance.EnableTrayIcon;
            set {
                if (SetProperty(_appearance.EnableTrayIcon, value, _appearance, (a, v) => a.EnableTrayIcon = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(EnableTrayIcon));
                }
            }
        }

        public double ViewboxFloatingBarOpacityInPPTValue
        {
            get => _appearance.ViewboxFloatingBarOpacityInPPTValue;
            set {
                if (SetProperty(_appearance.ViewboxFloatingBarOpacityInPPTValue, value, _appearance, (a, v) => a.ViewboxFloatingBarOpacityInPPTValue = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(ViewboxFloatingBarOpacityInPPTValue));
                }
            }
        }

        public bool EnableViewboxBlackBoardScaleTransform
        {
            get => _appearance.EnableViewboxBlackBoardScaleTransform;
            set {
                if (SetProperty(_appearance.EnableViewboxBlackBoardScaleTransform, value, _appearance, (a, v) => a.EnableViewboxBlackBoardScaleTransform = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(EnableViewboxBlackBoardScaleTransform));
                }
            }
        }

        public bool IsTransparentButtonBackground
        {
            get => _appearance.IsTransparentButtonBackground;
            set {
                if (SetProperty(_appearance.IsTransparentButtonBackground, value, _appearance, (a, v) => a.IsTransparentButtonBackground = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsTransparentButtonBackground));
                }
            }
        }

        public bool IsShowExitButton
        {
            get => _appearance.IsShowExitButton;
            set {
                if (SetProperty(_appearance.IsShowExitButton, value, _appearance, (a, v) => a.IsShowExitButton = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowExitButton));
                }
            }
        }

        public bool IsShowEraserButton
        {
            get => _appearance.IsShowEraserButton;
            set {
                if (SetProperty(_appearance.IsShowEraserButton, value, _appearance, (a, v) => a.IsShowEraserButton = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowEraserButton));
                }
            }
        }

        public bool EnableTimeDisplayInWhiteboardMode
        {
            get => _appearance.EnableTimeDisplayInWhiteboardMode;
            set {
                if (SetProperty(_appearance.EnableTimeDisplayInWhiteboardMode, value, _appearance, (a, v) => a.EnableTimeDisplayInWhiteboardMode = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(EnableTimeDisplayInWhiteboardMode));
                }
            }
        }

        public bool EnableChickenSoupInWhiteboardMode
        {
            get => _appearance.EnableChickenSoupInWhiteboardMode;
            set {
                if (SetProperty(_appearance.EnableChickenSoupInWhiteboardMode, value, _appearance, (a, v) => a.EnableChickenSoupInWhiteboardMode = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(EnableChickenSoupInWhiteboardMode));
                }
            }
        }

        public bool IsShowHideControlButton
        {
            get => _appearance.IsShowHideControlButton;
            set {
                if (SetProperty(_appearance.IsShowHideControlButton, value, _appearance, (a, v) => a.IsShowHideControlButton = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowHideControlButton));
                }
            }
        }

        public int UnFoldButtonImageType
        {
            get => _appearance.UnFoldButtonImageType;
            set {
                if (SetProperty(_appearance.UnFoldButtonImageType, value, _appearance, (a, v) => a.UnFoldButtonImageType = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(UnFoldButtonImageType));
                }
            }
        }

        public bool IsShowLRSwitchButton
        {
            get => _appearance.IsShowLRSwitchButton;
            set {
                if (SetProperty(_appearance.IsShowLRSwitchButton, value, _appearance, (a, v) => a.IsShowLRSwitchButton = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowLRSwitchButton));
                }
            }
        }

        public bool IsShowQuickPanel
        {
            get => _appearance.IsShowQuickPanel;
            set {
                if (SetProperty(_appearance.IsShowQuickPanel, value, _appearance, (a, v) => a.IsShowQuickPanel = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowQuickPanel));
                }
            }
        }

        public int ChickenSoupSource
        {
            get => _appearance.ChickenSoupSource;
            set {
                if (SetProperty(_appearance.ChickenSoupSource, value, _appearance, (a, v) => a.ChickenSoupSource = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(ChickenSoupSource));
                }
            }
        }

        public bool IsShowModeFingerToggleSwitch
        {
            get => _appearance.IsShowModeFingerToggleSwitch;
            set {
                if (SetProperty(_appearance.IsShowModeFingerToggleSwitch, value, _appearance, (a, v) => a.IsShowModeFingerToggleSwitch = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowModeFingerToggleSwitch));
                }
            }
        }

        public int Theme
        {
            get => _appearance.Theme;
            set {
                if (SetProperty(_appearance.Theme, value, _appearance, (a, v) => a.Theme = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(Theme));
                }
            }
        }

        public bool FloatingBarButtonLabelVisibility
        {
            get => _appearance.FloatingBarButtonLabelVisibility;
            set {
                if (SetProperty(_appearance.FloatingBarButtonLabelVisibility, value, _appearance, (a, v) => a.FloatingBarButtonLabelVisibility = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(FloatingBarButtonLabelVisibility));
                }
            }
        }

        public string FloatingBarIconsVisibility
        {
            get => _appearance.FloatingBarIconsVisibility;
            set {
                if (SetProperty(_appearance.FloatingBarIconsVisibility, value, _appearance, (a, v) => a.FloatingBarIconsVisibility = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(FloatingBarIconsVisibility));
                }
            }
        }

        public int EraserButtonsVisibility
        {
            get => _appearance.EraserButtonsVisibility;
            set {
                if (SetProperty(_appearance.EraserButtonsVisibility, value, _appearance, (a, v) => a.EraserButtonsVisibility = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(EraserButtonsVisibility));
                }
            }
        }

        public bool OnlyDisplayEraserBtn
        {
            get => _appearance.OnlyDisplayEraserBtn;
            set {
                if (SetProperty(_appearance.OnlyDisplayEraserBtn, value, _appearance, (a, v) => a.OnlyDisplayEraserBtn = v)) {
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(OnlyDisplayEraserBtn));
                }
            }
        }

        #region 按钮可见性属性

        /// <summary>
        /// 显示"形状"按钮
        /// </summary>
        public bool IsShowShapeButton
        {
            get => _appearance.IsShowShapeButton;
            set
            {
                if (_appearance.IsShowShapeButton != value)
                {
                    _appearance.IsShowShapeButton = value;
                    OnPropertyChanged(nameof(IsShowShapeButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowShapeButton));
                }
            }
        }

        /// <summary>
        /// 显示"冻结"按钮
        /// </summary>
        public bool IsShowFreezeButton
        {
            get => _appearance.IsShowFreezeButton;
            set
            {
                if (_appearance.IsShowFreezeButton != value)
                {
                    _appearance.IsShowFreezeButton = value;
                    OnPropertyChanged(nameof(IsShowFreezeButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowFreezeButton));
                }
            }
        }

        /// <summary>
        /// 显示"漫游"按钮
        /// </summary>
        public bool IsShowRoamingButton
        {
            get => _appearance.IsShowRoamingButton;
            set
            {
                if (_appearance.IsShowRoamingButton != value)
                {
                    _appearance.IsShowRoamingButton = value;
                    OnPropertyChanged(nameof(IsShowRoamingButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowRoamingButton));
                }
            }
        }

        /// <summary>
        /// 显示"撤销"按钮
        /// </summary>
        public bool IsShowUndoButton
        {
            get => _appearance.IsShowUndoButton;
            set
            {
                if (_appearance.IsShowUndoButton != value)
                {
                    _appearance.IsShowUndoButton = value;
                    OnPropertyChanged(nameof(IsShowUndoButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowUndoButton));
                }
            }
        }

        /// <summary>
        /// 显示"重做"按钮
        /// </summary>
        public bool IsShowRedoButton
        {
            get => _appearance.IsShowRedoButton;
            set
            {
                if (_appearance.IsShowRedoButton != value)
                {
                    _appearance.IsShowRedoButton = value;
                    OnPropertyChanged(nameof(IsShowRedoButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowRedoButton));
                }
            }
        }

        /// <summary>
        /// 显示"清并鼠"按钮（清除墨迹并切换到鼠标）
        /// </summary>
        public bool IsShowClearButton
        {
            get => _appearance.IsShowClearButton;
            set
            {
                if (_appearance.IsShowClearButton != value)
                {
                    _appearance.IsShowClearButton = value;
                    OnPropertyChanged(nameof(IsShowClearButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowClearButton));
                }
            }
        }

        /// <summary>
        /// 显示"框选"按钮
        /// </summary>
        public bool IsShowSelectButton
        {
            get => _appearance.IsShowSelectButton;
            set
            {
                if (_appearance.IsShowSelectButton != value)
                {
                    _appearance.IsShowSelectButton = value;
                    OnPropertyChanged(nameof(IsShowSelectButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowSelectButton));
                }
            }
        }

        /// <summary>
        /// 显示"白板"按钮
        /// </summary>
        public bool IsShowWhiteboardButton
        {
            get => _appearance.IsShowWhiteboardButton;
            set
            {
                if (_appearance.IsShowWhiteboardButton != value)
                {
                    _appearance.IsShowWhiteboardButton = value;
                    OnPropertyChanged(nameof(IsShowWhiteboardButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowWhiteboardButton));
                }
            }
        }

        /// <summary>
        /// 显示"隐藏"按钮
        /// </summary>
        public bool IsShowHideButton
        {
            get => _appearance.IsShowHideButton;
            set
            {
                if (_appearance.IsShowHideButton != value)
                {
                    _appearance.IsShowHideButton = value;
                    OnPropertyChanged(nameof(IsShowHideButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowHideButton));
                }
            }
        }

        /// <summary>
        /// 显示"手势"按钮
        /// </summary>
        public bool IsShowGestureButton
        {
            get => _appearance.IsShowGestureButton;
            set
            {
                if (_appearance.IsShowGestureButton != value)
                {
                    _appearance.IsShowGestureButton = value;
                    OnPropertyChanged(nameof(IsShowGestureButton));
                    _saveAction?.Invoke();
                    RaiseAppearanceSettingChanged(nameof(IsShowGestureButton));
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// 外观设置变化事件参数
    /// </summary>
    public class AppearanceSettingChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 变化的属性名称
        /// </summary>
        public string PropertyName { get; }

        public AppearanceSettingChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
