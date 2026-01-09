using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using Windows.UI.Input.Inking;
using Windows.UI.Input.Inking.Analysis;

namespace Ink_Canvas.Helpers
{
    /// <summary>
    /// 形状识别帮助类 - 使用 Windows.UI.Input.Inking.Analysis API
    /// </summary>
    public class InkRecognizeHelper
    {
        private static InkAnalyzer _analyzer;
        private static InkStrokeContainer _strokeContainer;
        private static readonly object _syncLock = new object();
        
        static InkRecognizeHelper()
        {
            InitializeAnalyzer();
        }
        
        /// <summary>
        /// 初始化分析器
        /// </summary>
        private static void InitializeAnalyzer()
        {
            try
            {
                _analyzer = new InkAnalyzer();
                _strokeContainer = new InkStrokeContainer();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile("InkRecognizeHelper initialization failed: " + ex.Message, LogHelper.LogType.Error);
            }
        }
        
        /// <summary>
        /// 识别形状（同步包装）
        /// </summary>
        /// <param name="strokes">WPF 笔画集合</param>
        /// <returns>识别结果，如果识别失败则返回 default</returns>
        public static ShapeRecognizeResult RecognizeShape(StrokeCollection strokes)
        {
            if (strokes == null || strokes.Count == 0)
                return default;
            
            try
            {
                // 使用 Task.Run 避免在同步上下文中死锁
                return Task.Run(() => RecognizeShapeAsync(strokes)).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile("RecognizeShape failed: " + ex.Message, LogHelper.LogType.Error);
                return default;
            }
        }
        
        /// <summary>
        /// 识别形状（异步）
        /// </summary>
        /// <param name="strokes">WPF 笔画集合</param>
        /// <returns>识别结果，如果识别失败则返回 default</returns>
        public static async Task<ShapeRecognizeResult> RecognizeShapeAsync(StrokeCollection strokes)
        {
            if (strokes == null || strokes.Count == 0)
                return default;
            
            try
            {
                // 创建新的分析器实例避免并发问题
                var analyzer = new InkAnalyzer();
                var strokeContainer = new InkStrokeContainer();
                
                // 转换 WPF strokes 到 UWP strokes
                var uwpStrokes = StrokeConverter.ToUwpStrokes(strokes);
                if (uwpStrokes.Count == 0)
                    return default;
                
                // 添加到容器和分析器
                foreach (var stroke in uwpStrokes)
                {
                    strokeContainer.AddStroke(stroke);
                }
                analyzer.AddDataForStrokes(strokeContainer.GetStrokes());
                
                // 执行分析
                var result = await analyzer.AnalyzeAsync();
                
                if (result.Status != InkAnalysisStatus.Updated)
                    return default;
                
                // 获取识别到的图形
                var drawingNodes = analyzer.AnalysisRoot.FindNodes(InkAnalysisNodeKind.InkDrawing);
                
                if (drawingNodes.Count == 0)
                    return default;
                
                // 取第一个识别到的图形
                var drawing = drawingNodes[0] as InkAnalysisInkDrawing;
                if (drawing == null)
                    return default;
                
                // 检查是否是我们支持的形状
                if (!IsContainShapeType(drawing.DrawingKind))
                    return default;
                
                // 构建结果
                var centroid = StrokeConverter.ToWpfPoint(drawing.Center);
                var hotPoints = StrokeConverter.ToWpfPointCollection(drawing.Points);
                var boundingRect = StrokeConverter.ToWpfRect(drawing.BoundingRect);
                
                return new ShapeRecognizeResult(
                    centroid, 
                    hotPoints, 
                    drawing.DrawingKind,
                    boundingRect,
                    strokes  // 保存原始 WPF strokes
                );
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile("RecognizeShapeAsync failed: " + ex.Message, LogHelper.LogType.Error);
                return default;
            }
        }
        
        /// <summary>
        /// 预热分析器，加速首次使用
        /// </summary>
        public static async Task PreloadAsync()
        {
            try
            {
                var analyzer = new InkAnalyzer();
                var container = new InkStrokeContainer();
                
                // 创建一个简单的测试笔画
                var builder = new InkStrokeBuilder();
                var points = new List<InkPoint> {
                    new InkPoint(new Windows.Foundation.Point(0, 0), 0.5f),
                    new InkPoint(new Windows.Foundation.Point(100, 100), 0.5f)
                };
                var stroke = builder.CreateStrokeFromInkPoints(points, System.Numerics.Matrix3x2.Identity);
                container.AddStroke(stroke);
                analyzer.AddDataForStrokes(container.GetStrokes());
                
                // 执行分析
                await analyzer.AnalyzeAsync();
                
                LogHelper.WriteLogToFile("Ink Analysis API preloaded successfully", LogHelper.LogType.Info);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile("Ink Analysis API preload failed: " + ex.Message, LogHelper.LogType.Error);
            }
        }
        
        /// <summary>
        /// 检查是否是支持的形状类型
        /// </summary>
        /// <param name="kind">形状类型枚举</param>
        /// <returns>是否支持</returns>
        public static bool IsContainShapeType(InkAnalysisDrawingKind kind)
        {
            return kind == InkAnalysisDrawingKind.Circle ||
                   kind == InkAnalysisDrawingKind.Ellipse ||
                   kind == InkAnalysisDrawingKind.Triangle ||
                   kind == InkAnalysisDrawingKind.Rectangle ||
                   kind == InkAnalysisDrawingKind.Square ||
                   kind == InkAnalysisDrawingKind.Diamond ||
                   kind == InkAnalysisDrawingKind.Trapezoid ||
                   kind == InkAnalysisDrawingKind.Parallelogram;
        }
        
        /// <summary>
        /// 检查是否是支持的形状类型（按名称，兼容旧代码）
        /// </summary>
        /// <param name="name">形状名称</param>
        /// <returns>是否支持</returns>
        public static bool IsContainShapeType(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
                
            return name.Contains("Triangle") || 
                   name.Contains("Circle") ||
                   name.Contains("Rectangle") || 
                   name.Contains("Diamond") ||
                   name.Contains("Parallelogram") || 
                   name.Contains("Square") ||
                   name.Contains("Ellipse") ||
                   name.Contains("Trapezoid");
        }
    }
    
    /// <summary>
    /// 形状识别结果 - 新版本，适配 Windows.UI.Input.Inking.Analysis API
    /// </summary>
    public class ShapeRecognizeResult
    {
        /// <summary>
        /// 创建形状识别结果
        /// </summary>
        public ShapeRecognizeResult(
            Point centroid, 
            PointCollection hotPoints, 
            InkAnalysisDrawingKind drawingKind,
            Rect boundingRect,
            StrokeCollection originalStrokes)
        {
            Centroid = centroid;
            HotPoints = hotPoints;
            DrawingKind = drawingKind;
            BoundingRect = boundingRect;
            OriginalStrokes = originalStrokes;
        }
        
        /// <summary>
        /// 形状中心点
        /// </summary>
        public Point Centroid { get; set; }
        
        /// <summary>
        /// 形状关键点（顶点）
        /// </summary>
        public PointCollection HotPoints { get; }
        
        /// <summary>
        /// 识别到的形状类型
        /// </summary>
        public InkAnalysisDrawingKind DrawingKind { get; }
        
        /// <summary>
        /// 边界矩形
        /// </summary>
        public Rect BoundingRect { get; }
        
        /// <summary>
        /// 原始笔画集合
        /// </summary>
        public StrokeCollection OriginalStrokes { get; }
        
        /// <summary>
        /// 获取形状名称（兼容旧代码）
        /// </summary>
        public string GetShapeName()
        {
            return DrawingKind.ToString();
        }
        
        /// <summary>
        /// 获取形状宽度（兼容旧代码）
        /// </summary>
        public double Width => BoundingRect.Width;
        
        /// <summary>
        /// 获取形状高度（兼容旧代码）
        /// </summary>
        public double Height => BoundingRect.Height;
        
        #region 兼容性属性 - 模拟旧版 InkDrawingNode
        
        /// <summary>
        /// 模拟旧版 InkDrawingNode，方便渐进式迁移
        /// </summary>
        public InkDrawingNodeAdapter InkDrawingNode => new InkDrawingNodeAdapter(this);
        
        #endregion
    }
    
    /// <summary>
    /// InkDrawingNode 适配器 - 模拟旧 API 的接口，减少代码修改量
    /// </summary>
    public class InkDrawingNodeAdapter
    {
        private readonly ShapeRecognizeResult _result;
        
        /// <summary>
        /// 创建适配器
        /// </summary>
        public InkDrawingNodeAdapter(ShapeRecognizeResult result)
        {
            _result = result;
        }
        
        /// <summary>
        /// 获取形状名称
        /// </summary>
        public string GetShapeName() => _result.DrawingKind.ToString();
        
        /// <summary>
        /// 获取形状对象
        /// </summary>
        public ShapeAdapter GetShape() => new ShapeAdapter(_result.BoundingRect);
        
        /// <summary>
        /// 关键点
        /// </summary>
        public PointCollection HotPoints => _result.HotPoints;
        
        /// <summary>
        /// 中心点
        /// </summary>
        public Point Centroid => _result.Centroid;
        
        /// <summary>
        /// 原始笔画
        /// </summary>
        public StrokeCollection Strokes => _result.OriginalStrokes;
    }
    
    /// <summary>
    /// Shape 适配器 - 模拟旧 API 的 Shape 对象
    /// </summary>
    public class ShapeAdapter
    {
        /// <summary>
        /// 创建形状适配器
        /// </summary>
        public ShapeAdapter(Rect bounds)
        {
            Width = bounds.Width;
            Height = bounds.Height;
        }
        
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; }
        
        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; }
    }
    
    /// <summary>
    /// 用于自动控制其他形状相对于圆的位置
    /// </summary>
    public class Circle
    {
        /// <summary>
        /// 创建圆形信息
        /// </summary>
        public Circle(Point centroid, double r, Stroke stroke)
        {
            Centroid = centroid;
            R = r;
            Stroke = stroke;
        }

        /// <summary>
        /// 圆心
        /// </summary>
        public Point Centroid { get; set; }
        
        /// <summary>
        /// 半径
        /// </summary>
        public double R { get; set; }
        
        /// <summary>
        /// 对应的笔画
        /// </summary>
        public Stroke Stroke { get; set; }
    }
}
