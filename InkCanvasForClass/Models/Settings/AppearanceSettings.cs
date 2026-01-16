using Newtonsoft.Json;

namespace Ink_Canvas.Models.Settings
{
    /// <summary>
    /// 外观设置类
    /// </summary>
    public class AppearanceSettings : SettingsBase
    {
        private bool _isEnableDisPlayNibModeToggler = true;
        private bool _isColorfulViewboxFloatingBar = false;
        private double _viewboxFloatingBarScaleTransformValue = 1.0;
        private int _floatingBarImg = 0;
        private double _viewboxFloatingBarOpacityValue = 1.0;
        private bool _enableTrayIcon = true;
        private double _viewboxFloatingBarOpacityInPPTValue = 0.5;
        private bool _enableViewboxBlackBoardScaleTransform = false;
        private bool _isTransparentButtonBackground = true;
        private bool _isShowExitButton = true;
        private bool _isShowEraserButton = true;
        private bool _enableTimeDisplayInWhiteboardMode = true;
        private bool _enableChickenSoupInWhiteboardMode = true;
        private bool _isShowHideControlButton = false;
        private int _unFoldButtonImageType = 0;
        private bool _isShowLRSwitchButton = false;
        private bool _isShowQuickPanel = true;
        private int _chickenSoupSource = 1;
        private bool _isShowModeFingerToggleSwitch = true;
        private int _theme = 0;

        /// <summary>
        /// 是否显示笔模式切换器
        /// </summary>
        [JsonProperty("isEnableDisPlayNibModeToggler")]
        public bool IsEnableDisPlayNibModeToggler
        {
            get => _isEnableDisPlayNibModeToggler;
            set => SetProperty(ref _isEnableDisPlayNibModeToggler, value);
        }

        /// <summary>
        /// 是否使用彩色浮动工具栏
        /// </summary>
        [JsonProperty("isColorfulViewboxFloatingBar")]
        public bool IsColorfulViewboxFloatingBar
        {
            get => _isColorfulViewboxFloatingBar;
            set => SetProperty(ref _isColorfulViewboxFloatingBar, value);
        }

        /// <summary>
        /// 浮动工具栏缩放值
        /// </summary>
        [JsonProperty("viewboxFloatingBarScaleTransformValue")]
        public double ViewboxFloatingBarScaleTransformValue
        {
            get => _viewboxFloatingBarScaleTransformValue;
            set => SetProperty(ref _viewboxFloatingBarScaleTransformValue, value);
        }

        /// <summary>
        /// 浮动工具栏图片
        /// </summary>
        [JsonProperty("floatingBarImg")]
        public int FloatingBarImg
        {
            get => _floatingBarImg;
            set => SetProperty(ref _floatingBarImg, value);
        }

        /// <summary>
        /// 浮动工具栏透明度
        /// </summary>
        [JsonProperty("viewboxFloatingBarOpacityValue")]
        public double ViewboxFloatingBarOpacityValue
        {
            get => _viewboxFloatingBarOpacityValue;
            set => SetProperty(ref _viewboxFloatingBarOpacityValue, value);
        }

        /// <summary>
        /// 是否启用托盘图标
        /// </summary>
        [JsonProperty("enableTrayIcon")]
        public bool EnableTrayIcon
        {
            get => _enableTrayIcon;
            set => SetProperty(ref _enableTrayIcon, value);
        }

        /// <summary>
        /// PPT 模式下浮动工具栏透明度
        /// </summary>
        [JsonProperty("viewboxFloatingBarOpacityInPPTValue")]
        public double ViewboxFloatingBarOpacityInPPTValue
        {
            get => _viewboxFloatingBarOpacityInPPTValue;
            set => SetProperty(ref _viewboxFloatingBarOpacityInPPTValue, value);
        }

        /// <summary>
        /// 是否启用黑板缩放变换
        /// </summary>
        [JsonProperty("enableViewboxBlackBoardScaleTransform")]
        public bool EnableViewboxBlackBoardScaleTransform
        {
            get => _enableViewboxBlackBoardScaleTransform;
            set => SetProperty(ref _enableViewboxBlackBoardScaleTransform, value);
        }

        /// <summary>
        /// 是否使用透明按钮背景
        /// </summary>
        [JsonProperty("isTransparentButtonBackground")]
        public bool IsTransparentButtonBackground
        {
            get => _isTransparentButtonBackground;
            set => SetProperty(ref _isTransparentButtonBackground, value);
        }

        /// <summary>
        /// 是否显示退出按钮
        /// </summary>
        [JsonProperty("isShowExitButton")]
        public bool IsShowExitButton
        {
            get => _isShowExitButton;
            set => SetProperty(ref _isShowExitButton, value);
        }

        /// <summary>
        /// 是否显示橡皮擦按钮
        /// </summary>
        [JsonProperty("isShowEraserButton")]
        public bool IsShowEraserButton
        {
            get => _isShowEraserButton;
            set => SetProperty(ref _isShowEraserButton, value);
        }

        /// <summary>
        /// 白板模式下是否显示时间
        /// </summary>
        [JsonProperty("enableTimeDisplayInWhiteboardMode")]
        public bool EnableTimeDisplayInWhiteboardMode
        {
            get => _enableTimeDisplayInWhiteboardMode;
            set => SetProperty(ref _enableTimeDisplayInWhiteboardMode, value);
        }

        /// <summary>
        /// 白板模式下是否显示鸡汤
        /// </summary>
        [JsonProperty("enableChickenSoupInWhiteboardMode")]
        public bool EnableChickenSoupInWhiteboardMode
        {
            get => _enableChickenSoupInWhiteboardMode;
            set => SetProperty(ref _enableChickenSoupInWhiteboardMode, value);
        }

        /// <summary>
        /// 是否显示隐藏控制按钮
        /// </summary>
        [JsonProperty("isShowHideControlButton")]
        public bool IsShowHideControlButton
        {
            get => _isShowHideControlButton;
            set => SetProperty(ref _isShowHideControlButton, value);
        }

        /// <summary>
        /// 展开按钮图片类型
        /// </summary>
        [JsonProperty("unFoldButtonImageType")]
        public int UnFoldButtonImageType
        {
            get => _unFoldButtonImageType;
            set => SetProperty(ref _unFoldButtonImageType, value);
        }

        /// <summary>
        /// 是否显示左右切换按钮
        /// </summary>
        [JsonProperty("isShowLRSwitchButton")]
        public bool IsShowLRSwitchButton
        {
            get => _isShowLRSwitchButton;
            set => SetProperty(ref _isShowLRSwitchButton, value);
        }

        /// <summary>
        /// 是否显示快捷面板
        /// </summary>
        [JsonProperty("isShowQuickPanel")]
        public bool IsShowQuickPanel
        {
            get => _isShowQuickPanel;
            set => SetProperty(ref _isShowQuickPanel, value);
        }

        /// <summary>
        /// 鸡汤来源
        /// </summary>
        [JsonProperty("chickenSoupSource")]
        public int ChickenSoupSource
        {
            get => _chickenSoupSource;
            set => SetProperty(ref _chickenSoupSource, value);
        }

        /// <summary>
        /// 是否显示手指模式切换开关
        /// </summary>
        [JsonProperty("isShowModeFingerToggleSwitch")]
        public bool IsShowModeFingerToggleSwitch
        {
            get => _isShowModeFingerToggleSwitch;
            set => SetProperty(ref _isShowModeFingerToggleSwitch, value);
        }

        /// <summary>
        /// 主题
        /// </summary>
        [JsonProperty("theme")]
        public int Theme
        {
            get => _theme;
            set => SetProperty(ref _theme, value);
        }

        private bool _floatingBarButtonLabelVisibility = true;
        private string _floatingBarIconsVisibility = "1111111111";
        private int _eraserButtonsVisibility = 0;
        private bool _onlyDisplayEraserBtn = false;

        /// <summary>
        /// 浮动工具栏按钮标签可见性
        /// </summary>
        [JsonProperty("floatingBarButtonLabelVisibility")]
        public bool FloatingBarButtonLabelVisibility
        {
            get => _floatingBarButtonLabelVisibility;
            set => SetProperty(ref _floatingBarButtonLabelVisibility, value);
        }

        /// <summary>
        /// 浮动工具栏图标可见性（10位字符串，每位代表一个按钮：形状、冻结、漫游、撤销、重做、清并鼠、框选、白板、隐藏、手势）
        /// </summary>
        [JsonProperty("floatingBarIconsVisibility")]
        public string FloatingBarIconsVisibility
        {
            get => _floatingBarIconsVisibility;
            set => SetProperty(ref _floatingBarIconsVisibility, value);
        }

        /// <summary>
        /// 橡皮擦按钮可见性（0=两个都显示，1=仅墨迹擦，2=仅板擦）
        /// </summary>
        [JsonProperty("eraserButtonsVisibility")]
        public int EraserButtonsVisibility
        {
            get => _eraserButtonsVisibility;
            set => SetProperty(ref _eraserButtonsVisibility, value);
        }

        /// <summary>
        /// 是否仅显示橡皮擦按钮
        /// </summary>
        [JsonProperty("onlyDisplayEraserBtn")]
        public bool OnlyDisplayEraserBtn
        {
            get => _onlyDisplayEraserBtn;
            set => SetProperty(ref _onlyDisplayEraserBtn, value);
        }

        #region 按钮可见性辅助属性

        /// <summary>
        /// 显示"形状"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowShapeButton
        {
            get => FloatingBarIconsVisibility.Length > 0 && FloatingBarIconsVisibility[0] == '1';
            set => UpdateIconVisibility(0, value);
        }

        /// <summary>
        /// 显示"冻结"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowFreezeButton
        {
            get => FloatingBarIconsVisibility.Length > 1 && FloatingBarIconsVisibility[1] == '1';
            set => UpdateIconVisibility(1, value);
        }

        /// <summary>
        /// 显示"漫游"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowRoamingButton
        {
            get => FloatingBarIconsVisibility.Length > 2 && FloatingBarIconsVisibility[2] == '1';
            set => UpdateIconVisibility(2, value);
        }

        /// <summary>
        /// 显示"撤销"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowUndoButton
        {
            get => FloatingBarIconsVisibility.Length > 3 && FloatingBarIconsVisibility[3] == '1';
            set => UpdateIconVisibility(3, value);
        }

        /// <summary>
        /// 显示"重做"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowRedoButton
        {
            get => FloatingBarIconsVisibility.Length > 4 && FloatingBarIconsVisibility[4] == '1';
            set => UpdateIconVisibility(4, value);
        }

        /// <summary>
        /// 显示"清并鼠"按钮（清除墨迹并切换到鼠标）
        /// </summary>
        [JsonIgnore]
        public bool IsShowClearButton
        {
            get => FloatingBarIconsVisibility.Length > 5 && FloatingBarIconsVisibility[5] == '1';
            set => UpdateIconVisibility(5, value);
        }

        /// <summary>
        /// 显示"框选"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowSelectButton
        {
            get => FloatingBarIconsVisibility.Length > 6 && FloatingBarIconsVisibility[6] == '1';
            set => UpdateIconVisibility(6, value);
        }

        /// <summary>
        /// 显示"白板"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowWhiteboardButton
        {
            get => FloatingBarIconsVisibility.Length > 7 && FloatingBarIconsVisibility[7] == '1';
            set => UpdateIconVisibility(7, value);
        }

        /// <summary>
        /// 显示"隐藏"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowHideButton
        {
            get => FloatingBarIconsVisibility.Length > 8 && FloatingBarIconsVisibility[8] == '1';
            set => UpdateIconVisibility(8, value);
        }

        /// <summary>
        /// 显示"手势"按钮
        /// </summary>
        [JsonIgnore]
        public bool IsShowGestureButton
        {
            get => FloatingBarIconsVisibility.Length > 9 && FloatingBarIconsVisibility[9] == '1';
            set => UpdateIconVisibility(9, value);
        }

        /// <summary>
        /// 更新图标可见性字符串中的指定位
        /// </summary>
        private void UpdateIconVisibility(int index, bool visible)
        {
            if (string.IsNullOrEmpty(FloatingBarIconsVisibility) || FloatingBarIconsVisibility.Length < 10)
            {
                FloatingBarIconsVisibility = "1111111111";
            }

            var chars = FloatingBarIconsVisibility.ToCharArray();
            chars[index] = visible ? '1' : '0';
            FloatingBarIconsVisibility = new string(chars);

            // 通知对应属性变化
            var propertyName = index switch
            {
                0 => nameof(IsShowShapeButton),
                1 => nameof(IsShowFreezeButton),
                2 => nameof(IsShowRoamingButton),
                3 => nameof(IsShowUndoButton),
                4 => nameof(IsShowRedoButton),
                5 => nameof(IsShowClearButton),
                6 => nameof(IsShowSelectButton),
                7 => nameof(IsShowWhiteboardButton),
                8 => nameof(IsShowHideButton),
                9 => nameof(IsShowGestureButton),
                _ => null
            };

            if (propertyName != null)
            {
                OnPropertyChanged(propertyName);
            }
        }

        #endregion
    }
}
