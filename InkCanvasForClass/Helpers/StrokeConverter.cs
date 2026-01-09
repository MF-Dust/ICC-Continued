using System;
using System.Collections.Generic;
using System.Windows.Ink;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI.Input.Inking;

namespace Ink_Canvas.Helpers
{
    /// <summary>
    /// WPF Stroke 与 UWP InkStroke 之间的转换工具
    /// </summary>
    public static class StrokeConverter
    {
        private static readonly InkStrokeBuilder _strokeBuilder;
        
        static StrokeConverter()
        {
            _strokeBuilder = new InkStrokeBuilder();
        }
        
        /// <summary>
        /// 将 WPF StrokeCollection 转换为 UWP InkStroke 列表
        /// </summary>
        /// <param name="wpfStrokes">WPF 笔画集合</param>
        /// <returns>UWP InkStroke 列表</returns>
        public static List<InkStroke> ToUwpStrokes(StrokeCollection wpfStrokes)
        {
            var uwpStrokes = new List<InkStroke>();
            
            if (wpfStrokes == null)
                return uwpStrokes;
            
            foreach (var wpfStroke in wpfStrokes)
            {
                var uwpStroke = ToUwpStroke(wpfStroke);
                if (uwpStroke != null)
                {
                    uwpStrokes.Add(uwpStroke);
                }
            }
            
            return uwpStrokes;
        }
        
        /// <summary>
        /// 将单个 WPF Stroke 转换为 UWP InkStroke
        /// </summary>
        /// <param name="wpfStroke">WPF 笔画</param>
        /// <returns>UWP InkStroke，如果转换失败则返回 null</returns>
        public static InkStroke ToUwpStroke(Stroke wpfStroke)
        {
            if (wpfStroke == null)
                return null;
                
            try
            {
                var inkPoints = new List<InkPoint>();
                
                foreach (var stylusPoint in wpfStroke.StylusPoints)
                {
                    var inkPoint = new InkPoint(
                        new Point(stylusPoint.X, stylusPoint.Y),
                        stylusPoint.PressureFactor
                    );
                    inkPoints.Add(inkPoint);
                }
                
                // InkStroke 至少需要 2 个点
                if (inkPoints.Count < 2)
                    return null;
                
                var uwpStroke = _strokeBuilder.CreateStrokeFromInkPoints(
                    inkPoints, 
                    System.Numerics.Matrix3x2.Identity
                );
                
                return uwpStroke;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile("StrokeConverter.ToUwpStroke failed: " + ex.Message, LogHelper.LogType.Error);
                return null;
            }
        }
        
        /// <summary>
        /// 将 UWP Point 转换为 WPF Point
        /// </summary>
        /// <param name="uwpPoint">UWP 点</param>
        /// <returns>WPF 点</returns>
        public static System.Windows.Point ToWpfPoint(Point uwpPoint)
        {
            return new System.Windows.Point(uwpPoint.X, uwpPoint.Y);
        }
        
        /// <summary>
        /// 将 UWP Point 列表转换为 WPF PointCollection
        /// </summary>
        /// <param name="uwpPoints">UWP 点列表</param>
        /// <returns>WPF 点集合</returns>
        public static System.Windows.Media.PointCollection ToWpfPointCollection(IReadOnlyList<Point> uwpPoints)
        {
            var wpfPoints = new System.Windows.Media.PointCollection();
            
            if (uwpPoints == null)
                return wpfPoints;
                
            foreach (var p in uwpPoints)
            {
                wpfPoints.Add(ToWpfPoint(p));
            }
            return wpfPoints;
        }
        
        /// <summary>
        /// 将 UWP Rect 转换为 WPF Rect
        /// </summary>
        /// <param name="uwpRect">UWP 矩形</param>
        /// <returns>WPF 矩形</returns>
        public static System.Windows.Rect ToWpfRect(Windows.Foundation.Rect uwpRect)
        {
            return new System.Windows.Rect(uwpRect.X, uwpRect.Y, uwpRect.Width, uwpRect.Height);
        }
    }
}