using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Ink_Canvas.Services.Events
{
    /// <summary>
    /// 编辑模式变更请求事件参数
    /// </summary>
    public class EditingModeChangeRequestedEventArgs : EventArgs
    {
        public InkCanvasEditingMode NewMode { get; set; }
        public EditingModeChangeRequestedEventArgs(InkCanvasEditingMode newMode)
        {
            NewMode = newMode;
        }
    }

    /// <summary>
    /// 橡皮擦反馈事件参数
    /// </summary>
    public class EraserFeedbackEventArgs : EventArgs
    {
        public Point Position { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsCircleShape { get; set; }
    }

    /// <summary>
    /// 操作增量事件参数
    /// </summary>
    public class TouchManipulationDeltaEventArgs : EventArgs
    {
        public ManipulationDelta Delta { get; set; }
        public Point Center { get; set; }
        public Matrix CachedMatrix { get; set; }
    }
}
