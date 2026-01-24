using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ink_Canvas.Popups
{
    public partial class ShapeDrawingPopup : UserControl
    {
        // 定义内部数据项集合
        private ObservableCollection<ShapeDrawingItem> _items = new ObservableCollection<ShapeDrawingItem>();
        
        // 记录鼠标按下时的 Border 元素
        private Border shapeDrawingButtonDownBorder = null;
        private bool isCloseButtonDown = false;

        // 事件定义
        public event EventHandler<RoutedEventArgs> ShapeDrawingPopupShouldCloseEvent;
        
        public class ShapeSelectedEventArgs : EventArgs
        {
            public MainWindow.ShapeDrawingType Type { get; set; }
            public string Name { get; set; }
        }

        public event EventHandler<ShapeSelectedEventArgs> ShapeSelectedEvent;

        public ShapeDrawingPopup()
        {
            InitializeComponent();

            // 确保 ItemsControl 不为空再赋值（虽然 InitializeComponent 后通常不为空，但为了严谨）
            if (ShapeDrawingItemsControl != null)
            {
                ShapeDrawingItemsControl.ItemsSource = _items;
            }

            InitializeItems();
        }

        /// <summary>
        /// 初始化图形列表项
        /// 使用 TryFindResource 避免资源缺失导致程序崩溃
        /// </summary>
        private void InitializeItems()
        {
            // 辅助方法：安全获取图标资源
            DrawingImage GetIcon(string key) => TryFindResource(key) as DrawingImage;

            _items.Add(new ShapeDrawingItem
            {
                Name = "直线",
                Image = GetIcon("LineIcon"),
                Type = MainWindow.ShapeDrawingType.Line,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "虚线",
                Image = GetIcon("DashedLineIcon"),
                Type = MainWindow.ShapeDrawingType.DashedLine,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "点虚线",
                Image = GetIcon("DottedLineIcon"),
                Type = MainWindow.ShapeDrawingType.DottedLine,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "箭头",
                Image = GetIcon("ArrowLineIcon"),
                Type = MainWindow.ShapeDrawingType.ArrowOneSide,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "双箭头",
                Image = GetIcon("ArrowLineTwoSideIcon"),
                Type = MainWindow.ShapeDrawingType.ArrowTwoSide,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "矩形",
                Image = GetIcon("RectIcon"),
                Type = MainWindow.ShapeDrawingType.Rectangle,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "椭圆",
                Image = GetIcon("CircleIcon"),
                Type = MainWindow.ShapeDrawingType.Ellipse,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "饼图形",
                Image = GetIcon("PieIcon"),
                Type = MainWindow.ShapeDrawingType.PieEllipse,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "三角形",
                Image = GetIcon("TriangleIcon"),
                Type = MainWindow.ShapeDrawingType.Triangle,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "直角三角",
                Image = GetIcon("RightTriangleIcon"),
                Type = MainWindow.ShapeDrawingType.RightTriangle,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "菱形",
                Image = GetIcon("DiamondIcon"),
                Type = MainWindow.ShapeDrawingType.Diamond,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "平行四边形",
                Image = GetIcon("ParallelogramIcon"),
                Type = MainWindow.ShapeDrawingType.Parallelogram,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "四线三格",
                Image = GetIcon("FourLineIcon"),
                Type = MainWindow.ShapeDrawingType.FourLine,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "五线谱",
                Image = GetIcon("FiveLineIcon"),
                Type = MainWindow.ShapeDrawingType.Staff,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "平面坐标轴",
                Image = GetIcon("DefaultAxisIcon"),
                Type = MainWindow.ShapeDrawingType.Axis2D,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "平面坐标轴2",
                Image = GetIcon("Axis2Icon"),
                Type = MainWindow.ShapeDrawingType.Axis2DA,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "平面坐标轴3",
                Image = GetIcon("Axis3Icon"),
                Type = MainWindow.ShapeDrawingType.Axis2DB,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "平面坐标轴4",
                Image = GetIcon("Axis4Icon"),
                Type = MainWindow.ShapeDrawingType.Axis2DC,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "三维坐标轴",
                Image = GetIcon("ThreeDimensionAxisIcon"),
                Type = MainWindow.ShapeDrawingType.Axis3D,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "双曲线",
                Image = GetIcon("HyperbolaIcon"),
                Type = MainWindow.ShapeDrawingType.Hyperbola,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "带焦点的双曲线",
                Image = GetIcon("HyperbolaWithFocalPointIcon"),
                Type = MainWindow.ShapeDrawingType.HyperbolaF,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "抛物线1",
                Image = GetIcon("ParabolaIcon"),
                Type = MainWindow.ShapeDrawingType.Parabola,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "抛物线2",
                Image = GetIcon("Parabola2Icon"),
                Type = MainWindow.ShapeDrawingType.ParabolaA,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "带焦点的抛物线2",
                Image = GetIcon("Parabola2WithFocalPointIcon"),
                Type = MainWindow.ShapeDrawingType.ParabolaAF,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "圆柱体",
                Image = GetIcon("CylinderIcon"),
                Type = MainWindow.ShapeDrawingType.Cylinder,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "立方体",
                Image = GetIcon("CubeIcon"),
                Type = MainWindow.ShapeDrawingType.Cube,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "圆锥体",
                Image = GetIcon("ConeIcon"),
                Type = MainWindow.ShapeDrawingType.Cone,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "带圆心的圆形",
                Image = GetIcon("CircleWithCenterPointIcon"),
                Type = MainWindow.ShapeDrawingType.EllipseC,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "带中心点的矩形",
                Image = GetIcon("RectWithCenterPointIcon"),
                Type = MainWindow.ShapeDrawingType.RectangleC,
            });

            _items.Add(new ShapeDrawingItem
            {
                Name = "网格辅助线",
                Image = GetIcon("CoordinateGridIcon"),
                Type = MainWindow.ShapeDrawingType.CoordinateGrid,
            });
        }

        /// <summary>
        /// 建议将此内部类设为 public，以便 XAML 绑定引擎可以更稳定地访问属性
        /// </summary>
        public class ShapeDrawingItem
        {
            public string Name { get; set; }
            public DrawingImage Image { get; set; }
            public MainWindow.ShapeDrawingType Type { get; set; }
        }

        private void CloseButtonBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isCloseButtonDown = true;
            if (CloseButtonBorder != null)
            {
                CloseButtonBorder.Background = new SolidColorBrush(Color.FromArgb(34, 220, 38, 38));
            }
        }

        private void CloseButtonBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            isCloseButtonDown = false;
            if (CloseButtonBorder != null)
            {
                CloseButtonBorder.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void CloseButtonBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isCloseButtonDown) return;

            CloseButtonBorder_MouseLeave(sender, e);
            ShapeDrawingPopupShouldCloseEvent?.Invoke(this, new RoutedEventArgs());
        }

        private void ShapeDrawingButtonBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (shapeDrawingButtonDownBorder != null) return;
            
            // 安全转换
            if (sender is Border border)
            {
                shapeDrawingButtonDownBorder = border;
                shapeDrawingButtonDownBorder.Background = new SolidColorBrush(Color.FromRgb(228, 228, 231));
            }
        }

        private void ShapeDrawingButtonBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            // 确保 sender 不为 null 且确实是当前按下的 Border
            if (shapeDrawingButtonDownBorder == null || !ReferenceEquals(shapeDrawingButtonDownBorder, sender)) 
                return;

            shapeDrawingButtonDownBorder.Background = new SolidColorBrush(Colors.Transparent);
            shapeDrawingButtonDownBorder = null;
        }

        private void ShapeDrawingButtonBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 确保 sender 不为 null 且确实是当前按下的 Border
            if (shapeDrawingButtonDownBorder == null || !ReferenceEquals(shapeDrawingButtonDownBorder, sender)) 
                return;

            ShapeDrawingButtonBorder_MouseLeave(sender, e);
            ShapeDrawingPopupShouldCloseEvent?.Invoke(this, new RoutedEventArgs());

            // 安全获取 Tag 数据，防止 NullReferenceException
            if (sender is Border border && border.Tag is ShapeDrawingItem item)
            {
                ShapeSelectedEvent?.Invoke(this, new ShapeSelectedEventArgs()
                {
                    Type = item.Type,
                    Name = item.Name
                });
            }
        }
    }
}
